namespace VendingMachine
{
    public class Cookie : Product
   {

        public Cookie(string title, int price)
            : base(title, price)
        {
            Type = "Печенье";
            NumberInMachine = 1;
        }
    }
}
