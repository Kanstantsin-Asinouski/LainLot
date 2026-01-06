import { pointInPolygon } from "./polygonOps.js";

export const offsetArcInside = (arcPts, outlinePoly, inset) => {
    const res = [];
    for (let k = 0; k < arcPts.length; k++) {
        const pPrev = arcPts[Math.max(0, k - 1)], pNext = arcPts[Math.min(arcPts.length - 1, k + 1)];
        let tx = pNext.x - pPrev.x, ty = pNext.y - pPrev.y; const L = Math.hypot(tx, ty) || 1; tx /= L; ty /= L;
        let nx = -ty, ny = tx;
        let P = { x: arcPts[k].x + nx * inset, y: arcPts[k].y + ny * inset };
        if (!pointInPolygon(P, outlinePoly)) P = { x: arcPts[k].x - nx * inset, y: arcPts[k].y - ny * inset };
        res.push(P);
    }

    return res;
}