using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class Client
    {
        public Balance Money { get; private set; }
        /// <summary>
        /// Хранит информацию об остатке
        /// </summary>
        public int Exchange { get; set; }
        public Client()
        {
            Money = new Balance(150);
            
        }
        /// <summary>
        /// Проверка наличия у пользователя монеты введенного номинала
        /// </summary>
        /// <param name="coin">номинал монеты</param>
        public bool CheckTheCoin(int coin)
        {
            for (int i = 0; i < Money.Coins.Count(); i++)
            {
                if (Money.Coins[i] == coin)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Изъятие монеты из кошелька пользователя
        /// </summary>
        /// <param name="coin">номинал монеты</param>
        public void PutCoin(int coin)
        {
            switch (coin)
            {
                case 1:
                    Money.One--;
                    break;
                case 2:
                    Money.Two--;
                    break;
                case 5:
                    Money.Five--;
                    break;
                case 10:
                    Money.Ten--;
                    break;
            }

            Money.Coins.RemoveAt(Money.Coins.FindLastIndex(x => x == coin));

        }
        /// <summary>
        /// Выдача сдачи пользователю
        /// </summary>
        /// <param name="coins">список выданных пользователю монет</param>
        public void GetExchange(List<int> coins)
        {
            foreach (var coin in coins)
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
            }
        }
        public void ExchangeUsed()
        {
            Exchange = 0;
        }
        public bool IfClientHasMoney()
        {
            if (Money.One != 0 || Money.Two != 0 || Money.Five != 0 || Money.Ten != 0)
                return true;
            return false;
        }
    }
}
