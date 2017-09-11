using Machikoro.Logic.GameItems.Cards.Epic;
using System;
using System.Linq;
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
            this.SubType = CardSubType.Restauration;
        }

        public override Task<bool> DoEffect()
        {
            if (Owner != CurrentGame.CurrentPlayer)
            {
                var addOne = Owner.Cards.Any(x => x is ShoppingMall) ? 1 : 0;
                CurrentGame.BankService.TransferMoney(2 + addOne, Owner, CurrentGame.CurrentPlayer);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
