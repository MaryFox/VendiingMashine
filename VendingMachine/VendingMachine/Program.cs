using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var automate = new Machine();
            for (int i = 0; i < 3; i++)
            {
                automate.Products.Add(new Cookie("Юбилейное", 10));
            }
            for (int i = 0; i < 10; i++)
            {
                automate.Products.Add(new Waffle("Венская", 30));
            }
            for (int i = 0; i < 4; i++)
            {
                automate.Products.Add(new Cake("Домашний", 50));
            }

            var transaction = new CurrentTransaction();//создание транзакии для оплаты выбранного товара
            var currentClient = new Client();//создание экземпляра клиента
            string choice = "";//переменная, вводимая пользователем, чтобы сделать выбор о преращении покупки или продолжении
            bool flag = true;
            bool check = true;
            currentClient.Money.ShowBalance();//узнать баланс покупателя
            //automate.Money.ShowBalance();//узнать баланс автомата

            while ((choice != "f" && choice != "F" && choice != "ф" && choice != "Ф" ) && check)//до тех пор пока пользователь не напишет "ф"
            {
                while ((choice != "f" && choice != "F" && choice != "ф" && choice != "Ф") && check)//до тех пор пока пользователь не напишет "ф"
                {
                    //он будет продолжать покупать товары. Иначе получит сдачу. 

                    Console.WriteLine("Выбирите желаемый пункт меню, указав его номер в списке товаров");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    automate.ShowMenu();//вывод на экран меню автомата 
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Console.WriteLine("*Нажмите любую другую клавишу, если не хотите выбирать товар");
                    int s;//переменная, в которой хранится выбранный пользователем номер товара из меню (не показывает товар, который закончился)
                    int.TryParse(Console.ReadLine(), out s);

                    var prod = automate.FindByNumber(s);//поиск товара по номеру

                    if (prod != null)//проверка на существование товара
                    {
                        Console.WriteLine();
                        Console.WriteLine("{0,-8}  \"{1}\" {2} рублей", prod.Type, prod.Title, prod.Price);//вывод информации о товаре
                        transaction = new CurrentTransaction();//создание транзакии для оплаты выбранного товара

                        transaction.ToPay = prod.Price;
                        Console.WriteLine();
                        flag = true;

                        while ((transaction.ToPay - currentClient.Exchange > transaction.MoneyAdded || flag ) && check)//пока автомат не получит достаточное для оплаты товара кол-во денег
                        {
                            //автомат продолжает запрашивать монеты.
                            int price = 0;
                            flag = false;

                            price = transaction.ToPay - transaction.MoneyAdded - currentClient.Exchange;

                            Console.WriteLine();
                            Console.WriteLine("Для опаты товара внесите еще {0} рублей", price);//в переменной price хранится разница между ценой товара и кол-вом внесенных денег
                            Console.WriteLine("Подсазка: закиньте монету в автомат и дождитесь подтверждения");
                            Console.WriteLine("Если хотите узнать свой баланс, нажмите \"b\"");

                            var value = Console.ReadLine();

                            if (value == "b" || value == "B" || value == "б" || value == "Б" )
                            {
                                Console.WriteLine();
                                Console.WriteLine("Ваш баланс:");
                                currentClient.Money.ShowBalance();//узнать баланс покупателя
                            }
                            else
                            {
                                int coin;//переменная, содержащия информацию о номинале брошенной пользователем монеты
                                int.TryParse(value, out coin);
                                if (automate.CheckTheCoin(coin))//проверяет, принимает ли автомат монеты такого номинала
                                {
                                    if (currentClient.CheckTheCoin(coin))//проверяет, если ли у пользователя монета такого номинала
                                    {
                                        Console.WriteLine("{0}р внесено.", coin);//подтвержение успешного зачисления монеты

                                        transaction.Money.AddToList(coin, 1);//добавить моенту на счет текущей транзакции в список имеющихся монет
                                        transaction.MoneyAdded += coin;//добавить кол-во рублей, которое внес пользователь за покупку

                                        currentClient.PutCoin(coin);//изъятие монеты coin из кошелька пользователя
                                        transaction.AddCoin(coin);//добавление монету на счет текущей транзации по номиналу 
                                    }
                                    else
                                    {
                                        if (currentClient.IfClientHasMoney())
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("У вас нет такой монеты");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        else
                                        {
                                            currentClient.GetExchange(currentClient.Money.ExchangeReturn(transaction.MoneyAdded));

                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("У вас закончились деньги. Недостаточно для оплаты товара. Вам автомат возвращает деньги.");
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            currentClient.Money.ShowBalance();//узнать баланс покупателя
                                            Console.WriteLine("Нажмите любую кнопку для завершения работы с автоматом");
                                            //automate.Money.ShowBalance();//узнать баланс автомата
                                            check = false;
                                            //Environment.Exit(0);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Автомат не принимает деньги такого номинала");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                            }
                          
                        }
                        if (!flag && check)//попадаем в эту ветвь кода, только если выбрали и успешно оплатили покупку
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Спасибо за покупку!");//покупка успешно совершена. Завершение текущей транзакции
                            Console.WriteLine("Остаток: {0}", transaction.Exchanger(currentClient.Exchange));//вывод информации об остатке от оплаты товара
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine();

                            automate.Money.Five += transaction.Money.Five;//Перечисление денег за товар на счет автомата со счета..
                            automate.Money.One += transaction.Money.One;//..текущей транзакции. Перечисление по монетам разного номинала:..
                            automate.Money.Ten += transaction.Money.Ten;//..1, 2, 5, 10 рублей соответственно
                            automate.Money.Two += transaction.Money.Two;

                            currentClient.Exchange = transaction.Exchange - currentClient.Exchange; //сдача, которая находится еще в автомате;

                            automate.DeletiongBoughtProduct(automate.FindByNumber(s));
                            Console.WriteLine("Ваш баланс:");
                            currentClient.Money.ShowBalance();//узнать баланс покупателя для проверки корректности начисления денег 
                            //automate.Money.ShowBalance();//узнать баланс автомата

                        }
                    }
                    else if (check)
                    {
                        Console.WriteLine("Выбранного товара нет в наличии, или вы ошиблись с номером товара.");
                    }
                    if (check)
                    {
                        Console.WriteLine("Если вы хотите совершить другую покупку, нажмите любую кнопку. Иначе нажмите \"ф\"для получения сдачи");
                        choice = Console.ReadLine();//пользователь совершает выбор
                    }
                       
                }
                if (check && automate.CheckForExchange(transaction.Exchanger(currentClient.Exchange)))
                {
                    List<int> coins = currentClient.Money.ExchangeReturn(currentClient.Exchange);//если пользователь захотел получить сдачу, он попадает в эту ветвь кода.
                    currentClient.GetExchange(coins);//выдача сдачи пользователю

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Сдача выдана");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.ReadLine();
                }

                else if (check)
                {
                    currentClient.GetExchange(currentClient.Money.ExchangeReturn(transaction.MoneyAdded));

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Извините, но автомат не может выдать вам сдачу. Поэтому он вернем вам ваши деньги. Приносим извинения за неудобства.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    // currentClient.GetExchange(transaction.Money.Coins);
                    Console.WriteLine("Ваш баланс:");
                    currentClient.Money.ShowBalance();//узнать баланс покупателя
                    //automate.Money.ShowBalance();//узнать баланс автомата
                }
            }
            Console.ReadLine();
        }

    }
}
