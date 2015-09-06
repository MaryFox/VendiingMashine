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
