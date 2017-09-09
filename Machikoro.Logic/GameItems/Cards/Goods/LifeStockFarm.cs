using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Goods
{
    public class LiveStockFarm : ACard
    {
        public LiveStockFarm(Player owner) : base(owner)
        {
            this.Name = "Livestock farm";
            this.Activation = new int[] { 2 };
            this.CardType = CardType.Goods;
            this.Cost = 1;
            this.SubType = CardSubType.LiveStock;
        }

        public override Task<bool> DoEffect()
        {
            CurrentGame.BankService.TransferMoney(1, Owner, null);

            return Task.FromResult(true);
        }
    }
}
