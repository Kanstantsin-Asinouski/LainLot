import { getBounds } from "../geometry/bounds.js";
import { renderPresetGroup } from "./renderPresetGroup.js";

const esc = s => String(s).replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

const bboxForPanels = (panels) => {
    let bb = null;
    for (const p of panels) {
        const b = getBounds(p.segs);
        if (!bb) bb = { ...b };
        else {
            const x1 = Math.min(bb.x, b.x);
            const y1 = Math.min(bb.y, b.y);
            const x2 = Math.max(bb.x + bb.w, b.x + b.w);
            const y2 = Math.max(bb.y + bb.h, b.y + b.h);
            bb = { x: x1, y: y1, w: x2 - x1, h: y2 - y1 };
        }
    }
    return bb || { x: 0, y: 0, w: 800, h: 500 };
};

export const buildCombinedSVG = async ({
    svgCache,
    currentPresetId,
    currentCurves,
    currentFills,
    savedByPreset,
    inkscapeCompat = true
}) => {
    const mergedSnaps = {
        ...savedByPreset,
        [currentPresetId]: {
            ...(savedByPreset?.[currentPresetId] || {}),
            curvesByPanel: currentCurves,
            fills: currentFills
        }
    };

    const frontPanels = Array.isArray(svgCache.front) ? svgCache.front : [];
    const backPanels = Array.isArray(svgCache.back) ? svgCache.back : [];

    const gFront = renderPresetGroup(frontPanels, mergedSnaps.front?.curvesByPanel, mergedSnaps.front?.fills, { inkscapeCompat, maskId: "under-hood-front" });
    const gBack = renderPresetGroup(backPanels, mergedSnaps.back?.curvesByPanel, mergedSnaps.back?.fills, { inkscapeCompat, maskId: "under-hood-back" });

    const bbF = bboxForPanels(frontPanels);
    const bbB = bboxForPanels(backPanels);
    const pad = 24, gap = Math.max(60, Math.round(Math.max(bbF.h, bbB.h) * 0.08));
    const width = pad + bbF.w + gap + bbB.w + pad;
    const height = pad + Math.max(bbF.h, bbB.h) + pad;

    const tFront = `translate(${pad - bbF.x}, ${pad - bbF.y})`;
    const tBack = `translate(${pad + bbF.w + gap - bbB.x}, ${pad - bbB.y})`;

    const w = Math.round(width), h = Math.round(height);

    return [
        `<?xml version="1.0" encoding="UTF-8"?>`,
        `<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="${w}px"  height="${h}px" viewBox="0 0 ${w} ${h}">`,
        `  <desc>${esc("Costume — front & back")}</desc>`,
        `  <g transform="${tFront}">${gFront}</g>`,
        `  <g transform="${tBack}">${gBack}</g>`,
        `</svg>`
    ].join("\n");
};