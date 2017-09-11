using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards.Epic; 
using Machikoro.Logic.GameItems.Game;
using Machikoro.Logic.Service.BankService;
using Machikoro.Logic.Service.CardService;
using Machikoro.Logic.Service.DiceService;

namespace Machikoro.Test
{
    public class GameTests
    {
        public void WhenAllEpicsAreCollected_EndGame()
        {
            var game = new TestGame();
            game.SetDependencies(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));
            
            var player = new Player(game); 
            var card1 = new TrainStation(player);
            game.CheckEndGame();
        }
    }
}