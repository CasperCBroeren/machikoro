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
        Guid Id { get;  }

        Task ExecuteRound();
        Task<ACard> PickCardOfOtherCollection();
        Task<ACard> PickCardOfOwnCollection();
        Task<bool> BuyACardAtRound();
        Task<bool> RethrowDice();
        Task<bool> DoDoubleTrow();
        Task<bool> TradeACard();        
        Task<IPlayer> PickPlayer();
    }
}