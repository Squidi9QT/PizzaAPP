namespace KurbanChef
{
    public class Ingredient : BaseProduct //!!!!!
    {

        public Ingredient (string name, decimal price) : base(name)
        {
            SetPrice(price);
        }
    }
}