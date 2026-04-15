import { useEffect, useState } from 'react';
import type { FoodItemDto } from '../types/types';
import './OurFood.css';

function OurFood() {
    const [foodItems, setFoodItems] = useState<FoodItemDto[]>([]);
    const [allFoodItems, setAllFoodItems] = useState<FoodItemDto[]>([]);
    const [error, setError] = useState("");
    const [categSel, setCategSel] = useState('');
    const [search, setSearch] = useState("");

    useEffect(() => {
        getFoodItems();
    }, []);

    useEffect(() => {
        filterFood(search, categSel);
    }, [search, categSel, allFoodItems]);

    async function getFoodItems() {
        try {
            setError("");

            const resp = await fetch("http://127.0.0.1:5072/api/FoodItems", {
                cache: "no-store"
            });

            const data = await resp.json();

            const filteredData = data.filter((f: FoodItemDto) => f.tags.length !== 0);

            setAllFoodItems(filteredData);
            setFoodItems(filteredData);
        } catch (err) {
            setError(`Failed to load menus. Reason:\n ${err}`);
        }
    }

    function filterFood(searchText: string, category: string) {
        let filtered = [...allFoodItems];

        if (searchText.trim() !== "") {
            filtered = filtered.filter(f =>
                f.name.toLowerCase().includes(searchText.toLowerCase())
            );
        }

        if (category === "soup") {
            filtered = filtered.filter(f => f.categ.toLowerCase() === "soup");
        } else if (category === "dish") {
            filtered = filtered.filter(f => f.categ.toLowerCase() === "dish");
        }

        setFoodItems(filtered);
    }

    return (
        <>
            <h1>Our Food</h1>

            {error && <p>{error}</p>}

            <section id='filt'>
                <input type="text" id="search" placeholder='Search here...' value={search} onChange={e => setSearch(e.target.value)} />

                <select id="Category" value={categSel} onChange={e => setCategSel(e.target.value)}>
                    <option value="">All</option>
                    <option value="soup">Soups</option>
                    <option value="dish">Main Dishes</option>
                </select>
            </section>

            <main id='maino'>
                {foodItems.map(f => (
                    <div className="card" key={f.name}>
                        <h3>Name: <span>{f.name}</span></h3>
                        <h5>Category: <span>{f.categ}</span></h5>
                        <p>Ingredients: <span>{f.ingreds.join(', ')}</span></p>
                        <p>Allergens: <span>{f.allergens.length !== 0 ? f.allergens.join(', ') : 'None'}</span></p>
                        <p>Tags: <span>{f.tags.join(', ')}</span></p>
                    </div>
                ))}
            </main>
        </>
    );
}

export default OurFood;