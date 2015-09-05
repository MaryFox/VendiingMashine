using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Machine m = new Machine();
            //m.MachineMoney.ShowBalance(); узнать баланс автомата
            Client c = new Client();
            //c.Wallet.ShowBalance(); узнать баланс покупателя
            Console.WriteLine("Выбирите желаемый пункт меню");
            m.ShowMenu();



            Console.ReadLine();
        }
    }
}
