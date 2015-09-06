using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var automate = new Machine();//создание экземпляра автомата
            var currentClient = new Client();//создание экземпляра клиента
            string choice="";//переменная, вводимая пользователем, чтобы сделать выбор о преращении покупки или продолжении
            currentClient.Money.ShowBalance();//узнать баланс покупателя
            //automate.Money.ShowBalance();//узнать баланс автомата
            while (choice != "f" && choice != "F" && choice != "ф" && choice != "Ф")//до тех пор пока пользователь не напишет "ф"
            {//он будет продолжать покупать товары. Иначе получит сдачу. 
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
                    CurrentTransaction cur = new CurrentTransaction();//создание транзакии для оплаты выбранного товара
                    cur.Price = prod.Price;//ДОПИСАТЬ ПЕРЕДАТЬ ТОВАР, А НЕ ТОЛЬКО ЕГО ЦЕНУ!!!!!!!!!!!!1!!!!!!!!!!!!!!!!
                    cur.MoneyAdded = 0;//МОЖНО ЛИ УБРАТЬ ЭТУ СТРОЧКУ?!
                    Console.WriteLine();
                    bool flag = true;//НЕ ПОМНЮ, ЧТО ЭТО!!! вроде ДЛЯ СДАЧИ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    while (cur.Price > cur.MoneyAdded || flag)//пока автомат не получит достаточное для оплаты товара кол-во денег
                    {//автомат продолжает запрашивать монеты.
                        int price=0;
                        flag = false;//доделать со сдачей и покупкой множества товаров!!!!!!!!!1!!!!!!!!!!!!!!!!!!!!!!!
                        if (flag)
                        {
                            price = cur.Price - cur.MoneyAdded - currentClient.Exchange;//????!!!!!!!!!!!!!!!!!!!1
                        }
                        else
                        {
                            price = cur.Price - cur.MoneyAdded;
                        }
                        Console.WriteLine("Для опаты товара внесите еще {0} рублей", price);//в переменной price хранится разница между ценой товара и кол-вом внесенных денег
                        Console.WriteLine();
                        Console.WriteLine("Подсазка: закиньте монету в автомат и дождитесь подтверждения");
                        int coin;//переменная, содержащия информацию о номинале брошенной пользователем монеты
                        int.TryParse(Console.ReadLine(), out coin);
                        if (automate.CheckTheCoin(coin))//проверяет, принимает ли автомат монеты такого номинала
                        {
                            if (currentClient.CheckTheCoin(coin))//проверяет, если ли у пользователя монета такого номинала
                            {
                                Console.WriteLine("{0}р внесено.", coin);//подтвержение успешного зачисления монеты
                                cur.Money.AddToList(coin, 1);//добавить моенту на счет текущей транзакции в список имеющихся монет
                                cur.MoneyAdded += coin;//добавить кол-во рублей, которое внес пользователь за покупку
                                currentClient.PutCoin(coin);//изъятие монеты coin из кошелька пользователя
                                cur.AddCoin(coin);//добавление монету на счет текущей транзации по номиналу 
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
                    if (!flag)//попадаем в эту ветвь кода, только если выбрали и успешно оплатили покупку
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Спасибо за покупку!");//покупка успешно совершена. Завершение текущей транзакции
                        Console.WriteLine("Остаток: {0}", cur.Exchange());//вывод информации об остатке от оплаты товара
                        Console.WriteLine();
                        automate.Money.Five += cur.Money.Five;//Перечисление денег за товар на счет автомата со счета..
                        automate.Money.One += cur.Money.One;//..текущей транзакции. Перечисление по монетам разного номинала:..
                        automate.Money.Ten += cur.Money.Ten;//..1, 2, 5, 10 рублей соответственно
                        automate.Money.Two += cur.Money.Two;
                        currentClient.Exchange += cur.Exchange(); //сдача, которая находится еще в автомате
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.WriteLine("Выбранного товара нет в наличии, или вы ошиблись с номером товара.");
                }
                Console.WriteLine("Если вы хотите совершить другую покупку, нажмите любую кнопку. Иначе нажмите \"ф\" для получения сдачи");
                choice = Console.ReadLine();//пользователь совершает выбор
            }
            List<int> coins = currentClient.Money.ExchangeReturn(currentClient.Exchange);//если пользователь захотел получить сдачу, он попадает в эту ветвь кода.
            currentClient.GetExchange(coins);//выдача сдачи пользователю ЭТИ МОНЕТЫ НАДО ВЫЧЕСТЬ У АВТОМАТА!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Сдача выдана");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }


    }
}
