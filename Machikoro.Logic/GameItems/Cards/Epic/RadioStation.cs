using System;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Epic
{
    public class RadioStation : ACard
    {
        public RadioStation(IPlayer owner) : base(owner)
        {
            this.Name = "Radio station";
            this.Activation = new int[] { 0 };
            this.CardType = CardType.Epic;
            this.Cost = 22; 
        }

        public override async Task<bool> DoEffect()
        {
            return await Owner.RethrowDice();
        }
    }
}
