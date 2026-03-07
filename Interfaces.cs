using System;

namespace KurbanChef
{
    public interface IPriceable
    {
        string Name { get; set; }
        decimal Price { get; set; }
    }

    public abstract class BaseProduct : IPriceable
    {
        public Guid Id { get; protected set; } = Guid.NewGuid(); // GUID !!!
        public string Name { get; set; }
        public virtual decimal Price { get; set; } //!!!!

        protected BaseProduct(string name)
        {
            Name = name;
        }
    }
}