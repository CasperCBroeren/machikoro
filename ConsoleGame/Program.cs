using Machikoro.Logic.GameItems.Game;
using Machikoro.Logic.GameItems.Player;
using Machikoro.Logic.Service.BankService;
using Machikoro.Logic.Service.CardService;
using Machikoro.Logic.Service.DiceService;
using System;
using System.Linq;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#### Welcome to Machikoro Console game ####");
            var game = new RealGame();

            var player1 = new ConsolePlayer(game);
            var player2 = new TestPlayer(game);

            game.DiceThrown += Game_DiceThrown;
            game.CardActivated += Game_CardActivated; 
            game.CardSaleFailed += Game_CardSaleFailed;
            game.CardBought += Game_CardBought;
            game.GameEnded += Game_GameEnded;
            game.StartGame(new RealDice(),
                new BankService(game),
                new CardService(game));

            while(!game.CheckEndGame())
            {
                game.ExecuteRound().Wait();
                Console.WriteLine("Round end with");
                foreach (var player in game.Players)
                {
                    Console.WriteLine($"{player.GetType().Name} has {player.Coins} coins and {player.Cards.Count(x=>x.CardType == Machikoro.Logic.GameItems.CardType.Epic)} epics");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Machikoro end");
            Console.ReadKey();
        }

        private static void Game_GameEnded(Machikoro.Logic.GameItems.IPlayer player)
        {
            Console.WriteLine($"{player.GetType().Name} won the game! ");
        }

        private static void Game_CardBought(Machikoro.Logic.GameItems.Cards.ACard card, Machikoro.Logic.GameItems.IPlayer forPlayer)
        {
            Console.WriteLine($"Card {card.Name} was bought by {forPlayer.Name} ");
        }

        private static void Game_CardSaleFailed(Machikoro.Logic.GameItems.Cards.ACard card, Machikoro.Logic.GameItems.IPlayer forPlayer, Machikoro.Logic.Service.BuyFailedReason reason)
        {
            Console.WriteLine($"Card {card.Name} failed to buy for {forPlayer.GetType().Name} because {reason}");
        }

        private static void Game_CardActivated(Machikoro.Logic.GameItems.Cards.ACard card, Machikoro.Logic.GameItems.IPlayer forPlayer)
        {
            Console.WriteLine($"Card {card.Name} activated for {forPlayer.GetType().Name}");
        }

        private static void Game_DiceThrown(Machikoro.Logic.GameItems.IPlayer thrownBy, int pips)
        {
            Console.WriteLine($"Dice thrown by {thrownBy.GetType().Name}, the total pips were {pips}"); 
        }
    }
}