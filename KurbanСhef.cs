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
                Console.WriteLine($"{i + 1}. Пицца '{p.Name}' ({p.Base.Name} тесто) - {p.TotalPrice} тенге.");
                Console.Write("   Состав: ");
                string composition = string.Join(", ", p.Ingredients.Select(x => x.Name));
                Console.WriteLine(string.IsNullOrEmpty(composition) ? "Классический состав" : composition);
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
    Console.Clear();
    Console.WriteLine("=== КОНСТРУКТОР ПИЦЦЫ ===");
    Console.Write("Назовите вашу пиццу (например, 'Моя Пицца'): ");
    string name = Console.ReadLine()?.Trim() ?? "Пользовательская пицца";

    Console.WriteLine("\nВыберите основу (введите номер):");
    for (int i = 0; i < bases.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {bases[i].Name} — {bases[i].Price} тенге");
    }

    int bIdx;
    while (!int.TryParse(Console.ReadLine(), out bIdx) || bIdx < 1 || bIdx > bases.Count)
    {
        Console.Write("Ошибка! Введите корректный номер основы: ");
    }

    Pizza customPizza = new Pizza(name, bases[bIdx - 1]);

    while (true)
    {
        Console.Clear();
        Console.WriteLine($"--- Собираем пиццу: {customPizza.Name} ---");
        Console.WriteLine($"Текущая цена: {customPizza.TotalPrice} тенге");
        Console.Write("Состав: ");
        string currentComposition = string.Join(", ", customPizza.Ingredients.Select(x => x.Name));
        Console.WriteLine(string.IsNullOrEmpty(currentComposition) ? "пусто" : currentComposition);
        Console.WriteLine("\nДобавьте ингредиент (введите номер) или 0, чтобы ЗАКОНЧИТЬ:");
        for (int i = 0; i < ingredients.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ingredients[i].Name} (+{ingredients[i].Price} тенге)");
        }

        Console.Write("\nВаш выбор: ");
        string input = Console.ReadLine()!;

        if (input == "0") break;

        if (int.TryParse(input, out int ingIdx) && ingIdx > 0 && ingIdx <= ingredients.Count)
        {
            var selectedIng = ingredients[ingIdx - 1];
            customPizza.AddIngredient(selectedIng);
            Console.WriteLine($"\n[+] {selectedIng.Name} добавлен!");
        }
        else
        {
            Console.WriteLine("\n[!] Ошибка: Нет такого номера. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    pizzas.Add(customPizza);
    Console.WriteLine($"\nПицца '{customPizza.Name}' успешно создана и добавлена в меню!");
    Console.WriteLine($"Итоговая стоимость: {customPizza.TotalPrice} тенге. Нажмите клавишу...");
    Console.ReadKey();
}
    }
}