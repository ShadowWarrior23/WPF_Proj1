import { useEffect, useState } from "react";

type DailyMenuDto = {
  day: string;
  soup: string;
  dishA: string;
  dishB: string;
};

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
      const response = await fetch("/api/menu");
      const data = await response.json();

      setMenus(data);
    } catch {
      setError("Failed to load menus.");
    } finally {
      setLoading(false);
    }
  }

  if (loading) return <p>Loading menus...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <h1>Daily Menus</h1>
      {menus.map((m) => (
        <div key={m.day}>
          <h3>{m.day}</h3>
          <p>Soup: {m.soup}</p>
          <p>Dish A: {m.dishA}</p>
          <p>Dish B: {m.dishB}</p>
        </div>
      ))}
    </div>
  );
}