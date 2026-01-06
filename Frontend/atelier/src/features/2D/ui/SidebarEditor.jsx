import { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { getVisibleSlotsForFace } from "../../../core/variables/variants.js";
import SectionSlider from "./SectionSlider.jsx";
import VariantsGrid from "./VariantsGrid.jsx";
import clsx from "clsx";
import styles from "../styles/CostumeEditor.module.css";

const PALETTE = [
    "#f26522", "#30302e", "#93c5fd", "#a7f3d0", "#fde68a", "#d8b4fe",
    "#ef4444", "#10b981", "#22c55e", "#0ea5e9", "#f59e0b", "#a855f7"
];

export default function SidebarEditor(props) {
    const {
        mode, setMode, modeGroup,
        // paint
        paintColor, setPaintColor, paletteOpen, setPaletteOpen, paletteRef,

        // lines
        lineStyle, setLineStyle, defaultSubCount, setDefaultSubCount,
        selectedCurveKey, setSelectedCurveKey, setHoverCurveKey,
        curvesByPanel, setCurvesByPanelExtern,
        recomputeWaveForCurve, waveAmpPx, setWaveAmpPx, waveLenPx, setWaveLenPx,

        // history
        historyItems, historyIndex, historyUndo, historyRedo, canUndo, canRedo,

        details, activeDetailId,

        setSlotVariant
    } = props;

    const { t } = useTranslation();

    // What slots are available on the current side
    const [visibleSlots, setVisibleSlots] = useState(new Set());
    useEffect(() => {
        let alive = true;
        (async () => {
            const arr = await getVisibleSlotsForFace(activeDetailId); // 'front' | 'back'
            if (!alive) return;
            setVisibleSlots(new Set(arr.map(s => String(s).toLowerCase())));
        })();
        return () => { alive = false; };
    }, [activeDetailId]);

    const [historyOpen, setHistoryOpen] = useState(false);

    useEffect(() => {
        try { setHistoryOpen(localStorage.getItem("ce.history.open") === "1"); } catch { }
    }, []);

    const toggleHistory = () => {
        setHistoryOpen(v => {
            const nv = !v;
            try { localStorage.setItem("ce.history.open", nv ? "1" : "0"); } catch { }
            return nv;
        });
    };

    // Which preset is currently active
    const hasSelection = !!selectedCurveKey;
    let curve = null, pid = null, cid = null;
    if (hasSelection) {
        [pid, cid] = selectedCurveKey.split(":");
        curve = (curvesByPanel[pid] || []).find(c => c.id === cid) || null;
    }
    const currentSub = hasSelection
        ? Math.max(2, Math.min(10, curve?.subCount ?? 2))
        : Math.max(2, Math.min(10, defaultSubCount));

    const manualCount = hasSelection && Array.isArray(curve?.extraStops) ? curve.extraStops.length : 0;
    const manualLock = hasSelection && manualCount > 0;

    const changeSub = (n) => {
        if (hasSelection) {
            if (manualLock) return;
            setCurvesByPanelExtern(prev => {
                const arr = [...(prev[pid] || [])];
                const i = arr.findIndex(x => x.id === cid);
                if (i >= 0) arr[i] = { ...arr[i], subCount: n };
                return { ...prev, [pid]: arr };
            }, `${t('Peaks')}: ${n}`);
        } else {
            setDefaultSubCount(n);
        }
    };

    const curveIsWavyCapable = !!(curve && curve.type === "wavy" && curve.basePts);
    const currentAmp = hasSelection ? (curve?.waveAmpPx ?? waveAmpPx) : waveAmpPx;
    const currentLen = hasSelection ? (curve?.waveLenPx ?? waveLenPx) : waveLenPx;
    const changeAmp = (val) => {
        if (hasSelection && curveIsWavyCapable)
            recomputeWaveForCurve(pid, cid, val, currentLen, `${t('Amplitude')}: ${val}px`);
        else
            setWaveAmpPx(val);
    };
    const changeLen = (val) => {
        if (hasSelection && curveIsWavyCapable)
            recomputeWaveForCurve(pid, cid, currentAmp, val, `${t('Wavelength')}: ${val}px`);
        else
            setWaveLenPx(val);
    };

    return (
        <aside className={styles.sidebar}>
            <div className={styles.panel}>
                <h3 className={styles.panelTitle}>{t('Editor')}</h3>

                {/* Palette */}
                {modeGroup === "fill" && (
                    <div className={styles.section}>
                        <div className={styles.sectionTitle}>{t('FillColor')}</div>

                        {/* Submodes */}
                        <div className={styles.segmented} style={{ gap: 8, marginBottom: 8 }}>
                            <button
                                className={clsx(styles.segBtn, mode === "paint" && styles.segActive)}
                                onClick={() => setMode("paint")}
                            >🪣 {t('Fill')}</button>

                            <button
                                className={clsx(styles.segBtn, mode === "deleteFill" && styles.segActive)}
                                onClick={() => setMode("deleteFill")}
                            >✖ {t('Clear')}</button>
                        </div>

                        {mode === "paint" ? (
                            <>
                                {/* Current color + popover */}
                                <div className={styles.colorRow}>
                                    <button
                                        className={styles.colorChip}
                                        style={{ background: paintColor }}
                                        onClick={() => setPaletteOpen(v => !v)}
                                        aria-label={t('OpenPalette')}
                                    />
                                    {paletteOpen && (
                                        <div className={styles.palettePopover}>
                                            <div ref={paletteRef} className={styles.palette}>
                                                <div className={styles.paletteGrid}>
                                                    {PALETTE.map(c => (
                                                        <button
                                                            key={c}
                                                            className={styles.swatchBtn}
                                                            style={{ background: c }}
                                                            onClick={() => { setPaintColor(c); setPaletteOpen(false); }}
                                                            aria-label={c}
                                                        />
                                                    ))}
                                                </div>
                                                <div className={styles.paletteFooter}>
                                                    <span className={styles.paletteLabel}>{t('ArbitraryColor')}</span>
                                                    <input
                                                        type="color"
                                                        className={styles.colorInline}
                                                        value={paintColor}
                                                        onChange={(e) => setPaintColor(e.target.value)}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    )}
                                </div>

                                {/* Quick colors */}
                                <div className={styles.swatches} style={{ marginTop: 8 }}>
                                    {PALETTE.map(c => (
                                        <button
                                            key={c}
                                            className={styles.swatch}
                                            style={{ background: c }}
                                            title={c}
                                            onClick={() => setPaintColor(c)}
                                        />
                                    ))}
                                </div>
                            </>
                        ) : (
                            <div className={styles.hintSmall} style={{ marginTop: 8 }}>
                                {t('ClearModePressX')} <span className={styles.kbd}>X</span>.
                            </div>
                        )}
                    </div>
                )}

                {/* Lines */}
                {modeGroup === "line" && (
                    <div className={styles.section}>
                        <div className={styles.sectionTitle}>{t('Line')}</div>

                        <div className={styles.segmented}>
                            <button className={clsx(styles.segBtn, lineStyle === "straight" && styles.segActive)}
                                onClick={() => { setLineStyle("straight"); setSelectedCurveKey(null); setHoverCurveKey(null); }}>{t('Straight')}</button>
                            <button className={clsx(styles.segBtn, lineStyle === "wavy" && styles.segActive)}
                                onClick={() => { setLineStyle("wavy"); setSelectedCurveKey(null); setHoverCurveKey(null); }}>{t('Wavy')}</button>
                        </div>

                        <div className={clsx(styles.segmented, styles.two)} style={{ marginBottom: 8 }}>
                            <button className={clsx(styles.segBtn, mode === "add" && styles.segActive)} onClick={() => { setMode("add"); setSelectedCurveKey(null); setHoverCurveKey(null); }}>＋ {t('Add')}</button>
                            <button className={clsx(styles.segBtn, mode === "delete" && styles.segActive)} onClick={() => { setMode("delete"); setSelectedCurveKey(null); setHoverCurveKey(null); }}>🗑 {t('Delete')}</button>
                            <button className={clsx(styles.segBtn, mode === "insert" && styles.segActive)} onClick={() => { setMode("insert"); setSelectedCurveKey(null); setHoverCurveKey(null); }}>● {t('InsertVertex')}</button>
                            <button className={clsx(styles.segBtn, mode === "deleteVertex" && styles.segActive)} onClick={() => { setMode("deleteVertex"); setSelectedCurveKey(null); setHoverCurveKey(null); }}>○ {t('RemoveVertex')}</button>
                        </div>

                        {/* Settings */}
                        <SectionSlider label={hasSelection ? t('VerticesOnTheLine') : t('PeaksForNew')}
                            value={currentSub} min={2} max={10} step={1}
                            onChange={changeSub} disabled={manualLock} />

                        {manualLock && (
                            <div className={styles.lockNote}>
                                {t('ThereAreHandPeaksOnTheLine')} ({manualCount}). {t('AutomaticDistributionIsDisabled')}.
                                <div style={{ marginTop: 6 }}>
                                    <button type="button" className={styles.linkBtn}
                                        onClick={() => {
                                            if (!hasSelection) return;
                                            const [pp, cc] = selectedCurveKey.split(":");
                                            setCurvesByPanelExtern(prev => {
                                                const list = [...(prev[pp] || [])];
                                                const i = list.findIndex(x => x.id === cc);
                                                if (i < 0) return prev;
                                                list[i] = { ...list[i], extraStops: [] };
                                                return { ...prev, [pp]: list };
                                            });
                                        }}>
                                        {t('DeleteAllManualVertices')}
                                    </button>
                                </div>
                            </div>
                        )}

                        {lineStyle === "wavy" && (
                            <>
                                <SectionSlider
                                    label={hasSelection ? (curveIsWavyCapable ? t('AmplitudeOnTheLine') : t('AmplitudeTemplate')) : t('AmplitudeForNew')}
                                    value={currentAmp} min={2} max={24} step={1}
                                    onChange={changeAmp} disabled={manualLock} suffix="px"
                                />

                                <SectionSlider
                                    label={hasSelection ? (curveIsWavyCapable ? t('WavelengthOnTheLine') : t('WavelengthTemplate')) : t('WavelengthForNew')}
                                    value={currentLen} min={12} max={80} step={2}
                                    onChange={changeLen} disabled={manualLock} suffix="px"
                                />

                            </>
                        )}
                    </div>
                )}

                {mode === "variants" && (
                    <>
                        {/* --- HOODIE --- */}
                        {[
                            { slot: "hoodie.cuff", title: t('Cuff') },
                            { slot: "hoodie.sleeve", title: t('Sleeve') },
                            { slot: "hoodie.neck", title: t('Neck') },
                            { slot: "hoodie.belt", title: t('Belt') },
                            { slot: "hoodie.body", title: t('Body') },
                            { slot: "hoodie.hood", title: t('Hood') },
                            { slot: "hoodie.pocket", title: t('Pocket') }
                        ]
                            .filter(sec => visibleSlots.has(String(sec.slot).toLowerCase()))
                            .map(sec => {
                                return (
                                    <div className={styles.section} key={sec.slot}>
                                        <div className={styles.sectionTitle}>{sec.title}</div>
                                        <VariantsGrid
                                            slot={sec.slot}
                                            face={activeDetailId}
                                            value={details[activeDetailId]?.[sec.slot] || "base"}
                                            onChange={(id) => setSlotVariant(activeDetailId, sec.slot, id)}
                                        />
                                    </div>
                                );
                            })}

                        {/* --- PANTS --- */}
                        {[
                            { slot: "pants.leg", title: t('Leg') },
                            { slot: "pants.belt", title: t('Belt') },
                            { slot: "pants.cuff", title: t('Cuff') }
                        ]
                            .filter(sec => visibleSlots.has(String(sec.slot).toLowerCase()))
                            .map(sec => {
                                return (
                                    <div className={styles.section} key={`pants-${sec.slot}`}>
                                        <div className={styles.sectionTitle}>{sec.title}</div>
                                        <VariantsGrid
                                            slot={sec.slot}
                                            face={activeDetailId}
                                            value={details[activeDetailId]?.[sec.slot] || "base"}
                                            onChange={(id) => setSlotVariant(activeDetailId, sec.slot, id)}
                                        />
                                    </div>
                                );
                            })}
                    </>
                )}

                {/* History (not in the preview—the sidebar itself is hidden in the preview) */}
                <div className={styles.section}>
                    <div className={styles.historyHeader}>
                        <div className={styles.sectionTitle}>{t('History')}</div>

                        <div className={styles.historyToggles}>
                            <button
                                className={styles.historyToggleBtn}
                                aria-label={historyOpen ? t('CollapseHistory') : t('ExpandHistory')}
                                aria-expanded={historyOpen}
                                aria-controls="history-panel"
                                title={historyOpen ? t('Collapse') : t('Expand')}
                                onClick={toggleHistory}
                            >
                                {historyOpen ? "▾" : "▸"}
                            </button>

                            <button
                                className={styles.historyBtn}
                                onClick={historyUndo}
                                disabled={!canUndo}
                                aria-label={`${t('Cancel')} (Ctrl+Y / Ctrl+Shift+Z)`}
                                title={`${t('Cancel')} (Ctrl+Y / Ctrl+Shift+Z)`}
                            >↶</button>

                            <button
                                className={styles.historyBtn}
                                onClick={historyRedo}
                                disabled={!canRedo}
                                aria-label={`${t('Repeat')} (Ctrl+Y / Ctrl+Shift+Z)`}
                                title={`${t('Repeat')} (Ctrl+Y / Ctrl+Shift+Z)`}
                            >↷</button>
                        </div>
                    </div>

                    {/* Only two states: open -> show the entire feed, closed -> nothing */}
                    {historyOpen && (
                        <div id="history-panel" className={styles.historyViewport}>
                            <ol className={styles.historyList} aria-label={t("ActionHistory")}>
                                {historyItems.map((it, i) => (
                                    <li
                                        key={i}
                                        className={[
                                            styles.histItem,
                                            i === historyIndex ? ' ' + styles.now : '',
                                            i > historyIndex ? ' ' + styles.future : '',
                                        ].join('')}
                                        title={new Date(it.at).toLocaleTimeString()}
                                    >
                                        <span className={styles.histStep}>{i}</span>
                                        <span className={styles.histLabel}>{it.label}</span>
                                    </li>
                                ))}
                            </ol>
                        </div>
                    )}
                </div>


            </div>
        </aside>
    );
}