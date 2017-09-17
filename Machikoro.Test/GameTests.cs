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
            
            var player1 = new TestPlayer(game);
            var player2 = new TestPlayer(game);
            
            game.StartGame(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));
             
            var card1 = new TrainStation(player1);
            var card2 = new RadioStation(player1);
            var card3 = new ThemePark(player1); 
            
            var result = game.CheckEndGame();
            result.ShouldBe(false);
        }
        
        [Fact]
        public void WhenAllEpicsAreCollected_EndGame()
        {
            var game = new TestGame();
            
            var player1 = new TestPlayer(game);  
            var player2 = new TestPlayer(game); 
            
            game.StartGame(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));
            
            var card1 = new TrainStation(player1);
            var card2 = new RadioStation(player1);
            var card3 = new ThemePark(player1);
            var card4 = new ShoppingMall(player1);
             
            var result = game.CheckEndGame();
            result.ShouldBe(true);
        }
    }
}