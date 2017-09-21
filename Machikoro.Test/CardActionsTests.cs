using System;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards.Catering;
using Machikoro.Logic.GameItems.Cards.Commercial;
using Machikoro.Logic.GameItems.Cards.Goods;
using Machikoro.Logic.GameItems.Cards.Large;
using Machikoro.Logic.Service.BankService;
using Machikoro.Logic.Service.CardService;
using Machikoro.Logic.Service.DiceService;
using Shouldly;
using Xunit;
using Machikoro.Logic.GameItems.Game;
using Machikoro.Logic.GameItems.Player;

namespace Machikoro.Test
{
    public class CardActionsTests
    {
        [Fact]
        public async Task  GrainField_NoPips()
        {
            var game = new TestGame();
            
            var player1 = new TestPlayer(game);  
            var player2 = new TestPlayer(game); 
            
            game.StartGame(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));
            
            var card = new GrainField(player1);
            await game.ExecuteRound();
            player1.Coins.ShouldBe(3);
        }
        
        [Fact]
        public async Task  GrainField_ShouldAdd_1()
        {
            var game = new TestGame();
            
            var player1 = new TestPlayer(game); 
            var player2 = new TestPlayer(game);
             
            game.StartGame(new FixedDice() {FixedNumber = 1},
                                new BankService(game),
                                new CardService(game));


            await game.CardService.BuyCard(player1, typeof(GrainField));
            await game.ExecuteRound();
            player1.Coins.ShouldBe(3);
        }
        
        [Fact]
        public async Task Cafe_ShouldAdd_1_and_Deduct_2()
        {
            var game = new TestGame();
            game.StartCoinCount = 4;
            var player1 = new TestPlayer(game) {Name = "player1"};
            var player2 = new TestPlayer(game) {Name = "player2"};
            var player3 = new TestPlayer(game) {Name = "player3"};
            
            game.StartGame(new FixedDice() {FixedNumber = 3},
                new BankService(game),
                new CardService(game));


            game.CurrentPlayer = player1;
            await game.CardService.BuyCard(player1, typeof(Cafe));
            await game.CardService.BuyCard(player3, typeof(Cafe));
            await  game.ExecuteRound();
            player1.Coins.ShouldBe(3);
            player2.Coins.ShouldBe(2);
            player3.Coins.ShouldBe(3);
        }
        
        [Fact]
        public async Task  Bakery_ShouldAdd_1()
        {
            var game = new TestGame();
            game.StartCoinCount = 4;
            var player1 = new TestPlayer(game);
            var player2 = new TestPlayer(game); 
            
            game.StartGame(new FixedDice() {FixedNumber = 2},
                new BankService(game),
                new CardService(game));

            await game.CardService.BuyCard(player1, typeof(Bakery));
            await game.ExecuteRound();
            player1.Coins.ShouldBe(4); 
        }
        
        [Fact]
        public async Task  Stadium_ShouldAdd_2()
        {
            var game = new TestGame();
            game.StartCoinCount = 6;
            var player1 = new TestPlayer(game); 
            var player2 = new TestPlayer(game); 
            var player3 = new TestPlayer(game);
            
            game.StartGame(new FixedDice() {FixedNumber = 6},
                new BankService(game),
                new CardService(game));

            await game.CardService.BuyCard(player1, typeof(Stadium));
            await game.ExecuteRound();
            player1.Coins.ShouldBe(4);
            player2.Coins.ShouldBe(4); 
            player3.Coins.ShouldBe(4);
        }
    }
}