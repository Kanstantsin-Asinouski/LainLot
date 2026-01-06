/* ---- доп. helpers для дуги по контуру ---- */
export const lerpPt = (A, B, t) => {
    return { x: A.x + (B.x - A.x) * t, y: A.y + (B.y - A.y) * t };
}

export const segLen = (A, B) => {
    return Math.hypot(B.x - A.x, B.y - A.y);
}

export const projectPointToSegment = (P, A, B) => {
    const vx = B.x - A.x, vy = B.y - A.y;
    const len2 = vx * vx + vy * vy || 1e-9;
    let t = ((P.x - A.x) * vx + (P.y - A.y) * vy) / len2;
    t = Math.max(0, Math.min(1, t));
    const x = A.x + t * vx, y = A.y + t * vy;
    const dx = P.x - x, dy = P.y - y;

    return { x, y, t, d2: dx * dx + dy * dy };
}