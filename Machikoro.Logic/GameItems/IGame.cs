using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;
using Machikoro.Logic.Service;
using System.Collections.Generic;

namespace Machikoro.Logic.GameItems
{
    public interface IGame
    {
        IDiceService DiceService { get; }
        IBankService BankService { get; }
        ICardService CardService { get; }
        List<IPlayer> Players { get;}
        int StartCoinCount { get; }
        IPlayer CurrentPlayer { get; set; }
        bool CheckEndGame();
         

        void OnDiceThrown(IPlayer player, int pips);
        void OnCardActivated(ACard card, IPlayer player);
        void OnCoinsDeducted(IPlayer deductedFrom, int amount, IPlayer taker);
        void OnCoinsReceived(IPlayer receiver, int amount, IPlayer sender);
        void OnCardTraded(ACard ownCard, ACard otherCard);
        void OnCardBought(ACard card, IPlayer player);
        void OnCardSaleFailed(ACard card, IPlayer player, BuyFailedReason reason);
        void OnGameEnded(IPlayer winner);
    }
}