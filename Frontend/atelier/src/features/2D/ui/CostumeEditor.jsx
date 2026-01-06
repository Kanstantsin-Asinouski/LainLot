import { useEffect, useMemo, useRef, useState, useCallback } from "react";

import { useTranslation } from "react-i18next";

import { sampleBezierPoints } from "../../../core/geometry/geometry.js";
import { waveAlongPolyline } from "../../../core/geometry/polylineOps.js";

import { faceKey } from "../../../core/svg/faceUtils.js";
import { catmullRomToBezierPath } from "../../../core/svg/polylineOps.js";
import { pointInAnyFace } from "../../../core/svg/buildFaces.js";
import { composePanelsForSide } from "../../../core/svg/extractPanels.js";
import { makeUserCurveBetween } from "../../../core/svg/curves.js";

import { downloadText } from "../../../core/export/export.js";
import { buildCombinedSVG } from "../../../core/export/buildCombinedSVG.js";

import { useHistory } from "../../../shared/hooks/useHistory.jsx";
import { useInsertPreviewRAF } from "../../../shared/hooks/useInsertPreviewRAF.jsx";
import { useSceneGeometry } from "../../../shared/hooks/useSceneGeometry.jsx";
import { useEditorPrefs } from "../../../shared/hooks/useEditorPrefs.jsx";
import { useVariantsComposition } from "../../../shared/hooks/useVariantsComposition.jsx";
import useCanvasZoom from "../../../shared/hooks/useCanvasZoom.jsx";

import SidebarEditor from "./SidebarEditor.jsx";
import BodyParams from "./BodyParams.jsx";
import OrderForm from "./OrderForm.jsx";
import Topbar from "./Topbar.jsx";
import CanvasStage from "./CanvasStage.jsx";
import ZoomControls from "./ZoomControls.jsx";

import { PRESETS } from "../../../core/variables/presets.js";
import { reduceSetSlotVariant } from "../../../core/variables/variants.js";

import { panelsFromRaw } from "../../../core/testhelper/testhelper.js";

import styles from "../styles/CostumeEditor.module.css";
import clsx from "clsx";

export default function CostumeEditor() {
    const { t } = useTranslation();
    const scopeRef = useRef(null);
    const [showTopbarHint, setShowTopbarHint] = useState(false);
    const MIN_GAP_WORLD = 20;
    const [lastLineMode, setLastLineMode] = useState('add');
    const [activePanelId, setActivePanelId] = useState(null);
    const [prevPanels, setPrevPanels] = useState(null);
    const [isSwapping, setIsSwapping] = useState(false);
    const SWAP_MS = 180;
    const didEverSwapRef = useRef(false);
    const swapTimerRef = useRef(null);
    const [presetIdx, setPresetIdx] = useState(0);
    const activeId = presetIdx === 0 ? "front" : "back";
    const [curvesByPanel, setCurvesByPanel] = useState({});
    const [fills, setFills] = useState([]);
    const [paintColor, setPaintColor] = useState("#f26522");
    const [mode, setMode] = useState("preview");
    const [defaultSubCount, setDefaultSubCount] = useState(2);
    const [selectedCurveKey, setSelectedCurveKey] = useState(null);
    const [lineStyle, setLineStyle] = useState("straight");
    const [addBuffer, setAddBuffer] = useState(null);
    const [hoverAnchorIdx, setHoverAnchorIdx] = useState(null);
    const [hoverCurveKey, setHoverCurveKey] = useState(null);
    const [clickedCurveKey, setClickedCurveKey] = useState(null);
    const [hoverFace, setHoverFace] = useState(null);
    const [toast, setToast] = useState(null);
    const zoomScopeRef = useRef(null);
    const [waveAmpPx, setWaveAmpPx] = useState(6);
    const [waveLenPx, setWaveLenPx] = useState(36);
    const [paletteOpen, setPaletteOpen] = useState(false);
    const paletteRef = useRef(null);

    const activeDetailId = (presetIdx === 0 ? "front" : "back");
    const [details, setDetails] = useState({
        front: { "hoodie.cuff": "base", "hoodie.belt": "base" },
        back: { "hoodie.cuff": "base", "hoodie.belt": "base" }
    });

    // --- Developer Test Mode ---
    // Надёжно работаем как в `vite dev`, так и при кастомной переменной среды.
    const IS_DEV = Boolean(
        import.meta.env?.DEV ||
        String(import.meta.env?.MODE).toLowerCase() === "development" ||
        String(import.meta.env?.VITE_ENVIRONMENT).toLowerCase() === "development"
    );
    // свернуть/развернуть панель разработчика
    const [devOpen, setDevOpen] = useState(() => {
        try { return localStorage.getItem("ce.devOpen.v1") !== "0"; } catch { return true; }
    });
    useEffect(() => {
        try { localStorage.setItem("ce.devOpen.v1", devOpen ? "1" : "0"); } catch { }
    }, [devOpen]);
    const [testSide, setTestSide] = useState("front");   // 'front' | 'back'
    const [testPanels, setTestPanels] = useState(null);  // null => обычные панели
    const inTest = testPanels !== null;

    const loadTestPair = async (side /* 'front'|'back' */) => {
        const base = `/2d/test`;
        const files = [
            `${base}/hoodie_${side}.svg`,
            `${base}/pants_${side}.svg`
        ];

        const texts = await Promise.all(files.map(async (url) => {
            const res = await fetch(url);
            if (!res.ok) throw new Error(`Failed to load ${url}: ${res.status}`);
            return await res.text();
        }));

        // генерим много панелей из каждого файла
        const panelsParsed = [
            ...panelsFromRaw(`hoodie-${side}`, texts[0]),
            ...panelsFromRaw(`pants-${side}`, texts[1]),
        ];

        // активную ставим первую
        setActivePanelId(panelsParsed[0]?.id ?? null);

        // чистим пользовательские линии/заливки и включаем test-панели
        setCurvesByPanel({});
        setFills([]);
        setTestPanels(panelsParsed);

        setPrevPanels(null);
        setIsSwapping(false);
        if (swapTimerRef.current) { clearTimeout(swapTimerRef.current); swapTimerRef.current = null; }
    };

    const exitTestMode = useCallback(() => {
        // полностью гасим тест
        setTestPanels(null);                   // <- главное: очистить тестовые панели
        setPrevPanels(null);
        setIsSwapping(false);
        if (swapTimerRef.current) {
            clearTimeout(swapTimerRef.current);
            swapTimerRef.current = null;
        }
        setDevOpen(false);
        setMode('preview');                    // вернуться в просмотр
    }, []);

    const [savedByPreset, setSavedByPreset] = useState({});
    const savedByPresetRef = useRef({});

    const dismissTopbarHint = useCallback(() => {
        if (!showTopbarHint) return;
        setShowTopbarHint(false);
        try { localStorage.setItem("ce.topbarHint.v1", "1"); } catch (e) { }
    }, [showTopbarHint]);

    const prevPreset = () => setPresetIdx(i => (i - 1 + PRESETS.length) % PRESETS.length);
    const nextPreset = () => setPresetIdx(i => (i + 1) % PRESETS.length);

    const snapshotFor = useCallback(() => ({
        curvesByPanel,
        fills,
        activePanelId,
    }), [curvesByPanel, fills, activePanelId]);

    const applySnapshot = useCallback((snap, panelsParsed) => {
        if (!snap) return;
        const allowed = new Set((panelsParsed || []).map(p => p.id));
        const curvesIn = snap.curvesByPanel || {};
        const curves = Object.fromEntries(Object.entries(curvesIn).filter(([pid]) => allowed.has(pid)));
        const fills = (snap.fills || []).filter(f => allowed.has(f.panelId));

        const active = allowed.has(snap.activePanelId) ? snap.activePanelId : (panelsParsed[0]?.id || null);
        setCurvesByPanel(curves);
        setFills(fills);
        setActivePanelId(active);
    }, []);

    const {
        historyUndo, historyRedo, canUndo, canRedo,
        applyFillChange, applyCurvesChange,
        historyItems, historyIndex, pushHistory
    } = useHistory({
        fills, curvesByPanel, presetIdx,
        setFills, setCurvesByPanel,
        max: 50
    });

    const {
        manifest, isLoadingPreset, panels, svgCacheRef, svgCache,
        svgMountKey, hoodPanelIds, hoodRings, hoodHoles, panelSlotMapRef,
        currentPresetIdRef
    } = useVariantsComposition({ presetIdx, details, savedByPresetRef, applySnapshot });

    const panelsRef = useRef(panels);

    // в тесте работаем *только* с тестовыми панелями
    const livePanels = inTest ? testPanels : panels;

    const { insertPreview, setInsertPreview, setInsertPreviewRAF } = useInsertPreviewRAF();

    const { svgRef, scale, gridDef, baseFacesByPanel, outerRingByPanel,
        facesByPanel, extraAnchorsByPanel, mergedAnchorsOf, getCursorWorld, closestPointOnCurve
    } = useSceneGeometry({ panels: livePanels, curvesByPanel, defaultSubCount });

    const { applyingPrefsRef, setBothLastModePreview } = useEditorPrefs({
        activeId, mode, setMode, paintColor, setPaintColor,
        lineStyle, setLineStyle, defaultSubCount, setDefaultSubCount, waveAmpPx,
        setWaveAmpPx, waveLenPx, setWaveLenPx, lastLineMode, setLastLineMode,
        presetIdx
    });

    const {
        zoom, zoomedViewBox, zoomIn, zoomOut, reset,
        setZoomExact
    } = useCanvasZoom({ bbox: gridDef.b, svgRef });

    const tooCloseToExistingAnchors = (panel, curve, testPt) => {
        const merged = mergedAnchorsOf(panel);
        const pts = [];
        const a = merged[curve.aIdx] ?? (curve.ax != null ? { x: curve.ax, y: curve.ay } : null);
        const b = merged[curve.bIdx] ?? (curve.bx != null ? { x: curve.bx, y: curve.by } : null);
        if (a) pts.push(a);
        if (b) pts.push(b);
        const extras = extraAnchorsByPanel[panel.id] || [];
        for (const e of extras) {
            if ((e.id || '').startsWith(`${curve.id}:`) || (e.id || '').startsWith(`${curve.id}@m`)) {
                pts.push({ x: e.x, y: e.y });
            }
        }
        return pts.some(q => Math.hypot(q.x - testPt.x, q.y - testPt.y) < MIN_GAP_WORLD);
    };

    const onCurveClick = (panelId, curveId, e) => {
        if (mode === "delete") {
            onCurveClickDelete(panelId, curveId);
            return;
        }
        if (mode === "preview") {
            e?.stopPropagation?.();
            return;
        }

        e?.stopPropagation?.();
        setSelectedCurveKey(`${panelId}:${curveId}`);
        setClickedCurveKey(`${panelId}:${curveId}`);
        setTimeout(() => setClickedCurveKey(k => (k === `${panelId}:${curveId}` ? null : k)), 220);
    };

    const onCanvasClick = useCallback(() => {
        if (mode === "preview" || applyingPrefsRef.current)
            return;
        if (mode !== "delete") {
            setSelectedCurveKey(null);
        }
    }, [mode]);

    const onPanelActivate = (panelId) => {
        if (mode === 'preview')
            return;

        if (mode === 'paint' || mode === 'deleteFill')
            return;

        setActivePanelId(panelId);
        setSelectedCurveKey(null);
        setHoverCurveKey(null);
        setAddBuffer(null);
    };

    const recomputeWaveForCurve = (pid, cid, ampPx, lenPx) => {
        applyCurvesChange(prev => {
            const list = [...(prev[pid] || [])];
            const i = list.findIndex(x => x.id === cid);
            if (i < 0) return prev;
            const c = list[i];

            const ampW = ampPx * (scale.k || 1);
            const lambdaW = lenPx * (scale.k || 1);

            if (c.type === 'wavy' && Array.isArray(c.basePts) && c.basePts.length >= 2) {
                let wpts = waveAlongPolyline(c.basePts, ampW, lambdaW, null);
                wpts = snapEnds(wpts, c.ax, c.ay, c.bx, c.by);
                const d = catmullRomToBezierPath(wpts);
                list[i] = { ...c, pts: wpts, d, waveAmpPx: ampPx, waveLenPx: lenPx };
                return { ...prev, [pid]: list };
            }

            return prev;
        }, t("WaveParameters"));
    };

    const modeGroup =
        (mode === 'paint' || mode === 'deleteFill') ? 'fill' :
            (mode === 'add' || mode === 'delete' || mode === 'insert' || mode === 'deleteVertex') ? 'line' :
                (mode === 'variants' ? 'variants' : 'preview');


    const activePanel = useMemo(
        () => livePanels.find(p => p.id === activePanelId) || livePanels[0] || null,
        [livePanels, activePanelId]
    );

    const manualLeftInActive = useMemo(() => {
        if (!activePanel) return 0;
        const extras = extraAnchorsByPanel[activePanel.id] || [];
        return extras.filter(a => String(a.id).includes('@m')).length;
    }, [extraAnchorsByPanel, activePanel]);

    const snapEnds = (pts, ax, ay, bx, by) => {
        if (!Array.isArray(pts) || pts.length < 2) return pts;
        const out = pts.slice();
        out[0] = { x: ax, y: ay };
        out[out.length - 1] = { x: bx, y: by };
        return out;
    };

    const makeRefForMergedIndex = (panel, mi) => {
        const base = panel.anchors || [];
        const extras = extraAnchorsByPanel[panel.id] || [];
        if (mi < base.length) {
            return { type: 'base', panelId: panel.id, anchorIndex: mi };
        }
        const ex = extras[mi - base.length];
        let curveId = null, subIdx = null;
        if (ex?.id) {
            const [cid, k] = String(ex.id).split(':');
            curveId = cid || null;
            subIdx = k != null ? +k : null;
        }
        return { type: 'extra', panelId: panel.id, curveId, subIdx };
    };

    const onAnchorClickAddMode = (idx) => {
        if (!activePanel) return;

        if (addBuffer == null) {
            setAddBuffer(idx);
            return;
        }

        if (addBuffer === idx) { setAddBuffer(null); return; }

        const merged = mergedAnchorsOf(activePanel);
        const a = merged[addBuffer];
        const b = merged[idx];

        const { c1, c2 } = makeUserCurveBetween(a, b);

        const aRef = makeRefForMergedIndex(activePanel, addBuffer);
        const bRef = makeRefForMergedIndex(activePanel, idx);

        const draft = {
            id: crypto.randomUUID(),
            aIdx: addBuffer,
            bIdx: idx,
            c1,
            c2,
        };

        const faces = baseFacesByPanel[activePanel.id] || [];
        const allInside = sampleBezierPoints(
            a.x, a.y, c1.x, c1.y, c2.x, c2.y, b.x, b.y, 40
        ).every((pt) => pointInAnyFace(pt, faces));

        if (allInside) {
            if (lineStyle === "straight") {
                applyCurvesChange((map) => {
                    const arr = [...(map[activePanel.id] || [])];
                    arr.push({
                        ...draft,
                        type: "cubic",
                        ax: a.x,
                        ay: a.y,
                        bx: b.x,
                        by: b.y,
                        aRef,
                        bRef,
                        subCount: defaultSubCount
                    });
                    return { ...map, [activePanel.id]: arr };
                }, t("AddAStraightLine"));
            }
            else {
                const base = sampleBezierPoints(a.x, a.y, draft.c1.x, draft.c1.y, draft.c2.x, draft.c2.y, b.x, b.y, 64);
                const ampW = waveAmpPx * (scale.k || 1);
                const lambdaW = waveLenPx * (scale.k || 1);
                let wpts = waveAlongPolyline(base, ampW, lambdaW, null);
                wpts = snapEnds(wpts, a.x, a.y, b.x, b.y);
                const d = catmullRomToBezierPath(wpts);

                applyCurvesChange((map) => {
                    const arr = [...(map[activePanel.id] || [])];
                    arr.push({
                        id: draft.id,
                        type: "wavy",
                        aIdx: addBuffer, bIdx: idx,
                        d, pts: wpts,
                        basePts: base,
                        waveAmpPx, waveLenPx,
                        ax: a.x, ay: a.y, bx: b.x, by: b.y, aRef, bRef,
                        subCount: defaultSubCount
                    });
                    return { ...map, [activePanel.id]: arr };
                }, t("AddAWavyLine"));
            }

            setAddBuffer(null);
            return;
        }

        setToast({ text: t("TheLinExtendsBeyondThePart") });
        setAddBuffer(null);
        return;
    };

    const cascadeDeleteCurve = (panelId, rootCurveId) => {
        applyCurvesChange(prev => {
            const arr = [...(prev[panelId] || [])];
            const toDelete = new Set([rootCurveId]);
            let changed = true;
            while (changed) {
                changed = false;
                for (const c of arr) {
                    if (toDelete.has(c.id)) continue;
                    const aHit = c.aRef?.type === 'extra' && c.aRef.curveId && toDelete.has(c.aRef.curveId);
                    const bHit = c.bRef?.type === 'extra' && c.bRef.curveId && toDelete.has(c.bRef.curveId);
                    if (aHit || bHit) { toDelete.add(c.id); changed = true; }
                }
            }
            if (selectedCurveKey) {
                const [pid, cid] = selectedCurveKey.split(':');
                if (pid === panelId && toDelete.has(cid)) setSelectedCurveKey(null);
            }
            const kept = arr.filter(c => !toDelete.has(c.id));
            return { ...prev, [panelId]: kept };
        }, t("DeleteLine"));
    };

    const eraseManualAnchor = (panelId, manual) => {
        const manualId = String(manual?.id ?? '');
        const manualT = Number(manual?.t ?? NaN);
        const curveId = manualId.split('@m')[0];
        if (!curveId) return;

        applyCurvesChange(prev => {
            const list = [...(prev[panelId] || [])];
            const i = list.findIndex(c => c.id === curveId);
            if (i < 0) return prev;

            const cur = list[i];

            let stops = Array.isArray(cur.extraStops) ? cur.extraStops.slice() : [];
            if (stops.length === 0) return prev;
            if (typeof stops[0] === 'number') {
                const idx = Number.isFinite(manualT)
                    ? stops.reduce((best, v, j) =>
                        Math.abs(v - manualT) < Math.abs(stops[best] - manualT) ? j : best, 0)
                    : -1;
                if (idx >= 0) stops.splice(idx, 1);
            } else {
                const ts = stops.map(s => s?.t ?? 0);
                let idx = Number.isFinite(manualT)
                    ? ts.reduce((best, v, j) =>
                        Math.abs(v - manualT) < Math.abs(ts[best] - manualT) ? j : best, 0)
                    : -1;
                if (idx >= 0) stops.splice(idx, 1);
            }
            list[i] = { ...cur, extraStops: stops };
            return { ...prev, [panelId]: list };
        }, t("DeleteVertex"));
    };

    const handleDevClick = useCallback(() => {
        if (inTest) {
            // если тест загружен — выходим из него и переходим в preview
            exitTestMode();
        } else {
            // иначе просто показать/скрыть Dev-панель
            setDevOpen(v => !v);
        }
    }, [inTest, exitTestMode]);

    const onCurveEnter = (panelId, id) => {
        if (mode === 'preview')
            return;
        setHoverCurveKey(`${panelId}:${id}`);
    };
    const onCurveLeave = (panelId, id) => {
        if (mode === 'preview')
            return;
        setHoverCurveKey(k => (k === `${panelId}:${id}` ? null : k));
    };
    const onCurveClickDelete = (panelId, id) => {
        if (mode !== "delete") return;
        cascadeDeleteCurve(panelId, id);
        setHoverCurveKey(null);
    };

    const onFaceEnter = (panelId, poly) => { if (mode === "paint") setHoverFace({ panelId, faceKey: faceKey(poly) }); };
    const onFaceLeave = (panelId, poly) => { if (mode === "paint") setHoverFace(h => (h && h.panelId === panelId && h.faceKey === faceKey(poly) ? null : h)); };
    const onFaceClick = (panelId, poly) => {
        if (mode !== "paint") return;
        const fk = faceKey(poly);
        applyFillChange(fs => {
            const i = fs.findIndex(f => f.panelId === panelId && f.faceKey === fk);
            if (i >= 0) { const cp = fs.slice(); cp[i] = { ...cp[i], color: paintColor }; return cp; }
            return [...fs, { id: crypto.randomUUID(), panelId, faceKey: fk, color: paintColor }];
        }, `${t("Filling")} (${presetIdx === 0 ? t("Front") : t("Back")})`);

    };
    const onFilledEnter = (panelId, fk) => { if (mode === "deleteFill") setHoverFace({ panelId, faceKey: fk }); };
    const onFilledLeave = (panelId, fk) => { if (mode === "deleteFill") setHoverFace(h => (h && h.panelId === panelId && h.faceKey === fk ? null : h)); };
    const onFilledClick = (panelId, fk) => {
        if (mode !== "deleteFill") return;
        applyFillChange(fs => fs.filter(f => !(f.panelId === panelId && f.faceKey === fk)),
            `${t("CleaningTheFill")}  (${presetIdx === 0 ? t("Front") : t("Back")})`);
        setHoverFace(null);
    };

    const [isExporting, setIsExporting] = useState(false);

    const doExportSVG = async () => {
        if (isExporting) return;
        try {
            setIsExporting(true);

            const [frontPanels, backPanels] = await Promise.all([
                (svgCacheRef.current.front?.length ? svgCacheRef.current.front : composePanelsForSide("front", details, manifest)),
                (svgCacheRef.current.back?.length ? svgCacheRef.current.back : composePanelsForSide("back", details, manifest)),
            ]);
            svgCacheRef.current = { ...svgCacheRef.current, front: frontPanels, back: backPanels };

            const svgText = await buildCombinedSVG({
                svgCache: svgCacheRef.current,
                currentPresetId: PRESETS[presetIdx]?.id || "front",
                currentCurves: curvesByPanel,
                currentFills: fills,
                savedByPreset
            });
            downloadText("costume.svg", svgText);
        } finally {
            setIsExporting(false);
        }
    };

    const BP_DEF = {
        hoodie: { chest: "", waist: "", hips: "", height: "", sleeve: "", back: "" },
        pants: { waist: "", hips: "", outseam: "", inseam: "", thigh: "", ankle: "" }
    };

    const OF_DEF = {
        fullName: "", email: "", phone: "", country: "", region: "", city: "",
        district: "", street: "", house: "", apt: "", postal: "", notes: ""
    };

    const [bodyParams, setBodyParams] = useState(() => {
        try { return JSON.parse(localStorage.getItem("ce.bodyParams.v1")) || BP_DEF; } catch { return BP_DEF; }
    });
    const [orderInfo, setOrderInfo] = useState(() => {
        try { return JSON.parse(localStorage.getItem("ce.orderInfo.v1")) || OF_DEF; } catch { return OF_DEF; }
    });

    const isOrderValid = (() => {
        const e = orderInfo?.email?.trim?.() || "";
        const p = orderInfo?.phone?.trim?.() || "";
        const n = orderInfo?.fullName?.trim?.() || "";
        return n.length > 1 && /.+@.+\..+/.test(e) && p.length >= 6;
    })();

    const prevNeckByFaceRef = useRef({ front: null, back: null });

    const setSlotVariant = (face, slot, variantId) => {
        setDetails(prev => {
            const { nextDetails, nextPrevNeck } = reduceSetSlotVariant(prev, {
                face,
                slot,
                variantId,
                prevNeckByFace: prevNeckByFaceRef.current
            });
            prevNeckByFaceRef.current = nextPrevNeck;
            return nextDetails;
        });

        const label = `${t("Variant")}: ${slot.split(".").pop()} → ${variantId || "base"}`;
        pushHistory(label);
    };
    const changeKindRef = useRef(null);

    const detailsRef = useRef(details);
    const lastChangedSlotRef = useRef(null);
    const restoringPresetRef = useRef(false);

    const resetAll = useCallback(() => {
        if (!confirm(t("ClearlyResetEverything")))
            return;

        savedByPresetRef.current = {};
        setSavedByPreset({});
        setCurvesByPanel({});
        setFills([]);
        setActivePanelId(panels[0]?.id ?? null);
        setDetails({ front: {}, back: {} });
        setMode("preview");

        setBothLastModePreview();
    }, [panels, setSavedByPreset, setCurvesByPanel, setFills, setActivePanelId, setDetails, setMode]);

    useEffect(() => {
        zoomScopeRef.current?.setAttribute("tabindex", "0");
        zoomScopeRef.current?.focus();
    }, []);

    useEffect(() => {
        const prev = detailsRef.current;
        const cur = details;
        const changes = [];
        for (const face of ['front', 'back']) {
            const p = prev[face] || {}, c = cur[face] || {};
            for (const slot of Object.keys({ ...p, ...c })) {
                if (p[slot] !== c[slot]) changes.push({ presetId: face, slot });
            }
        }
        if (changes.length) {
            changeKindRef.current = 'slot';
            const preferred = changes.find(ch => ch.presetId === (presetIdx === 0 ? 'front' : 'back')) || changes[0];
            lastChangedSlotRef.current = preferred;
        }
        detailsRef.current = cur;
    }, [details, presetIdx]);

    useEffect(() => {
        try { localStorage.setItem("ce.activeFace", presetIdx === 0 ? "front" : "back"); } catch { }
    }, [presetIdx]);

    useEffect(() => { savedByPresetRef.current = savedByPreset; }, [savedByPreset]);

    useEffect(() => {
        if (restoringPresetRef.current) return;
        if (changeKindRef.current === 'preset') return;

        const id = currentPresetIdRef.current;
        const snap = snapshotFor();
        savedByPresetRef.current = { ...savedByPresetRef.current, [id]: snap };
        setSavedByPreset(prev => ({ ...prev, [id]: snap }));
    }, [fills, curvesByPanel, activePanelId]);

    useEffect(() => {
        const target = PRESETS[presetIdx];
        if (!target)
            return;

        changeKindRef.current = 'preset';
        restoringPresetRef.current = true;
        const oldId = currentPresetIdRef.current;
        const snap = snapshotFor();
        savedByPresetRef.current = { ...savedByPresetRef.current, [oldId]: snap };
        setSavedByPreset(prev => ({ ...prev, [oldId]: snap }));
        currentPresetIdRef.current = target.id;
    }, [presetIdx]);

    useEffect(() => {
        try {
            localStorage.setItem("ce.bodyParams.v1", JSON.stringify(bodyParams));
        }
        catch { }
    }, [bodyParams]);

    useEffect(() => {
        try {
            localStorage.setItem("ce.orderInfo.v1", JSON.stringify(orderInfo));
        }
        catch { }
    }, [orderInfo]);

    useEffect(() => {
        const onKey = (e) => {
            const ctrl = e.ctrlKey || e.metaKey;
            if (!ctrl)
                return;

            const k = e.key.toLowerCase();
            if (k === 'z') {
                e.preventDefault();
                if (e.shiftKey)
                    historyRedo();
                else historyUndo();
            }
            else if (k === 'y') {
                e.preventDefault();
                historyRedo();
            }
        };
        window.addEventListener('keydown', onKey);

        return () => window.removeEventListener('keydown', onKey);
    }, [historyUndo, historyRedo]);

    useEffect(() => {
        const onKey = (e) => {
            if (e.key.toLowerCase() === 'h') {
                e.preventDefault();
                try { localStorage.removeItem('ce.topbarHint.v1'); } catch { }
                setShowTopbarHint(true);
            }
        };
        window.addEventListener('keydown', onKey);
        return () => window.removeEventListener('keydown', onKey);
    }, []);


    useEffect(() => {
        try {
            const seen = localStorage.getItem("ce.topbarHint.v1");
            setShowTopbarHint(seen !== "1");
        } catch (e) { /* noop */ }
    }, []);

    useEffect(() => { svgCacheRef.current = svgCache; }, [svgCache]);

    useEffect(() => {
        if (mode !== 'deleteVertex') return;
        if (manualLeftInActive === 0) {
            setMode('insert');
            setToast({ text: t("AllManualVerticesHaveBeenRemoved") });
        }
    }, [mode, manualLeftInActive]);

    useEffect(() => {
        if (mode === 'preview') {
            setSelectedCurveKey(null);
            setHoverCurveKey(null);
            setAddBuffer(null);
        }
    }, [mode]);

    useEffect(() => { panelsRef.current = livePanels; }, [livePanels]);

    useEffect(() => {
        if (!paletteOpen) return;
        const onKey = (e) => e.key === "Escape" && setPaletteOpen(false);
        const onClick = (e) => {
            if (paletteRef.current && !paletteRef.current.contains(e.target)) setPaletteOpen(false);
        };
        window.addEventListener("keydown", onKey);
        window.addEventListener("pointerdown", onClick);
        return () => { window.removeEventListener("keydown", onKey); window.removeEventListener("pointerdown", onClick); };
    }, [paletteOpen]);

    useEffect(() => {
        if (mode === 'add' || mode === 'delete' || mode === 'insert') setLastLineMode(mode);
    }, [mode]);

    useEffect(() => {
        if (mode !== 'insert') setInsertPreview(null);
    }, [mode]);

    useEffect(() => {
        const el = scopeRef.current;
        if (!el) return;

        const onKey = (e) => {
            const tag = (e.target?.tagName || "").toLowerCase();
            if (["input", "textarea", "select", "button"].includes(tag) || e.target?.isContentEditable) return;
            if (e.ctrlKey || e.metaKey || e.altKey) return;

            const k = e.key.toLowerCase?.();
            if (k === "arrowleft") prevPreset();
            if (k === "arrowright") nextPreset();

            if (k === "Q" || k === "q") { setPresetIdx(0); e.preventDefault(); }
            if (k === "E" || k === "e") { setPresetIdx(1); e.preventDefault(); }

            if (k === '[') {
                if (livePanels.length) {
                    const i = Math.max(0, livePanels.findIndex(p => p.id === activePanel?.id));
                    const prev = livePanels[(i - 1 + livePanels.length) % livePanels.length];
                    setActivePanelId(prev?.id ?? livePanels[0]?.id ?? null);
                }
            }
            if (k === ']') {
                if (livePanels.length) {
                    const i = Math.max(0, livePanels.findIndex(p => p.id === activePanel?.id));
                    const next = livePanels[(i + 1) % livePanels.length];
                    setActivePanelId(next?.id ?? livePanels[0]?.id ?? null);
                }
            }
        };

        el.addEventListener("keydown", onKey);
        return () => el.removeEventListener("keydown", onKey);
    }, [panels, activePanel]);

    useEffect(() => {
        if (!livePanels || livePanels.length === 0) return;

        if (inTest) {
            const map = new Map();
            for (const p of livePanels) map.set(p.id, null);
            panelSlotMapRef.current = map;
            panelsRef.current = livePanels;
            return;
        }

        const kind = changeKindRef.current;
        if (kind === 'preset') restoringPresetRef.current = true;

        const old = panelsRef.current;
        if (old && old.length) {
            didEverSwapRef.current = true;
            setPrevPanels(old);
            setIsSwapping(true);
            if (swapTimerRef.current) clearTimeout(swapTimerRef.current);
            swapTimerRef.current = setTimeout(() => {
                setPrevPanels(null);
                setIsSwapping(false);
                swapTimerRef.current = null;
            }, SWAP_MS);
        }

        const map = new Map();
        for (const p of livePanels) map.set(p.id, p.meta?.slot || null);
        panelSlotMapRef.current = map;

        const presetId = currentPresetIdRef.current;
        const changed = lastChangedSlotRef.current;

        if (kind === 'preset') {
            const snap = savedByPresetRef.current[presetId];
            applySnapshot(snap, livePanels);
        } else if (changed && !inTest) {// не трогаем снапы в тест-режиме
            const { presetId: chPreset, slot: chSlotFull } = changed;
            if (chPreset === presetId && chSlotFull) {
                const panelSlotMap = panelSlotMapRef.current || new Map();
                const chSlot = String(chSlotFull).split(".").pop();
                setFills(fs => fs.filter(f => panelSlotMap.get(f.panelId) !== chSlot));
                setCurvesByPanel(prev => {
                    const next = { ...prev };
                    for (const pid of Object.keys(next)) {
                        if (panelSlotMap.get(pid) === chSlot) delete next[pid];
                    }
                    return next;
                });
            }
        }

        changeKindRef.current = null;
        lastChangedSlotRef.current = null;
        if (toast) setToast(null);
        if (restoringPresetRef.current) setTimeout(() => { restoringPresetRef.current = false; }, 0);

        return () => {
            if (swapTimerRef.current) {
                clearTimeout(swapTimerRef.current);
                swapTimerRef.current = null;
            }
        };
    }, [livePanels]);

    useEffect(() => {
        if (restoringPresetRef.current) return;

        const visibleIds = new Set(Object.keys(facesByPanel).map(String));

        setFills(fs =>
            fs.filter(f => {
                const pid = String(f.panelId);

                if (!visibleIds.has(pid)) return true;

                const polys = facesByPanel[pid] || [];
                return polys.some(poly => faceKey(poly) === f.faceKey);
            })
        );
    }, [facesByPanel]);

    useEffect(() => {
        const el = scopeRef.current;
        if (!el) return;

        const onKey = (e) => {
            if (document.activeElement !== el) return;

            if (e.key === 'Escape') {
                e.preventDefault();
                exitTestMode();
                setMode('preview');
                setDevOpen(false);
                return;
            }
            else if (e.key === 'T' || e.key === 't') {
                e.preventDefault();
                if (devOpen && inTest) {
                    exitTestMode();
                } else {
                    setDevOpen(v => !v);
                }
                return;
            }
            else if (e.key === 'a' || e.key === 'A') {
                setMode('add');
                setAddBuffer(null);
                e.preventDefault();
            }
            else if (e.key === 'd' || e.key === 'D') {
                setMode('delete');
                e.preventDefault();
            }
            else if (e.key === 'f' || e.key === 'F') {
                setMode('paint');
                e.preventDefault();
            }
            else if (e.key === 'x' || e.key === 'X') {
                setMode('deleteFill');
                e.preventDefault();
            }
            else if (e.key === 'v' || e.key === 'V') {
                setMode('variants');
                e.preventDefault();
            }
        };

        el.addEventListener('keydown', onKey);
        return () => el.removeEventListener('keydown', onKey);
    }, []);

    return (
        <div>
            <div
                ref={scopeRef}
                className={clsx(styles.layout, modeGroup === 'preview' && styles.layoutPreview)}
                tabIndex={0}
                role="region"
                aria-label={t("CostumeEditor")}
            >
                <div className={styles.canvasWrap} onMouseDown={() => scopeRef.current?.focus()}>
                    {toast && (
                        <div className={styles.toast} role="status" aria-live="polite">
                            {toast.text}
                        </div>
                    )}

                    {isLoadingPreset && <div className={styles.loader}>{t("Loading")}</div>}

                    <Topbar
                        mode={mode}
                        setMode={setMode}
                        lastLineMode={lastLineMode}
                        setShowTopbarHint={setShowTopbarHint}
                        showTopbarHint={showTopbarHint}
                        dismissTopbarHint={dismissTopbarHint}
                        presetIdx={presetIdx}
                        setPresetIdx={setPresetIdx}
                        resetAll={resetAll}
                        doExportSVG={doExportSVG}
                        isExporting={isExporting}
                        IS_DEV={IS_DEV}
                        devOpen={devOpen}
                        setDevOpen={setDevOpen}
                        handleDevClick={handleDevClick}
                    />

                    {IS_DEV && devOpen && (
                        <div style={{ display: "flex", gap: 8, alignItems: "center", flexWrap: "wrap", padding: "4px 0" }}>
                            <select
                                value={testSide}
                                onChange={(e) => setTestSide(e.target.value)}
                                style={{ padding: "6px 8px", borderRadius: 8 }}
                            >
                                <option value="front">Front (hoodie_front + pants_front)</option>
                                <option value="back">Back (hoodie_back + pants_back)</option>
                            </select>

                            <button type="button" className={styles.navBtn} onClick={() => loadTestPair(testSide)}>
                                Load
                            </button>

                            {inTest && (
                                <button type="button" className={styles.navBtn} onClick={() => {
                                    setTestPanels(null);
                                    setPrevPanels(null);
                                    setIsSwapping(false);
                                    if (swapTimerRef.current) { clearTimeout(swapTimerRef.current); swapTimerRef.current = null; }
                                }}>
                                    Exit
                                </button>
                            )}
                        </div>
                    )}

                    <CanvasStage
                        mode={mode}
                        scale={scale}
                        facesByPanel={facesByPanel}
                        outerRingByPanel={outerRingByPanel}
                        activePanel={activePanel}
                        onPanelActivate={onPanelActivate}
                        fills={fills}
                        onFilledEnter={onFilledEnter}
                        onFaceEnter={onFaceEnter}
                        onFilledLeave={onFilledLeave}
                        onFaceLeave={onFaceLeave}
                        onFilledClick={onFilledClick}
                        onFaceClick={onFaceClick}
                        onCurveLeave={onCurveLeave}
                        mergedAnchorsOf={mergedAnchorsOf}
                        curvesByPanel={curvesByPanel}
                        setInsertPreview={setInsertPreview}
                        getCursorWorld={getCursorWorld}
                        closestPointOnCurve={closestPointOnCurve}
                        tooCloseToExistingAnchors={tooCloseToExistingAnchors}
                        setInsertPreviewRAF={setInsertPreviewRAF}
                        applyCurvesChange={applyCurvesChange}
                        insertPreview={insertPreview}
                        extraAnchorsByPanel={extraAnchorsByPanel}
                        setHoverAnchorIdx={setHoverAnchorIdx}
                        eraseManualAnchor={eraseManualAnchor}
                        hoverFace={hoverFace}
                        hoverAnchorIdx={hoverAnchorIdx}
                        addBuffer={addBuffer}
                        onAnchorClickAddMode={onAnchorClickAddMode}
                        hoverCurveKey={hoverCurveKey}
                        selectedCurveKey={selectedCurveKey}
                        clickedCurveKey={clickedCurveKey}
                        onCurveEnter={onCurveEnter}
                        setToast={setToast}
                        onCurveClickDelete={onCurveClickDelete}
                        onCurveClick={onCurveClick}
                        svgRef={svgRef}
                        viewBox={zoomedViewBox}
                        gridDef={gridDef}
                        svgMountKey={svgMountKey}
                        panels={livePanels}
                        prevPanels={prevPanels}
                        isSwapping={isSwapping}
                        hoodPanelIds={hoodPanelIds}
                        hoodRings={hoodRings}
                        hoodHoles={hoodHoles}
                        onCanvasClick={onCanvasClick}
                        didEverSwapRef={didEverSwapRef}
                        presetIdx={presetIdx}
                        PRESETS={PRESETS}
                    />

                    <div className={styles.bottomUI}>
                        <div className={styles.presetNav}>
                            <button className={styles.navBtn} onClick={prevPreset} aria-label={t("PreviousWorkpiece")}>⟵</button>
                            <div className={styles.presetChip}>{t(PRESETS[presetIdx]?.title) || "—"}</div>
                            <button className={styles.navBtn} onClick={nextPreset} aria-label={t("NextWorkpiece")}>⟶</button>
                        </div>
                        <ZoomControls
                            onIn={zoomIn}
                            onOut={zoomOut}
                            onReset={reset}
                            zoom={zoom}
                            onSet={setZoomExact}
                        />
                    </div>

                </div>

                {
                    modeGroup !== 'preview' && (
                        <SidebarEditor
                            mode={mode}
                            setMode={setMode}
                            modeGroup={modeGroup}
                            paintColor={paintColor}
                            setPaintColor={setPaintColor}
                            paletteOpen={paletteOpen}
                            setPaletteOpen={setPaletteOpen}
                            paletteRef={paletteRef}
                            lineStyle={lineStyle}
                            setLineStyle={setLineStyle}
                            defaultSubCount={defaultSubCount}
                            setDefaultSubCount={setDefaultSubCount}
                            selectedCurveKey={selectedCurveKey}
                            setSelectedCurveKey={setSelectedCurveKey}
                            hoverCurveKey={hoverCurveKey}
                            setHoverCurveKey={setHoverCurveKey}
                            curvesByPanel={curvesByPanel}
                            setCurvesByPanelExtern={applyCurvesChange}
                            recomputeWaveForCurve={recomputeWaveForCurve}
                            waveAmpPx={waveAmpPx}
                            setWaveAmpPx={setWaveAmpPx}
                            waveLenPx={waveLenPx}
                            setWaveLenPx={setWaveLenPx}
                            historyItems={historyItems}
                            historyIndex={historyIndex}
                            historyUndo={historyUndo}
                            historyRedo={historyRedo}
                            canUndo={canUndo}
                            canRedo={canRedo}
                            details={details}
                            activeDetailId={activeDetailId}
                            setSlotVariant={setSlotVariant}
                        />
                    )
                }
            </div >

            < section className={styles.flow} aria-label={t("PlacingAnOrder")} >
                <div className={styles.flowContainer}>
                    <header className={styles.flowHeader}>
                        <h2 className={styles.flowTitle}>{t("PlacingAnOrder")}</h2>
                        <p className={styles.flowSub}>{t("StepByStep")}</p>
                    </header>

                    <div className={styles.stepCard} id="step-body">
                        <div className={styles.stepTitle}><span className={styles.stepBadge}>1</span> {t("BodyParameters")}</div>
                        <BodyParams value={bodyParams} onChange={setBodyParams} />
                    </div>

                    <div className={styles.stepCard} id="step-order">
                        <div className={styles.stepTitle}><span className={styles.stepBadge}>2</span> {t("DeliveryDetailsAndAddress")}</div>
                        <OrderForm value={orderInfo} onChange={setOrderInfo} />
                    </div>

                    <div className={styles.ctaBar}>
                        <button
                            type="button"
                            className={styles.ctaButton}
                            disabled={!isOrderValid}
                            onClick={() => {
                                if (!isOrderValid) { alert(t("FillData")); return; }
                                console.log("Finalize payload", { bodyParams, orderInfo, fills, curvesByPanel });
                            }}
                        >
                            {t("GoToFinalization")}
                        </button>
                        {!isOrderValid && <div className={styles.ctaNote}>{t("ToActivateTheButton")}</div>}
                    </div>
                </div>
            </section >

        </div >
    );
}