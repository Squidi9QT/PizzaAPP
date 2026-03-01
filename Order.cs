using System;
using System.Collections.Generic;
using System.Linq;

namespace KurbanChef
{
    public class Order
    {
        public int OrderNumber{get; set;}
        public List<Pizza> Pizzas {get; set;} = new List<Pizza>();
        public string Comment {get; set;} = "";
        public DateTime OrderTime {get; set;}

        public Order(int orderNumber)
        {
            OrderNumber = orderNumber;
            OrderTime = DateTime.Now;
        }

        public void AddPizza(Pizza pizza)
        {
            Pizzas.Add(pizza);
        }

        public decimal TotalPrice
        {
            get { return Pizzas.Sum(p => p.TotalPrice); }
        }
    }
}