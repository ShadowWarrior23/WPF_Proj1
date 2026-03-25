# PixaF0rk - your BEST bet for a canteen system

## PixaF0rk is an interactive canteen-managing system built for students and managers.


### Branch
Console:
while-lal:
opciók:
- 1: mai/legközelebbi napi lekérése (hogy az hogy van, majd megadom)
- 2: havi menü lekérése
- 3: felhasználók sorolása (név, felh. név, email, jelszó) -admin (IsAdmin)
  - mindegyik után lesz opció exportra (dailyMenu.txt, monthlyMenu.txt, users.txt)
- 0: kilépés

## db működés:
using var db = new AppDbContext();

táblák:
db.Users => felhasználók
db.Orders => rendelések
db.DailyMenus => menü napokra bontva

a táblákban lévő mezők neve és típusa a Data > Models mappában található

innentől már LinQ jellegű (pl. db.Users.Where(...) )

### legközelebbi nap:
List<DateOnly> weekdays = db.DailyMenus.Select(d => d.Day).OrderBy(d => d).ToList();

DateOnly today = DateOnly.FromDateTime(DateTime.Today);

DateOnly elDate = weekdays.Any(d => d >= today) ? weekdays.First(d => d >= today) : weekdays.First();
