using System;
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

                if (choice == "1") { ShowMenuAndOrder(allPizzas); }
                else if (choice == "2") { CreateCustomPizza(allIngredients, allBases, allPizzas); }
                else if (choice == "0") { Admin.ShowAdminMenu(allIngredients, allBases, allPizzas); }
                else if (choice == "5") break;
            }
        }

        static void ShowMenuAndOrder(List<Pizza> pizzas)
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
                var selected = pizzas[orderNum - 1];
                Console.WriteLine($"\nВыберите размер: 1.Маленькая(база) 2.Средняя(+20%) 3.Большая(+50%)");
                string sChoice = Console.ReadLine()!;
                selected.Size = sChoice == "2" ? PizzaSize.Medium : (sChoice == "3" ? PizzaSize.Large : PizzaSize.Small);

                Console.Write("Добавить сырный бортик? (+500 тенге) (да/нет):");
                selected.HasCheeseCrust = Console.ReadLine()?.ToLower() == "да";

                Console.Clear();
                Console.WriteLine("========== ЧЕК ==========");
                Console.WriteLine($"Заказ: {selected.Size} пицца '{selected.Name}'");
                if (selected.HasCheeseCrust) Console.WriteLine("Доп: Сырный бортик");
                Console.WriteLine($"ИТОГО: {selected.TotalPrice} тенге.");
                Console.WriteLine("=========================");
                Console.ReadKey();
            }
        }

       static void CreateCustomPizza(List<Ingredient> ingredients, List<PizzaBase> bases, List<Pizza> pizzas)
{
    Console.Clear();
    Console.WriteLine("=== КОНСТРУКТОР ПИЦЦЫ ===");
    Console.Write("Назовите вашу пиццу (например, 'Моя Пицца'): ");
    string name = Console.ReadLine()?.Trim() ?? "Пользовательская пицца";

    Console.WriteLine("\nВыберите основу (номер):");
    for (int i = 0; i < bases.Count; i++) Console.WriteLine($"{i + 1}. {bases[i].Name} ({bases[i].Price} тенге)");

    int bIdx;
    while (!int.TryParse(Console.ReadLine(),out bIdx) || bIdx < 1 || bIdx > bases.Count) Console.Write("ОШИБКА ЕМАЕ");

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

            Console.Write("Сырный бортик? (+500 тенге) (да/нет): ");
            customPizza.HasCheeseCrust = Console.ReadLine()?.ToLower() == "да";

            pizzas.Add(customPizza);
            Console.WriteLine($"\nГотово! Цена: {customPizza.TotalPrice} тенге. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}