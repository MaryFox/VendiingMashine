using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Product
    {
        public uint Price { get; private set; }
        public string Title { get; private set; }
        public string Type { get; set; }
        public int NumberInMachine { get; set; }

        public Product(string title, uint price)
        {
            Price = price;
            Title = title;
        }
    }
}
