/* ========== построение граней ========== */
export const segIntersect = (p, q, r, s) => {
    const EPS = 1e-9;

    const ux = q.x - p.x, uy = q.y - p.y, vx = s.x - r.x, vy = s.y - r.y, wx = p.x - r.x, wy = p.y - r.y;
    const D = ux * vy - uy * vx; if (Math.abs(D) < EPS) return null;
    const t = (vx * wy - vy * wx) / D, u = (ux * wy - uy * wx) / D;

    // вне отрезков — нет
    if (t < -EPS || t > 1 + EPS || u < -EPS || u > 1 + EPS) return null;

    const onEndT = (t <= EPS || t >= 1 - EPS);
    const onEndU = (u <= EPS || u >= 1 - EPS);

    // если оба только на концах (общая вершина) — пропускаем
    if (onEndT && onEndU) return null;

    // иначе допускаем (конец-внутрь или внутрь-внутрь)
    return {
        t: Math.max(0, Math.min(1, t)),
        u: Math.max(0, Math.min(1, u)),
        x: p.x + t * ux,
        y: p.y + t * uy
    };
}

export const splitByIntersections = (segments) => {
    const lists = segments.map(() => [0, 1]);
    const pts = segments.map(() => ({}));

    for (let i = 0; i < segments.length; i++) {
        for (let j = i + 1; j < segments.length; j++) {
            const A = segments[i], B = segments[j];
            const hit = segIntersect(A.a, A.b, B.a, B.b);

            if (!hit)
                continue;

            lists[i].push(hit.t); pts[i][hit.t] = { x: hit.x, y: hit.y };
            lists[j].push(hit.u); pts[j][hit.u] = { x: hit.x, y: hit.y };
        }
    }
    const res = [];
    for (let i = 0; i < segments.length; i++) {
        const S = segments[i];
        const tsRaw = lists[i].slice().sort((a, b) => a - b);
        const ts = [];
        for (const t of tsRaw) {
            if (!ts.length || Math.abs(t - ts[ts.length - 1]) > 1e-6) {
                ts.push(Math.max(0, Math.min(1, t)));
            }
        }
        const p = (t) => ({ x: S.a.x + (S.b.x - S.a.x) * t, y: S.a.y + (S.b.y - S.a.y) * t });
        for (let k = 0; k + 1 < ts.length; k++) {
            const t1 = ts[k], t2 = ts[k + 1]; if (t2 - t1 < 1e-6)
                continue;

            const P1 = pts[i][t1] || p(t1);
            const P2 = pts[i][t2] || p(t2);
            res.push({ a: P1, b: P2 });
        }
    }
    return res;
}