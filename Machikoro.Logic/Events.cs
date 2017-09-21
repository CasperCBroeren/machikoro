using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;
using Machikoro.Logic.Service;

namespace Machikoro.Logic
{
    public class Events
    {


        public delegate void CardBought(ACard card, IPlayer forPlayer);

        public delegate void CardSaleFailed(ACard card, IPlayer forPlayer, BuyFailedReason reason);

        public delegate void CardActivated(ACard card, IPlayer forPlayer);

        public delegate void CoinsReceived(IPlayer receiver, int amount, IPlayer sender = null);

        public delegate void CoinsDeducted(IPlayer deductedFrom, int amount, IPlayer taker = null);

        public delegate void DiceThrown(IPlayer thrownBy, int pips);

        public delegate void CardTraded(ACard ownCard, ACard otherCard);

        public delegate void GameEnded(IPlayer player);
    }
}