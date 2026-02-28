using System;
using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    public static class Admin
    {
        public static void ShowAdminMenu(List<Ingredient> ingredients, List<PizzaBase> bases)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============== ПАНЕЛЬ КУРБАНА ===============");
                Console.WriteLine("1. Управление Ингредиентами (Список/Добавить/Удалить)");
                Console.WriteLine("2. Управление Основами (Список/Добавить/Удалить)");
                Console.WriteLine("0. Назад в главное меню");
                Console.WriteLine("=====================================================");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine()!;
                if (choice == "0") break;

                switch (choice)
                {
                    case "1": ManageIngredients(ingredients); break;
                    case "2": ManageBases(bases); break;
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

            Console.WriteLine("\n[A] Добавить | [D] Удалить | [0] Назад");
            string act = Console.ReadLine()?.ToUpper()!;

            if (act == "A")
            {
                Console.Write("Название: "); string name = Console.ReadLine()!;
                Console.Write("Цена: "); decimal price = decimal.Parse(Console.ReadLine()!);
                ingredients.Add(new Ingredient(name, price));
                Console.WriteLine("Добавлено!");
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
            Console.ReadKey();
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

            Console.WriteLine("\n[A] Добавить | [D] Удалить | [0] Назад");
            string act = Console.ReadLine()?.ToUpper()!;

            if (act == "A")
            {
                Console.Write("Название основы: "); string bName = Console.ReadLine()!;
                Console.Write("Это классика? (да/нет): "); bool isClassic = Console.ReadLine()?.ToLower() == "да";
                Console.Write("Цена: "); decimal bPrice = decimal.Parse(Console.ReadLine()!);

                if (!isClassic && classicPrice > 0 && bPrice > classicPrice * 1.2m)
                {
                    Console.WriteLine($"\nОШИБКА! Цена ({bPrice}) выше лимита ({classicPrice * 1.2m}).");
                    Console.WriteLine("По ТЗ особая основа не может быть дороже классики более чем на 20%.");
                }
                else
                {
                    bases.Add(new PizzaBase(bName, bPrice, isClassic));
                    Console.WriteLine("Основа добавлена!");
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
            Console.ReadKey();
        }
    }
}