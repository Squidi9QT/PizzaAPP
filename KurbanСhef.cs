using System;
using System.Collections.Generic;

namespace KurbanChef
{
    class KurbanChefApp
    {
        static void Main(string[] args)
        {
            List<Ingredient> allIngredients = new List<Ingredient>();
            while (true)
            {
                Console.WriteLine("---Пиццерия Курбана---");
                Console.WriteLine("1. Показать че вообще можно добавить");
                Console.WriteLine("2. Все фигня->добавить что-то новое");
                Console.WriteLine("3. Выйти");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("--Список ингредиентов--");
                    if (allIngredients.Count == 0)
                    {
                        Console.WriteLine("Пока что нет ингредиентов. Добавьте что-нибудь!");
                    }
                    else
                    {
                        foreach (var ingredient in allIngredients)
                        {
                            Console.WriteLine($"{ingredient.Name}: {ingredient.Price} руб.");
                        }
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Введите название ингредиента:");
                    string ingName = Console.ReadLine();
                    Console.Write("Введите цену ингредиента:");
                    decimal ingPrice = decimal.Parse(Console.ReadLine());
                    Ingredient newIngredient = new Ingredient(ingName, ingPrice);
                    allIngredients.Add(newIngredient);
                    Console.WriteLine($"Ингредиент '{ingName}' добавлен с ценой {ingPrice} руб.");
                }
                        else if (choice == "3")
                        {
                            Console.WriteLine("Саубол!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Такого варианта даже нету але");
                        }
                    }
            }
        }
    }
