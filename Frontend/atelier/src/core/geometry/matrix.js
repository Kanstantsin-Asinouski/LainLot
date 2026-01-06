/* ===== SVG meta & transforms ===== */
export const parseViewBox = (raw) => {
    // пробуем viewBox, иначе width/height
    const vb = raw.match(/\bviewBox="([^"]+)"/i)?.[1]?.trim();
    if (vb) {
        const [minx, miny, w, h] = vb.split(/\s+/).map(parseFloat);
        if (isFinite(w) && isFinite(h) && w > 0 && h > 0) return { w, h };
    }
    const w = parseFloat(raw.match(/\bwidth="([\d.]+)(?:px)?"/i)?.[1] ?? "0");
    const h = parseFloat(raw.match(/\bheight="([\d.]+)(?:px)?"/i)?.[1] ?? "0");

    return (isFinite(w) && isFinite(h) && w > 0 && h > 0) ? { w, h } : { w: 1, h: 1 };
}

export const parseMatrix = (str) => {
    // transform="matrix(a b c d e f)" или matrix(a,b,c,d,e,f)
    const m = str?.match(/matrix\(\s*([^\)]+)\)/i);

    if (!m)
        return null;

    const nums = m[1].split(/[\s,]+/).map(parseFloat);

    if (nums.length !== 6 || nums.some(n => !isFinite(n)))
        return null;
    const [a, b, c, d, e, f] = nums;

    return { a, b, c, d, e, f };
}

export const applyMatrixToPoint = (p, M) => {
    return { x: M.a * p.x + M.c * p.y + M.e, y: M.b * p.x + M.d * p.y + M.f };
}

export const applyMatrixToSegs = (segs, M) => {
    if (!M) return segs;
    return segs.map(s => {
        const r = { ...s };
        if (s.kind === "L" || s.kind === "M") {
            const p = applyMatrixToPoint({ x: s.x, y: s.y }, M);
            r.x = p.x; r.y = p.y;
            if ("ax" in s) {
                const a = applyMatrixToPoint({ x: s.ax, y: s.ay }, M);
                r.ax = a.x; r.ay = a.y;
            }
        }
        else if (s.kind === "C") {
            const a = applyMatrixToPoint({ x: s.ax, y: s.ay }, M);
            const p1 = applyMatrixToPoint({ x: s.x1, y: s.y1 }, M);
            const p2 = applyMatrixToPoint({ x: s.x2, y: s.y2 }, M);
            const p = applyMatrixToPoint({ x: s.x, y: s.y }, M);
            r.ax = a.x; r.ay = a.y; r.x1 = p1.x; r.y1 = p1.y; r.x2 = p2.x; r.y2 = p2.y; r.x = p.x; r.y = p.y;
        }

        return r;
    });
}
