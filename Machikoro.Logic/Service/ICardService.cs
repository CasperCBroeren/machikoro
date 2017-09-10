using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;
using System;

namespace Machikoro.Logic.Service
{
    public interface ICardService
    {
        Task<bool> BuyCard(IPlayer buyForPlayer, Type card);
        int AmountInStock(Type card);
        Task TradeCardFromOwner(ACard ownCard, ACard otherCard);
    }
}