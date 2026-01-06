import { useLayoutEffect, useMemo, useRef, useState, useCallback } from "react";
import { getBounds } from "../../core/geometry/bounds.js";
import { cumulativeLengths, nearestOnPolyline } from "../../core/geometry/polylineOps.js";
import { sampleBezierPoints } from "../../core/geometry/geometry.js";
import {
    computeBaseFaces, computeRingsByPanel, pickOuterRing, computeFacesWithUserLines
} from "../../core/svg/buildFaces.js";

export function useSceneGeometry({ panels, curvesByPanel, defaultSubCount }) {
    const svgRef = useRef(null);

    const baseFacesCacheRef = useRef(new Map()); // panelId -> { sig, faces }

    const worldBBox = useMemo(() => {
        const isGood = (v) => Number.isFinite(v);
        let bb = null;
        for (const p of Array.isArray(panels) ? panels : []) {
            if (!p || !Array.isArray(p.segs) || p.segs.length === 0) continue;
            const b = getBounds(p.segs);
            if (!isGood(b?.x) || !isGood(b?.y) || !isGood(b?.w) || !isGood(b?.h) || b.w <= 0 || b.h <= 0) {
                continue; // пропускаем «битые» панели
            }
            if (!bb) bb = { x: b.x, y: b.y, w: b.w, h: b.h };
            else {
                const x1 = Math.min(bb.x, b.x);
                const y1 = Math.min(bb.y, b.y);
                const x2 = Math.max(bb.x + bb.w, b.x + b.w);
                const y2 = Math.max(bb.y + bb.h, b.y + b.h);
                bb = { x: x1, y: y1, w: x2 - x1, h: y2 - y1 };
            }
        }
        // безопасный дефолт
        if (!bb) bb = { x: 0, y: 0, w: 800, h: 500 };
        return bb;
    }, [panels]);

    const [scale, setScale] = useState({ k: 1 });
    useLayoutEffect(() => {
        const update = () => {
            const svg = svgRef.current; if (!svg || !panels.length) return;
            const vb = svg.viewBox.baseVal;
            const kx = vb.width / svg.clientWidth;
            const ky = vb.height / svg.clientHeight;
            setScale({ k: Math.max(kx, ky) });
        };
        update();
        const ro = new ResizeObserver(update);
        if (svgRef.current) ro.observe(svgRef.current);
        window.addEventListener("resize", update);
        return () => { ro.disconnect(); window.removeEventListener("resize", update); };
    }, [panels.length]);

    const gridDef = useMemo(() => {
        const W = Number.isFinite(worldBBox.w) ? worldBBox.w : 800;
        const H = Number.isFinite(worldBBox.h) ? worldBBox.h : 500;
        const X = Number.isFinite(worldBBox.x) ? worldBBox.x : 0;
        const Y = Number.isFinite(worldBBox.y) ? worldBBox.y : 0;
        const step = Math.max(1, Math.min(W, H) / 20);
        return { step, b: { x: X, y: Y, w: W, h: H } };
    }, [worldBBox]);

    const baseFacesByPanel = useMemo(() => {
        return computeBaseFaces(panels, baseFacesCacheRef.current);
    }, [panels]);

    const ringsByPanel = useMemo(() => {
        return computeRingsByPanel(panels);
    }, [panels]);

    const outerRingByPanel = useMemo(() => {
        return pickOuterRing(panels, ringsByPanel);
    }, [panels, ringsByPanel]);

    const pointAtS = (pts, Larr, s) => {
        const total = Larr[Larr.length - 1] || 1;
        const t = Math.max(0, Math.min(total, s));
        let i = 0;
        while (i + 1 < Larr.length && Larr[i + 1] < t) i++;
        const l0 = Larr[i], l1 = Larr[Math.min(Larr.length - 1, i + 1)];
        const p0 = pts[i], p1 = pts[Math.min(pts.length - 1, i + 1)];
        const seg = Math.max(1e-12, l1 - l0);
        const a = (t - l0) / seg;
        return { x: p0.x + (p1.x - p0.x) * a, y: p0.y + (p1.y - p0.y) * a };
    };

    const extraAnchorsByPanel = useMemo(() => {
        const map = {};
        for (const p of panels) {
            const arr = [];
            const curves = (curvesByPanel[p.id] || []);
            for (const c of curves) {
                // получаем опорные точки полилинии кривой
                let poly = null;
                if (c.type === "cubic") {
                    const a = p.anchors?.[c.aIdx] ?? (c.ax != null ? { x: c.ax, y: c.ay } : null);
                    const b = p.anchors?.[c.bIdx] ?? (c.bx != null ? { x: c.bx, y: c.by } : null);
                    if (!a || !b) continue;
                    poly = sampleBezierPoints(a.x, a.y, c.c1.x, c.c1.y, c.c2.x, c.c2.y, b.x, b.y, 128);
                } else {
                    // для wavy берём дискретизацию самой линии
                    if (Array.isArray(c.pts) && c.pts.length >= 2) {
                        poly = c.pts;
                    } else {
                        continue;
                    }
                }

                if (!poly || poly.length < 2) continue;

                const L = cumulativeLengths(poly);
                const total = L[L.length - 1] || 1;
                const n = Math.max(2, Math.min(10, c?.subCount ?? defaultSubCount ?? 2));
                for (let k = 1; k <= n; k++) {
                    const s = (total * k) / (n + 1);
                    const pt = pointAtS(poly, L, s);
                    arr.push({ id: `${c.id}:${k}`, x: pt.x, y: pt.y });
                }

                // ручные точки (новое): extraStops — доли 0..1
                if (Array.isArray(c.extraStops)) {
                    c.extraStops.forEach((stop, idx) => {
                        const t = typeof stop === 'number' ? stop : (stop?.t ?? 0);
                        const s = Math.max(0, Math.min(1, t)) * total;
                        const pt = pointAtS(poly, L, s);
                        // id можно оставить индексный — удалять будем по t:
                        arr.push({ id: `${c.id}@m${idx}`, x: pt.x, y: pt.y, t });
                    });
                }
            }
            map[p.id] = arr;
        }
        return map;
    }, [panels, curvesByPanel, defaultSubCount]);

    // утилита для объединённого списка вершин панели
    const mergedAnchorsOf = useCallback((p) => {
        const extras = extraAnchorsByPanel[p.id] || [];
        // порядок важен: базовые, затем дополнительные — индексы merged совпадут с кликами
        return [...(p.anchors || []), ...extras];
    }, [extraAnchorsByPanel]);

    // faces с учётом пользовательских линий
    const facesByPanel = useMemo(() => {
        return computeFacesWithUserLines(panels, curvesByPanel, mergedAnchorsOf);
    }, [panels, curvesByPanel, mergedAnchorsOf]);

    const getCursorWorld = useCallback((e) => {
        const svg = svgRef.current; if (!svg) return null;
        const pt = svg.createSVGPoint(); pt.x = e.clientX; pt.y = e.clientY;
        const inv = svg.getScreenCTM()?.inverse?.(); if (!inv) return null;
        const p = pt.matrixTransform(inv); return { x: p.x, y: p.y };
    }, []);

    const closestPointOnCurve = useCallback((panel, curve, P) => {
        let poly = null;
        if (curve.type === 'cubic') {
            const a = panel.anchors?.[curve.aIdx] ?? (curve.ax != null ? { x: curve.ax, y: curve.ay } : null);
            const b = panel.anchors?.[curve.bIdx] ?? (curve.bx != null ? { x: curve.bx, y: curve.by } : null);
            if (!a || !b) return null;
            poly = sampleBezierPoints(a.x, a.y, curve.c1.x, curve.c1.y, curve.c2.x, curve.c2.y, b.x, b.y, 128);
        } else if (Array.isArray(curve.pts)) {
            poly = curve.pts;
        }
        if (!poly || poly.length < 2 || !P) return null;
        const near = nearestOnPolyline(poly, P);
        const L = cumulativeLengths(poly);
        const total = L[L.length - 1] || 1;
        const t = total > 0 ? near.s / total : 0;
        return { x: near.x, y: near.y, t, total, poly, L };
    }, []);

    return {
        svgRef, scale, gridDef, baseFacesByPanel, outerRingByPanel,
        facesByPanel, extraAnchorsByPanel, mergedAnchorsOf, getCursorWorld, closestPointOnCurve
    };
}