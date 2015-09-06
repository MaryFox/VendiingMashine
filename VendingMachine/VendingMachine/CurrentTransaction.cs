using System;

namespace VendingMachine
{
    public class CurrentTransaction
    {
        public Balance Money { get; set; }
        public int ToPay { get; set; }
        public int MoneyAdded { get; set; }
        public int Exchange { get; set; }

        public CurrentTransaction()
        {
            Money = new Balance(0);
        }
        /// <summary>
        /// Возвращает информацию о сдаче 
        /// </summary>
        public int Exchanger(int clientExchange)
        {
            Exchange = MoneyAdded - ToPay + clientExchange;
            return MoneyAdded - ToPay+clientExchange;
        }
        /// <summary>
        /// Добавление монеты в счет текущей транзакции
        /// </summary>
        /// <param name="coin">номинал монеты</param>
        public void AddCoin(int coin)
        {
            switch (coin)
            {
                case 1:
                    Money.One++;
                    break;
                case 2:
                    Money.Two++;
                    break;
                case 5:
                    Money.Five++;
                    break;
                case 10:
                    Money.Ten++;
                    break;
            }
            Money.Coins.Add(coin);
            
        }
        public void ExchangeUsed()
        {
            //exchange = 0;
        }

    }
}
