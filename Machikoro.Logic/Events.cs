using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;

namespace Machikoro.Logic
{
    public class Events
    {
        

        public delegate bool CardActivated(ACard card, IPlayer forPlayer);

        public delegate void CoinsReceived(IPlayer receiver, int amount, IPlayer sender = null);

        public delegate void CoinsDeducted(IPlayer deductedFrom, int amount, IPlayer taker = null);

        public delegate bool DiceThrown(IPlayer thrownBy, int pips);

        public delegate bool CardTraded(ACard ownCard, ACard otherCard);
    }
}