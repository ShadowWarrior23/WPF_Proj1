import { useEffect, useState } from 'react';
import type { FoodItemDto } from '../types/types';
import '../Rating.css';
import Stars from './Stars';

const initFI: FoodItemDto = {
    name: '',
    categ: '',
    ingreds: [],
    allergens: [],
    tags: [],
    rating: 0
}

function Rating() {

    const [foodItems, setFoodItems] = useState<FoodItemDto[]>([]);
    const [foodItem, setFoodItem] = useState<FoodItemDto>(initFI);
    const [error, setError] = useState("");

    useEffect(() => {
        getFoodItems();
    }, []);

    async function getFoodItems() {
        try {
            setError("");
            const response = await fetch("http://localhost:5072/api/FoodItems", {
                cache: "no-store"
            });
            const data = await response.json();
            setFoodItems(data);

            if (data.length > 0) {
                setFoodItem(data[0]);
            }
        } catch (err) {
            setError(`Failed to load food items. Reason:\n ${err}`);
        }
    }

    async function getFoodItem(foodItemName: string) {
        try {
            setError("");
            const response = await fetch(`http://localhost:5072/api/FoodItems/${encodeURIComponent(foodItemName)}`, {
                cache: "no-store"
            });

            const data: FoodItemDto = await response.json();
            setFoodItem(data);
        } catch (err) {
            setError(`Failed to load food item. Reason:\n${err}`);
        }
    }

    function changeCurrFoodItem(foodItemName: string) {
        getFoodItem(foodItemName);
    }

    async function saveRating() {
        try {
            setError("");
            const response = await fetch(
                `http://localhost:5072/api/FoodItems/${encodeURIComponent(foodItem.name)}/rating`,
                {
                    method: "PATCH",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        rating: foodItem.rating
                    })
                }
            );

            if (!response.ok) {
                throw new Error(`HTTP ${response.status}`);
            }

            alert("Rating saved successfully.");
        } catch (err) {
            setError(`Failed to save rating. Reason:\n${err}`);
        }
    }


    return (
        <>
            <h1>Rating</h1>
            <main>
                <select id='food' onChange={(e) => changeCurrFoodItem(e.target.value)}>
                    {foodItems.map(f => (
                        <option key={f.name} value={f.name}>{f.name}</option>
                    ))}
                </select>

                <div className="card">
                    <h3>Name: <span>{foodItem.name}</span></h3>
                    <h5>Category: <span>{foodItem.categ}</span></h5>
                    <p>Ingredients: <span>{foodItem.ingreds.join(', ')}</span></p>
                    <p>Allergens: <span>{foodItem.allergens.length !== 0 ? foodItem.allergens.join(', ') : 'None'}</span></p>
                    <p>Tags: <span>{foodItem.tags.join(', ')}</span></p>
                    <Stars value={foodItem.rating} onChange={(newRating) => setFoodItem(prev => ({ ...prev, rating: newRating }))} />
                    <div className="saveBtn">
                        <button onClick={saveRating}>Save</button>
                    </div>
                </div>
            </main>
        </>
    )
}

export default Rating;