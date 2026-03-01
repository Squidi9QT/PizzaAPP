using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    public enum PizzaSize { Small, Medium, Large }
    public class Pizza
    {
        public string Name { get; set; }
        public PizzaBase Base { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public PizzaSize Size { get; set; } = PizzaSize.Small;
        public Crust? SelectedCrust { get; set; }
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

                if (SelectedCrust != null)
                {
                    priceWithSize += SelectedCrust.Price;
                }
                return priceWithSize;
            }
        }
    }
}