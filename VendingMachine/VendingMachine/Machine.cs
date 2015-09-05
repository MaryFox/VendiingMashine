using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Machine
    {
        IEnumerable<Product> Products { get; set; }
        //   int Money { get; set; }
        public Machine(IEnumerable<Product> products)
        {
            Products = products;
        }
        public void ShowMenu()
        {
            foreach (var prod in Products.GroupBy(x => x.Type))
            {
                Console.WriteLine($"{prod.Key + "\t" + prod.Count() + " шт."}" );
            }
        } 
    }
}
