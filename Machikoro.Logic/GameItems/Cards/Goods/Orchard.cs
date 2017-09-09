
using System;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Goods
{
    public class Orchard : ACard
    {
        public Orchard(Player owner) : base(owner)
        {
            this.Name = "Orchard";
            this.Activation = new int[] { 10 };
            this.CardType = CardType.Goods;
            this.Cost = 3;
            this.SubType = CardSubType.FruitVegetableGrain;
        }

        public override Task<bool> DoEffect()
        {
            CurrentGame.BankService.TransferMoney(3, Owner, null);

            return Task.FromResult(true);
        }
    }
}
