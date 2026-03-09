using System;
using System.Collections.Generic;

namespace KurbanChef
{
    public class OrderTimeComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return -1;
            if (y is null) return 1;
            int byTime = x.OrderTime.CompareTo(y.OrderTime);
            if (byTime != 0) return byTime;
            return x.OrderNumber.CompareTo(y.OrderNumber);
        }
    }
}