using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    public class Pizza
    {
        public string Name {get; set;}
        public PizzaBase Base { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public Pizza(string name, PizzaBase pizzaBase)
        {
             Name = name;
            Base = pizzaBase;
            Ingredients = new List<Ingredient>();
        }
        public void AddIngredient(Ingredient item)
        {
            Ingredients.Add(item);
        }
        public decimal TotalPrice
        {
            get
            {
                return Base.Price + Ingredients.Sum(x => x.Price);
            }
        }
    }
}