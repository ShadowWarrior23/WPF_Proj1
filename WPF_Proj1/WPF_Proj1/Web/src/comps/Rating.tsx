import { useEffect, useState } from 'react';
import type { DailyMenuDto } from '../types/types';
import '../Rating.css';


function Rating() {

    const [menus, setMenus] = useState<DailyMenuDto[]>([]);
    const [error, setError] = useState("");

    useEffect(() => {
        loadMenus();
    }, []);

    async function loadMenus() {
        try {
            setError("");
            const response = await fetch("http://127.0.0.1:5072/api/menu", {
                cache: "no-store"
            });
            const data = await response.json();
            setMenus(data);
        } catch (err) {
            setError(`Failed to load menus. Reason:\n ${err}`);
        }
    }

    return (
        <>
            <h1>Rating</h1>
            <main>
                <select id='food'>
                    {menus.map(f => (
                        <option key={f.day}>{f.soup}</option>
                    ))}
                    {menus.map(f => (
                        <option key={f.day}>{f.dishA}</option>
                    ))}
                    {menus.map(f => (
                        <option key={f.day}>{f.dishB}</option>
                    ))}
                </select>
            </main>
        </>
    )
}

export default Rating;