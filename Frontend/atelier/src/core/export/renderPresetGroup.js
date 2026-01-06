import { area } from "../geometry/bounds.js";
import { waveAlongPolyline } from "../geometry/polylineOps.js";
import { facePath, faceKey, segsToD } from "../svg/faceUtils.js";
import { splitSegsIntoSubpaths, polylineFromSubpath, catmullRomToBezierPath } from "../svg/polylineOps.js";
import { buildFacesForExport } from "./facesForExport.js";

// === RENDER-PRESET-GROUP: BUILD TAG ===
console.log("[rGroup] MODULE LOADED A1");
if (typeof window !== "undefined") window.__RG_BUILD = "A1";

const shortId = (id) => {
    const s = String(id);
    return s.includes("-") ? s.split("-").pop() : s;
};

/** Рендер одной стороны (цвета, контуры, линии) */
export const renderPresetGroup = (
    panels,
    curvesByPanelLocal = {},
    fillsLocal = [],
    opts = { inkscapeCompat: true, maskId: undefined }
) => {
    // Вспомогательные: берём «внешнее кольцо» панели (самый большой по площади полигон)
    const ringByPanel = {};
    for (const p of panels) {
        const subs = splitSegsIntoSubpaths(p.segs);
        let best = null, bestA = -Infinity;
        for (const sp of subs) {
            const r = polylineFromSubpath(sp);
            if (r && r.length >= 3) {
                const A = Math.abs(area(r));
                if (A > bestA) { bestA = A; best = r; }
            }
        }
        ringByPanel[p.id] = best;
    }

    const facesByPanel = buildFacesForExport(panels, curvesByPanelLocal);
    const isHood = (p) => String(p.meta?.slot || '').toLowerCase() === 'hood';

    const piecesNonHood = [];
    const piecesHood = [];
    // DIAG: на время диагностики принудительно отключаем маску
    const useMask = false;
    const maskId = undefined;

    // DIAG-лог (что реально летит в рендер)
    console.log("[rGroup] start", {
        panels: Array.isArray(panels) ? panels.length : 0,
        fills: Array.isArray(fillsLocal) ? fillsLocal.length : 0,
        curvesPanels: curvesByPanelLocal ? Object.keys(curvesByPanelLocal).length : 0
    });

    // Рендер одной панели в «корзину»
    const renderPanelPieces = (p, bucket) => {
        const full = String(p.id);
        const short = shortId(p.id);

        // 1) Заливки
        const panelFills = (Array.isArray(fillsLocal) ? fillsLocal : []).filter(f => {
            const fid = String(f.panelId);
            return fid === full || fid === short;
        });
        if (panelFills.length) {
            const faces = facesByPanel[p.id] || [];
            const ring = ringByPanel[p.id];
            for (const f of panelFills) {
                const poly = faces.find(poly => faceKey(poly) === f.faceKey) || f.poly || ring;
                if (!poly) continue;
                bucket.push(
                    `<path d="${facePath(poly)}" fill="${f.color}" fill-opacity="${f.opacity ?? 0.9}" stroke="none"/>`
                );
            }
        }

        // 2) Контуры деталей — stroke по исходным сегментам (без принудительного замыкания)
        const dStroke = segsToD(p.segs);
        if (dStroke) {
            bucket.push(
                `<path d="${dStroke}" fill="none" stroke="#111" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/>`
            );
        }

        // 3) Пользовательские линии/швы
        const userCurves = (curvesByPanelLocal?.[p.id] || curvesByPanelLocal?.[short] || []);
        for (const c of userCurves) {
            let d = null;
            if (c.type === "cubic" && c.ax != null) {
                d = `M ${c.ax} ${c.ay} C ${c.c1.x} ${c.c1.y} ${c.c2.x} ${c.c2.y} ${c.bx} ${c.by}`;
            }
            if (!d && typeof c.d === "string") d = c.d;
            if (!d && Array.isArray(c.segs) && c.segs.length) d = segsToD(c.segs);
            if (!d && Array.isArray(c.pts) && c.pts.length >= 2 && (c.type === "wavy" || c.lineStyle === "wavy")) {
                const waved = waveAlongPolyline(c.pts, c.waveAmpPx ?? 6, c.waveLenPx ?? 28);
                d = catmullRomToBezierPath(waved);
            }
            if (d) {
                bucket.push(
                    `<path d="${d}" fill="none" stroke="${c.color || "#1f2937"}" stroke-width="${c.width || 2}" stroke-linecap="round"/>`
                );
            }
        }
    };

    // Собираем детали по двум слоям: «не капюшон» и «капюшон»
    for (const p of panels) {
        renderPanelPieces(p, isHood(p) ? piecesHood : piecesNonHood);
    }

    // Маска «под капюшоном»
    const hoodRings = panels.filter(isHood).map(p => ringByPanel[p.id]).filter(Boolean);

    // Если капюшона нет ИЛИ маску не просили — плоский рендер без маски
    if (!hoodRings.length || !useMask) {
        console.log("[rGroup] flat-render", { hoodRings: hoodRings.length, useMask });
        return `<g>${piecesNonHood.join("")}${piecesHood.join("")}</g>`;
    }

    // Иначе — применяем маску (maskId должен быть явно задан)
    const hugeRect = `<rect x="-10000" y="-10000" width="40000" height="40000" fill="#fff"/>`;
    const mask = `<mask id="${maskId}" maskUnits="userSpaceOnUse">${hugeRect}${hoodRings.map(r =>
        `<path d="${facePath(r)}" fill="#000"/>`
    ).join("")}</mask>`;

    return `<g><defs>${mask}</defs><g mask="url(#${maskId})">${piecesNonHood.join("")}</g>${piecesHood.join("")}</g>`;

};
