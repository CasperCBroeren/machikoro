using System;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Catering
{
    public class Restaurant : ACard
    {
        public Restaurant(IPlayer owner) : base(owner)
        {
            this.Name = "Restaurant";
            this.Activation = new int[] { 9, 10 };
            this.CardType = CardType.Catering;
            this.Cost = 3;
        }

        public override Task<bool> DoEffectAsync()
        {
            if (Owner != CurrentGame.CurrentPlayer)
            {
                CurrentGame.BankService.TransferMoney(2, Owner, CurrentGame.CurrentPlayer);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
