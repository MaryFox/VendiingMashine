namespace VendingMachine
{
    public class Cake: Product
    {
        public Cake(string title, int price)
            : base(title, price)
        {
            Type = "Кекс";
            NumberInMachine = 3;
        }
    }
}
