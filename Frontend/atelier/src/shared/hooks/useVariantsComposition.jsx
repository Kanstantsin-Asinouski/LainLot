import { useEffect, useMemo, useRef, useState } from "react";
import { PRESETS } from "../../core/variables/presets.js";
import { getBaseSources, loadSvgManifest } from "../../core/variables/variants.js";
import { loadPresetToPanels } from "../../core/svg/extractPanels.js";
import { computeRingsByPanel, pickOuterRing } from "../../core/svg/buildFaces.js";

export function useVariantsComposition({ presetIdx, details, savedByPresetRef, applySnapshot }) {
    const [manifest, setManifest] = useState(null);
    const [isLoadingPreset, setIsLoadingPreset] = useState(false);
    const [composedPanels, setComposedPanels] = useState(null);
    const [svgMountKey, setSvgMountKey] = useState(0);
    const [svgCache, setSvgCache] = useState({});
    const [panels, setPanels] = useState([]);
    const svgCacheRef = useRef({});
    const panelSlotMapRef = useRef(new Map());
    const currentPresetIdRef = useRef(PRESETS[0]?.id || "front");
    const changeKindRef = useRef(null);
    const lastChangedSlotRef = useRef(null);
    const restoringPresetRef = useRef(false);
    const detailsRef = useRef(details);

    // manifest
    useEffect(() => {
        (async () => {
            try {
                const m = await loadSvgManifest();
                setManifest(m);
            } catch (e) {
                console.error(e);
            }
        })();
    }, []);

    useEffect(() => {
        const prev = detailsRef.current;
        const cur = details;

        const changes = [];
        for (const face of ['front', 'back']) {
            const p = prev[face] || {}, c = cur[face] || {};
            for (const slot of Object.keys({ ...p, ...c })) {
                if (p[slot] !== c[slot]) {
                    changes.push({ presetId: face, slot });
                }
            }
        }

        if (changes.length) {
            changeKindRef.current = 'slot';
            const curFace = currentPresetIdRef.current;
            const preferred = changes.find(ch => ch.presetId === curFace) || changes[0];
            lastChangedSlotRef.current = preferred;
        }

        detailsRef.current = cur;
    }, [details]);

    useEffect(() => {
        if (!manifest) return;
        let alive = true;
        (async () => {
            const preset = PRESETS[presetIdx];
            if (!preset) return;
            setIsLoadingPreset(true);

            let baseSources = await getBaseSources(preset.id);
            baseSources = (Array.isArray(baseSources) ? baseSources.map(e => ({
                file: e.file,
                slot: e.slot ?? null,
                side: e.side ?? null,
                which: e.which ?? null,
                offset: e.offset ?? { x: 0, y: 0 },
                product: e.product ?? null,
                scale: e.scale ?? { x: 1, y: 1 }
            })) : []);

            const keyOf = (s) => [s.product || "hoodie", s.slot || "", s.side || "", s.which || ""].join("|");
            const baseIdx = new Map(baseSources.map(s => [keyOf(s), s]));

            const chosen = details[preset.id] || {};
            const hoodActive = Object.entries(chosen).some(
                ([slotKey, val]) => val && val !== "base" && String(slotKey).split(".").pop().toLowerCase() === "hood"
            );

            if (hoodActive) {
                baseSources = baseSources.filter(s => (s.slot || "").toLowerCase() !== "neck");
            }

            const sources = baseSources.slice();

            for (const [slotFull, variantId] of Object.entries(chosen)) {
                if (!variantId || variantId === "base") continue;
                const pure = String(slotFull).split(".").pop();
                const product = slotFull.includes(".") ? slotFull.split(".")[0] : "hoodie";
                const list =
                    (manifest?.variants?.[pure]) ||
                    (manifest?.variants?.[slotFull]) ||
                    [];
                const v = list.find(x => x.id === variantId);
                if (!v) continue;

                const fmap = v.files?.[preset.id] || {};

                if (!fmap || Object.keys(fmap).length === 0) {
                    continue;
                }
                const sLower = pure.toLowerCase();
                const allowSides = (sLower === "cuff" || sLower === "sleeve");

                let entries = [];
                if (fmap.file) entries.push({ file: fmap.file, side: null, which: null });
                if (allowSides && fmap.left) entries.push({ file: fmap.left, side: "left", which: null });
                if (allowSides && fmap.right) entries.push({ file: fmap.right, side: "right", which: null });
                if (fmap.inner) entries.push({ file: fmap.inner, side: null, which: "inner" });

                const hasBaseFor = (side, which) => baseIdx.has([product, pure, side || "", which || ""].join("|"));
                // hood и pocket — разрешены даже без базы
                if (sLower !== "hood" && sLower !== "pocket") {
                    entries = entries.filter(e => hasBaseFor(e.side, e.which));
                }

                for (const e of entries) {
                    const k = [product, pure, e.side || "", e.which || ""].join("|");
                    const baseHit = baseIdx.get(k);
                    if (baseHit) {
                        baseHit.file = e.file;
                        baseHit.offset = baseHit.offset ?? { x: 0, y: 0 };
                    } else {
                        sources.push({
                            file: e.file,
                            slot: pure,
                            side: e.side || null,
                            which: e.which || null,
                            product,
                            offset: baseIdx.get([product, pure, "", ""].join("|"))?.offset ?? { x: 0, y: 0 },
                            scale: baseIdx.get([product, pure, "", ""].join("|"))?.scale ?? { x: 1, y: 1 }
                        });
                    }
                }
            }

            if (sources.length) {
                const hoodParts = [];
                const rest = [];
                for (const src of sources) {
                    const isHood = (String(src.slot).toLowerCase() === "hood") && ((src.product || "hoodie") === "hoodie");
                    if (isHood) hoodParts.push(src); else rest.push(src);
                }
                sources.length = 0;
                sources.push(...rest, ...hoodParts);
            }

            const compiled = await loadPresetToPanels({ ...preset, sources });
            if (!alive) return;
            setComposedPanels(Array.isArray(compiled) ? compiled : []);
            setSvgCache(prev => ({ ...prev, [preset.id]: Array.isArray(compiled) ? compiled : [] }));
            setSvgMountKey(k => k + 1);

        })().catch(() => {
            if (alive)
                setComposedPanels([]);
        }).finally(() => {
            if (alive)
                setIsLoadingPreset(false);
        });
        return () => { alive = false; };
    }, [presetIdx, manifest, details]);

    useEffect(() => {
        if (!composedPanels) return;

        setPanels(composedPanels);
        const map = new Map();
        for (const p of composedPanels) map.set(p.id, p.meta?.slot || null);
        panelSlotMapRef.current = map;

        const presetId = currentPresetIdRef.current;

        if (changeKindRef.current === 'preset') {
            const snap = savedByPresetRef.current[presetId];
            applySnapshot(snap, composedPanels);
        }
        changeKindRef.current = null;
        lastChangedSlotRef.current = null;

        if (restoringPresetRef.current) setTimeout(() => { restoringPresetRef.current = false; }, 0);
    }, [composedPanels, applySnapshot, savedByPresetRef]);

    const hoodPanelIds = useMemo(() => {
        return new Set(
            panels
                .filter(p => String(p.meta?.slot || '').toLowerCase() === 'hood')
                .map(p => p.id)
        );
    }, [panels]);

    const ringsByPanel = useMemo(() => computeRingsByPanel(panels), [panels]);
    const outerRingByPanel = useMemo(() => pickOuterRing(panels, ringsByPanel), [panels, ringsByPanel]);

    const hoodRings = useMemo(() => {
        return panels
            .filter(p => hoodPanelIds.has(p.id))
            .map(p => outerRingByPanel[p.id])
            .filter(Boolean);
    }, [panels, outerRingByPanel, hoodPanelIds]);

    const hoodHoles = useMemo(() => {
        const holes = [];
        for (const p of panels) {
            if (!hoodPanelIds.has(p.id)) continue;
            const rings = ringsByPanel[p.id] || [];
            const outer = outerRingByPanel[p.id];
            for (const r of rings) {
                if (!outer || r !== outer) holes.push(r);
            }
        }
        return holes;
    }, [panels, ringsByPanel, outerRingByPanel, hoodPanelIds]);

    return {
        manifest, isLoadingPreset, panels, svgCacheRef, svgCache,
        svgMountKey, hoodPanelIds, hoodRings, hoodHoles, panelSlotMapRef,
        currentPresetIdRef
    };
}