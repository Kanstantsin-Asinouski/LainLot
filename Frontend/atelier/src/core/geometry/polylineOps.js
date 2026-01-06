import { projectPointToSegment } from "./primitives.js";
import { pointInPolygon } from "./polygonOps.js";

export const sampleLine = (ax, ay, x, y) => {
    return [{ x: ax, y: ay }, { x, y }];
}

export const cumulativeLengths = (pts) => {
    const L = [0];
    for (let i = 1; i < pts.length; i++) {
        L.push(L[i - 1] + Math.hypot(pts[i].x - pts[i - 1].x, pts[i].y - pts[i - 1].y));
    }

    return L;
}

export const normalsOnPolyline = (pts) => {
    const nrm = [];
    for (let i = 0; i < pts.length; i++) {
        const a = pts[Math.max(0, i - 1)], b = pts[Math.min(pts.length - 1, i + 1)];
        let tx = b.x - a.x, ty = b.y - a.y;
        const len = Math.hypot(tx, ty) || 1; tx /= len; ty /= len;
        nrm.push({ x: -ty, y: tx });
    }

    return nrm;
}

export const nearestOnPolyline = (pts, P) => {
    if (!pts || pts.length < 2) return { idx: -1, x: P.x, y: P.y, s: 0, d2: Infinity };
    const L = cumulativeLengths(pts);
    let best = { idx: -1, x: P.x, y: P.y, s: 0, d2: Infinity };
    let acc = 0;
    for (let i = 0; i + 1 < pts.length; i++) {
        const pr = projectPointToSegment(P, pts[i], pts[i + 1]); // уже есть
        const segLen = Math.hypot(pts[i + 1].x - pts[i].x, pts[i + 1].y - pts[i].y);
        const sHere = acc + pr.t * segLen;
        if (pr.d2 < best.d2) best = { idx: i, x: pr.x, y: pr.y, s: sHere, d2: pr.d2 };
        acc += segLen;
    }
    return best;
};

export const pointsToPairedPolyline = (pts) => {
    const line = [];
    for (let i = 0; i + 1 < pts.length; i++) {
        line.push(pts[i], pts[i + 1]);
    }

    return line;
}

/** волна вдоль ломаной pts. amp/lambda — в мировых единицах. 
 *  Если outlinePoly задан, точка при необходимости отражается внутрь детали. */
export const waveAlongPolyline = (pts, amp, lambda, outlinePoly = null, phase = 0) => {
    if (!pts || pts.length < 2) return pts || [];
    const L = cumulativeLengths(pts), total = L[L.length - 1] || 1;
    const nrms = normalsOnPolyline(pts);
    const out = [];
    for (let i = 0; i < pts.length; i++) {
        const s = L[i];
        const a = amp * Math.sin((2 * Math.PI * s) / lambda + phase);
        let p = { x: pts[i].x + nrms[i].x * a, y: pts[i].y + nrms[i].y * a };
        if (outlinePoly && !pointInPolygon(p, outlinePoly)) {
            // если «выстрелили» наружу — перегибаем на другую сторону
            p = { x: pts[i].x - nrms[i].x * a, y: pts[i].y - nrms[i].y * a };
        }
        out.push(p);
    }
    // гарантируем совпадение концов с исходными
    out[0] = { ...pts[0] };
    out[out.length - 1] = { ...pts[pts.length - 1] };

    return out;
}