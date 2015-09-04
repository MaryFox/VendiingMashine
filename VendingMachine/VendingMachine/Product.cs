using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Product
    {
        public double Price { get; private set; }
        public string Title { get; private set; }

        public Product(string title, double price)
        {
            Price = price;
            Title = title;
        }
    }
}
