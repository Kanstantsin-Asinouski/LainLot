// OrderForm.jsx
import { useId } from "react";
import styles from "../styles/CostumeEditor.module.css";

const DEF = {
    fullName: "", email: "", phone: "",
    country: "", region: "", city: "", district: "",
    street: "", house: "", apt: "", postal: "", notes: ""
};

export default function OrderForm({ value, onChange }) {
    const form = value ?? DEF;
    const set = (k, v) => onChange?.({ ...form, [k]: v });

    const ID = (name) => useId() + "-" + name;
    const emailId = ID("email");
    const phoneId = ID("phone");
    const fullId = ID("fullName");

    const L = ({ htmlFor, children }) => (
        <label htmlFor={htmlFor} className={styles.formLabel}>{children}</label>
    );

    return (
        <div className={styles.section}>
            <div className={styles.sectionTitle}>Данные для заказа</div>

            <div className={styles.grid1}>
                <L htmlFor={fullId}>ФИО</L>
                <input id={fullId} className={styles.input}
                    value={form.fullName} onChange={(e) => set("fullName", e.target.value)} />

                <L htmlFor={emailId}>E-mail</L>
                <input id={emailId} className={styles.input} type="email"
                    placeholder="name@example.com"
                    value={form.email} onChange={(e) => set("email", e.target.value)} />

                <L htmlFor={phoneId}>Телефон</L>
                <input id={phoneId} className={styles.input}
                    inputMode="tel" placeholder="+48 123 456 789"
                    value={form.phone} onChange={(e) => set("phone", e.target.value)} />
            </div>

            <div className={styles.sectionTitle} style={{ marginTop: 10 }}>Адрес доставки</div>
            <div className={styles.grid2}>
                <div className={styles.col}>
                    <label className={styles.formLabel}>Страна
                        <input className={styles.input}
                            value={form.country} onChange={(e) => set("country", e.target.value)} />
                    </label>
                </div>
                <div className={styles.col}>
                    <label className={styles.formLabel}>Регион / Область
                        <input className={styles.input}
                            value={form.region} onChange={(e) => set("region", e.target.value)} />
                    </label>
                </div>

                <div className={styles.col}>
                    <label className={styles.formLabel}>Город
                        <input className={styles.input}
                            value={form.city} onChange={(e) => set("city", e.target.value)} />
                    </label>
                </div>
                <div className={styles.col}>
                    <label className={styles.formLabel}>Район
                        <input className={styles.input}
                            value={form.district} onChange={(e) => set("district", e.target.value)} />
                    </label>
                </div>

                <div className={styles.col}>
                    <label className={styles.formLabel}>Улица
                        <input className={styles.input}
                            value={form.street} onChange={(e) => set("street", e.target.value)} />
                    </label>
                </div>
                <div className={styles.col}>
                    <label className={styles.formLabel}>Дом
                        <input className={styles.input}
                            value={form.house} onChange={(e) => set("house", e.target.value)} />
                    </label>
                </div>

                <div className={styles.col}>
                    <label className={styles.formLabel}>Квартира
                        <input className={styles.input}
                            value={form.apt} onChange={(e) => set("apt", e.target.value)} />
                    </label>
                </div>
                <div className={styles.col}>
                    <label className={styles.formLabel}>Индекс
                        <input className={styles.input}
                            value={form.postal} onChange={(e) => set("postal", e.target.value)} />
                    </label>
                </div>
            </div>

            <label className={styles.formLabel} style={{ marginTop: 8 }}>
                Комментарий к доставке
                <textarea rows={3} className={styles.textarea}
                    value={form.notes} onChange={(e) => set("notes", e.target.value)} />
            </label>
        </div>
    );
}