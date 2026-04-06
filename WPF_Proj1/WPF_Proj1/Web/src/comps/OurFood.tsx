import { useEffect, useState } from 'react';
import type { FoodItemDto } from '../types/types';


function OurFood() {

    const [foodItems, setFoodItems] = useState<FoodItemDto[]>([]);
    const [ingreds, setIngreds] = useState<string[]>([]);
    const [allergens, setAllergens] = useState<string[]>([]);
    const [tags, setTags] = useState<string[]>([]);
    const [error, setError] = useState("");
    const [categSel, setCategSel] = useState<'' | 'Soups' | 'Dishes'>('');

    useEffect(() => {
        getFoodItems();
    }, []);

    async function getFoodItems() {
        try {
            setError("");
            const resp = await fetch("http://127.0.0.1:5072/api/FoodItems", {
                cache: "no-store"
            });
            // Select filter w/ soup/dish
            /* const baseUrl = "http://127.0.0.1:5072/api/FoodItems";

            const url =
                selected === "Soups"
                    ? `${baseUrl}?categ=soup`
                    : selected === "Dishes"
                        ? `${baseUrl}?categ=dish`
                        : baseUrl;

            const resp = await fetch(url, {
                cache: "no-store"
            }); */
            const data = await resp.json();
            setFoodItems(data);
            filterCorrect();
            setArrs();
        } catch (err) {
            setError(`Failed to load menus. Reason:\n ${err}`);
        }
    }

    function filterCorrect() {
        setFoodItems(foodItems => foodItems.filter(f => f.tags.length !== 0))
    }

    function setArrs() {
        const ingArr: string[] = Array.from(new Set(foodItems.flatMap(f => f.ingreds)));
        setIngreds(ingArr);
        const allArr: string[] = Array.from(new Set(foodItems.flatMap(f => f.allergens)));
        setAllergens(allArr);
        const tagArr: string[] = Array.from(new Set(foodItems.flatMap(f => f.tags)));
        setTags(tagArr);
    }

    function filterFood() {

    }

    return (
        <>
            <h1>Our Food</h1>
            <section>
                <input type="text" id="search" placeholder='Search here...' />
                <select id="Category" onChange={e => setCategSel(e.target)}>
                    <option value="all">All</option>
                    <option value="soup">Soups</option>
                    <option value="dish">Main Dishes</option>
                </select>
                <select id="Ingredients">
                    <option value="">None</option>
                    {ingreds.map(ing => (
                        <option key={ing} value={ing}>{ing}</option>
                    ))}
                </select>
            </section>
            <main>
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
    )
}

export default OurFood;