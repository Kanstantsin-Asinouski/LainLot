import { bboxIoU, segsSignature } from "../geometry/geometry.js";
import { getBounds } from "../geometry/bounds.js";
import { ensureClosed, splitClosedSubpaths } from "../svg/polylineOps.js";
import { parsePathD, segsFromPoints } from "../svg/parsePath.js";
import { looksLikeBackground } from "../svg/heuristics.js";
import { parseViewBox, parseMatrix, applyMatrixToSegs } from "../geometry/matrix.js";
import { collectAnchors } from "../svg/anchors.js";
import { resolveSvgSrcPath } from "../../core/variables/svgPath.js";
import { getBaseSources } from "../../core/variables/variants.js";

// === EXTRACT-PANELS: BUILD TAG ===
console.log("[extractPanels] MODULE LOADED B1");
if (typeof window !== "undefined") window.__EP_BUILD = "B1";


localStorage.setItem("ce.debug.panels", "1"); // включить
// перезагрузить страницу, повторить действие
//localStorage.removeItem("ce.debug.panels");   // выключить

// Включение логов: localStorage.setItem("ce.debug.panels", "1")
// Выключение:       localStorage.removeItem("ce.debug.panels")
const __DBG_PANELS = (() => {
    try { return (typeof window !== "undefined") && localStorage.getItem("ce.debug.panels") === "1"; }
    catch { return false; }
})();
const __logPanels = (...args) => { if (__DBG_PANELS) console.log("[extractPanels]", ...args); };


/* ================== настройки ================== */
const PANEL_MAX_COUNT = 12;
const PANEL_MIN_AREA_RATIO_DEFAULT = 0.005; // 0.3–0.8% обычно хватает для деталей
const translateScaleMatrix = (dx = 0, dy = 0, sx = 1, sy = sx) => ({ a: sx, b: 0, c: 0, d: sy, e: dx, f: dy });

export const pushCandidate = (candidates, segs, tag, label) => {
    if (!segs || !segs.length) return;
    const bb = getBounds(segs);
    // имя берём строго из SVG: label → id
    const mLabel = tag.match(/\s(?:inkscape:label|data-name|title)=["']([^"']+)["']/i);
    const mId = tag.match(/\sid=["']([^"']+)["']/i);
    const name = (label ?? mLabel?.[1] ?? mId?.[1] ?? "").trim();
    candidates.push({
        segs,
        label: name,
        bbox: bb,
        bboxArea: Math.abs(bb.w * bb.h) || 1,
        rawTag: tag
    });
}

export const extractPanels = (rawSVG) => {

    __logPanels("start", { rawLen: (rawSVG?.length || 0) });

    const rootBox = parseViewBox(rawSVG);

    // --- собираем теги
    const pathTags = [...rawSVG.matchAll(/<path\b[^>]*>/gi)].map(m => m[0]);
    const polygonTags = [...rawSVG.matchAll(/<polygon\b[^>]*>/gi)].map(m => m[0]);
    const polylineTags = [...rawSVG.matchAll(/<polyline\b[^>]*>/gi)].map(m => m[0]);
    const rectTags = [...rawSVG.matchAll(/<rect\b[^>]*>/gi)].map(m => m[0]);
    const circleTags = [...rawSVG.matchAll(/<circle\b[^>]*>/gi)].map(m => m[0]);
    const ellipseTags = [...rawSVG.matchAll(/<ellipse\b[^>]*>/gi)].map(m => m[0]);
    const lineTags = [...rawSVG.matchAll(/<line\b[^>]*>/gi)].map(m => m[0]);

    // маски/фильтры пропускаем, clip-path НЕ режем (часто это просто «окно»)
    const skipByAttr = (tag) => /\bmask=|\bfilter=/i.test(tag);

    let candidates = [];
    const push = (segs, tag, label) => {
        pushCandidate(candidates, segs, tag, label);
    };

    // ----- PATH
    for (const tag of pathTags) {
        if (skipByAttr(tag)) continue;

        const d = tag.match(/\sd="([^"]+)"/i)?.[1];
        if (!d) continue;

        const M = parseMatrix(tag.match(/\btransform="([^"]+)"/i)?.[1] || "");

        // корректно: не разбиваем по M/m
        let chunks = splitClosedSubpaths(d);
        if (!chunks.length) {
            chunks = [d];  // один path → одна панель
        }

        for (const subD of chunks) {
            let segs = parsePathD(subD);

            // ❌ НЕ закрываем не-замкнутые швы/линии
            // if (!segs.some(s => s.kind === "Z")) segs = ensureClosed(segs);

            if (M) segs = applyMatrixToSegs(segs, M);
            push(segs, tag);
        }
    }

    // ----- POLYGON
    for (const tag of polygonTags) {
        if (skipByAttr(tag))
            continue;

        const pts = tag.match(/\spoints="([^"]+)"/i)?.[1]; if (!pts)
            continue;

        let segs = segsFromPoints(pts, true);
        const M = parseMatrix(tag.match(/\btransform="([^"]+)"/i)?.[1] || "");

        if (M)
            segs = applyMatrixToSegs(segs, M);

        push(segs, tag);
    }

    // ----- POLYLINE (закрываем)
    for (const tag of polylineTags) {
        if (skipByAttr(tag))
            continue;

        const pts = tag.match(/\spoints="([^"]+)"/i)?.[1]; if (!pts)
            continue;

        let segs = ensureClosed(segsFromPoints(pts, false));
        const M = parseMatrix(tag.match(/\btransform="([^"]+)"/i)?.[1] || "");

        if (M)
            segs = applyMatrixToSegs(segs, M);

        push(segs, tag);
    }

    // ----- RECT
    for (const tag of rectTags) {
        if (skipByAttr(tag))
            continue;

        const get = (n, def = 0) => +(tag.match(new RegExp(`\\s${n}="([^"]+)"`, "i"))?.[1] ?? def);
        let x = get("x"), y = get("y"), w = get("width"), h = get("height");

        if (!(isFinite(w) && isFinite(h) && w > 0 && h > 0))
            continue;

        const rx = Math.max(0, get("rx")), ry = Math.max(0, get("ry"));
        const r = Math.min(rx || ry || 0, Math.min(w, h) / 2);
        const pts = r > 0
            ? [{ x: x + r, y }, { x: x + w - r, y }, { x: x + w, y: y + r }, { x: x + w, y: y + h - r }, { x: x + w - r, y: y + h }, { x: x + r, y: y + h }, { x, y: y + h - r }, { x, y: y + r }]
            : [{ x, y }, { x: x + w, y }, { x: x + w, y: y + h }, { x, y: y + h }];
        let segs = segsFromPoints(pts.map(p => `${p.x},${p.y}`).join(" "), true);
        const M = parseMatrix(tag.match(/\btransform="([^"]+)"/i)?.[1] || "");

        if (M)
            segs = applyMatrixToSegs(segs, M);

        push(segs, tag);
    }

    // ----- CIRCLE/ELLIPSE (аппроксимация многоугольником)
    const approxEllipse = (cx, cy, rx, ry, steps = 72) => {
        const pts = [];
        for (let k = 0; k < steps; k++) {
            const t = (k / steps) * Math.PI * 2;
            pts.push({
                x: cx + rx * Math.cos(t), y: cy + ry * Math.sin(t)
            });
        }

        return segsFromPoints(pts.map(p => `${p.x},${p.y}`).join(" "), true);
    };

    for (const tag of circleTags) {
        if (skipByAttr(tag))
            continue;

        const get = (n, def = 0) => +(tag.match(new RegExp(`\\s${n}="([^"]+)"`, "i"))?.[1] ?? def);
        const cx = get("cx"), cy = get("cy"), r = get("r");

        if (!(isFinite(r) && r > 0))
            continue;

        let segs = approxEllipse(cx, cy, r, r);
        const M = parseMatrix(tag.match(/\btransform="([^"]+)"/i)?.[1] || "");

        if (M)
            segs = applyMatrixToSegs(segs, M);

        push(segs, tag);
    }

    for (const tag of ellipseTags) {
        if (skipByAttr(tag))
            continue;

        const get = (n, def = 0) => +(tag.match(new RegExp(`\\s${n}="([^"]+)"`, "i"))?.[1] ?? def);

        const cx = get("cx"), cy = get("cy"), rx = get("rx"), ry = get("ry"); if (!(isFinite(rx) && rx > 0 && isFinite(ry) && ry > 0))
            continue;

        let segs = approxEllipse(cx, cy, rx, ry);
        const M = parseMatrix(tag.match(/\btransform="([^"]+)"/i)?.[1] || "");

        if (M)
            segs = applyMatrixToSegs(segs, M);

        push(segs, tag);
    }

    // ----- LINE → тонкий «прямоугольник» по stroke-width
    for (const tag of lineTags) {
        if (skipByAttr(tag))
            continue;

        const get = (n, def = 0) => +(tag.match(new RegExp(`\\s${n}="([^"]+)"`, "i"))?.[1] ?? def);

        const x1 = get("x1"), y1 = get("y1"), x2 = get("x2"), y2 = get("y2"); if (![x1, y1, x2, y2].every(isFinite))
            continue;

        const sw = +(tag.match(/\sstroke-width="([^"]+)"/i)?.[1] ?? 1);
        const dx = x2 - x1, dy = y2 - y1, len = Math.hypot(dx, dy) || 1; const nx = -dy / len, ny = dx / len, t = (sw || 1) / 2;
        const pts = [
            { x: x1 + nx * t, y: y1 + ny * t },
            { x: x2 + nx * t, y: y2 + ny * t },
            { x: x2 - nx * t, y: y2 - ny * t },
            { x: x1 - nx * t, y: y1 - ny * t }
        ];

        let segs = segsFromPoints(pts.map(p => `${p.x},${p.y}`).join(" "), true);
        const M = parseMatrix(tag.match(/\btransform="([^"]+)"/i)?.[1] || "");

        if (M)
            segs = applyMatrixToSegs(segs, M);

        push(segs, tag);
    }

    __logPanels("tags", {
        path: pathTags.length,
        polygon: polygonTags.length,
        polyline: polylineTags.length,
        rect: rectTags.length,
        circle: circleTags.length,
        ellipse: ellipseTags.length,
        line: lineTags.length
    });


    // fallback: если вообще ничего не нашли — берём первый path
    if (!candidates.length) {
        const m = rawSVG.match(/<path[^>]*\sd="([^"]+)"[^>]*>/i);
        if (m) {
            const segs0 = ensureClosed(parsePathD(m[1]));
            pushCandidate(candidates, segs0, "<path>", "panel");
        }
    }
    if (!candidates.length)
        return [];

    __logPanels("candidates", { count: candidates.length });

    // --- Выбор явного основного контура (если в SVG есть path id="panel")
    const tagged = candidates.filter(c =>
        c.label === "panel" ||
        /id=["']panel["']/i.test(c.rawTag)
    );
    const hasExplicitPanel = tagged.length > 0;

    // если ВСЕ кандидаты являются panel'ами — просто оставляем их;
    // иначе panel'ы помечаем, но НЕ выкидываем остальной декор
    if (hasExplicitPanel && candidates.length === tagged.length) {
        candidates = tagged;
    }

    // --- Фильтрация по площади (адаптивная)
    candidates.sort((a, b) => b.bboxArea - a.bboxArea);
    const maxA = candidates[0].bboxArea || 1;
    const dominatesView = (candidates[0].bboxArea / (rootBox.w * rootBox.h || 1)) > 0.45;

    // если есть явный panel — ничего не режем по площади,
    // берём и основную деталь, и весь декор; иначе используем обычный порог
    const ratio = hasExplicitPanel ? 0 : 0.00001;

    __logPanels("areas", {
        maxA: Math.round(maxA || 0),
        dominatesView,
        ratio
    });

    let filtered = hasExplicitPanel
        ? candidates
        : candidates.filter(c => (c.bboxArea / maxA) >= ratio);


    // --- Dedup и ограничение количества
    const uniq = [];
    for (const c of filtered) {
        // геометрический отпечаток
        c.sig ||= segsSignature(c.segs);

        const same = uniq.some(u => {
            const areaRatio = Math.min(u.bboxArea, c.bboxArea) / Math.max(u.bboxArea, c.bboxArea);
            const iou = bboxIoU(u.bbox, c.bbox);

            // считаем дубликатом только если bbox почти совпал И геометрия совпала
            return (areaRatio > 0.999 && iou > 0.995) || (u.sig === c.sig);
        });

        if (!same)
            uniq.push(c);

        if (uniq.length >= PANEL_MAX_COUNT)
            break;
    }
    filtered = uniq;

    if (!filtered.length && candidates.length) {
        __logPanels("fallback", "keep-largest");
        filtered = [candidates[0]];
    }
    __logPanels("return", { out: filtered.length });


    // --- Финально
    return filtered.map((c, idx) => ({
        id: String(idx + 1),
        // показываем ровно то, что пришло из SVG; если пусто — нейтральный fallback
        label: c.label || `panel_${idx + 1}`,
        segs: c.segs,
        anchors: collectAnchors(c.segs)
    }));
}

// Загружает пресет: если sources[] — склеивает их в один набор панелей; если file — вернёт строку SVG (как раньше)
export const loadPresetToPanels = async (preset) => {
    if (Array.isArray(preset.sources) && preset.sources.length) {
        const partsAll = [];
        for (let i = 0; i < preset.sources.length; i++) {
            const src = preset.sources[i];
            const fileResolved = src.file; // (или твой resolveSourceFile, если используешь)
            const url = resolveSvgSrcPath(fileResolved);
            const txt = await fetch(url).then(r => r.text())
            const parts = extractPanels(txt); // парсим в панели (как обычно)
            const dx = +(src?.offset?.x ?? 0);
            const dy = +(src?.offset?.y ?? 0);
            const sx = +(src?.scale?.x ?? 1);
            const sy = +(src?.scale?.y ?? 1);
            const M = translateScaleMatrix(dx, dy, sx, sy);

            // детерминированный префикс по слоту/стороне/варианту:
            const prefix = (src.idPrefix ||
                [String(preset?.id || 'part'), src.product || 'hoodie', src.slot || 'part', src.side || 'both', src.which || 'main'].join('_')
            ).toLowerCase();

            let localIdx = 0;
            for (const p of parts) {
                const segsT = applyMatrixToSegs(p.segs, M);
                partsAll.push({
                    // стаб. id: НЕ зависит от внутренних id в svg-файле
                    id: `${prefix}__${localIdx++}`,
                    segs: segsT,
                    anchors: collectAnchors(segsT),
                    meta: { product: src.product || 'hoodie', slot: src.slot || null, side: src.side || null, which: src.which || null }
                });
            }
        }
        return partsAll;
    }

    console.warn('Preset without "sources" is not supported anymore:', preset);
    return [];
};

export const composePanelsForSide = async (sideId, details, manifest) => {
    // 1) база
    let baseSources = await getBaseSources(sideId);
    baseSources = (Array.isArray(baseSources) ? baseSources : []).map(e => ({
        file: e.file,
        slot: e.slot ?? null,          // «голый» слот (belt/cuff/neck/…)
        side: e.side ?? null,
        which: e.which ?? null,
        offset: e.offset ?? { x: 0, y: 0 },
        scale: e.scale ?? { x: 1, y: 1 },
        product: e.product ?? "hoodie" // ✅ сохраняем продукт
    }));
    const keyOf = (s) => [s.product || "hoodie", s.slot || "", s.side || "", s.which || ""].join("|");
    const baseIdx = new Map(baseSources.map(s => [keyOf(s), s]));

    const chosen = (details[sideId] || {});
    const hoodActive = !!(chosen["hoodie.hood"] && chosen["hoodie.hood"] !== "base"); // ✅ неймспейс
    if (hoodActive) {
        baseSources = baseSources.filter(s => !((s.product || "hoodie") === "hoodie" && (s.slot || "").toLowerCase() === "neck")); // ✅ убираем только шею худи
    }

    const sources = baseSources.slice();

    // 2) применяем варианты (ровно как в useEffect)
    for (const [nsSlot, variantId] of Object.entries(chosen)) {
        if (!variantId || variantId === "base") continue;
        // nsSlot: "hoodie.cuff" | "pants.belt" ...
        const [productRaw, ...rest] = String(nsSlot || "").split(".");
        const product = productRaw || "hoodie";
        const slot = rest.length ? rest.join(".") : nsSlot; // «голый» слот
        const list = manifest?.variants?.[slot] || [];
        const v = list.find(x => x.id === variantId && ((x.product || "hoodie") === product));
        if (!v) continue;

        const fmap = v.files?.[sideId] || {};
        if (!fmap || Object.keys(fmap).length === 0) continue;

        const sLower = (slot || "").toLowerCase();
        const allowSides = (sLower === "cuff" || sLower === "sleeve");

        let entries = [];
        if (fmap.file) entries.push({ file: fmap.file, side: null, which: null });
        if (allowSides && fmap.left) entries.push({ file: fmap.left, side: "left", which: null });
        if (allowSides && fmap.right) entries.push({ file: fmap.right, side: "right", which: null });
        if (fmap.inner) entries.push({ file: fmap.inner, side: null, which: "inner" });

        // const hasBaseFor = (side, which) => baseIdx.has([product, slot, side || "", which || ""].join("|"));
        // if (sLower !== "hood") entries = entries.filter(e => hasBaseFor(e.side, e.which));

        for (const e of entries) {
            const k = [product, slot, e.side || "", e.which || ""].join("|");
            const baseHit = baseIdx.get(k);
            if (baseHit) {
                baseHit.file = e.file;                      // смещения/масштабы остаются от baseHit
            } else {
                // если слот добавляется впервые — кладём с нейтральными offset/scale
                sources.push({
                    file: e.file, slot,
                    side: e.side || null, which: e.which || null,
                    product,
                    offset: { x: 0, y: 0 }, scale: { x: 1, y: 1 }
                });
            }
        }
    }

    // 3) капюшон — в конец (перекрывает всё)
    if (sources.length) {
        const hoodParts = [], rest = [];
        for (const src of sources) {
            const isHood = (String(src.slot).toLowerCase() === "hood") && ((src.product || "hoodie") === "hoodie");
            (isHood ? hoodParts : rest).push(src);
        }
        sources.length = 0;
        sources.push(...rest, ...hoodParts); // ✅ корректный спред
    }

    // DIAG-лог: что реально идёт в сборку панелей
    console.log("[compose]", sideId, {
        count: sources.length,
        sources: sources.map(s => ({
            file: s.file, slot: s.slot, side: s.side, which: s.which,
            scale: s.scale, offset: s.offset, product: s.product
        }))
    });

    // 4) собрать панели с тем же загрузчиком
    return await loadPresetToPanels({ id: sideId, sources });

};