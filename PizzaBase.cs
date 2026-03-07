namespace KurbanChef
{
    public class PizzaBase : BaseProduct
    {
        public bool IsClassic { get; set; }
        private decimal _basePrice;

        public override decimal Price => _basePrice;

        public PizzaBase(string name, decimal price, bool isClassic, decimal classicPrice = 0) : base(name)
        {
            IsClassic = isClassic;
            // тут инкапусляция
            if (!isClassic && classicPrice > 0 && price > classicPrice * 1.2m)
            {
                _basePrice = classicPrice * 1.2m;
            }
            else
            {
                Price = price;
            }
        }
    }
}