using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    public class Crust
    {
        public string Name {set; get;}
        public List<Ingredient> Ingredients {get; set;} = new List<Ingredient>();

        public List<string> ForbiddenPizzas {get; set;} = new List<string>();
        public List<string> AllowedPizzas {get; set;} = new List<string>();

        public bool CanUseWith (string pizzaName) {
            if(string.IsNullOrWhiteSpace(pizzaName)) return false;
            string normalised = pizzaName.Replace("(Двойная)","").Trim();

            if(AllowedPizzas.Count>0)
                return AllowedPizzas.Contains(normalised);

            return !ForbiddenPizzas.Contains(normalised);
        }

        public bool CanUseWith(string pizzaName1, string pizzaName2)
        {
            return CanUseWith(pizzaName1) && CanUseWith(pizzaName2);
        }   

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
