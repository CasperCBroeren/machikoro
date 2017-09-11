using Machikoro.Logic.GameItems.Cards.Epic; 
using Machikoro.Logic.GameItems.Game;
using Machikoro.Logic.GameItems.Player;
using Machikoro.Logic.Service.BankService;
using Machikoro.Logic.Service.CardService;
using Machikoro.Logic.Service.DiceService;
using Shouldly;
using Xunit;

namespace Machikoro.Test
{
    public class GameTests
    {
        [Fact]
        public void WhenAllEpicsAreCollected_NoEndGame()
        {
            var game = new TestGame();
            game.SetDependencies(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));
            
            var player = new TestPlayer(game); 
            var card1 = new TrainStation(player);
            var card2 = new RadioStation(player);
            var card3 = new ThemePark(player); 
            
            var result = game.CheckEndGame();
            result.ShouldBe(false);
        }
        
        [Fact]
        public void WhenAllEpicsAreCollected_EndGame()
        {
            var game = new TestGame();
            game.SetDependencies(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));
            
            var player = new TestPlayer(game); 
            var card1 = new TrainStation(player);
            var card2 = new RadioStation(player);
            var card3 = new ThemePark(player);
            var card4 = new ShoppingMall(player);
             
            var result = game.CheckEndGame();
            result.ShouldBe(true);
        }
    }
}