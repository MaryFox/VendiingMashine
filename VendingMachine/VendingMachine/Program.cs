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
            var automate = new Machine();
            var currentClient = new Client();
            string choice="";
            currentClient.Money.ShowBalance();//узнать баланс покупателя
            automate.Money.ShowBalance();//узнать баланс автомата
            while (choice != "f" && choice != "F" && choice != "ф" && choice != "Ф")
            {
                Console.WriteLine("Выбирите желаемый пункт меню, указав его номер в списке товаров");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                automate.ShowMenu();
                Console.ForegroundColor = ConsoleColor.Gray;
                // Console.WriteLine("*Нажмите любую другую клавишу, если не хотите выбирать товар");
                int s;
                int.TryParse(Console.ReadLine(), out s);
                var prod = automate.FindByNumber(s);
                if (prod != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("{0,-8}  \"{1}\" {2} рублей", prod.Type, prod.Title, prod.Price);
                    CurrentTransaction cur = new CurrentTransaction();
                    cur.Price = prod.Price;
                    cur.MoneyAdded = 0;
                    Console.WriteLine();
                    bool flag = true;
                    while (cur.Price > cur.MoneyAdded || flag)
                    {
                        int price=0;
                        flag = false;
                        if (flag)
                        {
                            price = cur.Price - cur.MoneyAdded - currentClient.Exchange;
                        }
                        else
                        {
                            price = cur.Price - cur.MoneyAdded;
                        }
                        Console.WriteLine("Для опаты товара внесите еще {0} рублей", price);
                        Console.WriteLine();
                        Console.WriteLine("Подсазка: закиньте монету в автомат и дождитесь подтверждения того, что она получена");
                        int coin;
                        int.TryParse(Console.ReadLine(), out coin);
                        if (automate.CheckTheCoin(coin))
                        {
                            if (currentClient.CheckTheCoin(coin))
                            {
                                Console.WriteLine("{0}р внесено.", coin);
                                cur.Money.AddToList(coin, 1);
                                cur.MoneyAdded += coin;
                                currentClient.PutCoin(coin);
                                cur.AddCoin(coin);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("У вас нет такой монеты");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Автомат не принимает деньги такого номинала");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    if (!flag)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Спасибо за покупку!");
                        Console.WriteLine("Остаток: {0}", cur.Exchange());
                        Console.WriteLine();
                        automate.Money.Five += cur.Money.Five;
                        automate.Money.One += cur.Money.One;
                        automate.Money.Ten += cur.Money.Ten;
                        automate.Money.Two += cur.Money.Two;
                        // дозаполнить listint> для машины
                        currentClient.Exchange += cur.Exchange(); //сдача, которая находится еще в автомате
                        //c.Pay(m.MachineMoney.Coins);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.WriteLine("Выбранного товара нет в наличии, или вы ошиблись с номером товара.");
                }
                Console.WriteLine("Если вы хотите совершить другую покупку, нажмите любую кнопку. Иначе нажмите \"ф\" для получения сдачи");
             

                choice = Console.ReadLine();
              
            }
            List<int> coins = currentClient.Money.ExchangeReturn(currentClient.Exchange);
            currentClient.GetExchange(coins);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Сдача выдана");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
            //foreach (var coin in c.Money.Coins)
            //{
            //    AddToList(coin, 1);
            //}
        }


    }
}
