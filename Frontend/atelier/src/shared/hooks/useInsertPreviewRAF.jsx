import { useCallback, useEffect, useRef, useState } from "react";

export function useInsertPreviewRAF() {
    const [insertPreview, setInsertPreview] = useState(null);
    const rAFRef = useRef(0);
    const lastRef = useRef(null);

    const setInsertPreviewRAF = useCallback((next) => {
        const prev = lastRef.current;
        if (
            prev &&
            prev.curveId === next.curveId &&
            prev.panelId === next.panelId &&
            prev.allowed === next.allowed &&
            Math.abs(prev.x - next.x) < 0.5 &&
            Math.abs(prev.y - next.y) < 0.5 &&
            Math.abs(prev.t - next.t) < 1e-4
        ) return;

        cancelAnimationFrame(rAFRef.current);
        rAFRef.current = requestAnimationFrame(() => {
            lastRef.current = next;
            setInsertPreview(next);
        });
    }, []);

    useEffect(() => () => cancelAnimationFrame(rAFRef.current), []);

    return { insertPreview, setInsertPreview, setInsertPreviewRAF };
}