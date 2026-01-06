import { parseViewBox, parseMatrix, applyMatrixToSegs } from "../geometry/matrix.js";
import { parsePathD, segsFromPoints } from "../svg/parsePath.js";
import { looksLikeBackground } from "../svg/heuristics.js";
import { collectAnchors } from "../svg/anchors.js";
import { splitSegsIntoSubpaths } from "../svg/polylineOps.js";

export const rectToSegs = (x, y, w, h) => {
    const p = [
        { x: +x, y: +y },
        { x: +x + +w, y: +y },
        { x: +x + +w, y: +y + +h },
        { x: +x, y: +y + +h },
    ];
    return segsFromPoints(p.map(q => `${q.x},${q.y}`).join(" "), true);
};
export const circleToSegs = (cx, cy, r) => {
    const N = 12, pts = [];
    for (let i = 0; i < N; i++) {
        const a = (i / N) * Math.PI * 2;
        pts.push({ x: +cx + +r * Math.cos(a), y: +cy + +r * Math.sin(a) });
    }
    return segsFromPoints(pts.map(q => `${q.x},${q.y}`).join(" "), true);
};
export const ellipseToSegs = (cx, cy, rx, ry) => {
    const N = 12, pts = [];
    for (let i = 0; i < N; i++) {
        const a = (i / N) * Math.PI * 2;
        pts.push({ x: +cx + +rx * Math.cos(a), y: +cy + +ry * Math.sin(a) });
    }
    return segsFromPoints(pts.map(q => `${q.x},${q.y}`).join(" "), true);
};

// собираем кандидатов из SVG-текста
export const extractCandidates = (raw) => {
    const tags = [];
    const re = /<(path|polygon|polyline|rect|circle|ellipse)\b([^>]*)>/ig;
    let m;
    while ((m = re.exec(raw))) {
        const tag = m[0];
        const name = m[1].toLowerCase();
        const attrs = m[2] || "";
        const get = (k) => attrs.match(new RegExp(`${k}="([^"]+)"`, "i"))?.[1] || "";

        const transform = get("transform");
        let segs = [];

        if (name === "path") {
            const d = get("d");
            if (d) segs = parsePathD(d);                                       // :contentReference[oaicite:5]{index=5}
        } else if (name === "polygon") {
            const points = get("points");
            if (points) segs = segsFromPoints(points, true);                    // :contentReference[oaicite:6]{index=6}
        } else if (name === "polyline") {
            const points = get("points");
            if (points) segs = segsFromPoints(points, false);
        } else if (name === "rect") {
            segs = rectToSegs(get("x") || 0, get("y") || 0, get("width") || 0, get("height") || 0);
        } else if (name === "circle") {
            segs = circleToSegs(get("cx") || 0, get("cy") || 0, get("r") || 0);
        } else if (name === "ellipse") {
            segs = ellipseToSegs(get("cx") || 0, get("cy") || 0, get("rx") || 0, get("ry") || 0);
        }

        // применяем transform="matrix(...)"
        const M = transform ? parseMatrix(transform) : null;                  // :contentReference[oaicite:7]{index=7}
        if (M) segs = applyMatrixToSegs(segs, M);                             // :contentReference[oaicite:8]{index=8}

        if (segs.length) tags.push({ segs, rawTag: tag });
    }
    return tags;
};

export const panelsFromRaw = (idPrefix, raw) => {
    const root = parseViewBox(raw);                                         // :contentReference[oaicite:9]{index=9}
    const cands = extractCandidates(raw).map(c => {
        const xs = [], ys = [];
        for (const s of c.segs) {
            if (s.kind === "M") { xs.push(s.x); ys.push(s.y); }
            else if (s.kind === "L") { xs.push(s.ax, s.x); ys.push(s.ay, s.y); }
            else if (s.kind === "C") { xs.push(s.ax, s.x1, s.x2, s.x); ys.push(s.ay, s.y1, s.y2, s.y); }
        }
        const minX = Math.min(...xs), maxX = Math.max(...xs);
        const minY = Math.min(...ys), maxY = Math.max(...ys);
        const bbox = { x: minX, y: minY, w: maxX - minX, h: maxY - minY };
        return { ...c, bbox };
    });

    const filtered = cands.filter(c => !looksLikeBackground(c, root));      // :contentReference[oaicite:10]{index=10}

    const out = [];
    let idx = 0;
    for (const cand of filtered) {
        const subs = splitSegsIntoSubpaths(cand.segs);                         // :contentReference[oaicite:11]{index=11}
        for (const sub of subs) {
            const xs = [], ys = [];
            for (const s of sub) {
                if (s.kind === "M") { xs.push(s.x); ys.push(s.y); }
                else if (s.kind === "L") { xs.push(s.ax, s.x); ys.push(s.ay, s.y); }
                else if (s.kind === "C") { xs.push(s.ax, s.x1, s.x2, s.x); ys.push(s.ay, s.y1, s.y2, s.y); }
            }
            const w = Math.max(...xs) - Math.min(...xs);
            const h = Math.max(...ys) - Math.min(...ys);
            if (w * h < (root.w * root.h) * 0.002) continue;

            const anchors = collectAnchors(sub);                                  // :contentReference[oaicite:12]{index=12}
            out.push({
                id: `${idPrefix}-${(++idx).toString().padStart(2, "0")}`,
                segs: sub,
                anchors,
                meta: { slot: null }
            });
        }
    }
    return out;
};