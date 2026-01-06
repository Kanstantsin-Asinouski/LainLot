import { Trans, useTranslation } from "react-i18next";
import Tooltip from "./Tooltip.jsx";
import styles from "../styles/CostumeEditor.module.css";
import clsx from "clsx";

export default function Topbar({
    mode, setMode, lastLineMode, setShowTopbarHint, showTopbarHint,
    dismissTopbarHint, presetIdx, setPresetIdx, resetAll, doExportSVG,
    isExporting, IS_DEV, devOpen, handleDevClick
}) {

    const { t } = useTranslation();
    const K = ({ children }) => <span className={styles.kbd}>{children}</span>;
    const KM = ({ children }) => <span className={styles.kbd} style={{ marginLeft: 6 }}>{children}</span>;

    const modeGroup =
        (mode === 'paint' || mode === 'deleteFill') ? 'fill' :
            (mode === 'add' || mode === 'delete' || mode === 'insert' || mode === 'deleteVertex') ? 'line' :
                (mode === 'variants' ? 'variants' : 'preview');

    return (
        <div className={styles.topbar}>
            {/* Modes (icons) */}
            <div className={styles.tbLeft} role="toolbar" aria-label={t('Modes')}>
                <Tooltip label={`${t('View')} (Esc)`}>
                    <button
                        className={clsx(styles.iconBtn, mode === "preview" && styles.iconActive)}
                        aria-label={t('View')}
                        aria-keyshortcuts="Esc"
                        aria-pressed={mode === "preview"}
                        onClick={() => { dismissTopbarHint(); setMode("preview"); }}
                    >👁️</button>
                </Tooltip>

                <Tooltip label={`${t('Filling')} (F)`}>
                    <button
                        className={clsx(styles.iconBtn, (mode === "paint" || mode === "deleteFill") && styles.iconActive)}
                        aria-label={t('Filling')}
                        aria-keyshortcuts="F"
                        aria-pressed={mode === "paint" || mode === "deleteFill"}
                        onClick={() => { dismissTopbarHint(); setMode("paint"); }}
                    >🪣</button>
                </Tooltip>

                <Tooltip label={`${t('Lines')} (A)`}>
                    <button
                        className={clsx(styles.iconBtn, modeGroup === "line" && styles.iconActive)}
                        aria-label={t('Lines')}
                        aria-keyshortcuts="A"
                        aria-pressed={modeGroup === "line"}
                        onClick={() => { dismissTopbarHint(); setMode(lastLineMode || "add"); }}
                    >✏️</button>
                </Tooltip>

                <Tooltip label={`${t('Variants')} (V)`}>
                    <button
                        className={clsx(styles.iconBtn, mode === "variants" && styles.iconActive)}
                        aria-label={t('ClothingDetailOptions')}
                        aria-keyshortcuts="V"
                        aria-pressed={mode === "variants"}
                        onClick={() => { dismissTopbarHint(); setMode("variants"); }}
                    >🧩</button>
                </Tooltip>

                {IS_DEV && (
                    <Tooltip label={devOpen ? 'Hide test mode (T)' : 'Show test mode (T)'}>
                        <button
                            className={clsx(styles.iconBtn, devOpen && styles.iconActive)}
                            aria-label="Test mode"
                            aria-keyshortcuts="T"
                            aria-pressed={devOpen}
                            onClick={() => handleDevClick()}
                            title="Test mode"
                        >🧪</button>
                    </Tooltip>
                )}

                <Tooltip label={`${t('ShowClue')} (H)`}>
                    <button
                        className={styles.iconBtn}
                        aria-label={t('Clue')}
                        aria-keyshortcuts="H"
                        onClick={() => { try { localStorage.removeItem('ce.topbarHint.v1'); } catch { }; setShowTopbarHint(true); }}
                    >?</button>
                </Tooltip>
            </div>

            {showTopbarHint && (
                <div className={styles.topbarHint} role="dialog" aria-label={t('ModeHint')}>
                    <div className={styles.hintClose} onClick={dismissTopbarHint} aria-label={t('Close')}>×</div>
                    <div className={styles.hintTitle}>{t('QuickStart')}</div>
                    <div className={styles.hintRow}>
                        <Trans
                            i18nKey="hint.inline"
                            components={{ K: <K />, KM: <KM /> }}
                            values={{ left: '←', right: '→' }}
                        />
                    </div>
                    <div className={styles.hintRow} style={{ marginTop: 6 }}>
                        {t('HideHelpWindow')}
                    </div>
                </div>
            )}

            {/* Tabs container */}
            <div className={clsx(styles.topbarGroup, styles.tbCenter)} role="tablist" aria-label="Деталь">
                <button
                    role="tab"
                    id="tab-front"
                    aria-selected={presetIdx === 0}
                    aria-controls="panel-front"
                    className={clsx(styles.segBtn, presetIdx === 0 && styles.segActive)}
                    onClick={() => setPresetIdx(0)}
                    aria-keyshortcuts="Q"
                    title={`${t('Front')} (Q)`}
                >{t('Front')}</button>

                <button
                    role="tab"
                    id="tab-back"
                    aria-selected={presetIdx === 1}
                    aria-controls="panel-back"
                    className={clsx(styles.segBtn, presetIdx === 1 && styles.segActive)}
                    onClick={() => setPresetIdx(1)}
                    aria-keyshortcuts="E"
                    title={`${t('Back')} (E)`}
                >{t('Back')}</button>
            </div>

            {/* Reset — one button */}
            <div className={styles.tbRight}>
                <button
                    className={styles.resetBtn}
                    onClick={resetAll}
                    aria-label={t('ResetAll')}
                    title={t('ResetAll')}
                >
                    ⚠️ {t('ResetAll')}
                </button>

                <button
                    className={styles.exportBtn}
                    onClick={doExportSVG}
                    disabled={isExporting}
                    aria-label={t('ExportSVG')}
                    title={t('ExportSVG')}
                >
                    {isExporting ? t('Export') : t('ExportSVG')}
                </button>
            </div>

        </div>
    );
}