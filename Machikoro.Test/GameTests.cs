using Machikoro.Logic.GameItems.Cards.Epic; 
using Machikoro.Logic.GameItems.Game;
using Machikoro.Logic.GameItems.Player;
using Machikoro.Logic.Service.BankService;
using Machikoro.Logic.Service.CardService;
using Machikoro.Logic.Service.DiceService;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Machikoro.Test
{
    public class GameTests
    {
        [Fact]
        public async Task WhenAllEpicsAreCollected_NoEndGame()
        {
            var game = new TestGame();
            
            var player1 = new TestPlayer(game);
            var player2 = new TestPlayer(game);
            
            game.StartGame(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));

            await game.CardService.BuyCard(player1, typeof(TrainStation));
            await game.CardService.BuyCard(player1, typeof(RadioStation));
            await game.CardService.BuyCard(player1, typeof(ThemePark));

            var result = game.CheckEndGame();
            result.ShouldBe(false);
        }
        
        [Fact]
        public async Task WhenAllEpicsAreCollected_EndGame()
        {
            var game = new TestGame();
            game.StartCoinCount = 100;
            var player1 = new TestPlayer(game);  
            var player2 = new TestPlayer(game); 
            
            game.StartGame(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));

            await game.CardService.BuyCard(player1, typeof(TrainStation));
            await game.CardService.BuyCard(player1, typeof(RadioStation));
            await game.CardService.BuyCard(player1, typeof(ThemePark));
            await game.CardService.BuyCard(player1, typeof(ShoppingMall));

            var result = game.CheckEndGame();
            result.ShouldBe(true);
        }
    }
}