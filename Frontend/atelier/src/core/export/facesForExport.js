import { sampleBezier } from "../geometry/geometry.js";
import { pointsToPairedPolyline } from "../geometry/polylineOps.js";
import { polylinesFromSegs, segmentsFromPolylines } from "../svg/polylineOps.js";
import { buildFacesFromSegments } from "../svg/buildFaces.js";
import { splitByIntersections } from "../geometry/intersections.js";

const shortId = (id) => {
    const s = String(id);
    return s.includes("-") ? s.split("-").pop() : s;
};

/** Faces с учётом пользовательских линий (как на канве) */
export const buildFacesForExport = (panels, curvesByPanelLocal = {}) => {
    const res = {};
    for (const p of panels) {
        const baseLines = polylinesFromSegs(p.segs);

        // Пользовательские линии в виде полилиний (для разбиения на faces)
        const userLines =
            (curvesByPanelLocal[p.id] || curvesByPanelLocal[shortId(p.id)] || [])
                .flatMap(c => {
                    if (c.type === "cubic" && c.ax != null) {
                        return [sampleBezier(c.ax, c.ay, c.c1.x, c.c1.y, c.c2.x, c.c2.y, c.bx, c.by)];
                    }
                    if (Array.isArray(c.pts) && c.pts.length >= 2) {
                        return [pointsToPairedPolyline(c.pts)];
                    }
                    return [];
                });

        const segsFlat = segmentsFromPolylines([...baseLines, ...userLines]);
        res[p.id] = buildFacesFromSegments(splitByIntersections(segsFlat));
    }
    return res;
};