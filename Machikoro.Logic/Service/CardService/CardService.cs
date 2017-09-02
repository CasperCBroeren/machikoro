using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;

namespace Machikoro.Logic.Service.CardService
{
    public class CardService : ICardService
    {
        public Task BuyCard(Player buyForPlayer, ACard card)
        {
            return Task.CompletedTask;
        }
    }
}