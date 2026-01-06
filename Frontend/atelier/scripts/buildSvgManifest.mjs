import fs from "fs/promises";
import path from "path";

const ROOT = process.cwd();
const IMG_EXT_RE = /\.(svg|png|jpe?g)$/i;

// ——— конфиг изделий и желаемые смещения на канве ———
const PRODUCTS = [
    {
        key: "hoodie",
        slots: ["body", "belt", "sleeve", "cuff", "neck", "hood", "pocket"],
        // смещение базовых деталей на канве
        offsets: {
            front: { x: 0, y: 0 },
            back: { x: 0, y: 0 }
        },
        scale: {
            front: { x: 1, y: 1 },
            back: { x: 1, y: 1 }
        }
    },
    {
        key: "pants",
        slots: ["body", "belt", "leg", "cuff"],
        offsets: {
            front: { x: 0, y: 0 },
            back: { x: 0, y: 0 }
        },
        scale: {
            front: { x: 1, y: 1 },
            back: { x: 1, y: 1 }
        }
    },
];

function toUnix(p) { return p.split(path.sep).join("/"); }
function urlOf(abs) {
    return toUnix(abs).split("/public").pop().replace(/^\/+/, "").toLowerCase();
}
async function firstExisting(...cands) {
    for (const p of cands) { try { await fs.access(p); return p; } catch { } }
    return cands[0];
}

function detectBaseFileMeta(filename) {
    const f = filename.toLowerCase();
    const meta = { slot: null, side: null, which: null };
    if (f.includes("cuff")) meta.slot = "cuff";
    else if (f.includes("sleeve")) meta.slot = "sleeve";
    else if (f.includes("belt")) meta.slot = "belt";
    else if (f.includes("neck")) meta.slot = "neck";
    else if (f.includes("body")) meta.slot = "body";
    else if (f.includes("hood")) meta.slot = "hood";
    else if (f.includes("pocket")) meta.slot = "pocket";
    else if (f.includes("leg")) meta.slot = "leg";

    if (f.includes("left")) meta.side = "left";
    if (f.includes("right")) meta.side = "right";
    if (f.includes("internal")) meta.which = "inner";

    return meta.slot ? meta : null;
}

function parseVariantName(file, slot, faceHint = null) {
    const raw = path.basename(file).toLowerCase().replace(IMG_EXT_RE, "");
    const isPreview = raw.endsWith("_preview");
    const name = isPreview ? raw.replace(/_preview$/, "") : raw;

    let face = null, side = null, which = null, id = null;
    if (name.startsWith("front_")) face = "front";
    if (name.startsWith("back_")) face = "back";
    if (!face) face = faceHint;

    const rest = name.replace(/^(front_|back_)/, "");
    const sideSlots = new Set(["cuff", "sleeve", "leg"]);
    if (sideSlots.has(slot)) {
        const m = rest.match(/^(.*?)(?:_(left|right))(.*)$/);
        side = m?.[2] || (rest.endsWith("_left") ? "left" : rest.endsWith("_right") ? "right" : null);
        id = m ? (m[1] + (m[3] || "")) : rest.replace(/_(left|right)$/, "");
    } else if (slot === "neck") {
        which = rest.endsWith("_internal") ? "inner" : null;
        id = rest.replace(/_internal$/, "");
    } else {
        id = rest;
    }
    return { face, side, which, id, preview: isPreview };
}

async function collectBase({ product, FRONT_DIR, BACK_DIR }, slotOrder, offsets, scaleByFace) {
    const base = { front: [], back: [] };

    for (const [dir, face] of [[FRONT_DIR, "front"], [BACK_DIR, "back"]]) {
        const entries = await fs.readdir(dir, { withFileTypes: true }).catch(() => []);
        for (const d of entries) {
            if (!d.isFile() || !d.name.toLowerCase().endsWith(".svg")) continue;

            // не тащим превью в базовые детали
            if (/_preview\.(svg|png|jpe?g)$/i.test(d.name)) continue;

            const meta = detectBaseFileMeta(d.name);
            if (!meta) continue;

            const scale = (scaleByFace && (scaleByFace.front || scaleByFace.back))
                ? (scaleByFace[face] || { x: 1, y: 1 })
                : (scaleByFace || { x: 1, y: 1 });

            base[face].push({
                file: urlOf(path.join(dir, d.name)),
                product,
                slot: meta.slot,
                side: meta.side ?? null,
                which: meta.which ?? null,
                offset: offsets?.[face] ?? { x: 0, y: 0 },
                scale
            });
        }
    }
    // стабильная сортировка по порядку слотов
    for (const face of ["front", "back"]) {
        base[face].sort((a, b) => slotOrder.indexOf(a.slot) - slotOrder.indexOf(b.slot));
    }
    return base;
}

async function collectVariants({ product, VAR_DIR, FRONT_DIR, BACK_DIR }, slotOrder, offsets) {
    const bySlot = Object.fromEntries(slotOrder.map(s => [s, []]));
    const roots = [VAR_DIR, path.join(FRONT_DIR, "variants"), path.join(BACK_DIR, "variants")];

    for (const slot of slotOrder) {
        const files = [];
        for (const root of roots) {
            try {
                const dir = path.join(root, slot);
                const list = (await fs.readdir(dir, { withFileTypes: true }))
                    .filter(d => d.isFile() && IMG_EXT_RE.test(d.name.toLowerCase()))
                    .map(d => ({
                        name: d.name,
                        abs: path.join(dir, d.name),
                        faceHint: root.includes(`${path.sep}front`) ? "front" :
                            root.includes(`${path.sep}back`) ? "back" : null,
                    }));
                files.push(...list);
            } catch { }
        }

        const groups = new Map();
        for (const f of files) {
            const meta = parseVariantName(f.name, slot, f.faceHint);
            if (!meta?.id) continue;

            // если face не определён (например, файл лежит в общих /variants/slot),
            // считаем его подходящим и для front, и для back
            if (!meta.face) {
                // для слотов с левой/правой стороной — копируем в обе стороны
                if (["cuff", "sleeve", "leg"].includes(slot)) {
                    meta.face = "both";
                } else {
                    // остальные — хотя бы для front
                    meta.face = "front";
                }
            }

            if (!groups.has(meta.id)) {
                groups.set(meta.id, {
                    id: meta.id,
                    name: meta.id,
                    product,
                    preview: null,
                    files: { front: {}, back: {} },
                    offset: offsets || { front: { x: 0, y: 0 }, back: { x: 0, y: 0 } },
                });
            }
            const g = groups.get(meta.id);
            const url = urlOf(f.abs);

            if (meta.preview) {
                g.preview = url;
            } else if (meta.face === "front" || meta.face === "both") {
                if (["cuff", "sleeve", "leg"].includes(slot)) {
                    if (meta.side) g.files.front[meta.side] = url;
                } else if (slot === "neck" && meta.which === "inner") {
                    g.files.front.inner = url;
                } else {
                    g.files.front.file = url;
                }
            }

            if (meta.face === "back" || meta.face === "both") {
                if (["cuff", "sleeve", "leg"].includes(slot)) {
                    if (meta.side) g.files.back[meta.side] = url;
                } else {
                    g.files.back.file = url;
                }
            }

        }
        bySlot[slot] = [...groups.values()].sort((a, b) => a.id.localeCompare(b.id));
    }

    return bySlot;
}

async function build() {
    // собираем hoodie + pants в один манифест
    const allBase = { front: [], back: [], previews: { front: {}, back: {} } };
    const allVariants = {};
    const layout = {};

    for (const prod of PRODUCTS) {
        const PRODUCT_DIR = await firstExisting(
            path.join(ROOT, "public/2d/svg", prod.key),
            path.join(ROOT, "public/2d/svg", prod.key[0].toUpperCase() + prod.key.slice(1)),
        );
        const FRONT_DIR = await firstExisting(path.join(PRODUCT_DIR, "front"), path.join(PRODUCT_DIR, "Front"));
        const BACK_DIR = await firstExisting(path.join(PRODUCT_DIR, "back"), path.join(PRODUCT_DIR, "Back"));
        const VAR_DIR = await firstExisting(path.join(PRODUCT_DIR, "variants"), path.join(PRODUCT_DIR, "Variants"));

        // layout по продукту и стороне
        layout[prod.key] = {
            front: {
                offset: prod.offsets?.front ?? { x: 0, y: 0 },
                scale: (prod.scale?.front ?? prod.scale ?? { x: 1, y: 1 })
            },
            back: {
                offset: prod.offsets?.back ?? { x: 0, y: 0 },
                scale: (prod.scale?.back ?? prod.scale ?? { x: 1, y: 1 })
            }
        };

        // базовые детали
        const base = await collectBase({ product: prod.key, FRONT_DIR, BACK_DIR }, prod.slots, prod.offsets, prod.scale);
        allBase.front.push(...base.front);
        allBase.back.push(...base.back);

        // превью базовых слотов
        for (const face of ["front", "back"]) {
            for (const s of prod.slots) {
                const candidates = [
                    path.join(FRONT_DIR, `${s}_preview.svg`), path.join(FRONT_DIR, `${s}_preview.png`), path.join(FRONT_DIR, `${s}_preview.jpg`),
                    path.join(BACK_DIR, `${s}_preview.svg`), path.join(BACK_DIR, `${s}_preview.png`), path.join(BACK_DIR, `${s}_preview.jpg`),
                ];
                for (const c of candidates) {
                    try {
                        await fs.access(c);
                        if (!allBase.previews[face][s]) allBase.previews[face][s] = urlOf(c);
                    } catch { }
                }
            }
        }

        // варианты слотов
        const variants = await collectVariants({ product: prod.key, VAR_DIR, FRONT_DIR, BACK_DIR }, prod.slots, prod.offsets);
        for (const slot of Object.keys(variants)) {
            allVariants[slot] = (allVariants[slot] || []).concat(variants[slot]);
        }
    }

    // базовые «виртуальные» опции для каждого слота
    const baseVariantBySlot = {};
    for (const slot of new Set(Object.keys(allVariants).concat(PRODUCTS.flatMap(p => p.slots)))) {
        baseVariantBySlot[slot] = { id: "base", name: "Базовая", preview: null, files: { front: {}, back: {} } };
    }

    const manifest = {
        version: 2,
        // теперь включает hoodie + pants вместе
        base: allBase,
        variants: allVariants,
        baseVariantBySlot,
        // универсальный пустой превьюшник (если понадобится в UI)
        emptyPreview: "2d/svg/empty.svg",
        layout
    };

    // Пишем в манифест худи (чтобы ничего не менять в пути загрузки)
    const OUT = path.join(ROOT, "public/2d/svg", "manifest.json");
    await fs.writeFile(OUT, JSON.stringify(manifest, null, 2), "utf8");
    console.log("✅ manifest (hoodie+pants):", toUnix(OUT));
}

build().catch(e => { console.error(e); process.exit(1); });