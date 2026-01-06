// svgPath.js
// Путь к SVG из public, без абсолютных доменов:
export const SVG_BASE = `/2d/svg`;                 // было /2d/svg/hoodie
export const MANIFEST_URL = `${SVG_BASE}/manifest.json`;
export const EMPTY_PREVIEW = `${SVG_BASE}/empty.svg`;

// Разрешение относительных путей из манифеста / UI
export const resolveSvgSrcPath = (p) => {
    if (!p) return "";
    const clean = p.replace(/^\/+/, "");
    if (clean.startsWith("2d/")) return `/${clean}`;
    // теперь относительные пути собираем от /2d/svg (а не «hoodie»)
    return `${SVG_BASE}/${clean}`;
};