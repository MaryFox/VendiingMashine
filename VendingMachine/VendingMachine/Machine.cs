using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class Machine
    {
        public List<Product> Products { get; set; }
        public Balance Money { get; set; }
        public Machine()
        {
            Products = new List<Product>();
            Random m = new Random();//создание экземпляра класса Рандом для создания произвольного кол-ва монет разного номинала
            Money = new Balance(m.Next(1, 1000));
            //Money = new Balance(0); //для тестирования случая, когда автомат не может выдать деньги
        }
        /// <summary>
        /// Вывод меню автомата
        /// </summary>
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
        /// <summary>
        /// Поиск товара по номеру
        /// </summary>
        /// <param name="num">номер товара</param>
        public Product FindByNumber(int num)
        {
            return Products.FirstOrDefault(x => x.NumberInMachine == num);
        }
        /// <summary>
        /// Проверка, принимает ли автомат монету такого номинала
        /// </summary>
        /// <param name="coin">номинал монеты</param>
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
        /// <summary>
        /// Удаление купленного товара из автомата
        /// </summary>
        /// <param name="product">товар</param>
        public void DeletiongBoughtProduct(Product product)
        {
            Products.RemoveAt(Products.FindLastIndex(x=> x==product));
        }
        public bool CheckForExchange(int exchange)
        {
            
                if (Money.CountAllMoney()>= exchange)
                    return true;   
            return false;
        }
        
    }
}
