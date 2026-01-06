import { sampleBezier } from "../geometry/geometry.js";
import { sampleLine } from "../geometry/polylineOps.js";

/* ========== полилинии/сегменты ========== */
export const polylinesFromSegs = (segs) => {
    const lines = []; let start = null, curr = null;
    for (const s of segs) {
        if (s.kind === "M") {
            start = {
                x: s.x, y: s.y
            };
            curr = start;
        }
        else if (s.kind === "L") {
            lines.push(sampleLine(s.ax, s.ay, s.x, s.y));
            curr = {
                x: s.x, y: s.y
            };
        }
        else if (s.kind === "C") {
            lines.push(sampleBezier(s.ax, s.ay, s.x1, s.y1, s.x2, s.y2, s.x, s.y));
            curr = {
                x: s.x, y: s.y
            };
        }
        else if (s.kind === "Z" && curr && start && (curr.x !== start.x || curr.y !== start.y)) {
            lines.push(sampleLine(curr.x, curr.y, start.x, start.y));
        }
    }
    return lines;
}

export const segmentsFromPolylines = (polylines) => {
    const segs = [];
    for (const line of polylines)
        for (let i = 0; i + 1 < line.length; i += 2)
            segs.push({ a: line[i], b: line[i + 1] });

    return segs;
}

export const ensureClosed = (segs) => {
    if (segs.some(s => s.kind === "Z"))
        return segs;

    let start = null, last = null;

    for (const s of segs) {
        if (s.kind === "M") {
            start = {
                x: s.x, y: s.y
            };
            last = start;
        } else if (s.kind === "L" || s.kind === "C") {
            last = { x: s.x, y: s.y };
        }
    }

    if (!start || !last)
        return segs;

    const out = [...segs];

    if (start.x !== last.x || start.y !== last.y) {
        out.push({
            kind: "L", ax: last.x, ay: last.y, x: start.x, y: start.y
        });
    }

    out.push({ kind: "Z" });

    return out;
}

/* ========== разбор svg в панели ========== */
export const splitClosedSubpaths = (d) => {
    const parts = [];
    const re = /([Mm][^MmZz]*[Zz])/g; let m; while ((m = re.exec(d))) {
        parts.push(m[1]);
    }

    if (!parts.length) {
        const all = d.split(/(?=[Mm])/).map(s => s.trim()).filter(Boolean);
        for (const p of all) {
            if (/[Zz]/.test(p)) parts.push(p);
        }
    }

    return parts;
}

export const splitSegsIntoSubpaths = (segs) => {
    const out = [];
    let cur = [];
    for (const s of segs) {
        if (s.kind === "M") {
            if (cur.length) out.push(cur);
            cur = [s];
        }
        else {
            cur.push(s);
            if (s.kind === "Z") {
                out.push(cur);
                cur = [];
            }
        }
    }
    if (cur.length) {
        out.push(cur);
    }

    return out.filter(arr => arr.some(x => x.kind !== "M"));
}

export const polylineFromSubpath = (subSegs) => {
    const pts = [];
    let start = null, curr = null;
    const push = (p) => {
        if (!pts.length || Math.hypot(pts[pts.length - 1].x - p.x, pts[pts.length - 1].y - p.y) > 1e-6) {
            pts.push(p);
        }
    };
    for (const s of subSegs) {
        if (s.kind === "M") {
            start = { x: s.x, y: s.y };
            curr = start; push(curr);
        }
        else if (s.kind === "L") {
            const line = sampleLine(s.ax, s.ay, s.x, s.y);
            for (const p of line) push(p);
            curr = { x: s.x, y: s.y };
        }
        else if (s.kind === "C") {
            const line = sampleBezier(s.ax, s.ay, s.x1, s.y1, s.x2, s.y2, s.x, s.y);
            for (const p of line) {
                push(p);
            }
            curr = {
                x: s.x, y: s.y
            };
        }
        else if (s.kind === "Z" && curr && start) {
            push(start);
        }
    }
    if (pts.length > 1) {
        const A = pts[0], B = pts[pts.length - 1];
        if (Math.hypot(A.x - B.x, A.y - B.y) < 1e-6) {
            pts.pop();
        }
    }
    return pts;
}

export const catmullRomToBezierPath = (pts) => {
    if (pts.length < 2)
        return "";

    let d = `M ${pts[0].x} ${pts[0].y}`;

    for (let i = 0; i < pts.length - 1; i++) {
        const p0 = i > 0 ? pts[i - 1] : pts[i];
        const p1 = pts[i];
        const p2 = pts[i + 1];
        const p3 = i + 2 < pts.length ? pts[i + 2] : p2;
        const c1 = { x: p1.x + (p2.x - p0.x) / 6, y: p1.y + (p2.y - p0.y) / 6 };
        const c2 = { x: p2.x - (p3.x - p1.x) / 6, y: p2.y - (p3.y - p1.y) / 6 };
        d += ` C ${c1.x} ${c1.y} ${c2.x} ${c2.y} ${p2.x} ${p2.y}`;
    }

    return d;
}