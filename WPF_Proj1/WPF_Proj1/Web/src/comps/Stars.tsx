import { useState } from "react";
import "../stars.css";
import type { StarProps } from "../types/types";


function Stars({ max = 5, value, onChange }: StarProps) {

    const [hoverValue, setHoverValue] = useState(0);
    const displayValue = hoverValue || value;

    return (
        <div className="rating" onMouseLeave={() => setHoverValue(0)}>
            {Array.from({ length: max }, (_, i) => {
                const starValue = i + 1;
                const isFilled = starValue <= displayValue!;

                return (
                    <span key={starValue} className={`star ${isFilled ? "filled" : "empty"}`} onMouseEnter={() => setHoverValue(starValue)} onClick={() => onChange?.(starValue)}>★</span>
                );
            })}
        </div>
    );
}

export default Stars;