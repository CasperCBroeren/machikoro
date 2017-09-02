using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;

namespace Machikoro.Logic.GameItems
{
    public interface IGame
    {
        Task ThrowDice();
        void OnDiceThrown(Player player, int pips);
        void OnCardActivated(ACard card, Player player);
        void OnCoinsDeducted(Player deductedFrom, int amount, Player taker);
        void OnCoinsReceived(Player receiver, int amount, Player sender);
    }
}