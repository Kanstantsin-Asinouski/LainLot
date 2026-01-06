export const facePath = (poly) => {
    return `M ${poly.map(p => `${p.x} ${p.y}`).join(" L ")} Z`;
}

export const faceKey = (poly) => {
    return poly.map(p => `${p.x.toFixed(2)},${p.y.toFixed(2)}`).join("|");
}

// утилита для простого outline-пути из сегментов (для нижнего слоя)
export const segsToD = (segs) =>
    segs.map(s => s.kind === "M" ? `M ${s.x} ${s.y}` :
        s.kind === "L" ? `L ${s.x} ${s.y}` :
            s.kind === "C" ? `C ${s.x1} ${s.y1} ${s.x2} ${s.y2} ${s.x} ${s.y}`
                : "Z").join(" ");