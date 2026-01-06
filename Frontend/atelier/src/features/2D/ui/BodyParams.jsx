// BodyParams.jsx
import { useId, useState } from "react";
import clsx from "clsx";
import styles from "../styles/CostumeEditor.module.css";

const DEF = {
    hoodie: { chest: "", waist: "", hips: "", height: "", sleeve: "", back: "" },
    pants: { waist: "", hips: "", outseam: "", inseam: "", thigh: "", ankle: "" },
};

export default function BodyParams({ value, onChange }) {
    const [tab, setTab] = useState("hoodie");
    const form = value ?? DEF;
    const num = (v) => (v === "" ? "" : v.replace(",", ".").replace(/[^\d.]/g, ""));
    const set = (sect, key, v) => onChange?.({ ...form, [sect]: { ...form[sect], [key]: num(v) } });

    // ⚠️ НЕ компонент, а обычная функция — не вызывает размонтирование инпута
    const renderInput = ({ sect, name, label }) => {
        const id = useId();
        return (
            <label htmlFor={id} className={styles.formLabel}>
                {label} <span className={styles.formUnit}>см</span>
                <input
                    id={id}
                    inputMode="decimal"
                    className={styles.input}
                    placeholder="—"
                    value={form[sect][name]}
                    onChange={(e) => set(sect, name, e.target.value)}
                />
            </label>
        );
    };

    return (
        <div className={styles.section}>
            <div className={styles.sectionTitle}>Параметры тела</div>

            <div className={clsx(styles.segmented, styles.tabs2, styles.mb8)}>
                <button
                    className={clsx(styles.segBtn, tab === "hoodie" && styles.segActive)}
                    onClick={() => setTab("hoodie")}
                >
                    Худи
                </button>
                <button
                    className={clsx(styles.segBtn, tab === "pants" && styles.segActive)}
                    onClick={() => setTab("pants")}
                >
                    Штаны
                </button>
            </div>

            {tab === "hoodie" ? (
                <div className={styles.grid2}>
                    {renderInput({ sect: "hoodie", name: "chest", label: "Грудь" })}
                    {renderInput({ sect: "hoodie", name: "waist", label: "Талия" })}
                    {renderInput({ sect: "hoodie", name: "hips", label: "Бёдра" })}
                    {renderInput({ sect: "hoodie", name: "height", label: "Рост" })}
                    {renderInput({ sect: "hoodie", name: "sleeve", label: "Рукав (длина)" })}
                    {renderInput({ sect: "hoodie", name: "back", label: "Спинка (длина)" })}
                </div>
            ) : (
                <div className={styles.grid2}>
                    {renderInput({ sect: "pants", name: "waist", label: "Талия" })}
                    {renderInput({ sect: "pants", name: "hips", label: "Бёдра" })}
                    {renderInput({ sect: "pants", name: "outseam", label: "Длина по боку" })}
                    {renderInput({ sect: "pants", name: "inseam", label: "Длина по шагу" })}
                    {renderInput({ sect: "pants", name: "thigh", label: "Бедро (обхват)" })}
                    {renderInput({ sect: "pants", name: "ankle", label: "Низ брючины" })}
                </div>
            )}
        </div>
    );
}
