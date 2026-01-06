import { offsetArcInside } from "../geometry/offset.js";
import { waveAlongPolyline } from "../geometry/polylineOps.js";
import { lerpPt } from "../geometry/primitives.js";
import { arcOnRing, nearestOnRing } from "../geometry/polygonOps.js";
import { splitSegsIntoSubpaths, polylineFromSubpath, catmullRomToBezierPath } from "../svg/polylineOps.js";

export const makeUserCurveBetween = (a, b) => {
    const k = 1 / 3;
    return {
        c1: {
            x: a.x + (b.x - a.x) * k, y: a.y + (b.y - a.y) * k
        },
        c2: {
            x: b.x - (b.x - a.x) * k, y: b.y - (b.y - a.y) * k
        }
    };
}

export const routeCurveAlongOutline = (panel, draftCurve, insetWorld, opts = {}) => {

    const rings = splitSegsIntoSubpaths(panel.segs)
        .map(polylineFromSubpath)
        .filter(r => r.length >= 3);

    if (!rings.length)
        return null;

    const a = panel?.anchors?.[draftCurve.aIdx];
    const b = panel?.anchors?.[draftCurve.bIdx];
    if (!a || !b) return null;

    let best = null;
    for (const ring of rings) {
        const pa = nearestOnRing(a, ring);
        const pb = nearestOnRing(b, ring);
        if (pa && pb) {
            const score = (pa.d2 || 0) + (pb.d2 || 0);
            if (!best || score < best.score)
                best = { ring, pa, pb, score };
        }
    }
    if (!best)
        return null;

    const arc = arcOnRing(best.ring, best.pa, best.pb);
    const offsetArc = offsetArcInside(arc, best.ring, insetWorld);

    if (offsetArc.length < 2)
        return null;

    let workingPts = offsetArc;
    if (opts.style === "wavy") {
        const amp = Math.max(0, opts.ampWorld || 0);
        const lambda = Math.max(1e-6, opts.lambdaWorld || 1);
        workingPts = waveAlongPolyline(offsetArc, amp, lambda, best.ring);
    }

    const d = catmullRomToBezierPath(workingPts);

    // точки на контуре (без отступа)
    const N = best.ring.length;
    const P0 = lerpPt(best.ring[best.pa.idx], best.ring[(best.pa.idx + 1) % N], best.pa.t);
    const P1 = lerpPt(best.ring[best.pb.idx], best.ring[(best.pb.idx + 1) % N], best.pb.t);
    // точки на прижатой дуге (с отступом)
    const Q0 = workingPts[0];
    const Q1 = workingPts[workingPts.length - 1];

    return {
        d,
        pts: workingPts,
        connA: [Q0, P0],   // невидимый коннектор к контуру
        connB: [Q1, P1]
    };
}