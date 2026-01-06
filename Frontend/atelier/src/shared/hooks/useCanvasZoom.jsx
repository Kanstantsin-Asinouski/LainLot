import { useMemo, useState, useCallback } from "react";

export default function useCanvasZoom({ bbox, padRatio = 0.06 }) {
    const [zoom, setZoom] = useState(1);                 // 0.5 … 1.0
    const clamp = (z) => Math.max(0.5, Math.min(1, z));
    const setZoomExact = useCallback((k) => setZoom(clamp(k)), []);

    // базовый бокс с небольшими полями
    const baseBox = useMemo(() => {
        if (!bbox) return null;
        const pad = Math.max(bbox.w, bbox.h) * padRatio;
        return { x: bbox.x - pad, y: bbox.y - pad, w: bbox.w + pad * 2, h: bbox.h + pad * 2 };
    }, [bbox, padRatio]);

    // текущий viewBox с учётом zoom
    const zoomedViewBox = useMemo(() => {
        if (!baseBox) return "0 0 100 100";
        const k = clamp(zoom);
        const cx = baseBox.x + baseBox.w / 2;
        const cy = baseBox.y + baseBox.h / 2;
        const w = baseBox.w / k;
        const h = baseBox.h / k;
        return `${cx - w / 2} ${cy - h / 2} ${w} ${h}`;
    }, [baseBox, zoom]);

    const zoomIn = useCallback(() => setZoom((z) => clamp(z * 1.1)), []);
    const zoomOut = useCallback(() => setZoom((z) => clamp(z * 0.9)), []);
    const reset = useCallback(() => setZoom(1), []);

    return {
        zoom, zoomedViewBox, zoomIn, zoomOut, reset,
        setZoomExact
    };
}