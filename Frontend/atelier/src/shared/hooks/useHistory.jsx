import { useState, useCallback, useEffect } from "react";
import { useTranslation } from "react-i18next";

/**
 * Action history separately for "Front" (presetIdx=0) and "Back" (presetIdx=1).
 * We write snapshots of the STATE AFTER the change (as in PS/AI).
 * We also maintain an array of logs with action labels.
 */
export function useHistory({
    fills,
    curvesByPanel,
    presetIdx,
    setFills,
    setCurvesByPanel,
    max = 50,
}) {

    const { t } = useTranslation();

    // { front:{stack:[snap], idx:number}, back:{...} }
    const [histByPreset, setHistByPreset] = useState({});
    // { front:{logs:[{label,at}], idx:number}, ... }
    const [logByPreset, setLogByPreset] = useState({});

    const pid = presetIdx === 0 ? "front" : "back";

    // current snapshot
    const snapNow = useCallback(() => ({
        fills: JSON.parse(JSON.stringify(fills)),
        curvesByPanel: JSON.parse(JSON.stringify(curvesByPanel)),
    }), [fills, curvesByPanel]);

    // Initializing the stack and logs when first entering a part
    useEffect(() => {
        setHistByPreset(prev => {
            const h = prev[pid];
            if (h && h.idx >= 0) return prev;
            const snap = snapNow();
            return { ...prev, [pid]: { stack: [snap], idx: 0 } };
        });
        setLogByPreset(prev => {
            const l = prev[pid];
            if (l && l.idx >= 0) return prev;
            return { ...prev, [pid]: { logs: [{ label: t("Start"), at: Date.now() }], idx: 0 } };
        });
    }, [pid, snapNow]);

    const canUndo = !!(histByPreset[pid] && histByPreset[pid].idx > 0);
    const canRedo = !!(histByPreset[pid] && histByPreset[pid].idx < (histByPreset[pid].stack.length - 1));

    // General commit of the State AFTER the change + action log
    const commitState = useCallback((nextSnap, label = t("Step")) => {
        setHistByPreset(prev => {
            const h = prev[pid] || { stack: [], idx: -1 };
            const pruned = h.stack.slice(0, h.idx + 1);
            pruned.push(nextSnap);
            // limited
            while (pruned.length > max) pruned.shift();
            const newIdx = pruned.length - 1;
            return { ...prev, [pid]: { stack: pruned, idx: newIdx } };
        });
        setLogByPreset(prev => {
            const l = prev[pid] || { logs: [], idx: -1 };
            const pruned = l.logs.slice(0, Math.max(0, l.idx) + 1);
            pruned.push({ label, at: Date.now() });
            while (pruned.length > max + 1) pruned.shift();
            const newIdx = pruned.length - 1;
            return { ...prev, [pid]: { logs: pruned, idx: newIdx } };
        });
    }, [pid, max]);

    // Allows you to add a record to history without changing the data, capturing the current state (as PS/AI does "after action").
    const pushHistory = useCallback((label = t("Step")) => {
        const snap = snapNow();
        commitState(snap, label);
    }, [snapNow, commitState]);

    // Отмена / Повтор
    const historyUndo = useCallback(() => {
        setHistByPreset(prev => {
            const h = prev[pid];
            if (!h || h.idx <= 0) return prev;
            const idx = h.idx - 1;
            const snap = h.stack[idx];
            setFills(snap.fills);
            setCurvesByPanel(snap.curvesByPanel);
            // We're moving the log index too.
            setLogByPreset(pl => {
                const l = pl[pid];
                if (!l) return pl;
                return { ...pl, [pid]: { ...l, idx: Math.max(0, l.idx - 1) } };
            });
            return { ...prev, [pid]: { ...h, idx } };
        });
    }, [pid, setFills, setCurvesByPanel]);

    const historyRedo = useCallback(() => {
        setHistByPreset(prev => {
            const h = prev[pid];
            if (!h || h.idx >= h.stack.length - 1) return prev;
            const idx = h.idx + 1;
            const snap = h.stack[idx];
            setFills(snap.fills);
            setCurvesByPanel(snap.curvesByPanel);
            setLogByPreset(pl => {
                const l = pl[pid];
                if (!l) return pl;
                return { ...pl, [pid]: { ...l, idx: Math.min(l.logs.length - 1, l.idx + 1) } };
            });
            return { ...prev, [pid]: { ...h, idx } };
        });
    }, [pid, setFills, setCurvesByPanel]);

    // Wrappers: calculate nextState, commit, then apply
    const applyFillChange = useCallback((updater, label = t("Filling")) => {
        const nextFills = typeof updater === "function" ? updater(fills) : updater;
        const nextSnap = { fills: JSON.parse(JSON.stringify(nextFills)), curvesByPanel: snapNow().curvesByPanel };
        commitState(nextSnap, label);
        setFills(nextFills);
    }, [fills, setFills, snapNow, commitState]);

    const applyCurvesChange = useCallback((updater, label = t("Lines")) => {
        const nextCurves = typeof updater === "function" ? updater(curvesByPanel) : updater;
        const nextSnap = { fills: snapNow().fills, curvesByPanel: JSON.parse(JSON.stringify(nextCurves)) };
        commitState(nextSnap, label);
        setCurvesByPanel(nextCurves);
    }, [curvesByPanel, setCurvesByPanel, snapNow, commitState]);

    // Public data for the UI feed
    const hist = histByPreset[pid] || { stack: [], idx: -1 };
    const log = logByPreset[pid] || { logs: [], idx: -1 };
    const historyItems = log.logs;
    const historyIndex = log.idx;

    return {
        historyUndo, historyRedo, canUndo, canRedo,
        applyFillChange, applyCurvesChange,
        pushHistory,
        historyItems, historyIndex,
    };
}
