import React from "react";
import { useTranslation } from "react-i18next";
import { getVariantsForSlot, getBasePreview, baseHasSlot, isForcedSlot } from "../../../core/variables/variants.js";
import { EMPTY_PREVIEW, SVG_BASE } from "../../../core/variables/svgPath.js"
import styles from "../styles/CostumeEditor.module.css";

export default function VariantsGrid({ slot, face, value, onChange }) {

    const { t } = useTranslation();

    const [items, setItems] = React.useState([]);
    const abs = (p) => (p ? (p.startsWith("/") ? p : `/${p}`) : null);
    // product-aware fallbacks
    // product + face aware fallbacks (всегда *_preview.svg)
    const fallbackPreview = (product, pure, face) =>
        `${product}/${face}/${pure}_preview.svg`;
    const parseSlot = (s) => {
        const parts = String(s || "").split(".");
        return parts.length >= 2 ? { product: parts[0], pure: parts.slice(1).join(".") } : { product: null, pure: s };
    };

    React.useEffect(() => {
        let alive = true;
        (async () => {

            const { product, pure } = parseSlot(slot);
            // the product default was calculated once
            const prod = product || "hoodie";
            const raw = await getVariantsForSlot(slot);
            const withPreviews = await Promise.all(raw.map(async (v) => {
                let preview = v?.preview || null;
                let name = v?.name;
                if (v?.id === "base") {
                    // First, the "correct" preview from the manifest for the current side:
                    preview = (await getBasePreview(slot, face)) || preview;
                }
                // If still not, standard fallback.
                if (!preview) {
                    if (v?.id === "base") {
                        // supports namespace
                        const hasBase = await baseHasSlot(face, slot);
                        // pure inside is taken from it
                        const forced = isForcedSlot(face, slot);
                        if (hasBase) {
                            // We already have prod and pure - we use them
                            preview = `${SVG_BASE}/${fallbackPreview(prod, pure, face)}`;
                        }
                        else if (forced) {
                            // There is no base slot, but the slot should be displayed → “Missing”
                            preview = EMPTY_PREVIEW;
                            name = t('Empty');
                        }
                        else {
                            preview = null;
                        }
                    }
                    else {
                        const map = (v?.files && v.files[face]) || {};
                        const cand = map.right || map.file || map.left || Object.values(map)[0] || null;
                        preview = cand || null;
                    }
                }
                return { ...v, name: name || v?.name, preview: abs(preview) };
            }));

            if (alive) setItems(withPreviews);

        })();
        return () => { alive = false; };
    }, [face, slot]);

    // If synchronization sets the value on the other side, 
    // and there are no files on the current side for this variant, it will not be included in the items.
    const unavailableSelected =
        !!value && value !== "base" && !new Set(items.map(it => it.id)).has(value);

    return (
        <div className={styles.pickerGrid}>
            {unavailableSelected && (
                <div
                    className={styles.noteUnavailable}
                    title={t('MissedVariantOnThatSide')}
                    style={{
                        gridColumn: "1 / -1",
                        fontSize: "12px",
                        lineHeight: 1.3,
                        padding: "6px 8px",
                        border: "1px dashed #999",
                        borderRadius: "8px",
                        opacity: 0.8,
                        marginBottom: "6px"
                    }}
                >
                    {t('NotAvailableOnThisSide')}
                </div>
            )}
            {items.map(it => (
                <button
                    key={it.id}
                    type="button"
                    className={`${styles.pickerBtn} ${value === it.id ? styles.pickerBtnActive : ""}`}
                    onClick={() => onChange(it.id)}
                    title={it.name || it.id}
                >
                    {it.preview ? (
                        <img
                            className={styles.pickerImg}
                            alt={it.name || it.id}
                            src={it.preview}
                            onError={(e) => { e.currentTarget.style.opacity = 0; }}
                        />
                    ) : (
                        <div className={styles.pickerImg} style={{ opacity: 0 }} />
                    )}
                    <span className={styles.pickerCaption}>{it.name || it.id}</span>
                </button>
            ))}
        </div>
    );
}