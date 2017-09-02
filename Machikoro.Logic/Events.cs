using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;

namespace Machikoro.Logic
{
    public class Events
    {
        public delegate bool CardActivated(ACard card, Player forPlayer);

        public delegate void CoinsReceived(Player receiver, int amount, Player sender = null);

        public delegate void CoinsDeducted(Player deductedFrom, int amount, Player taker = null);

        public delegate bool DiceThrown(Player thrownBy, int pips);
    }
}