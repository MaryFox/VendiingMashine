using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
   public class Cake: Product
    {
        public Cake(string title, uint price)
            : base(title, price)
        {
            Type = "Кекс";
        }
    }
}
