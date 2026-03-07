using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    class KurbanChefApp
    {
        static void Main(string[] args)
        {
            List<Ingredient> allIngredients = new List<Ingredient>();
            List<PizzaBase> allBases = new List<PizzaBase>();
            List<Pizza> allPizzas = new List<Pizza>();
            List<Crust> allCrusts = new List<Crust>();

            List<Order> allOrders = new List<Order>();
            int nextOrderNumber = 1;
            Order currentOrder = new Order(nextOrderNumber);

            Sclad.SeedData(allIngredients, allBases, allPizzas, allCrusts);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==============================================================");
                Console.WriteLine("       ДОБРО ПОЖАЛОВАТЬ В ЛУЧШУЮ ПИЦЦЕРИЮ KURBAN CHEF     ");
                Console.WriteLine("==============================================================");
                Console.WriteLine("1. ПОСМОТРЕТЬ МЕНЮ И ЗАКАЗАТЬ");
                Console.WriteLine("2. СОБРАТЬ СВОЮ ПИЦЦУ (КОНСТРУКТОР)");
                Console.WriteLine("3. ПИЦЦА 50/50)");
                Console.WriteLine($"4. КОРЗИНА И ОФОРМЛЕНИЕ ЗАКАЗА (Пицц: {currentOrder.Pizzas.Count})");
                Console.WriteLine("0. ПАНЕЛЬ АДМИНИСТРАТОРА");
                Console.WriteLine("5. ВЫХОД");
                Console.WriteLine("==========================================");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine()!;

                if (choice == "1") { ShowMenuAndOrder(allPizzas, allCrusts, currentOrder); }
                else if (choice == "2") { CreateCustomPizza(allIngredients, allBases, allCrusts, currentOrder); }
                else if (choice == "3") { CreateComboPizza(allPizzas, allBases, allCrusts, currentOrder); }
                else if (choice == "4")
                {
                    if (Checkout(ref currentOrder, allOrders))
                    {
                        nextOrderNumber++;
                        currentOrder = new Order(nextOrderNumber);
                    }
                }
                else if (choice == "0") { Admin.ShowAdminMenu(allIngredients, allBases, allPizzas, allOrders); }
                else if (choice == "5") break;
            }
        }

        static void ShowMenuAndOrder(List<Pizza> pizzas, List<Crust> crusts, Order currentOrder)
        {
            Console.Clear();
            Console.WriteLine("\n--- НАШЕ ФИРМЕННОЕ МЕНЮ ---");
            for (int i = 0; i < pizzas.Count; i++)
            {
                var p = pizzas[i];
                Console.WriteLine($"{i + 1}. Пицца '{p.Name}' ({p.Base.Name} тесто) - {p.TotalPrice} тенге.");
                Console.Write("   Состав: ");
                string composition = string.Join(", ", p.Ingredients.Select(x => x.Name));
                Console.WriteLine(string.IsNullOrEmpty(composition) ? "Классический состав" : composition);
                Console.WriteLine("-----------------------------------------------");
            }

            Console.Write("\nВведите номер пиццы для заказа (или 0 для отмена): ");
            if (int.TryParse(Console.ReadLine(), out int orderNum) && orderNum > 0 && orderNum <= pizzas.Count)
            {
                var originalPizza = pizzas[orderNum - 1];
                Pizza selected = new Pizza(originalPizza.Name, originalPizza.Base);
                selected.Ingredients = new List<Ingredient>(originalPizza.Ingredients);

                Console.WriteLine($"\nВыберите размер: 1.Маленькая(база) 2.Средняя(+20%) 3.Большая(+50%)");
                string sChoice = Console.ReadLine()!;
                selected.Size = sChoice == "2" ? PizzaSize.Medium : (sChoice == "3" ? PizzaSize.Large : PizzaSize.Small);

                Console.Write("\nУдвоить порцию ингредиентов? (да/нет): ");
                if (Console.ReadLine()?.ToLower() == "да")
                {
                    selected.Ingredients.AddRange(originalPizza.Ingredients);
                    selected.Name += " (Двойная)";
                    Console.WriteLine("Ингредиенты удвоены!");
                }

                Console.WriteLine("\nВыберите бортик:");
                Console.WriteLine("0. Без бортика");
                for (int j = 0; j < crusts.Count; j++)
                {
                    if (crusts[j].ForbiddenPizzas.Contains(selected.Name))
                        Console.WriteLine($"{j + 1}. {crusts[j].Name} (+{crusts[j].Price} тенге) - НЕДОСТУПНО");
                    else
                        Console.WriteLine($"{j + 1}. {crusts[j].Name} (+{crusts[j].Price} тенге)");
                }

                Console.Write("Ваш выбор: ");
                if (int.TryParse(Console.ReadLine(), out int cChoice) && cChoice > 0 && cChoice <= crusts.Count)
                {
                    var chosenCrust = crusts[cChoice - 1];
                    if (!chosenCrust.ForbiddenPizzas.Contains(selected.Name.Replace(" (Двойная)", "")))
                    {
                        selected.SelectedCrust = chosenCrust;
                    }
                    else
                    {
                        Console.WriteLine("ШефКурбан старался!! а ты портишь ее своими бортиками, НЕЛЬЗЯ!");
                        selected.SelectedCrust = null;
                    }
                }
                else
                {
                    selected.SelectedCrust = null;
                }

                currentOrder.AddPizza(selected);

                Console.Clear();
                Console.WriteLine("========== ДОБАВЛЕНО В КОРЗИНУ ==========");
                Console.WriteLine($"Заказ: {selected.Size} пицца '{selected.Name}'");
                if (selected.SelectedCrust != null) Console.WriteLine($"Доп: {selected.SelectedCrust.Name}");
                Console.WriteLine($"ИТОГО: {selected.TotalPrice} тенге.");
                Console.WriteLine("=========================================");
                Console.ReadKey();
            }
        }

        static void CreateCustomPizza(List<Ingredient> ingredients, List<PizzaBase> bases, List<Crust> crusts, Order currentOrder)
        {
            Console.Clear();
            Console.WriteLine("=== КОНСТРУКТОР ПИЦЦЫ ===");
            Console.Write("Назовите вашу пиццу (например, 'Моя Пицца'): ");
            string name = Console.ReadLine()?.Trim() ?? "Пользовательская пицца";
            if (string.IsNullOrEmpty(name)) name = "Пользовательская пицца";

            Console.WriteLine("\nВыберите основу (номер):");
            for (int i = 0; i < bases.Count; i++) Console.WriteLine($"{i + 1}. {bases[i].Name} ({bases[i].Price} тенге)");

            int bIdx;
            while (!int.TryParse(Console.ReadLine(), out bIdx) || bIdx < 1 || bIdx > bases.Count) Console.Write("ОШИБКА ЕМАЕ");

            Pizza customPizza = new Pizza(name, bases[bIdx - 1]);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- Собираем: {customPizza.Name} | Цена: {customPizza.TotalPrice} тенге ---");
                Console.WriteLine("Ингредиенты (0 для выхода):");
                for (int i = 0; i < ingredients.Count; i++) Console.WriteLine($"{i + 1}. {ingredients[i].Name} (+{ingredients[i].Price})");

                string input = Console.ReadLine()!;
                if (input == "0") break;

                if (int.TryParse(input, out int ingIdx) && ingIdx > 0 && ingIdx <= ingredients.Count)
                    customPizza.AddIngredient(ingredients[ingIdx - 1]);
            }

            Console.WriteLine("\nРазмер: 1.Small 2.Medium(+20%) 3.Large(+50%)");
            string sz = Console.ReadLine()!;
            customPizza.Size = sz == "2" ? PizzaSize.Medium : (sz == "3" ? PizzaSize.Large : PizzaSize.Small);

            Console.WriteLine("\nВыберите бортик:");
            Console.WriteLine("0. Без бортика");
            for (int j = 0; j < crusts.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {crusts[j].Name} (+{crusts[j].Price} тг)");
            }
            Console.Write("Ваш выбор: ");
            if (int.TryParse(Console.ReadLine(), out int cChoice) && cChoice > 0 && cChoice <= crusts.Count)
            {
                customPizza.SelectedCrust = crusts[cChoice - 1];
            }
            else
            {
                customPizza.SelectedCrust = null;
            }

            currentOrder.AddPizza(customPizza);

            Console.Clear();
            string crustName = customPizza.SelectedCrust != null ? customPizza.SelectedCrust.Name : "Без бортика";
            Console.WriteLine($"\nГотово! Ваша пицца '{customPizza.Name}' собрана.");
            Console.WriteLine($"Размер: {customPizza.Size}, Бортик: {crustName}");
            Console.WriteLine($"Итоговая цена: {customPizza.TotalPrice} тенге. Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static bool Checkout(ref Order currentOrder, List<Order> allOrders)
        {
            Console.Clear();
            if (currentOrder.Pizzas.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста!");
                Console.ReadKey();
                return false;
            }

            Console.WriteLine($"=== ОФОРМЛЕНИЕ ЗАКАЗА #{currentOrder.OrderNumber} ===");
            for (int i = 0; i < currentOrder.Pizzas.Count; i++)
            {
                var p = currentOrder.Pizzas[i];
                string crustName = p.SelectedCrust != null ? p.SelectedCrust.Name : "Без бортика";
                Console.WriteLine($"{i + 1}. {p.Size} '{p.Name}' | Бортик: {crustName} | {p.TotalPrice} тг.");
            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"ИТОГО К ОПЛАТЕ: {currentOrder.TotalPrice} тенге.");

            Console.WriteLine("\n[Enter] Продолжить оформление | [0] Назад в меню");
            Console.Write("Ваш выбор: ");
            if (Console.ReadLine() == "0")
            {
                return false;
            }

            Console.Write("\nВведите комментарий к заказу (или нажмите Enter, чтобы пропустить): ");
            currentOrder.Comment = Console.ReadLine()!;

            Console.Write("Сделать заказ отложенным? (да/нет): ");
            string isDelayed = Console.ReadLine()?.ToLower().Trim() ?? "";

            if (isDelayed == "да")
            {
                bool validDate = false;
                while (!validDate)
                    {
                        Console.Write("Введите дату и время (например, 09.02.2007 17:30): ");
                        string dateInput = Console.ReadLine()!;

                        if (DateTime.TryParse(dateInput, out DateTime dt))
                        {
                            currentOrder.OrderTime = dt;
                            Console.WriteLine("Время заказа успешно установлено на: " + dt.ToString("g"));
                            validDate = true;
                        }
                        else
                        {
                            Console.WriteLine("ОШИБКА: Неверный формат времени!");
                        }
                    }
                }
                else
                {
                    currentOrder.OrderTime = DateTime.Now;
                }



            allOrders.Add(currentOrder);
            Console.WriteLine($"\nЗАКАЗ УСПЕШНО ОФОРМЛЕН! Ожидайте к {currentOrder.OrderTime}");
            Console.ReadKey();
            return true;
        }

        static void CreateComboPizza(List<Pizza> menu, List<PizzaBase> bases, List<Crust> crusts, Order currentOrder)
        {
            Console.Clear();
            Console.WriteLine("=== СБОРКА ПИЦЦЫ 50/50 ===");
            for (int i = 0; i < menu.Count; i++) Console.WriteLine($"{i + 1}. {menu[i].Name}");

            Console.Write("\nПервая половина (номер): "); int p1 = int.Parse(Console.ReadLine()!) - 1;
            Console.Write("Вторая половина (номер): "); int p2 = int.Parse(Console.ReadLine()!) - 1;
            Console.Write("Основа (1-Классика, 2-Тонкое, 3-Толстое): "); int bIdx = int.Parse(Console.ReadLine()!) - 1;

            Pizza combo = new Pizza($"50/50: {menu[p1].Name} / {menu[p2].Name}", bases[bIdx]);

            menu[p1].Ingredients.ForEach(i => combo.AddIngredient(new Ingredient(i.Name + " 1/2", i.Price / 2m)));
            menu[p2].Ingredients.ForEach(i => combo.AddIngredient(new Ingredient(i.Name + " 1/2", i.Price / 2m)));

            Console.Write("Размер (1-S, 2-M, 3-L): "); string sz = Console.ReadLine()!;
            combo.Size = sz == "2" ? PizzaSize.Medium : (sz == "3" ? PizzaSize.Large : PizzaSize.Small);

            Console.Write("Бортик (1-Сырный, 2-Кунжут, 0-Без): ");
            int.TryParse(Console.ReadLine(), out int cChoice);
            combo.SelectedCrust = cChoice > 0 ? crusts[cChoice - 1] : null;

            currentOrder.AddPizza(combo);
            Console.WriteLine($"Готово! Итог: {combo.TotalPrice} тг. Нажмите любую кнопку...");
            Console.ReadKey();
        }
    }
}