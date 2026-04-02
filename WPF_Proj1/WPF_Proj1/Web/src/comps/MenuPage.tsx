import { useEffect, useState } from "react";
import type { DailyMenuDto } from "../types/types";
import "./MenuPage.css";

export default function MenuPage() {
    const [menus, setMenus] = useState<DailyMenuDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        loadMenus();
    }, []);

    async function loadMenus() {
        try {
            setLoading(true);
            setError("");

            // this part depends on how WPF gives the data
            const response = await fetch("http://127.0.0.1:5072/api/menu", {
                cache: "no-store"
            });
            const data = await response.json();

            setMenus(data);
        } catch (err) {
            setError(`Failed to load menus. Reason:\n ${err}`);
        } finally {
            setLoading(false);
        }
    }

    if (loading) return <p>Loading menus...</p>;
    if (error) return <p>{error}</p>;

    return (
        <main className='w-full flex gap-4 flex-wrap flex-column justify-center align-middle p-4 h-full'>
            <h1 className="h-5 w-full text-center">Daily Menus</h1>
            <section className='w-full flex gap-4 flex-row flex-wrap'>
                {menus.map((m) => (
                    <div className="card" key={m.day}>
                        <h3 id="cDay">{m.day}</h3>
                        <p className="cInf">Soup: <span>{m.soup}</span></p>
                        <p className="cInf">Dish A: <span>{m.dishA}</span></p>
                        <p className="cInf">Dish B: <span>{m.dishB}</span></p>
                    </div>
                ))}
            </section>
        </main>
    )
}