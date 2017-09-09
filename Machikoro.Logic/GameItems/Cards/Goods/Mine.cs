using System;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Goods
{
    public class Mine : ACard
    {
        public Mine(Player owner) : base(owner)
        {
            this.Name = "Mine";
            this.Activation = new int[] { 9 };
            this.CardType = CardType.Goods;
            this.Cost = 6;
            this.SubType = CardSubType.BuildingMaterial;
        }

        public override Task<bool> DoEffect()
        {
            CurrentGame.BankService.TransferMoney(5, Owner, null);

            return Task.FromResult(true);
        }
    }
}
