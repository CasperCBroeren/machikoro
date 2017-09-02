using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service.BankService
{
    public class BankService : IBankService
    {
        private readonly IGame _game;
        public int MoneyInTheBank { get; set; }

        public BankService(IGame game)
        {
            this._game = game;
            this.MoneyInTheBank = 150;
        }

        public Task TransferMoney(Player receiver, int amount, Player sender = null)
        {
            var amountToReceive = amount;
            if (sender != null)
            {
                if (sender.Coins > 0)
                {
                    sender.Coins -= amount;
                    if (sender.Coins < 0)
                    {
                        amountToReceive = amount + sender.Coins;
                        sender.Coins = 0;
                    }
                }
                else
                {
                    amountToReceive = 0;
                }

                if (amountToReceive > 0)
                {
                    _game.OnCoinsDeducted(sender, amountToReceive, receiver);
                }
            }
            else
            {
                if (MoneyInTheBank > 0)
                {
                    MoneyInTheBank -= amount;
                    if (MoneyInTheBank < 0)
                    {
                        amountToReceive = amount + MoneyInTheBank;
                        MoneyInTheBank = 0;
                    }
                }
                else
                {
                    amount = 0;
                }
            }
            
            receiver.Coins += amountToReceive;
            _game.OnCoinsReceived(receiver, amountToReceive, sender);
            return Task.CompletedTask;
        }
    }
}