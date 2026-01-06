/* ========== якоря/прочее ========== */
export const collectAnchors = (segs) => {
    const out = [];
    for (const s of segs) {
        if (s.kind === "M")
            out.push({ x: s.x, y: s.y });

        if (s.kind === "L" || s.kind === "C")
            out.push({ x: s.x, y: s.y });
    }
    return out;
}