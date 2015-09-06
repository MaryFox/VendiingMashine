using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class Balance
    {
        public List<int> Coins = new List<int>();
        public int One { get; set; }
        public int Two { get; set; }
        public int Five { get; set; }
        public int Ten { get; set; }
        Random r = new Random();
        public Balance(int balancee)
        {
            CreateCoins(balancee);
        }
        /// <summary>
        /// Рандомное разбиение суммы в рублях на монеты разного номинала
        /// </summary>
        /// <param name="balancee">баланс</param>
        public void CreateCoins(int balancee)
        {
            int theRest = balancee;
            int randomResult=0;
            while (theRest > 0)
            {
                randomResult = Random(10, theRest);
                Ten += randomResult;
                theRest -= 10 * randomResult;
                AddToList(10, randomResult);
                if (theRest > 0)
                {
                    randomResult = Random(5, theRest);
                    Five += randomResult;
                    theRest -= 5 * randomResult;
                    AddToList(5, randomResult);
                }
                if (theRest > 0)
                {
                    randomResult = Random(2, theRest);
                    Two += randomResult;
                    theRest -= 2 * randomResult;
                    AddToList(2, randomResult);
                }
                if (theRest > 0)
                {
                    randomResult = Random(1, theRest);
                    One += randomResult;
                    theRest -= randomResult;
                    AddToList(1, randomResult);
                }
            }
        }
        /// <summary>
        /// Добавить созданные монеты в список монет
        /// </summary>
        /// <param name="s">номинал монеты</param>
        public void AddToList(int nominal, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Coins.Add(nominal);
            }
        }
        /// <summary>
        /// Создание рандомного кол-ва монет необходимого номинала
        /// </summary>
        /// <param name="nominal">номинал монеты</param>
        /// <param name="money">остаток баланса</param>
        public int Random(int nominal, int money)
        {
            int count=0;
            if (money >= nominal)
            {
                count = r.Next(1, money / nominal);
            }
            return count;
        }
        /// <summary>
        /// Вывод на экрна баланса
        /// </summary>
        public void ShowBalance()
        {
            
            Console.WriteLine();
            Console.WriteLine("Баланс: "+ CountAllMoney());
            Console.WriteLine(Ten + " Десяток");
            Console.WriteLine(Five + " Пятерок");
            Console.WriteLine(Two + " Двухрублевых монет");
            Console.WriteLine(One + " Рублевых монет");
        }
        /// <summary>
        /// Выводит сумму денег на счету/в кошельке
        /// </summary>
        public int CountAllMoney()
        {
            int summ = Ten * 10 + Five * 5 + Two * 2 + One;
            return summ;
        }
        /// <summary>
        /// Выдает сдачу наимельним кол-вом монет
        /// </summary>
        /// <param name="exchange">сумма сдачи</param>
        public List<int> ExchangeReturn(int exchange)
        {
            var ExchaneCoins = new List<int>();
            if (exchange!=0)
            {//сначала выдает крупные монеты
                while (exchange >= 10)
                {
                    ExchaneCoins.Add(10);
                    exchange -= 10;
                }
                while (exchange >= 5)
                {
                    ExchaneCoins.Add(5);
                    exchange -= 5;
                }
                while (exchange >= 2)
                {
                    ExchaneCoins.Add(2);
                    exchange -=2;
                }
                while (exchange >= 1)
                {
                    ExchaneCoins.Add(1);
                    exchange -= 1;
                }
            }
            return ExchaneCoins;
        }
    }    
}
