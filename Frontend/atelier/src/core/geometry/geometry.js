export const sampleBezier = (ax, ay, x1, y1, x2, y2, x, y, steps = 36) => {
    const pts = []; let px = ax, py = ay;
    for (let k = 1; k <= steps; k++) {
        const t = k / steps, mt = 1 - t;
        const xt = mt * mt * mt * ax + 3 * mt * mt * t * x1 + 3 * mt * t * t * x2 + t * t * t * x;
        const yt = mt * mt * mt * ay + 3 * mt * mt * t * y1 + 3 * mt * t * t * y2 + t * t * t * y;
        pts.push({ x: px, y: py }, { x: xt, y: yt }); px = xt; py = yt;
    }

    return pts;
}

export const sampleBezierPoints = (ax, ay, x1, y1, x2, y2, x, y, steps = 56) => {
    const out = []; for (let k = 0; k <= steps; k++) {
        const t = k / steps, mt = 1 - t;
        out.push({
            x: mt * mt * mt * ax + 3 * mt * mt * t * x1 + 3 * mt * t * t * x2 + t * t * t * x,
            y: mt * mt * mt * ay + 3 * mt * mt * t * y1 + 3 * mt * t * t * y2 + t * t * t * y
        });
    }

    return out;
}

export const bboxIoU = (a, b) => {
    const x1 = Math.max(a.x, b.x);
    const y1 = Math.max(a.y, b.y);
    const x2 = Math.min(a.x + a.w, b.x + b.w);
    const y2 = Math.min(a.y + a.h, b.y + b.h);
    const iw = Math.max(0, x2 - x1), ih = Math.max(0, y2 - y1);
    const inter = iw * ih;
    const union = a.w * a.h + b.w * b.h - inter || 1;

    return inter / union;
}

export const segsSignature = (segs) => {
    // стабильный «отпечаток» геометрии
    const parts = [];

    for (const s of segs) {
        if (s.kind === "M") parts.push(`M${s.x.toFixed(3)},${s.y.toFixed(3)}`);
        else if
            (s.kind === "L") parts.push(`L${s.x.toFixed(3)},${s.y.toFixed(3)}`);

        else if (s.kind === "C") parts.push(
            `C${s.x1.toFixed(3)},${s.y1.toFixed(3)},${s.x2.toFixed(3)},${s.y2.toFixed(3)},${s.x.toFixed(3)},${s.y.toFixed(3)}`);
        else if
            (s.kind === "Z") parts.push("Z");
    }

    return parts.join(";");
}