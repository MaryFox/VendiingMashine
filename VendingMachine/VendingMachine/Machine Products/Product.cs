namespace VendingMachine
{
    public class Product
    {
        public int Price { get; private set; }
        public string Title { get; private set; }
        public string Type { get; set; }
        public int NumberInMachine { get; set; }

        public Product(string title, int price)
        {
            Price = price;
            Title = title;
        }
    }
}
