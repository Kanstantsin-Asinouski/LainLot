import { lerpPt, segLen, projectPointToSegment } from "./primitives.js"

/* ========== утилиты «внутри/снаружи» ========== */
export const pointOnSegment = (p, a, b) => {
    const cross = (p.x - a.x) * (b.y - a.y) - (p.y - a.y) * (b.x - a.x);
    if (Math.abs(cross) > 1e-6) return false;
    const dot = (p.x - a.x) * (b.x - a.x) + (p.y - a.y) * (b.y - a.y);
    if (dot < -1e-6) return false;
    const len2 = (b.x - a.x) ** 2 + (b.y - a.y) ** 2;

    return dot <= len2 + 1e-6;
}

export const pointInPolygon = (p, poly) => {
    for (let i = 0, j = poly.length - 1; i < poly.length; j = i++) {
        if (pointOnSegment(p, poly[j], poly[i])) return true;
    }
    let inside = false;
    for (let i = 0, j = poly.length - 1; i < poly.length; j = i++) {
        const xi = poly[i].x, yi = poly[i].y, xj = poly[j].x, yj = poly[j].y;
        const intersect = ((yi > p.y) != (yj > p.y)) && (p.x < (xj - xi) * (p.y - yi) / (yj - yi + 1e-12) + xi);
        if (intersect) inside = !inside;
    }

    return inside;
}

export const nearestOnRing = (P, ringPts) => {
    let best = null, idx = -1, d2 = Infinity;
    for (let i = 0; i < ringPts.length; i++) {
        const A = ringPts[i], B = ringPts[(i + 1) % ringPts.length];
        const pr = projectPointToSegment(P, A, B);
        if (pr.d2 < d2) { d2 = pr.d2; best = { ...pr, A, B }; idx = i; }
    }

    return { ...best, idx };
}

export const arcOnRing = (ringPts, posA, posB) => {
    const N = ringPts.length;
    const P0 = lerpPt(ringPts[posA.idx], ringPts[(posA.idx + 1) % N], posA.t);
    const P1 = lerpPt(ringPts[posB.idx], ringPts[(posB.idx + 1) % N], posB.t);

    const forward = [P0];
    let i = posA.idx;
    if (posA.t < 1 - 1e-6) forward.push(ringPts[(i + 1) % N]);
    i = (i + 1) % N;
    while (i !== posB.idx) { forward.push(ringPts[i]); i = (i + 1) % N; }
    forward.push(P1);

    const backward = [P0];
    i = posA.idx;
    while (i !== (posB.idx + 1 + N) % N) { backward.push(ringPts[i]); i = (i - 1 + N) % N; }
    backward.push(P1);

    const len = arr => arr.slice(0, -1).reduce((s, _, k) => s + segLen(arr[k], arr[k + 1]), 0);

    return (len(forward) <= len(backward)) ? forward : backward;
}
