using System.Collections.Generic;
using System.Net.Security;

namespace KurbanChef
{
    public static class Sclad
    {
        public static void SeedData(List<Ingredient> ingredients, List<PizzaBase> bases, List<Pizza> pizzas,List<Crust> crusts)
        {
            bases.Add(new PizzaBase("Классическое", 1000, true));
            bases.Add(new PizzaBase("Тонкое", 1100, false, 1000));
            bases.Add(new PizzaBase("Толстое", 1200, false, 1000));

            ingredients.Add(new Ingredient("Сыр", 600));
            ingredients.Add(new Ingredient("Пепперони", 900));
            ingredients.Add(new Ingredient("Шампиньоны", 400));
            ingredients.Add(new Ingredient("Ветчина(Халал)", 1200));
            ingredients.Add(new Ingredient("Томаты", 300));
            ingredients.Add(new Ingredient("Кунжут", 200));


            Crust cheeseCrust = new Crust("Сырные бортики");
            cheeseCrust.AddIngredient(ingredients[0]);

            cheeseCrust.ForbiddenPizzas.Add("Маргарита");
            crusts.Add(cheeseCrust);

            Crust sesameCrust = new Crust("Кунжутные бортики");
            sesameCrust.AddIngredient(ingredients[5]);
            crusts.Add(sesameCrust);


            Pizza pepperoni = new Pizza("Пепперони", bases[1]);
            pepperoni.AddIngredient(ingredients[0]);
            pepperoni.AddIngredient(ingredients[1]);
            pizzas.Add(pepperoni);

            Pizza margarita = new Pizza("Маргарита", bases[0]);
            margarita.AddIngredient(ingredients[0]);
            margarita.AddIngredient(ingredients[4]);
            pizzas.Add(margarita);

            Pizza mushroom = new Pizza("Грибная", bases[1]);
            mushroom.AddIngredient(ingredients[0]);
            mushroom.AddIngredient(ingredients[2]);
            pizzas.Add(mushroom);

            Pizza meat = new Pizza("Мясная", bases[2]);
            meat.AddIngredient(ingredients[0]);
            meat.AddIngredient(ingredients[1]);
            meat.AddIngredient(ingredients[3]);
            pizzas.Add(meat);

            Pizza mix = new Pizza("Студенческий Микс", bases[0]);
            mix.AddIngredient(ingredients[0]);
            mix.AddIngredient(ingredients[2]);
            mix.AddIngredient(ingredients[3]);
            mix.AddIngredient(ingredients[4]);
            pizzas.Add(mix);

        }
    }
}