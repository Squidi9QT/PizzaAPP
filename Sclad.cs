using System.Collections.Generic;

namespace KurbanChef
{
    public static class Sclad
    {
        public static void SeedData(List<Ingredient> ingredients, List<PizzaBase> bases, List<Pizza> pizzas)
        {
            bases.Add(new PizzaBase("Классическое", 1000, true));
            bases.Add(new PizzaBase("Тонкое", 1100, false, 1000));
            bases.Add(new PizzaBase("Толстое", 1200, false, 1000));

            ingredients.Add(new Ingredient("Сыр", 600));
            ingredients.Add(new Ingredient("Пепперони", 900));
            ingredients.Add(new Ingredient("Шампиньоны", 400));
            ingredients.Add(new Ingredient("Ветчина(Халал)", 1200));
            ingredients.Add(new Ingredient("Томаты", 300));

            Pizza pepperoni = new Pizza("Пепперони", bases[1]);
            pepperoni.AddIngredient(ingredients[0]);
            pepperoni.AddIngredient(ingredients[1]);
            pizzas.Add(pepperoni);

            Pizza margarita = new Pizza("Маргарита", bases[0]);
            margarita.AddIngredient(ingredients[0]);
            margarita.AddIngredient(ingredients[4]);
            pizzas.Add(margarita);
        }
    }
}