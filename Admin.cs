using System;
using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    public static class Admin
    {
        public static void ShowAdminMenu(List<Ingredient> ingredients, List<PizzaBase> bases, List<Pizza> pizzas, List<Order> orders)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============== ПАНЕЛЬ КУРБАНА ===============");
                Console.WriteLine("1. Управление Ингредиентами");
                Console.WriteLine("2. Управление Основами");
                Console.WriteLine("3. Управление Пиццами (Меню)");
                Console.WriteLine("4. ИСТОРИЯ ЗАКАЗОВ (и фильтры)");
                Console.WriteLine("0. Назад в главное меню");
                Console.WriteLine("=====================================================");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine()!;
                if (choice == "0") break;

                switch (choice)
                {
                    case "1": ManageIngredients(ingredients); break;
                    case "2": ManageBases(bases); break;
                    case "3": ManagePizzas(pizzas, bases, ingredients); break;
                    case "4": ManageOrders(orders); break;
                }
            }
        }

        private static void ManageIngredients(List<Ingredient> ingredients)
        {
            Console.Clear();
            Console.WriteLine("--- СПИСОК ИНГРЕДИЕНТОВ ---");
            for (int i = 0; i < ingredients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ingredients[i].Name}: {ingredients[i].Price} тенге.");
            }

            Console.WriteLine("\n[A] Добавить | [E] Редактировать | [D] Удалить | [F] Поиск | [0] Назад");
            string act = Console.ReadLine()?.ToUpper()!;

            if (act == "A")
            {
                Console.Write("Название: "); string name = Console.ReadLine()!;
                Console.Write("Цена: "); decimal price = decimal.Parse(Console.ReadLine()!);
                ingredients.Add(new Ingredient(name, price));
                Console.WriteLine("Добавлено!");
            }
            else if (act == "E")
            {
                Console.Write("Введите номер для редактирования: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= ingredients.Count)
                {
                    var ing = ingredients[idx - 1];
                    Console.Write($"Новое название ({ing.Name}): ");
                    string newName = Console.ReadLine()!;
                    if (!string.IsNullOrWhiteSpace(newName)) ing.Rename(newName);

                    Console.Write($"Новая цена ({ing.Price}): ");
                    string newPrice = Console.ReadLine()!;
                    if (decimal.TryParse(newPrice, out decimal p)) ing.SetPrice(p);
                    Console.WriteLine("Обновлено!");
                }
            }
            else if (act == "D")
            {
                Console.Write("Введите номер для удаления: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= ingredients.Count)
                {
                    ingredients.RemoveAt(idx - 1);
                    Console.WriteLine("Удалено!");
                }
            }
            if (act == "F")
            {
                Console.Write("Введите название для поиска: ");
                string search = Console.ReadLine()?.ToLower() ?? "";

                // LINQ TODO
                var found = ingredients.Where(ing => ing.Name.ToLower().Contains(search)).ToList();

                Console.WriteLine($"\n--- Найдено: {found.Count} ---");
                foreach (var f in found) Console.WriteLine($"- {f.Name}: {f.Price} тг.");
                Console.ReadKey();
            }
            if (act != "0") Console.ReadKey();
        }
        private static void ManageBases(List<PizzaBase> bases)
        {
            Console.Clear();
            Console.WriteLine("--- СПИСОК ОСНОВ ---");

            var classicBase = bases.FirstOrDefault(b => b.IsClassic);
            decimal classicPrice = classicBase?.Price ?? 0;

            for (int i = 0; i < bases.Count; i++)
            {
                string type = bases[i].IsClassic ? "[КЛАССИКА]" : "[ОСОБАЯ]";
                Console.WriteLine($"{i + 1}. {type} {bases[i].Name}: {bases[i].Price} тенге.");
            }

            Console.WriteLine("\n[A] Добавить | [E] Редактировать | [D] Удалить | [F] Поиск | [0] Назад");
            string act = Console.ReadLine()?.ToUpper()!;

            if (act == "A")
            {
                Console.Write("Название основы: "); string bName = Console.ReadLine()!;
                Console.Write("Это классика? (да/нет): "); bool isClassic = Console.ReadLine()?.ToLower() == "да";
                Console.Write("Цена: "); decimal bPrice = decimal.Parse(Console.ReadLine()!);

                if (!isClassic && classicPrice > 0 && bPrice > classicPrice * 1.2m)
                {
                    Console.WriteLine($"\nОШИБКА! Цена ({bPrice}) выше лимита ({classicPrice * 1.2m}).");
                }
                else
                {
                    bases.Add(new PizzaBase(bName, bPrice, isClassic, classicPrice));
                    Console.WriteLine("Основа добавлена!");
                }
            }
            else if (act == "E")
            {
                Console.Write("Введите номер для редактирования: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= bases.Count)
                {
                    var b = bases[idx - 1];
                    Console.Write($"Новое название ({b.Name}): ");
                    string nName = Console.ReadLine()!;
                    if (!string.IsNullOrWhiteSpace(nName)) b.Rename(nName);

                    Console.Write($"Новая цена ({b.Price}): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal nPrice))
                    {
                        if (!b.IsClassic && classicPrice > 0 && nPrice > classicPrice * 1.2m)
                            Console.WriteLine($"ОШИБКА! Максимальная цена для особой основы: {classicPrice * 1.2m}");
                        else
                            b.SetPrice(nPrice);
                    }
                    Console.WriteLine("Обновлено!");
                }
            }
            else if (act == "D")
            {
                Console.Write("Введите номер для удаления: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= bases.Count)
                {
                    bases.RemoveAt(idx - 1);
                    Console.WriteLine("Удалено!");
                }
            }
            if (act == "F")
            {
                Console.Write("Введите название основы для поиска: ");
                string search = Console.ReadLine()?.ToLower() ?? "";

                var found = bases.Where(b => b.Name.ToLower().Contains(search)).ToList();

                Console.WriteLine($"\n--- Найдено основ: {found.Count} ---");
                foreach (var b in found)
                {
                    string type = b.IsClassic ? "[КЛАССИКА]" : "[ОСОБАЯ]";
                    Console.WriteLine($"- {type} {b.Name}: {b.Price} тг.");
                }
                Console.ReadKey();
            }
            if (act != "0") Console.ReadKey();
        }
        private static void ManagePizzas(List<Pizza> pizzas, List<PizzaBase> bases, List<Ingredient> ingredients)
        {
            Console.Clear();
            Console.WriteLine("--- ФИРМЕННЫЕ ПИЦЦЫ (МЕНЮ) ---");

            var sortedPizzas = pizzas.OrderBy(p => p.TotalPrice).ToList(); // TODO LING

            for (int i = 0; i < sortedPizzas.Count; i++)
            {
               Console.WriteLine($"{i + 1}. {sortedPizzas[i].Name} (Основа: {sortedPizzas[i].Base.Name}) - {sortedPizzas[i].TotalPrice} тг.");
            }

            Console.WriteLine("\n[A] Добавить новую | [E] Редактировать | [D] Удалить | [F] Фильтр | [0] Назад");
            string act = Console.ReadLine()?.ToUpper()!;

            if (act == "A")
            {
                Console.Write("Название новой пиццы: ");
                string name = Console.ReadLine()!;

                Console.WriteLine("\nВыберите основу:");
                for (int i = 0; i < bases.Count; i++) Console.WriteLine($"{i + 1}. {bases[i].Name}");

                int baseIdx;
                Console.Write("Ваш выбор: ");
                while (!int.TryParse(Console.ReadLine(), out baseIdx) || baseIdx < 1 || baseIdx > bases.Count)
                {
                    Console.Write("Ошибка! Введите правильный номер основы: ");
                }
                baseIdx--;

                Pizza newPizza = new Pizza(name, bases[baseIdx]);

                Console.WriteLine("\nДобавляем ингредиенты (введите 0 для завершения):");
                while (true)
                {
                    for (int i = 0; i < ingredients.Count; i++) Console.WriteLine($"{i + 1}. {ingredients[i].Name}");
                    Console.Write("Выбор: ");

                    if (!int.TryParse(Console.ReadLine(), out int ingIdx))
                    {
                        Console.WriteLine("Ошибка ввода! Пожалуйста, введите число.");
                        continue;
                    }

                    if (ingIdx == 0) break;

                    if (ingIdx > 0 && ingIdx <= ingredients.Count)
                    {
                        newPizza.AddIngredient(ingredients[ingIdx - 1]);
                        Console.WriteLine("Добавлено!");
                    }
                    else
                    {
                        Console.WriteLine("Такого ингредиента нет в списке!");
                    }
                }
                pizzas.Add(newPizza);
                Console.WriteLine("Пицца успешно добавлена в меню!");
            }

            else if (act == "E")
            {
                Console.Write("Введите номер пиццы для редактирования: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= sortedPizzas.Count)
                {
                    var pizzaToEdit = sortedPizzas[idx - 1];

                    Console.Write($"Новое название (Текущее: {pizzaToEdit.Name}): ");
                    string newName = Console.ReadLine()!;
                    if (!string.IsNullOrWhiteSpace(newName)) pizzaToEdit.Name = newName;

                    Console.Write($"Изменить основу? Текущая: {pizzaToEdit.Base.Name} (да/нет): ");
                    if (Console.ReadLine()?.ToLower() == "да")
                    {
                        for (int i = 0; i < bases.Count; i++) Console.WriteLine($"{i + 1}. {bases[i].Name}");
                        Console.Write("Выберите новую основу (номер): ");
                        if (int.TryParse(Console.ReadLine(), out int bIdx) && bIdx > 0 && bIdx <= bases.Count)
                        {
                            pizzaToEdit.Base = bases[bIdx - 1];
                        }
                    }

                    Console.Write("Очистить текущие ингредиенты и собрать их заново? (да/нет): ");
                    if (Console.ReadLine()?.ToLower() == "да")
                    {
                        pizzaToEdit.Ingredients.Clear();
                        Console.WriteLine("\nДобавляем ингредиенты (введите 0 для завершения):");
                        while (true)
                        {
                            for (int i = 0; i < ingredients.Count; i++) Console.WriteLine($"{i + 1}. {ingredients[i].Name}");
                            Console.Write("Выбор: ");

                            if (!int.TryParse(Console.ReadLine(), out int ingIdx)) continue;
                            if (ingIdx == 0) break;

                            if (ingIdx > 0 && ingIdx <= ingredients.Count)
                            {
                                pizzaToEdit.AddIngredient(ingredients[ingIdx - 1]);
                                Console.WriteLine("Добавлено!");
                            }
                        }
                    }
                    Console.WriteLine("Пицца успешно обновлена!");
                }
            }
            else if (act == "D")
            {
                Console.Write("Введите номер для удаления: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= sortedPizzas.Count)
                {
                    var pizzaToRemove = sortedPizzas[idx - 1];
                    pizzas.Remove(pizzaToRemove);
                    Console.WriteLine("Удалено!");
                }
            }
            else if (act == "F")
            {
                Console.Write("\nВведите название ингредиента для поиска (например, Томаты): ");
                string search = Console.ReadLine()?.Trim().ToLower() ?? "";

                //  TODO LINQ для поиска пиц с ингр
                var filtered = pizzas.Where(p => p.Ingredients.Any(ing => ing.Name.ToLower().Contains(search))).ToList();

                Console.WriteLine($"\n--- Результаты поиска для '{search}' ---");
                if (filtered.Count == 0)
                {
                    Console.WriteLine("Пиццы с таким ингредиентом не найдены.");
                }
                else
                {
                    foreach (var p in filtered)
                    {
                        string composition = string.Join(", ", p.Ingredients.Select(x => x.Name));
                        Console.WriteLine($"- {p.Name} | Состав: {composition}");
                    }
                }
            }

            if (act != "0") Console.ReadKey();
        }
        private static void FilterPizzasByIngredient(List<Pizza> pizzas)
        {
            Console.Write("Введите название ингредиента для поиска: ");
            string search = Console.ReadLine()?.ToLower() ?? "";

            var filtered = pizzas.Where(p => p.Ingredients.Any(i => i.Name.ToLower().Contains(search))).ToList();

            Console.WriteLine($"\n--- Найдено пицц с '{search}': {filtered.Count} ---");
            foreach (var p in filtered)
            {
                Console.WriteLine($"- {p.Name} (Состав: {string.Join(", ", p.Ingredients.Select(i => i.Name))})");
            }
            Console.ReadKey();
        }
        private static void ManageOrders(List<Order> orders)
        {
            Console.Clear();
            Console.WriteLine("--- ИСТОРИЯ ЗАКАЗОВ ---");
            Console.WriteLine("1. Показать все заказы");
            Console.WriteLine("2. Отфильтровать по дате");
            Console.WriteLine("3. Отфильтровать по минимальной сумме");
            Console.WriteLine("0. Назад");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine()!;

            IEnumerable<Order> filteredOrders = orders;

            if (choice == "2")
            {
                Console.Write("Введите дату (ДД.ММ.ГГГГ): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dt))
                {
                    filteredOrders = orders.Where(o => o.OrderTime.Date == dt.Date);
                }
                else
                {
                    Console.WriteLine("Неверный формат даты.");
                    Console.ReadKey();
                    return;
                }
            }
            else if (choice == "3")
            {
                Console.Write("Введите минимальную сумму: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal minPrice))
                {
                    filteredOrders = orders.Where(o => o.TotalPrice >= minPrice);
                }
            }
            else if (choice != "1") return;

            Console.Clear();
            Console.WriteLine("=== СПИСОК ЗАКАЗОВ ===");

            var list = filteredOrders.ToList();
            list.Sort(new OrderTimeComparer());

            if (!list.Any())
            {
                Console.WriteLine("\nЗаказы по вашему запросу не найдены.");
            }
            else
            {
                foreach (var order in list)
                {
                    Console.WriteLine($"\nЗаказ #{order.OrderNumber} | ID: {order.Id}");
                    Console.WriteLine($"Оформлен на: {order.OrderTime}");
                    Console.WriteLine($"Пицц: {order.Pizzas.Count} шт. | ИТОГ: {order.TotalPrice} тенге.");
                    if (!string.IsNullOrEmpty(order.Comment))
                        Console.WriteLine($"Комментарий: {order.Comment}");
                    Console.WriteLine("-------------------------------------------------");
                }
            }
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }
    }
}