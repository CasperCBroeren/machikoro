using Machikoro.Logic.GameItems.Cards;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems
{
    public interface IPlayer
    {
        List<ACard> Cards { get;  }
        int Coins { get; set; }
        IGame Game { get;   }
        string Name { get; set; }

        Task ExecuteRound();
        Task<ACard> PickCardOfOtherCollection(IPlayer player);
        Task<ACard> PickCardOfOwnCollection();
        Task<bool> BuyACardAtRound();
        Task<bool> RethrowDice();
        Task<bool> DoDoubleTrow();
        Task<bool> WantsToTradeACard(); 
        Task<IPlayer> PickPlayer();
    }
}