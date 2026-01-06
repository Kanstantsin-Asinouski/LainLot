import { MANIFEST_URL } from "./svgPath";

// Runtime-обёртка над manifest.json
let _manifestCache = null;

const FORCED_SLOTS = {
    front: new Set(["hood", "pocket"]),
    back: new Set(["hood"]),
};

// алиасы имён слотов (на случай разных названий в манифесте)
// + leg
const SLOT_ALIASES = {
    cuff: "cuff",
    sleeve: "sleeve",
    neck: "neck",
    belt: "belt",
    body: "body",
    hood: "hood",
    pocket: "pocket",
    leg: "leg",        // <— добавь
};

// ——— helpers ———
const parseSlot = (slot) => {
    const parts = String(slot || "").split(".");
    return {
        product: parts.length > 1 ? parts[0] : "hoodie",
        pure: parts.length > 1 ? parts.slice(1).join(".") : parts[0],
    };
};
const shouldSyncSlot = (slotOrPure) => {
    const pure = String(slotOrPure || "").split(".").pop().toLowerCase();
    // синхронизируем всё кроме кармана
    return pure !== "pocket";
};

export function reduceSetSlotVariant(
    prev,
    { face, slot, variantId, prevNeckByFace }
) {
    const { product, pure } = parseSlot(slot);
    const faceOther = face === "front" ? "back" : "front";
    const curFace = { ...(prev[face] || {}) };
    const curOther = { ...(prev[faceOther] || {}) };
    const nextPrevNeck = { ...(prevNeckByFace || {}) };

    const ns = `${product}.${pure}`;
    const hoodKey = "hoodie.hood";
    const neckKey = "hoodie.neck";

    const hoodTurnOn = (product === "hoodie" && pure === "hood" && variantId && variantId !== "base");
    const hoodTurnOff = (product === "hoodie" && pure === "hood" && (!variantId || variantId === "base"));
    const neckChanging = (product === "hoodie" && pure === "neck");

    // если меняют шею — гасим активный капюшон на этой стороне
    if (neckChanging) {
        if (curFace[hoodKey] && curFace[hoodKey] !== "base") {
            delete curFace[hoodKey];
        }
    }
    // включают капюшон → временно убираем шею и запоминаем её
    if (hoodTurnOn) {
        nextPrevNeck[face] = curFace[neckKey] ?? "base";
        delete curFace[neckKey];
    }
    // выключают капюшон → вернём сохранённую шею
    if (hoodTurnOff) {
        const prevNeck = nextPrevNeck[face];
        if (prevNeck != null) {
            if (prevNeck === "base") delete curFace[neckKey];
            else curFace[neckKey] = prevNeck;
        }
    }

    // применяем текущий слот
    if (!variantId || variantId === "base") {
        delete curFace[ns];
        if (shouldSyncSlot(pure)) delete curOther[ns];
    } else {
        curFace[ns] = variantId;
        if (shouldSyncSlot(pure)) curOther[ns] = variantId;
    }

    // при явной смене шеи — обновим "память" текущего состояния
    if (neckChanging) {
        nextPrevNeck[face] = curFace[neckKey] ?? "base";
    }

    const nextDetails = { ...prev, [face]: curFace, [faceOther]: curOther };
    return { nextDetails, nextPrevNeck };
}

export function isForcedSlot(face, slot) {
    const f = face === "back" ? "back" : "front";
    const pure = String(slot || "").split(".").pop();   // ← поддержка "hoodie.hood"
    return FORCED_SLOTS[f].has(pure);
}

export async function loadSvgManifest() {
    if (_manifestCache) return _manifestCache;
    const res = await fetch(MANIFEST_URL, { cache: "no-store" });
    if (!res.ok) throw new Error(`Manifest not found at ${MANIFEST_URL} (status ${res.status})`);
    const ct = (res.headers.get("content-type") || "").toLowerCase();
    if (!ct.includes("json")) {
        const head = await res.text();
        console.error("Manifest is not JSON. First bytes:", head.slice(0, 80));
        throw new Error("Manifest is not JSON");
    }
    _manifestCache = await res.json();
    return _manifestCache;
}

// База содержит ли слот (поддерживает неймспейсный слот "hoodie.cuff")
export async function baseHasSlot(face, slot) {
    const m = await loadSvgManifest();
    const list = m?.base?.[face] || [];
    const parts = String(slot || "").split(".");
    const product = parts.length > 1 ? parts[0] : null;
    const pure = parts.length > 1 ? parts.slice(1).join(".") : parts[0];
    return list.some(e => {
        const prod = e?.product || "hoodie";
        return e?.slot === pure && (!product || prod === product);
    });
}

// Варианты для слота (поддерживает неймспейс "hoodie.cuff" | "pants.cuff")
export async function getVariantsForSlot(slot) {
    const m = await loadSvgManifest();
    const { product, pure } = parseSlot(slot);

    const baseV = [{ id: "base", name: "Базовая", files: { front: {}, back: {} }, product }];

    // список по чистому слоту
    let list = m?.variants?.[pure] || [];
    // оставляем только варианты нужного продукта
    list = list.filter(v => (v?.product || "hoodie") === product);

    // если сохранена активная сторона — фильтруем по наличию файлов на стороне
    let face = null;
    try { face = (localStorage.getItem("ce.activeFace") || "").toLowerCase(); } catch { }
    if (face === "front" || face === "back") {
        list = list.filter(v => {
            const f = v?.files?.[face] || {};
            return !!(f.file || f.left || f.right || f.inner);
        });
    }

    // uniq по id
    const seen = new Set();
    const uniq = [];
    for (const v of list) {
        if (v?.id && !seen.has(v.id)) {
            seen.add(v.id);
            uniq.push(v);
        }
    }
    return [...baseV, ...uniq];
}

// База (глубокая копия + дефолт product = "hoodie", если не указан)
export async function getBaseSources(face) {
    const m = await loadSvgManifest();
    const f = (face === 'back') ? 'back' : 'front';
    const src = m.base?.[f] || [];
    return src.map(e => ({
        file: e.file,
        slot: e.slot ?? null,
        side: e.side ?? null,
        which: e.which ?? null,
        offset: e.offset ?? { x: 0, y: 0 },
        scale: e.scale ?? { x: 1, y: 1 },
        product: e.product ?? "hoodie",
    }));
}

// Превью базы: сначала ищем ключ с неймспейсом, затем — по чистому имени
export async function getBasePreview(slot, face) {
    const m = await loadSvgManifest();
    const f = (face === 'back') ? 'back' : 'front';
    const nsKey = String(slot || "");
    const pure = nsKey.split(".").pop();
    // приоритет: для pants/… НЕ откатываемся на "pure" (чтобы не тянуть hoodie-превью)
    const product = nsKey.includes(".") ? nsKey.split(".")[0] : null;
    if (product && product !== "hoodie") {
        return m?.base?.previews?.[f]?.[nsKey] || null;
    }
    return m?.base?.previews?.[f]?.[nsKey] || m?.base?.previews?.[f]?.[pure] || null;
}

// Слот доступен на стороне?
export async function hasSlotForFace(slot, face) {
    const m = await loadSvgManifest();
    const arr = (m?.base && m.base[face]) || [];
    const ns = String(slot || "");
    const pure = ns.split(".").pop();
    const product = ns.includes(".") ? ns.split(".")[0] : null;

    const hasBase = arr.some(x => {
        const prod = x?.product || "hoodie";        // ← дефолт как в baseHasSlot
        return x?.slot === pure && (!product || prod === product);
    });

    // Исключение для капюшона: если базы нет, но есть вариант с файлами на этой стороне — считаем слот доступным.
    let hasVariantWithFiles = false;
    if (!hasBase && pure === "hood" && (!product || product === "hoodie")) {
        const list = m?.variants?.[pure] || [];
        hasVariantWithFiles = list.some(v => {
            if ((v?.product || "hoodie") !== "hoodie") return false;
            const f = v?.files?.[face] || {};
            return !!(f.file || f.left || f.right || f.inner);
        });
    }

    // 🔹 Исключение для кармана — логика такая же, как у hood
    if (!hasBase && pure === "pocket" && (!product || product === "hoodie")) {
        const list = m?.variants?.[pure] || [];
        const hasPocketFiles = list.some(v => {
            if ((v?.product || "hoodie") !== "hoodie") return false;
            const f = v?.files?.[face] || {};
            return !!(f.file || f.left || f.right || f.inner);
        });
        if (hasPocketFiles) return true;
    }

    return hasBase || hasVariantWithFiles;
}

// 🔹 Какие слоты показывать в меню на этой стороне.
// ВОЗВРАЩАЕМ **НЕЙМСПЕЙСНЫЕ** ключи: "hoodie.cuff", "pants.cuff", ...
export async function getVisibleSlotsForFace(face) {
    const m = await loadSvgManifest();
    const f = face === 'back' ? 'back' : 'front';

    const candidates = new Set();

    // 1) База (если в manifest.base задан product/slоt)
    for (const e of (m?.base?.[f] || [])) {
        if (e?.slot) candidates.add(`${e.product || "hoodie"}.${e.slot}`);
    }

    // 2) Превью базы (ключ может быть с/без префикса → нормализуем)
    Object.keys(m?.base?.previews?.[f] || {}).forEach((k) => {
        const pure = String(k || "").split(".").pop();
        const product = k.includes(".") ? k.split(".")[0] : "hoodie";
        if (pure) candidates.add(`${product}.${pure}`);
    });

    // 3) Варианты, у которых есть файлы на этой стороне
    for (const [slot, list] of Object.entries(m?.variants || {})) {
        for (const v of (list || [])) {
            const map = v?.files?.[f] || {};
            if (map.file || map.left || map.right || map.inner) {
                const product = v?.product || "hoodie";
                candidates.add(`${product}.${slot}`);
            }
        }
    }

    // ✅ Финальный фильтр: оставляем только реально существующие на стороне слоты
    const result = new Set();

    for (const ns of candidates) {
        if (await hasSlotForFace(ns, f)) result.add(ns);
    }

    return Array.from(result);
}