namespace KurbanChef
{
    public class PizzaBase
    {
        public string Name {get; set;}
        public decimal Price { get; set; }
        public bool IsClassic { get; set; }

        public PizzaBase(string name, decimal price, bool isClassic, decimal classicPrice = 0)
        {
            Name = name;
            IsClassic = isClassic;

            if (!IsClassic && price > classicPrice * 1.2m)
            {
                price = classicPrice * 1.2m;
                Console.WriteLine($"Внимание: цена '{name}' была снижена до {price}");
            }
            else
            {
                Price = price;
            }
        }
    }
}