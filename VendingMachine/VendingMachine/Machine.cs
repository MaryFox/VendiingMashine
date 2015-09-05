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
        public Balance MachineMoney { get; set; }
        public Machine()
        {
            Products = new List<Product> { new Cookie("Юбилейное", 10), new Cookie("Юбилейное", 10),  new Cookie("Юбилейное", 1),
                new Waffle("Венские", 30), new Waffle("Венские", 30), new Waffle("Венские", 30), new Waffle("Венские", 30),
                new Waffle("Венские", 30), new Waffle("Венские", 30), new Waffle("Венские", 30), new Waffle("Венские", 30),
                new Waffle("Венские", 30), new Waffle("Венские", 30), new Cake("Домашний", 50), new Cake("Домашний", 50),
                new Cake("Домашний", 50), new Cake("Домашний", 50) };
            Random m = new Random();
            MachineMoney = new Balance(m.Next(1, 1000));
        }
        public void ShowMenu()
        {
            foreach (var prod in Products.GroupBy(x => x.Type))
            {
                int itemNumber = Products.FirstOrDefault(x => x.Type.ToString() == prod.Key.ToString()).NumberInMachine;
                string itemTitle = Products.FirstOrDefault(x => x.Type.ToString() == prod.Key.ToString()).Title;
                Console.WriteLine("{0} - {1,-8}  \"{2}\"", itemNumber, prod.Key, itemTitle);
                //Console.WriteLine($"{prod.Key + "\t" + prod.Count() + " шт."}" );
            }
        } 
    }
}
