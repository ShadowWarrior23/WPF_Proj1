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

    function changeCurrFoodItem(){
        
    }

    return (
        <>
            <h1>Rating</h1>
            <main>
                <select id='food' onSelect={changeCurrFoodItem}>
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

                <div className="card">
                    <h3>Name: <span></span></h3>
                    <h5>Category: <span></span></h5>
                    <p>Ingredients: <span></span></p>
                    <p>Allergens: <span></span></p>
                    <p>Tags: <span></span></p>
                </div>
            </main>
        </>
    )
}

export default Rating;