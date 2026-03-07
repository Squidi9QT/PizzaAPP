namespace KurbanChef
{
    public class Ingredient : BaseProduct //!!!!!
    {
        private decimal _price;
        public override decimal Price => _price; //!!!!!
        public Ingredient (string name, decimal price) : base(name)
        {
            Price = price;
        }
    }
}