using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Goods
{
    public class Forest : ACard
    {
        public Forest(IPlayer owner) : base(owner)
        {
            this.Name = "Forest";
            this.Activation = new int[] { 5 };
            this.CardType = CardType.Goods;
            this.Cost = 3;
            this.SubType = CardSubType.BuildingMaterial;
        }

        public override Task<bool> DoEffectAsync()
        {
            CurrentGame.BankService.TransferMoney(1, Owner, null);

            return Task.FromResult(true);
        }
    }
}
