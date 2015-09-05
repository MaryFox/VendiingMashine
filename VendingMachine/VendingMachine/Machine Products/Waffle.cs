using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
   public class Waffle : Product
   {
        public Waffle(string title, int price)
            : base(title, price)
        {
            Type = "Вафля";
            NumberInMachine = 2;
        }
    }
}
