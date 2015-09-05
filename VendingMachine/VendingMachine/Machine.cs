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
        public Balance Money { get; set; }
        public Machine()
        {
            Products = new List<Product> { new Cookie("Юбилейное", 10), new Cookie("Юбилейное", 10),  new Cookie("Юбилейное", 1),
                new Waffle("Венская", 30), new Waffle("Венская", 30), new Waffle("Венская", 30), new Waffle("Венская", 30),
                new Waffle("Венская", 30), new Waffle("Венская", 30), new Waffle("Венская", 30), new Waffle("Венская", 30),
                new Waffle("Венская", 30), new Waffle("Венская", 30), new Cake("Домашний", 50), new Cake("Домашний", 50),
                new Cake("Домашний", 50), new Cake("Домашний", 50)};
            Random m = new Random();
            Money = new Balance(m.Next(1, 100));
        }

        public void ShowMenu()
        {
            Console.WriteLine();
            foreach (var prod in Products.GroupBy(x => x.Title)) // Почему группировка не по названию?
            {
                int itemNumber = Products.FirstOrDefault(x => x.Title.ToString() == prod.Key.ToString()).NumberInMachine;
                string itemType = Products.FirstOrDefault(x => x.Title.ToString() == prod.Key.ToString()).Type;
                int price = Products.FirstOrDefault(x => x.Title.ToString() == prod.Key.ToString()).Price;
                Console.WriteLine("{0} - {1,-6}  \"{2}\" {3}р.", itemNumber, itemType, prod.Key, price);
                //Console.WriteLine($"{prod.Key + "\t" + prod.Count() + " шт."}" );
            }
            Console.WriteLine();
        }

        public Product FindByNumber(int num)
        {
            return Products.FirstOrDefault(x => x.NumberInMachine == num);
        }
        public bool CheckTheCoin(int coin)
        {
            if (coin==1 || coin == 2 || coin == 5 || coin == 10)
            {
                return true;
                //монета существующего номинала
            }
            else
            {
                return false;
                //такую монету автомат не принимает
            }
        }
        
    }
}
