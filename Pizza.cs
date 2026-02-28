using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace KurbanChef
{
    public enum PizzaSize {Small, Medium, Large}
    public class Pizza
    {
        public bool HasCheeseCrust { get; set; } = false;
        public string Name { get; set; }
        public PizzaBase Base { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public PizzaSize Size { get; set; } = PizzaSize.Small;
        public Pizza(string name, PizzaBase pizzaBase)
        {
            Name = name;
            Base = pizzaBase;
        }
        public void AddIngredient(Ingredient item) => Ingredients.Add(item);
        public decimal TotalPrice
        {
            get
            {
                decimal basePrise = Base.Price + Ingredients.Sum(x => x.Price);
                decimal priceWithSize = Size switch

                {
                    PizzaSize.Medium => basePrise * 1.2m,
                    PizzaSize.Large => basePrise * 1.5m,
                    _ => basePrise
                };

                if (HasCheeseCrust) priceWithSize +=500;
                return priceWithSize;
            }
        }
    }
}