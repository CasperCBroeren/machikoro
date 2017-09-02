using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;
using System;

namespace Machikoro.Logic.Service
{
    public interface ICardService
    {
        Task<bool> BuyCard(Player buyForPlayer, Type card);
    }
}