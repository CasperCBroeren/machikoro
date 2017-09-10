using System;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Epic
{
    public class ThemePark : ACard
    {
        public ThemePark(IPlayer owner) : base(owner)
        {
            this.Name = "Theme park";
            this.Activation = new int[] { 0 };
            this.CardType = CardType.Epic;
            this.Cost = 16;
        }

        public override Task<bool> DoEffect()
        {
            if (Owner.Game.DiceService.PipsDice1 == Owner.Game.DiceService.PipsDice2)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
