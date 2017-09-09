using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using System;

namespace Machikoro.Logic.Service.BankService
{
    public class BankService : IBankService
    {
        private readonly IGame _game;
        public int BankBalance { get; set; }

        public BankService(IGame game)
        {
            this._game = game;
            this.BankBalance = 192;
        }

        public Task<bool> TransferMoney(int amount, Player receiver = null, Player sender = null)
        {
            if (receiver == null && sender == null) throw new ArgumentException("Receiver or sender must be filled");
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
                else
                {
                    return Task.FromResult(false);
                }
            }
            else
            {
                if (BankBalance > 0)
                {
                    BankBalance -= amount;
                    if (BankBalance < 0)
                    {
                        amountToReceive = amount + BankBalance;
                        BankBalance = 0;
                    }
                }
                else
                {
                    amount = 0;

                }
            }

            if (receiver != null)
            {
                receiver.Coins += amountToReceive;
                _game.OnCoinsReceived(receiver, amountToReceive, sender);
            }
            else
            {
                BankBalance += amountToReceive;
            }


            return Task.FromResult(true);

        }
    }
}