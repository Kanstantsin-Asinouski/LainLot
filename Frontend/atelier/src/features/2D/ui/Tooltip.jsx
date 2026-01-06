import React, { useId, useState } from "react";

/**
 * A lightweight tooltip without dependencies.
 * Shown on hover/focus. Available: linked via aria-describedby.
 */
export default function Tooltip({ label, side = "bottom", offset = 8, children }) {
    const id = useId();
    const [open, setOpen] = useState(false);

    const pos = side === "bottom"
        ? { top: `calc(100% + ${offset}px)`, left: "50%", transform: "translateX(-50%)" }
        : side === "top"
            ? { bottom: `calc(100% + ${offset}px)`, left: "50%", transform: "translateX(-50%)" }
            : side === "left"
                ? { right: `calc(100% + ${offset}px)`, top: "50%", transform: "translateY(-50%)" }
                : { left: `calc(100% + ${offset}px)`, top: "50%", transform: "translateY(-50%)" };

    return (
        <span
            className="ll-tooltip-wrap"
            onMouseEnter={() => setOpen(true)}
            onMouseLeave={() => setOpen(false)}
            onFocus={() => setOpen(true)}
            onBlur={() => setOpen(false)}
            style={{ position: "relative", display: "inline-flex" }}
        >
            {React.cloneElement(children, { "aria-describedby": id })}
            {open && (
                <span
                    role="tooltip"
                    id={id}
                    className="ll-tooltip"
                    style={{
                        position: "absolute",
                        ...pos,
                        background: "#111827",
                        color: "#fff",
                        fontSize: 12,
                        lineHeight: "18px",
                        padding: "6px 8px",
                        borderRadius: 8,
                        boxShadow: "0 4px 16px rgba(0,0,0,.25)",
                        whiteSpace: "nowrap",
                        pointerEvents: "none",
                        zIndex: 50,
                    }}
                >
                    {label}
                </span>
            )}
        </span>
    );
}