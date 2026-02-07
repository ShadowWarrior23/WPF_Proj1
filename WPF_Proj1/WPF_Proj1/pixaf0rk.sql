PRAGMA foreign_keys = OFF;
BEGIN TRANSACTION;

CREATE TABLE IF NOT EXISTS daily_menu (
  day TEXT NOT NULL PRIMARY KEY, -- 'YYYY-MM-DD'
  soup TEXT,
  dish_a TEXT,
  dish_b TEXT
);

CREATE TABLE IF NOT EXISTS orders (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  user_id TEXT,
  day TEXT,              -- 'YYYY-MM-DD'
  wants_soup INTEGER,    -- 0/1
  dish_choice TEXT       -- 'n'/'a'/'b'
);

CREATE TABLE IF NOT EXISTS users (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  username TEXT,
  full_name TEXT,
  balance INTEGER
);

INSERT INTO daily_menu (day, soup, dish_a, dish_b) VALUES
('2026-02-02', 'Hungarian Goulash Soup', 'Pork Stew with Noodles', 'Breaded Cheese with Rice and Tartar Sauce'),
('2026-02-03', 'Chicken Noodle Soup', 'Fried Chicken Leg with Rice', 'Beef Stew with Potatoes'),
('2026-02-04', 'Bean Soup with Smoked Sausage', 'Potato Casserole with Sausage', 'Vegetable Lasagna'),
('2026-02-05', 'Vegetable Ragout Soup', 'Meatballs in Tomato Sauce with Potatoes', 'Chicken Stir Fry with Noodles'),
('2026-02-06', 'Tomato Soup with Pasta', 'Semolina Pasta with Cocoa', 'Cottage Cheese Dumplings with Sour Cream and Sugar'),
('2026-02-09', 'Cabbage Soup with Smoked Meat', 'Bean Stew with Sausage', 'Fish Fingers with Rice and Tartar Sauce'),
('2026-02-10', 'Creamy Mushroom Soup', 'Breaded Chicken Breast with Mashed Potatoes', 'Mushroom Risotto with Parmesan'),
('2026-02-11', 'Green Pea Soup', 'Rice with Chicken and Vegetables', 'Beef Stroganoff with Noodles'),
('2026-02-12', 'Lentil Soup with Smoked Meat', 'Layered Savoy Cabbage', 'Baked Fish Fillet with Vegetables'),
('2026-02-13', 'Fruit Soup', 'Sweet Cottage Cheese Pasta', 'Golden Dumpling with Vanilla Sauce'),
('2026-02-16', 'Hungarian Potato Soup', 'Stuffed Peppers in Tomato Sauce', 'Vegetable Fried Rice with Egg'),
('2026-02-17', 'Garlic Cream Soup', 'Fried Sausage with Mustard and Bread', 'Chicken Paprikash with Dumplings'),
('2026-02-18', 'Cauliflower Soup', 'Breaded Cheese with Rice and Tartar Sauce', 'Mushroom Risotto with Parmesan'),
('2026-02-19', 'Carrot Soup with Parsley', 'Chicken Stew with Rice', 'Chili Con Carne with Rice'),
('2026-02-20', 'Cherry Soup', 'Poppy Seed Pasta', 'Bread Pudding'),
('2026-02-23', 'Egg Drop Soup', 'Pork Stew with Potatoes', 'Vegetable Lasagna'),
('2026-02-24', 'Broccoli Cream Soup', 'Fried Chicken Breast with Rice', 'Stuffed Cabbage Rolls'),
('2026-02-25', 'Spinach Soup with Boiled Egg', 'Bean Stew with Fried Egg', 'Beef Stew with Dumplings'),
('2026-02-26', 'Pumpkin Cream Soup', 'Fish Fingers with Mashed Potatoes', 'Chicken Breast in Mushroom Cream Sauce'),
('2026-02-27', 'Sweet Corn Soup', 'Jam-filled Pancakes', 'Somloi Sponge Cake Dessert');

INSERT INTO orders (id, user_id, day, wants_soup, dish_choice) VALUES
(1, '1', '2026-02-02', 0, 'n'),
(2, '1', '2026-02-03', 0, 'n'),
(3, '1', '2026-02-04', 0, 'n'),
(4, '1', '2026-02-05', 0, 'n'),
(5, '1', '2026-02-06', 0, 'n'),
(6, '1', '2026-02-09', 0, 'n'),
(7, '1', '2026-02-10', 0, 'n'),
(8, '1', '2026-02-11', 0, 'n'),
(9, '1', '2026-02-12', 0, 'n'),
(10, '1', '2026-02-13', 1, 'a'),
(11, '1', '2026-02-16', 0, 'n'),
(12, '1', '2026-02-17', 0, 'n'),
(13, '1', '2026-02-18', 0, 'n'),
(14, '1', '2026-02-19', 0, 'n'),
(15, '1', '2026-02-20', 0, 'n'),
(16, '1', '2026-02-23', 0, 'n'),
(17, '1', '2026-02-24', 0, 'n'),
(18, '1', '2026-02-25', 0, 'n'),
(19, '1', '2026-02-26', 0, 'n'),
(20, '1', '2026-02-27', 0, 'n');

INSERT INTO users (Id, username, full_name, balance) VALUES
(1, 'S137B', 'Bence Szabó', 0);

COMMIT;