using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    public class Crust
    {
        public string Name {set; get;}
        public List<Ingredient> Ingredients {get; set;} = new List<Ingredient>();

        public List<string> ForbiddenPizzas {get; set;} = new List<string>();

        public Crust(string name)
        {
            Name = name;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public decimal Price
        {
            get { return Ingredients.Sum(i => i.Price); }
        }
    }
}