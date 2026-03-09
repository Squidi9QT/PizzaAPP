using System;

namespace KurbanChef
{
    public interface IPriceable
    {
        string Name { get; }
        decimal Price { get; }
    }

    public abstract class BaseProduct : IPriceable
    {
        public Guid Id { get; protected set; } = Guid.NewGuid(); // GUID !!!
        public string Name { get; private set; }
        public virtual decimal Price { get; private set; }
        protected BaseProduct(string name)
        {
            Rename(name);
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя не может быть пустым", nameof(name));

            Name = name.Trim();
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "ЭЭЭ норм цену пиши (- нельзя)");

            Price = price;
        }
    }
}