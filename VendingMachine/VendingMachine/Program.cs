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
            Machine M = new Machine(new List<Product> { new Cookie("Маша", 1), new Cookie("Маша", 1), new Waffle("Вафля", 5) });
            M.ShowMenu();
            Console.ReadLine();
        }
    }
}
