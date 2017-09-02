using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;

namespace Machikoro.Logic.Service
{
    public interface ICardService
    {
        Task BuyCard(Player buyForPlayer, ACard card);
    }
}