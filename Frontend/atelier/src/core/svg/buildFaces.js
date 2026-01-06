import { pointInPolygon } from "../geometry/polygonOps.js";
import { area } from "../geometry/bounds.js";
import { segsSignature, sampleBezier } from "../geometry/geometry.js";
import { polylinesFromSegs, segmentsFromPolylines, splitSegsIntoSubpaths, polylineFromSubpath } from "../svg/polylineOps.js";
import { pointsToPairedPolyline } from "../geometry/polylineOps.js";
import { splitByIntersections } from "../geometry/intersections.js";

export const buildFacesFromSegments = (segments) => {
    const nodes = new Map();
    const PREC = 1e-4; // при необходимости можно 1e-5
    const norm = (v) => Math.round(v / PREC) * PREC;
    const key = (p) => `${norm(p.x)}_${norm(p.y)}`;
    const node = (p) => {
        const k = key(p);
        if (!nodes.has(k)) nodes.set(k, { ...p, out: [] });
        return nodes.get(k);
    };
    const half = [];
    const add = (A, B) => {
        const h = {
            from: A, to: B, ang: Math.atan2(B.y - A.y, B.x - A.x), twin: null, next: null, visited: false
        };
        half.push(h); A.out.push(h);
        return h;
    };
    for (const s of segments) {
        const A = node(s.a), B = node(s.b);
        const h1 = add(A, B), h2 = add(B, A); h1.twin = h2; h2.twin = h1;
    }
    for (const n of nodes.values())
        n.out.sort((a, b) => a.ang - b.ang); // не критично, но удобно иметь CCW

    const TAU = Math.PI * 2;
    const dpos = (a, b) => {            // положительная разница углов a - b  в (0, 2π]
        let d = a - b;
        while (d <= 0) d += TAU;
        while (d > TAU) d -= TAU;
        return d;
    };

    for (const n of nodes.values()) {
        for (const h of n.out) {
            const arr = h.to.out;
            let best = null, bestDelta = Infinity;

            for (const e of arr) {
                if (e === h.twin)
                    continue;

                const delta = dpos(e.ang, h.twin.ang); // «сразу после twin» против часовой (левый поворот)
                if (delta < bestDelta - 1e-9) {
                    bestDelta = delta; best = e;
                }
            }
            h.next = best || h.twin; // на всякий случай fallback
        }
    }

    const faces = [];
    for (const h of half) {
        if (h.visited)
            continue;

        const poly = []; let cur = h, guard = 0;

        while (!cur.visited && guard++ < 20000) {
            cur.visited = true; poly.push({
                x: cur.from.x, y: cur.from.y
            }); cur = cur.next;
            if (cur === h)
                break;
        }
        if (poly.length >= 3)
            faces.push(poly);
    }
    if (faces.length) {
        const idxMax = faces.map((p, i) => ({
            i, A: Math.abs(area(p))
        })).sort((a, b) => b.A - a.A)[0].i;
        faces.splice(idxMax, 1);
    }
    const cleaned = faces.filter(poly => Math.abs(area(poly)) > 1e-4);

    return cleaned;
}

export const pointInAnyFace = (p, faces) => {
    for (const poly of faces) {
        if (pointInPolygon(p, poly))
            return true;
    }
    return false;
}

export const computeBaseFaces = (panels, cache) => {
    const res = {};
    for (const p of panels) {
        const sig = segsSignature(p.segs);
        const cached = cache.get(p.id);
        if (cached && cached.sig === sig) {
            res[p.id] = cached.faces;
            continue;
        }
        const baseLines = polylinesFromSegs(p.segs);
        const segsFlat = segmentsFromPolylines(baseLines);
        const faces = buildFacesFromSegments(splitByIntersections(segsFlat));
        cache.set(p.id, { sig, faces });
        res[p.id] = faces;
    }
    return res;
}

export const computeRingsByPanel = (panels) => {
    const res = {};
    for (const p of panels) {
        const subs = splitSegsIntoSubpaths(p.segs);
        res[p.id] = subs.map(polylineFromSubpath).filter(r => r.length >= 3);
    }
    return res;
}

export const pickOuterRing = (panels, ringsByPanel) => {
    const res = {};
    for (const p of panels) {
        const rings = ringsByPanel[p.id] || [];
        if (!rings.length) { res[p.id] = null; continue; }
        let best = null, bestA = -Infinity;
        for (const r of rings) {
            const A = Math.abs(area(r));
            if (A > bestA) { bestA = A; best = r; }
        }
        res[p.id] = best;
    }
    return res;
}

export const computeFacesWithUserLines = (panels, curvesByPanel, mergedAnchorsOf) => {
    const res = {};
    for (const p of panels) {
        const baseLines = polylinesFromSegs(p.segs);
        const merged = mergedAnchorsOf(p);

        const userLines = (curvesByPanel[p.id] || []).flatMap(c => {
            if (c.type === "cubic") {
                const a = merged[c.aIdx] ?? (c.ax != null ? { x: c.ax, y: c.ay } : null);
                const b = merged[c.bIdx] ?? (c.bx != null ? { x: c.bx, y: c.by } : null);
                if (!a || !b) return []; // пропускаем некорректную кривую
                return [sampleBezier(a.x, a.y, c.c1.x, c.c1.y, c.c2.x, c.c2.y, b.x, b.y)];
            }
            else {
                // wavy: используем уже дискретизированные точки линии
                if (Array.isArray(c.pts) && c.pts.length >= 2) {
                    return [pointsToPairedPolyline(c.pts)];
                }
                return [];
            }
        });

        const segsFlat = segmentsFromPolylines([...baseLines, ...userLines]);
        res[p.id] = buildFacesFromSegments(splitByIntersections(segsFlat));
    }
    return res;
}