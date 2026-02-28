using System;
using System.Collections.Generic;

namespace KurbanChef
{
    class KurbanChefApp
    {
        static void Main(string[] args)
        {
            List<Ingredient> allIngredients = new List<Ingredient>();
            List<PizzaBase> allBases = new List<PizzaBase>();
            List<Pizza> allPizzas = new List<Pizza>();

            Sclad.SeedData(allIngredients, allBases, allPizzas);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("       ДОБРО ПОЖАЛОВАТЬ В KURBAN CHEF     ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. ПОСМОТРЕТЬ МЕНЮ И ЗАКАЗАТЬ");
                Console.WriteLine("2. СОБРАТЬ СВОЮ ПИЦЦУ (КОНСТРУКТОР)");
                Console.WriteLine("0. ПАНЕЛЬ АДМИНИСТРАТОРА");
                Console.WriteLine("5. ВЫХОД");
                Console.WriteLine("==========================================");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine()!;

                if (choice == "1")
                {
                    ShowMenuAndOrder(allPizzas);
                }
                else if (choice == "2")
                {
                    CreateCustomPizza(allIngredients, allBases, allPizzas);
                }
                else if (choice == "0")
                {
                    Admin.ShowAdminMenu(allIngredients, allBases);
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Спасибо, что зашли! Приятного аппетита!");
                    break;
                }
            }
        }

        static void ShowMenuAndOrder(List<Pizza> pizzas)
        {
            Console.WriteLine("\n--- НАШЕ ФИРМЕННОЕ МЕНЮ ---");
            for (int i = 0; i < pizzas.Count; i++)
            {
                var p = pizzas[i];
                Console.WriteLine($"{i + 1}. Пицца '{p.Name}' ({p.Base.Name} тесто) - {p.TotalPrice} руб.");
                Console.Write("   Состав: ");
                foreach (var ing in p.Ingredients) Console.Write(ing.Name + ", ");
                Console.WriteLine("\n------------------------------------------");
            }

            Console.Write("\nВведите номер пиццы для заказа (или 0 для отмена): ");
            if (int.TryParse(Console.ReadLine(), out int orderNum) && orderNum > 0 && orderNum <= pizzas.Count)
            {
                var selected = pizzas[orderNum - 1];
                Console.WriteLine($"\n[УСПЕХ] Заказ принят: Пицца '{selected.Name}'.");
                Console.WriteLine($"К оплате: {selected.TotalPrice} руб. Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void CreateCustomPizza(List<Ingredient> ingredients, List<PizzaBase> bases, List<Pizza> pizzas)
        {
            Console.WriteLine("\n--- КОНСТРУКТОР ПИЦЦЫ ---");
            Console.Write("Назовите ваш шедевр: ");
            string name = Console.ReadLine()!;

            Console.WriteLine("Выберите основу:");
            for (int i = 0; i < bases.Count; i++)
                Console.WriteLine($"{i + 1}. {bases[i].Name} ({bases[i].Price} руб.)");

            int bIdx = int.Parse(Console.ReadLine()!) - 1;
            Pizza customPizza = new Pizza(name, bases[bIdx]);

            Console.WriteLine("Добавляйте ингредиенты (пишите название). Введите 'стоп' для завершения:");
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine()!;
                if (input.ToLower() == "стоп") break;

                var found = ingredients.Find(x => x.Name.ToLower() == input.ToLower());
                if (found != null)
                {
                    customPizza.AddIngredient(found);
                    Console.WriteLine($"Добавлено: {found.Name}");
                }
                else Console.WriteLine("Такого ингредиента нет!");
            }

            pizzas.Add(customPizza);
            Console.WriteLine($"\nПицца '{customPizza.Name}' добавлена в ваш заказ! Цена: {customPizza.TotalPrice} руб.");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}