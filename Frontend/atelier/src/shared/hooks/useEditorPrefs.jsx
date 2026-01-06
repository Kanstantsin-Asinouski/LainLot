import { useEffect, useRef, useState } from "react";

export function useEditorPrefs({
    activeId, mode, setMode, paintColor, setPaintColor,
    lineStyle, setLineStyle, defaultSubCount, setDefaultSubCount, waveAmpPx,
    setWaveAmpPx, waveLenPx, setWaveLenPx, lastLineMode, setLastLineMode,
    presetIdx
}) {
    const applyingPrefsRef = useRef(false);
    const [prefsLoaded, setPrefsLoaded] = useState(false);
    const [prefs, setPrefs] = useState({ front: {}, back: {} });

    // load once
    useEffect(() => {
        try {
            const v = JSON.parse(localStorage.getItem("ce.prefs.v1") || "{}");
            setPrefs({ front: {}, back: {}, ...v });
            setPrefsLoaded(true);
        } catch { }
    }, []);

    // apply on side switch
    useEffect(() => {
        if (!prefsLoaded) return;       // ещё не загрузили из LS — ничего не делать

        const p = prefs[activeId] || {};

        // Включаем "фазу применения" — остальные эффекты записей не должны срабатывать
        applyingPrefsRef.current = true;

        if (p.paintColor && p.paintColor !== paintColor) setPaintColor(p.paintColor);
        if (p.lineStyle && p.lineStyle !== lineStyle) setLineStyle(p.lineStyle);
        if (Number.isFinite(p.defaultSubCount) && p.defaultSubCount !== defaultSubCount) setDefaultSubCount(p.defaultSubCount);
        if (Number.isFinite(p.waveAmpPx) && p.waveAmpPx !== waveAmpPx) setWaveAmpPx(p.waveAmpPx);
        if (Number.isFinite(p.waveLenPx) && p.waveLenPx !== waveLenPx) setWaveLenPx(p.waveLenPx);
        if (p.lastLineMode && p.lastLineMode !== lastLineMode) setLastLineMode(p.lastLineMode);

        // применяем сохранённый режим (preview допускается)
        const desired = p.lastMode ?? "preview";                // можно оставить без фоллбэка, но так предсказуемей
        const safe = desired === "deleteFill" ? "paint" : desired;
        if (safe !== mode) setMode(safe);

        Promise.resolve().then(() => { applyingPrefsRef.current = false; });
    }, [presetIdx, prefsLoaded]);  // зависимости — только смена детали/загрузка prefs

    // persist mode (safeguard)
    useEffect(() => {
        // во время применения prefs не пишем обратно
        if (applyingPrefsRef.current) return;

        // единственная “правка безопасности”: не возвращаемся в deleteFill
        const safe = mode === "deleteFill" ? "paint" : mode;

        setPrefs(prev => {
            const cur = prev[activeId] || {};
            if (cur.lastMode === safe) return prev;            // ничего не меняется
            const next = { ...prev, [activeId]: { ...cur, lastMode: safe } };
            try { localStorage.setItem("ce.prefs.v1", JSON.stringify(next)); } catch { }
            return next;
        });
    }, [mode, activeId]);

    // persist knobs per side
    useEffect(() => {
        if (applyingPrefsRef.current) return;    // во время применения ничего не пишем
        setPrefs(prev => {
            const cur = prev[activeId] || {};
            const nextDetail = {
                ...cur,
                paintColor, lineStyle, defaultSubCount, waveAmpPx, waveLenPx, lastLineMode,
            };

            // shallow-equal: если ничего не поменялось — не дергаем setPrefs, чтобы не раскручивать эффекты
            const same =
                cur.paintColor === nextDetail.paintColor &&
                cur.lineStyle === nextDetail.lineStyle &&
                cur.defaultSubCount === nextDetail.defaultSubCount &&
                cur.waveAmpPx === nextDetail.waveAmpPx &&
                cur.waveLenPx === nextDetail.waveLenPx &&
                cur.lastLineMode === nextDetail.lastLineMode;

            if (same)
                return prev;

            const next = { ...prev, [activeId]: nextDetail };
            try { localStorage.setItem("ce.prefs.v1", JSON.stringify(next)); } catch { }

            return next;
        });
    }, [activeId, paintColor, lineStyle, defaultSubCount, waveAmpPx, waveLenPx, lastLineMode]);

    // утилита: выставить lastMode=preview на обеих сторонах (нужно твоей кнопке Reset)
    const setBothLastModePreview = () => {
        setPrefs(prev => {
            const next = {
                ...prev,
                front: { ...(prev.front || {}), lastMode: "preview" },
                back: { ...(prev.back || {}), lastMode: "preview" },
            };
            try { localStorage.setItem("ce.prefs.v1", JSON.stringify(next)); } catch { }
            return next;
        });
    };

    return { applyingPrefsRef, setBothLastModePreview };
}