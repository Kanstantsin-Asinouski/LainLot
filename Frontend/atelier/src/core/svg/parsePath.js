/* ========== парсер d: M/L/C/Z + H/V/S/Q/T, A->L ========== */
export const parsePathD = (d) => {
    d = d.replace(/,/g, " ").replace(/\s+/g, " ").trim();
    const tokens = d.match(/[MLHVCSQTAZmlhvcsqtaz]|-?\d*\.?\d+(?:e[-+]?\d+)?/g) || [];
    let i = 0, cmd = null;
    const segs = [];
    let curr = { x: 0, y: 0 }, start = { x: 0, y: 0 };
    let prevC2 = null, prevQ1 = null;
    const read = () => parseFloat(tokens[i++]);
    const isCmd = (t) => /[MLHVCSQTAZmlhvcsqtaz]/.test(t);

    while (i < tokens.length) {
        if (isCmd(tokens[i])) cmd = tokens[i++];

        switch (cmd) {
            case "M": case "m": {
                const rel = cmd === "m";
                const x = read(), y = read();
                const nx = rel ? curr.x + x : x, ny = rel ? curr.y + y : y;
                curr = { x: nx, y: ny };
                start = { ...curr };
                segs.push({
                    kind: "M", x: nx, y: ny
                });
                prevC2 = prevQ1 = null;

                while (i < tokens.length && !isCmd(tokens[i])) {
                    const lx = read(), ly = read();
                    const nx2 = rel ? curr.x + lx : lx, ny2 = rel ? curr.y + ly : ly;
                    segs.push({
                        kind: "L", ax: curr.x, ay: curr.y, x: nx2, y: ny2
                    });
                    curr = { x: nx2, y: ny2 };
                    prevC2 = prevQ1 = null;
                }

                break;
            }
            case "L": case "l": {
                const rel = cmd === "l";
                const x = read(), y = read();
                const nx = rel ? curr.x + x : x, ny = rel ? curr.y + y : y;
                segs.push({
                    kind: "L", ax: curr.x, ay: curr.y, x: nx, y: ny
                });
                curr = {
                    x: nx, y: ny
                };
                prevC2 = prevQ1 = null;

                break;
            }
            case "H": case "h": {
                const rel = cmd === "h"; const x = read();
                const nx = rel ? curr.x + x : x, ny = curr.y;
                segs.push({
                    kind: "L", ax: curr.x, ay: curr.y, x: nx, y: ny
                });
                curr = {
                    x: nx, y: ny
                };
                prevC2 = prevQ1 = null;

                break;
            }
            case "V": case "v": {
                const rel = cmd === "v"; const y = read();
                const nx = curr.x, ny = rel ? curr.y + y : y;
                segs.push({
                    kind: "L", ax: curr.x, ay: curr.y, x: nx, y: ny
                });
                curr = {
                    x: nx, y: ny
                };
                prevC2 = prevQ1 = null;

                break;
            }
            case "C": case "c": {
                const rel = cmd === "c";
                const x1 = read(), y1 = read(), x2 = read(), y2 = read(), x = read(), y = read();
                const seg = {
                    kind: "C", ax: curr.x, ay: curr.y,
                    x1: rel ? curr.x + x1 : x1, y1: rel ? curr.y + y1 : y1,
                    x2: rel ? curr.x + x2 : x2, y2: rel ? curr.y + y2 : y2,
                    x: rel ? curr.x + x : x, y: rel ? curr.y + y : y
                };
                segs.push(seg);
                prevC2 = {
                    x: seg.x2, y: seg.y2
                };
                prevQ1 = null; curr = {
                    x: seg.x, y: seg.y
                };

                break;
            }
            case "S": case "s": {
                const rel = cmd === "s"; const x2 = read(), y2 = read(), x = read(), y = read();
                const x1 = prevC2 ? 2 * curr.x - prevC2.x : curr.x;
                const y1 = prevC2 ? 2 * curr.y - prevC2.y : curr.y;
                const seg = {
                    kind: "C", ax: curr.x, ay: curr.y,
                    x1, y1,
                    x2: rel ? curr.x + x2 : x2, y2: rel ? curr.y + y2 : y2,
                    x: rel ? curr.x + x : x, y: rel ? curr.y + y : y
                };
                segs.push(seg);
                prevC2 = {
                    x: seg.x2, y: seg.y2
                };
                prevQ1 = null;
                curr = {
                    x: seg.x, y: seg.y
                };

                break;
            }
            case "Q": case "q": {
                const rel = cmd === "q"; const qx = read(), qy = read(), x = read(), y = read();
                const Q1 = {
                    x: rel ? curr.x + qx : qx, y: rel ? curr.y + qy : qy
                };
                const P0 = { ...curr }, P2 = { x: rel ? curr.x + x : x, y: rel ? curr.y + y : y };
                const C1 = {
                    x: P0.x + (2 / 3) * (Q1.x - P0.x), y: P0.y + (2 / 3) * (Q1.y - P0.y)
                };
                const C2 = {
                    x: P2.x + (2 / 3) * (Q1.x - P2.x), y: P2.y + (2 / 3) * (Q1.y - P2.y)
                };
                segs.push({
                    kind: "C", ax: P0.x, ay: P0.y, x1: C1.x, y1: C1.y, x2: C2.x, y2: C2.y, x: P2.x, y: P2.y
                });
                curr = P2;
                prevC2 = { ...C2 };
                prevQ1 = { ...Q1 };

                break;
            }
            case "T": case "t": {
                const rel = cmd === "t"; const refl = prevQ1 ? {
                    x: 2 * curr.x - prevQ1.x, y: 2 * curr.y - prevQ1.y
                } : { ...curr };
                const x = read(), y = read();
                const P2 = {
                    x: rel ? curr.x + x : x, y: rel ? curr.y + y : y
                };
                const C1 = {
                    x: curr.x + (2 / 3) * (refl.x - curr.x), y: curr.y + (2 / 3) * (refl.y - curr.y)
                };
                const C2 = {
                    x: P2.x + (2 / 3) * (refl.x - P2.x), y: P2.y + (2 / 3) * (refl.y - P2.y)
                };
                segs.push({
                    kind: "C", ax: curr.x, ay: curr.y, x1: C1.x, y1: C1.y, x2: C2.x, y2: C2.y, x: P2.x, y: P2.y
                });
                curr = P2; prevC2 = { ...C2 }; prevQ1 = { ...refl };

                break;
            }
            case "A": case "a": {
                const rel = cmd === "a";
                const _rx = read(), _ry = read(), _rot = read(), _laf = read(), _sw = read();
                const x = read(), y = read();
                const nx = rel ? curr.x + x : x, ny = rel ? curr.y + y : y;
                segs.push({
                    kind: "L", ax: curr.x, ay: curr.y, x: nx, y: ny
                });
                curr = { x: nx, y: ny }; prevC2 = prevQ1 = null;

                break;
            }
            case "Z":
            case "z": segs.push({ kind: "Z" });
                curr = { ...start }; prevC2 = prevQ1 = null;

                break;

            default:
                break;
        }
    }

    return segs;
}

/* ========== polygon / polyline ========== */
export const parsePoints = (pointsStr) => {
    const nums = pointsStr.trim().split(/[\s,]+/).map(parseFloat).filter(n => !isNaN(n));
    const pts = [];
    for (let i = 0; i + 1 < nums.length; i += 2)
        pts.push({ x: nums[i], y: nums[i + 1] });

    return pts;
}

export const segsFromPoints = (pointsStr, close = true) => {
    const pts = parsePoints(pointsStr); if (!pts.length) return [];
    const segs = [{ kind: "M", x: pts[0].x, y: pts[0].y }]; let prev = pts[0];
    for (let i = 1; i < pts.length; i++) {
        const p = pts[i]; segs.push({
            kind: "L", ax: prev.x, ay: prev.y, x: p.x, y: p.y
        });
        prev = p;
    }
    if (close && (prev.x !== pts[0].x || prev.y !== pts[0].y))
        segs.push({ kind: "L", ax: prev.x, ay: prev.y, x: pts[0].x, y: pts[0].y });
    if (close)
        segs.push({ kind: "Z" });

    return segs;
}