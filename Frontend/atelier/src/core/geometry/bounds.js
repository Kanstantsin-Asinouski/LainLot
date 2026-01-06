export const area = (poly) => {
    let s = 0; for (let i = 0; i < poly.length; i++) {
        const a = poly[i], b = poly[(i + 1) % poly.length]; s += a.x * b.y - b.x * a.y;
    }

    return s / 2;
}

export const getBounds = (segs) => {
    const xs = [], ys = [];
    for (const s of segs) {
        if (s.kind === "M") { xs.push(s.x); ys.push(s.y); }
        if (s.kind === "L") { xs.push(s.ax, s.x); ys.push(s.ay, s.y); }
        if (s.kind === "C") { xs.push(s.ax, s.x1, s.x2, s.x); ys.push(s.ay, s.y1, s.y2, s.y); }
    }
    if (!xs.length) return { x: 0, y: 0, w: 1, h: 1 };
    const minX = Math.min(...xs), maxX = Math.max(...xs);
    const minY = Math.min(...ys), maxY = Math.max(...ys);

    return { x: minX, y: minY, w: maxX - minX, h: maxY - minY };
}