import { memo } from "react";
import { facePath, faceKey, segsToD } from "../../../core/svg/faceUtils.js";
import { useTranslation } from "react-i18next";
import clsx from "clsx";
import styles from "../styles/CostumeEditor.module.css";

export default memo(function PanelView({
    panel, mode, scale, facesByPanel, outerRingByPanel,
    activePanel, onPanelActivate, fills, onFilledEnter, onFaceEnter,
    onFilledLeave, onFaceLeave, onFilledClick, onFaceClick, onCurveLeave,
    mergedAnchorsOf, curvesByPanel, setInsertPreview, getCursorWorld, closestPointOnCurve,
    tooCloseToExistingAnchors, setInsertPreviewRAF, applyCurvesChange, insertPreview, extraAnchorsByPanel,
    setHoverAnchorIdx, eraseManualAnchor, hoverFace, hoverAnchorIdx, addBuffer,
    onAnchorClickAddMode, hoverCurveKey, selectedCurveKey, clickedCurveKey, onCurveEnter,
    setToast, onCurveClickDelete, onCurveClick
}) {

    const { t } = useTranslation();
    const faces = facesByPanel[panel.id] || [];
    const ring = outerRingByPanel[panel.id];
    const isActive = activePanel?.id === panel.id;
    const clickableFaces = faces.length ? faces : (ring ? [ring] : []);
    const dimInactive = mode !== "preview" && !isActive;

    return (
        <g key={panel.id} className={dimInactive ? styles.panelDimmed : undefined}>
            {/* Selecting a part (do not interfere with the filling) */}
            {ring && mode !== "preview" && mode !== "paint" && mode !== "deleteFill" && (
                <path
                    d={facePath(ring)}
                    fill="transparent"
                    style={{ cursor: "pointer" }}
                    onClick={() => onPanelActivate(panel.id)}
                />
            )}

            {/* edges for painting/cleaning */}
            {clickableFaces.map(poly => {
                const fk = faceKey(poly);
                const fill = (fills.find(f => f.panelId === panel.id && f.faceKey === fk)?.color) || "none";
                const hasFill = fill !== "none";
                const isHover = !!hoverFace && hoverFace.panelId === panel.id && hoverFace.faceKey === fk;
                const canHit = mode === "paint" || mode === "deleteFill";

                return (
                    <g key={fk}>
                        <path
                            d={facePath(poly)}
                            fill={hasFill ? fill : (mode === "paint" && isHover ? "#9ca3af" : "transparent")}
                            fillOpacity={hasFill ? 0.9 : (mode === "paint" && isHover ? 0.35 : 0.001)}
                            stroke="none"
                            style={{ pointerEvents: canHit ? 'all' : 'none', cursor: canHit ? 'crosshair' : 'default' }}
                            onMouseEnter={() => (hasFill ? onFilledEnter(panel.id, fk) : onFaceEnter(panel.id, poly))}
                            onMouseLeave={() => (hasFill ? onFilledLeave(panel.id, fk) : onFaceLeave(panel.id, poly))}
                            onClick={() => (hasFill ? onFilledClick(panel.id, fk) : onFaceClick(panel.id, poly))}
                        />
                        {hasFill && mode === "deleteFill" && isHover && (
                            <path d={facePath(poly)} fill="#000" fillOpacity={0.18} style={{ pointerEvents: "none" }} />
                        )}
                    </g>
                );
            })}

            {/* исходная геометрия панели (все линии, в том числе декоративные) */}
            {panel.segs && panel.segs.length > 0 && (
                <path
                    d={segsToD(panel.segs)}
                    fill="none"
                    stroke="#111"
                    strokeWidth={1 * (scale.k || 1)}
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    vectorEffect="non-scaling-stroke"
                />
            )}

            {/* outer contour */}
            {ring && (
                <path
                    d={facePath(ring)}
                    fill="none"
                    stroke="#111"
                    strokeWidth={1.8 * (scale.k || 1)}
                    vectorEffect="non-scaling-stroke"
                    style={{ pointerEvents: "none" }}
                />
            )}

            {/* пользовательские линии */}
            {(curvesByPanel[panel.id] || []).map(c => {
                const merged = mergedAnchorsOf(panel);
                const a = merged[c.aIdx] ?? (c.ax != null ? { x: c.ax, y: c.ay } : null);
                const b = merged[c.bIdx] ?? (c.bx != null ? { x: c.bx, y: c.by } : null);
                if (!a || !b || !Number.isFinite(a.x) || !Number.isFinite(a.y) || !Number.isFinite(b.x) || !Number.isFinite(b.y)) {
                    return null;
                }

                const dRaw = c.type === "cubic"
                    ? `M ${a.x} ${a.y} C ${c.c1.x} ${c.c1.y} ${c.c2.x} ${c.c2.y} ${b.x} ${b.y}`
                    : c.d;

                const d = (dRaw && !/NaN/.test(String(dRaw))) ? dRaw : null;
                if (!d) return null;

                const key = `${panel.id}:${c.id}`;
                const isHover = hoverCurveKey === key;
                const isSelected = selectedCurveKey === key;
                const isClicked = clickedCurveKey === key;

                const cls = clsx(
                    styles.userCurve,
                    mode === "preview" && styles.userCurvePreview,
                    mode === "delete" && isHover && styles.userCurveDeleteHover,
                    isSelected && styles.userCurveSelected,
                    isClicked && styles.userCurveClicked
                );

                return (
                    <path
                        key={c.id}
                        d={d}
                        className={cls}
                        onClickCapture={(e) => {
                            if (mode === "delete") {
                                e.stopPropagation();
                                onCurveClickDelete(panel.id, c.id);
                            }
                        }}
                        onMouseEnter={() => { if (isActive) onCurveEnter(panel.id, c.id); }}
                        onMouseLeave={() => {
                            if (mode === "insert") setInsertPreview(prev => (prev && prev.curveId === c.id ? null : prev));
                            onCurveLeave(panel.id, c.id);
                        }}
                        onMouseMove={(e) => {
                            if (mode !== 'insert' || !isActive) return;
                            const P = getCursorWorld(e);
                            if (!P) return;
                            const merged = mergedAnchorsOf(panel);
                            const a = merged[c.aIdx] ?? (c.ax != null ? { x: c.ax, y: c.ay } : null);
                            const b = merged[c.bIdx] ?? (c.bx != null ? { x: c.bx, y: c.by } : null);
                            if (!a || !b) return;

                            const near = closestPointOnCurve(panel, c, P);
                            if (!near) return;

                            const allowed = !tooCloseToExistingAnchors(panel, c, { x: near.x, y: near.y });
                            setInsertPreviewRAF({ panelId: panel.id, curveId: c.id, x: near.x, y: near.y, allowed, t: near.t });
                        }}
                        onClick={(e) => {
                            if (!isActive) return;
                            if (mode === "insert") {
                                e.stopPropagation();
                                const P = getCursorWorld(e);
                                if (!P) return;
                                const near = closestPointOnCurve(panel, c, P);
                                if (!near) return;
                                if (tooCloseToExistingAnchors(panel, c, { x: near.x, y: near.y })) {
                                    setToast({ text: t("TooCloseToTheExistingPeak") });
                                    return;
                                }
                                applyCurvesChange(prev => {
                                    const list = [...(prev[panel.id] || [])];
                                    const i = list.findIndex(x => x.id === c.id);
                                    if (i < 0) return prev;
                                    const cur = list[i];
                                    const stops = Array.isArray(cur.extraStops) ? cur.extraStops.slice() : [];
                                    const t = Math.max(0, Math.min(1, near.t));
                                    stops.push({ t });
                                    const EPS = 1e-3;
                                    const cleaned = stops
                                        .sort((a, b) => a.t - b.t)
                                        .filter((s, idx, arr) => idx === 0 || Math.abs(s.t - arr[idx - 1].t) > EPS);
                                    list[i] = { ...cur, extraStops: cleaned };
                                    return { ...prev, [panel.id]: list };
                                }, t("InsertVertex"));
                                setInsertPreview(null);
                                return;
                            }
                            // все остальные режимы — общий клик по кривой
                            onCurveClick?.(panel.id, c.id, e);
                        }}
                        style={{ cursor: (mode === 'preview' || !isActive) ? 'default' : (mode === 'insert' ? 'copy' : 'pointer') }}
                        pointerEvents={(mode === "preview" || !isActive || mode === "deleteVertex") ? "none" : "auto"}
                        strokeLinecap="round"
                    />
                );
            })}

            {/* insertion point preview */}
            {isActive && mode === "insert" && insertPreview && insertPreview.panelId === panel.id && (
                Number.isFinite(insertPreview.x) && Number.isFinite(insertPreview.y) ? (
                    <circle
                        cx={insertPreview.x}
                        cy={insertPreview.y}
                        r={4}
                        fill={insertPreview.allowed ? "#22c55e" : "#ef4444"}
                        stroke={insertPreview.allowed ? "#166534" : "#991b1b"}
                        strokeWidth={1.5}
                        style={{ pointerEvents: "none" }}
                    />
                ) : null
            )}

            {/* basic + additional anchors */}
            {isActive && (mode === "add" || mode === "delete" || mode === "insert") && (() => {
                const base = panel.anchors || [];
                const extras = extraAnchorsByPanel[panel.id] || [];
                const merged = [...base, ...extras];
                return merged.map((pt, mi) => {
                    if (!pt || !Number.isFinite(pt.x) || !Number.isFinite(pt.y)) return null;
                    return (
                        <circle
                            key={`m-${mi}`}
                            cx={pt.x}
                            cy={pt.y}
                            r={3.5}
                            className={clsx(
                                styles.anchor,
                                styles.anchorClickable,
                                mi === hoverAnchorIdx && styles.anchorHovered,
                                mi === addBuffer && styles.anchorSelectedA
                            )}
                            onClick={(e) => { e.stopPropagation(); onAnchorClickAddMode(mi); }}
                            onMouseEnter={() => setHoverAnchorIdx(mi)}
                            onMouseLeave={() => setHoverAnchorIdx(null)}
                        />
                    );
                });
            })()}

            {/* manual vertices - for removal */}
            {isActive && mode === "deleteVertex" && (() => {
                const extras = (extraAnchorsByPanel[panel.id] || []).filter(ex => ex?.id?.includes("@m"));
                return extras.map(ex => {
                    if (!ex || !Number.isFinite(ex.x) || !Number.isFinite(ex.y)) return null;
                    return (
                        <circle
                            key={ex.id}
                            cx={ex.x}
                            cy={ex.y}
                            r={4}
                            className={styles.anchorManualDelete}
                            onClick={(e) => { e.stopPropagation(); eraseManualAnchor(panel.id, ex); }}
                        />
                    );
                });
            })()}
        </g>
    );
});