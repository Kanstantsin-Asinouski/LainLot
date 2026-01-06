import { facePath, segsToD } from "../../../core/svg/faceUtils.js";
import PanelView from "./PanelView.jsx";
import { useTranslation } from "react-i18next";
import styles from "../styles/CostumeEditor.module.css";

export default function CanvasStage({
    mode, scale, facesByPanel, outerRingByPanel,
    activePanel, onPanelActivate, fills, onFilledEnter, onFaceEnter,
    onFilledLeave, onFaceLeave, onFilledClick, onFaceClick, onCurveLeave,
    mergedAnchorsOf, curvesByPanel, setInsertPreview, getCursorWorld, closestPointOnCurve,
    tooCloseToExistingAnchors, setInsertPreviewRAF, applyCurvesChange, insertPreview, extraAnchorsByPanel,
    setHoverAnchorIdx, eraseManualAnchor, hoverFace, hoverAnchorIdx, addBuffer,
    onAnchorClickAddMode, hoverCurveKey, selectedCurveKey, clickedCurveKey, onCurveEnter,
    setToast, onCurveClickDelete, onCurveClick, svgRef, viewBox,
    gridDef, svgMountKey, panels, prevPanels, isSwapping,
    hoodPanelIds, hoodRings, hoodHoles, onCanvasClick, didEverSwapRef,
    presetIdx, PRESETS
}) {

    const { t } = useTranslation();

    // Universal: we take the offset if it came from the manifest
    const translateOf = (panel) => {
        const off = panel?.meta?.offset || panel?.offset || null;
        if (!off) return null;
        const x = +off.x || 0, y = +off.y || 0;
        return (x || y) ? `translate(${x}, ${y})` : null;
    };

    const scaleOf = (panel) => {
        const s = panel?.meta?.scale || panel?.scale || null;
        if (!s) return null;
        const sx = +s.x || 1, sy = +s.y || 1;
        return (sx !== 1 || sy !== 1) ? `scale(${sx}, ${sy})` : null;
    };

    const transformOf = (panel) => {
        const t = [translateOf(panel), scaleOf(panel)].filter(Boolean).join(" ");
        return t || undefined;
    };

    // Safe build d: don't render if segments are broken
    const toPathD = (segs) => {
        try {
            const d = segsToD(segs);
            return (d && !/NaN/.test(d)) ? d : null;
        } catch {
            return null;
        }
    };

    // применять маску только если есть кольца капюшона
    const useHoodMask = Array.isArray(hoodRings) && hoodRings.length > 0;

    // панели-капюшоны и панели-шеи (нужны их трансформации)
    const hoodPanels = panels.filter(p => hoodPanelIds.has(p.id));
    const neckPanels = panels.filter(p => String(p.slot).toLowerCase().startsWith("neck"));


    // кольца шеи: берём внешнее кольцо для всех панелей со слотом "neck"
    const neckPanelIds = new Set(
        panels
            .filter(p => String(p.slot).toLowerCase().startsWith("neck"))
            .map(p => p.id)
    );

    const neckRings = panels
        .filter(p => neckPanelIds.has(p.id))
        .map(p => outerRingByPanel[p.id])   // outerRingByPanel уже есть в пропсах
        .filter(Boolean);

    return (
        <div className={styles.canvasStack}>
            {/* bottom layer - pre-scene, outlines only */}
            {prevPanels && (
                <svg
                    className={`${styles.canvas} ${styles.stage} ${styles.swapOut}`}
                    viewBox={viewBox}
                    preserveAspectRatio="xMidYMin meet"
                    style={{ pointerEvents: "none" }}
                    aria-hidden="true"
                >
                    <g>
                        {prevPanels.map((p, i) => {
                            const d = (Array.isArray(p?.segs) && p.segs.length) ? toPathD(p.segs) : null;
                            if (!d) return null;
                            return (
                                <path
                                    key={`prev-${i}-${p.id}`}
                                    d={d}
                                    fill="none"
                                    stroke="#c9ced6"
                                    strokeWidth={1.2}
                                    vectorEffect="non-scaling-stroke"
                                />
                            );
                        })}
                    </g>
                </svg>
            )}

            {/* the top layer is the current interactive scene */}
            <svg
                key={svgMountKey}
                ref={svgRef}
                className={`${styles.canvas} ${styles.stage} ${isSwapping ? styles.swapIn : (!didEverSwapRef.current ? styles.svgEnter : "")}`}
                viewBox={viewBox}
                preserveAspectRatio="xMidYMin meet"
                onClick={onCanvasClick}
                role="tabpanel"
                id={presetIdx === 0 ? "panel-front" : "panel-back"}
                aria-labelledby={presetIdx === 0 ? "tab-front" : "tab-back"}
            >
                <title>{t("Detail")}: {t(PRESETS[presetIdx]?.title) || "—"}</title>
                {/* GRID */}
                <defs>
                    <pattern id={`grid-${svgMountKey}`} width={gridDef.step} height={gridDef.step} patternUnits="userSpaceOnUse">
                        <path
                            d={`M ${gridDef.step} 0 L 0 0 0 ${gridDef.step}`}
                            fill="none"
                            stroke="#000"
                            strokeOpacity=".06"
                            strokeWidth={0.6 * (scale.k || 1)}
                            vectorEffect="non-scaling-stroke"
                            shapeRendering="crispEdges"
                        />
                    </pattern>

                    {/* A mask that hides everything under the hood */}
                    {useHoodMask && (
                        <mask id={`under-hood-mask-${svgMountKey}`}
                            maskUnits="userSpaceOnUse"
                            maskContentUnits="userSpaceOnUse">
                            <rect x={gridDef.b.x} y={gridDef.b.y} width={gridDef.b.w} height={gridDef.b.h} fill="#fff" />

                            {/* чёрным скрываем область капюшона (учитываем transform панели!) */}
                            {hoodPanels.map((p, i) => {
                                const ring = outerRingByPanel[p.id];
                                return ring ? (
                                    <g key={`hood-ring-${i}`} transform={transformOf(p) || undefined}>
                                        <path d={facePath(ring)} fill="#000" />
                                    </g>
                                ) : null;
                            })}

                            {/* белым возвращаем видимость шеи (тоже c transform панели) */}
                            {neckPanels.map((p, i) => {
                                const ring = outerRingByPanel[p.id];
                                return ring ? (
                                    <g key={`neck-ring-${i}`} transform={transformOf(p) || undefined}>
                                        <path d={facePath(ring)} fill="#fff" />
                                    </g>
                                ) : null;
                            })}
                        </mask>
                    )}

                </defs>
                <rect
                    x={gridDef.b.x}
                    y={gridDef.b.y}
                    width={gridDef.b.w}
                    height={gridDef.b.h}
                    fill={`url(#grid-${svgMountKey})`} pointerEvents="none"
                />

                {/* 1) All parts EXCEPT the hood are under the mask */}
                <g {...(useHoodMask ? { mask: `url(#under-hood-mask-${svgMountKey})` } : {})}>
                    {panels.filter(p => !hoodPanelIds.has(p.id)).map((p, i) => (
                        <g key={`wrap-${i}-${p.id}`} transform={transformOf(p) || undefined}>
                            <PanelView
                                key={`${p.id}-${i}`}
                                panel={p}
                                mode={mode}
                                scale={scale}
                                facesByPanel={facesByPanel}
                                outerRingByPanel={outerRingByPanel}
                                activePanel={activePanel}
                                onPanelActivate={onPanelActivate}
                                fills={fills}
                                onFilledEnter={onFilledEnter}
                                onFaceEnter={onFaceEnter}
                                onFilledLeave={onFilledLeave}
                                onFaceLeave={onFaceLeave}
                                onFilledClick={onFilledClick}
                                onFaceClick={onFaceClick}
                                onCurveLeave={onCurveLeave}
                                mergedAnchorsOf={mergedAnchorsOf}
                                curvesByPanel={curvesByPanel}
                                setInsertPreview={setInsertPreview}
                                getCursorWorld={getCursorWorld}
                                closestPointOnCurve={closestPointOnCurve}
                                tooCloseToExistingAnchors={tooCloseToExistingAnchors}
                                setInsertPreviewRAF={setInsertPreviewRAF}
                                applyCurvesChange={applyCurvesChange}
                                insertPreview={insertPreview}
                                extraAnchorsByPanel={extraAnchorsByPanel}
                                setHoverAnchorIdx={setHoverAnchorIdx}
                                eraseManualAnchor={eraseManualAnchor}
                                hoverFace={hoverFace}
                                hoverAnchorIdx={hoverAnchorIdx}
                                addBuffer={addBuffer}
                                onAnchorClickAddMode={onAnchorClickAddMode}
                                hoverCurveKey={hoverCurveKey}
                                selectedCurveKey={selectedCurveKey}
                                clickedCurveKey={clickedCurveKey}
                                onCurveEnter={onCurveEnter}
                                setToast={setToast}
                                onCurveClickDelete={onCurveClickDelete}
                                onCurveClick={onCurveClick}
                            />
                        </g>
                    ))}
                </g>

                {/* 2) Hood - on top, without the "white erasers" */}
                {panels.filter(p => hoodPanelIds.has(p.id)).map((p, i) => (
                    <g key={`wrap-hood-${i}-${p.id}`} transform={transformOf(p) || undefined}>
                        <PanelView
                            key={`${p.id}-${i}`}
                            panel={p}
                            mode={mode}
                            scale={scale}
                            facesByPanel={facesByPanel}
                            outerRingByPanel={outerRingByPanel}
                            activePanel={activePanel}
                            onPanelActivate={onPanelActivate}
                            fills={fills}
                            onFilledEnter={onFilledEnter}
                            onFaceEnter={onFaceEnter}
                            onFilledLeave={onFilledLeave}
                            onFaceLeave={onFaceLeave}
                            onFilledClick={onFilledClick}
                            onFaceClick={onFaceClick}
                            onCurveLeave={onCurveLeave}
                            mergedAnchorsOf={mergedAnchorsOf}
                            curvesByPanel={curvesByPanel}
                            setInsertPreview={setInsertPreview}
                            getCursorWorld={getCursorWorld}
                            closestPointOnCurve={closestPointOnCurve}
                            tooCloseToExistingAnchors={tooCloseToExistingAnchors}
                            setInsertPreviewRAF={setInsertPreviewRAF}
                            applyCurvesChange={applyCurvesChange}
                            insertPreview={insertPreview}
                            extraAnchorsByPanel={extraAnchorsByPanel}
                            setHoverAnchorIdx={setHoverAnchorIdx}
                            eraseManualAnchor={eraseManualAnchor}
                            hoverFace={hoverFace}
                            hoverAnchorIdx={hoverAnchorIdx}
                            addBuffer={addBuffer}
                            onAnchorClickAddMode={onAnchorClickAddMode}
                            hoverCurveKey={hoverCurveKey}
                            selectedCurveKey={selectedCurveKey}
                            clickedCurveKey={clickedCurveKey}
                            onCurveEnter={onCurveEnter}
                            setToast={setToast}
                            onCurveClickDelete={onCurveClickDelete}
                            onCurveClick={onCurveClick}
                        />
                    </g>
                ))}

            </svg>
        </div>
    );
}