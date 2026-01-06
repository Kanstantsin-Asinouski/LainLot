import { splitSegsIntoSubpaths, polylineFromSubpath } from "../svg/polylineOps.js";
import { area } from "../geometry/bounds.js";

/** эвристика «фон/рамка» — выкидываем только когда кандидатов больше одного */
// cand: {segs,bbox,rawTag}, root: {w,h}
export const looksLikeBackground = (cand, root) => {
    const { bbox, rawTag, segs } = cand;

    const Av = Math.max(1, root.w * root.h);
    const A = Math.abs(bbox.w * bbox.h);
    const areaFrac = A / Av;
    const coverW = bbox.w / root.w;
    const coverH = bbox.h / root.h;

    // «очень большой»: закрывает заметную часть листа
    const isBig = areaFrac > 0.48 || (coverW > 0.90 && coverH > 0.90);

    // 1) явный <rect>
    const isRectTag = /^<rect\b/i.test(rawTag);

    // 2) path/polygon, который геометрически «как прямоугольник»:
    //    без кривых, оси X/Y, площадь ≈ площади bbox
    let rectLike = false;
    const subs = splitSegsIntoSubpaths(segs);
    if (subs.length) {
        const ring = polylineFromSubpath(subs[0]);
        if (ring.length >= 4) {
            const Ab = Math.abs(area(ring));
            const bboxLike = Math.abs(Ab - A) / Math.max(A, 1) < 0.06;
            const axisAligned = ring.every((p, i) => {
                const q = ring[(i + 1) % ring.length];
                const dx = Math.abs(q.x - p.x), dy = Math.abs(q.y - p.y);
                return dx < 1e-3 || dy < 1e-3;
            });
            rectLike = bboxLike && axisAligned && !segs.some(s => s.kind === "C");
        }
    }

    // 3) типичные «имена фона» или стили
    const namedBg = /inkscape:label="background"|id="(?:background|bg|frame|artboard)"/i.test(rawTag);
    const styleBg = /\sfill="(?!none)/i.test(rawTag) || /(?:\s|;)fill\s*:\s*[^;#)]/i.test(rawTag) || /\sstroke="none"/i.test(rawTag);

    return isBig && (isRectTag || rectLike || namedBg || styleBg);
}