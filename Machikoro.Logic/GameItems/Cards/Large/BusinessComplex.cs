using System;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Large
{
    public class BusinessComplex : ACard
    {
        public BusinessComplex(IPlayer owner) : base(owner)
        {
            this.Name = "Business complex";
            this.Activation = new int[] { 6 };
            this.CardType = CardType.Large;
            this.Cost = 8;
        }

        public override async Task<bool> DoEffect()
        {
            return await Owner.TradeACard();
        }
    }
}
