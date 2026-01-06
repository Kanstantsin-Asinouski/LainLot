import { useTranslation } from "react-i18next";

export default function ZoomControls({ onIn, onOut, onReset, zoom = 1, onSet }) {

    const { t } = useTranslation();

    const btn = {
        width: 42, height: 42, borderRadius: 9999,
        background: "#fff", border: "1px solid #e5e7eb",
        boxShadow: "0 4px 12px rgba(0,0,0,.08)", fontSize: 22, lineHeight: "42px"
    };

    return (
        <div
            style={{
                margin: "0 auto",
                display: "flex", gap: 12, alignItems: "center", justifyContent: "center",
                background: "rgba(255,255,255,.7)",
                backdropFilter: "blur(4px)",
                padding: 8, borderRadius: 9999, border: "1px solid #e5e7eb"
            }}
            aria-label={t("ScaleControl")}
        >
            <button
                type="button"
                style={btn}
                onClick={onOut}
                title={t("Decrease")}
                aria-label={t("Decrease")}>
                −</button>
            <button
                type="button"
                style={btn}
                onClick={onIn}
                title={t("Increase")}
                aria-label={t("Increase")}
                disabled={(zoom ?? 1) >= 1}
            >+</button>
            <input
                type="range"
                min={50}
                max={100}
                step={1}
                value={Math.round((zoom ?? 1) * 100)}
                onChange={(e) => onSet?.(Number(e.target.value) / 100)}
                style={{ width: 160 }}
                title={t("Scale")}
                aria-label={t("Scale")}
            />
            <input
                type="number"
                min={50}
                max={100}
                step={1}
                value={Math.round((zoom ?? 1) * 100)}
                onChange={(e) => {
                    const v = Math.max(50, Math.min(100, Number(e.target.value) || 100));
                    onSet?.(v / 100);
                }}
                style={{ width: 64, height: 42, borderRadius: 10, border: "1px solid #e5e7eb", textAlign: "center" }}
            />
            <button type="button" style={{ ...btn, width: 64, fontSize: 14 }} onClick={onReset} title={t("ResetScale")}>{t("Reset")}</button>
        </div>
    );
}