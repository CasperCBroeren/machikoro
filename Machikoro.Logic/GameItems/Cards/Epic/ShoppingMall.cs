using System;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Epic
{
    public class ShoppingMall : ACard
    {
        public ShoppingMall(IPlayer owner) : base(owner)
        {
            this.Name = "Shopping mall";
            this.Activation = new int[] { 0 };
            this.CardType = CardType.Epic;
            this.Cost = 16;
        }

        public override Task<bool> DoEffect()
        {
            // this effect is present in coffee and store sub type cards
            return Task.FromResult(true);
        }
    }
}
