using System;
using System.Collections.Generic;

namespace KurbanChef
{
    public static class Admin
    {
        public static void ShowAdminMenu(List<Ingredient> ingredients, List<PizzaBase> bases)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===============Admin===============");
                Console.WriteLine("1.Список всех ингредиентов");
                Console.WriteLine("2.Добавить новый ингредиент");
                Console.WriteLine("3 Список основ");
                Console.WriteLine("4 Добавить основу");
                Console.WriteLine("0 Назад в главное меню");
                Console.WriteLine("===================================");
                Console.Write("Выбор:");

                string choice = Console.ReadLine()!;
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Ингредиенты");
                            foreach (var i in ingredients)
                            {
                                Console.WriteLine($"{i.Name}: {i.Price} руб.");
                            }
                            break;
                        }

                    case "2":
                        {
                            Console.Write("Название:");
                            string name = Console.ReadLine()!;
                            Console.Write("Цена: ");
                            decimal price = decimal.Parse(Console.ReadLine()!);
                            ingredients.Add(new Ingredient(name, price));
                            Console.WriteLine("Добавлено!");
                            break;
                        }

                    case "3":
                        {
                            Console.WriteLine("Основы");
                            foreach (var b in bases)
                            {
                                Console.WriteLine($"{(b.IsClassic ? "[К]" : "[О]")} {b.Name}: {b.Price} руб.");
                            }
                            break;
                        }

                    case "4":
                        {
                            Console.Write("Название основы:");
                            string bName = Console.ReadLine()!;
                            Console.Write("Это классика? (да/нет):");
                            bool isClassic = Console.ReadLine()?.ToLower() == "да";
                            Console.Write("Цены: ");
                            decimal bPrice = decimal.Parse(Console.ReadLine()!);
                            decimal classicPrice = 0;
                            foreach (var b in bases)
                            {
                                if (b.IsClassic) classicPrice = b.Price;
                            }
                            bases.Add(new PizzaBase(bName, bPrice, isClassic, classicPrice));
                            Console.WriteLine("Основа добавлена!");
                            break;
                        }
                }
            }
        }
    }
}