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
            game.SetDependencies(new FixedDice() {FixedNumber = 0},
                new BankService(game),
                new CardService(game));
            
            var player = new TestPlayer(game); 
            var card = new GrainField(player);
            await game.ExecuteRound();
            player.Coins.ShouldBe(3);
        }
        
        [Fact]
        public async Task  GrainField_ShouldAdd_1()
        {
            var game = new TestGame();
            game.SetDependencies(new FixedDice() {FixedNumber = 1},
                                new BankService(game),
                                new CardService(game));
             
            var player = new TestPlayer(game);

            var card = new GrainField(player);
            await game.ExecuteRound();
            player.Coins.ShouldBe(4);
        }
        
        [Fact]
        public async Task Cafe_ShouldAdd_1_and_Deduct_1()
        {
            var game = new TestGame();
            game.SetDependencies(new FixedDice() {FixedNumber = 3},
                new BankService(game),
                new CardService(game));

            var player1 = new TestPlayer(game) {Name = "player1"};
            var player2 = new TestPlayer(game) {Name = "player2"};
            var player3 = new TestPlayer(game) {Name = "player3"};

            game.CurrentPlayer = player3;
            var card = new Cafe(player2);
            await  game.ExecuteRound();
            player1.Coins.ShouldBe(2);
            player2.Coins.ShouldBe(4);
            player3.Coins.ShouldBe(3);
        }
        
        [Fact]
        public async Task  Bakery_ShouldAdd_1()
        {
            var game = new TestGame();
            game.SetDependencies(new FixedDice() {FixedNumber = 2},
                new BankService(game),
                new CardService(game));
            
            var player1 = new TestPlayer(game);
            game.CurrentPlayer = player1;
            var card = new Bakery(player1);
            await game.ExecuteRound();
            player1.Coins.ShouldBe(4); 
        }
        
        [Fact]
        public async Task  Stadium_ShouldAdd_2()
        {
            var game = new TestGame();
            game.SetDependencies(new FixedDice() {FixedNumber = 6},
                new BankService(game),
                new CardService(game));
            
            var player1 = new TestPlayer(game); 
            var player2 = new TestPlayer(game); 
            var player3 = new TestPlayer(game);
            game.CurrentPlayer = player1;
            var card = new Stadium(player1);
            await game.ExecuteRound();
            player1.Coins.ShouldBe(7);
            player2.Coins.ShouldBe(1); 
            player3.Coins.ShouldBe(1);
        }
    }
}