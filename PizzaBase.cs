namespace KurbanChef
{
    public class PizzaBase : BaseProduct
    {
        public bool IsClassic { get; set; }

        public PizzaBase(string name, decimal price, bool isClassic, decimal classicPrice = 0) : base(name)
        {
            IsClassic = isClassic;

            if (!isClassic && classicPrice > 0 && price > classicPrice * 1.2m)
            {
                Price = classicPrice * 1.2m;
                Console.WriteLine($"Внимание: цена '{name}' была снижена до {Price}");
            }
            else
            {
                Price = price;
            }
        }
    }
}