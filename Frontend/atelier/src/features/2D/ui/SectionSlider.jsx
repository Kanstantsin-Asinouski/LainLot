import { useId } from "react";
import clsx from "clsx";
import styles from "../styles/CostumeEditor.module.css";

/**
 * Universal row with slider for the right panel.
 * onChange expects a number (not a string).
 */
export default function SectionSlider({
    label,
    value,
    min,
    max,
    step = 1,
    onChange,
    disabled = false,
    suffix = "",
    className,
}) {
    const id = useId();

    return (
        <div className={clsx(styles.subRow, className)} style={{ marginTop: 6 }}>
            <label className={styles.slimLabel} htmlFor={id}>
                {label}
            </label>
            <input
                id={id}
                type="range"
                min={min}
                max={max}
                step={step}
                value={value}
                onChange={(e) => onChange(+e.target.value)}
                disabled={disabled}
                className={styles.rangeCompact}
            />
            <span className={styles.value}>
                {value}
                {suffix}
            </span>
        </div>
    );
}