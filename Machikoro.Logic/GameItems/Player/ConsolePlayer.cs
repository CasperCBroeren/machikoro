using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;
using Machikoro.Logic.GameItems.Cards.Catering;
using Machikoro.Logic.GameItems.Cards.Commercial;
using Machikoro.Logic.GameItems.Cards.Epic;
using Machikoro.Logic.GameItems.Cards.Goods;
using Machikoro.Logic.GameItems.Cards.Large;
using System.Linq;

namespace Machikoro.Logic.GameItems.Player
{
    public class ConsolePlayer : IPlayer
    {
        public List<ACard> Cards { get; }

        public int Coins { get; set; }

        public IGame Game { get; }

        public Guid Id => Guid.NewGuid();

        public string Name { get; set; }

        public ConsolePlayer(IGame game)
        {
            Name = "You";
            this.Game = game;
            game.Players.Add(this);
            this.Cards = new List<ACard>();
        }

        public async Task<bool> BuyACardAtRound()
        {
            var option = AskCardType();
            if (option != null)
            {
                var card = AskCard(option);
                if (card == null)
                {
                    return false;
                }
                else
                { 
                    return await this.Game.CardService.BuyCard(this, card);
                }
            }
            else
            {
                return false;
            }
        }

        private Type AskCard(string option)
        {
            if (option == "c")
            {
                Console.WriteLine("Please choose your catering card? ");
                Console.WriteLine("[c]afe, [r]estaurant or x to cancel");

            }
            if (option == "o")
            {
                Console.WriteLine("Please choose your commercial card? ");
                Console.WriteLine("[b]akery, [c]heese factory, c[o]nvenience store, [f]urniture factory, [v]egetable fruit market or x to cancel");
            }
            if (option == "e")
            {
                Console.WriteLine("Please choose your epic card? ");
                Console.WriteLine("[r]adiostation, [s]hopping mall, [t]heme park, tr[a]in station or x to cancel");
            }
            if (option == "g")
            {
                Console.WriteLine("Please choose your goods card? ");
                Console.WriteLine("[f]orest, [g]rain field, [l]ifestock farm, [m]ine, [o]rchard or x to cancel");
            }
            if (option == "l")
            {
                Console.WriteLine("Please choose your large card? ");
                Console.WriteLine("[b]usiness complex, [s]tadium, [t]v stadium or x to cancel");
            }

            var cardNum = ReadFromConsole();
            if (cardNum == "x")
            {
                return null;
            }
            else
            {
                switch (option + cardNum)
                {
                    case "cc": return typeof(Cafe);
                    case "cr": return typeof(Restaurant);
                    case "ob": return typeof(Bakery);
                    case "oc": return typeof(CheeseFactory);
                    case "oo": return typeof(ConvenienceStore);
                    case "of": return typeof(FurnitureFactory);
                    case "ov": return typeof(VegetableFruitMarket);
                    case "er": return typeof(RadioStation);
                    case "es": return typeof(ShoppingMall);
                    case "et": return typeof(ThemePark);
                    case "ea": return typeof(TrainStation);
                    case "gf": return typeof(Forest);
                    case "gg": return typeof(GrainField);
                    case "gl": return typeof(LiveStockFarm);
                    case "gm": return typeof(Mine);
                    case "go": return typeof(Orchard);
                    case "lb": return typeof(BusinessComplex);
                    case "ls": return typeof(Stadium);
                    case "lt": return typeof(TvStation);
                    default: return AskCard(option);
                }
            }
        }

        private string AskCardType()
        {
            Console.WriteLine("Please choose your card type? ");
            Console.WriteLine("[g]oods, [c]atering, [e]pic, c[o]mmercial, [l]arge or x to cancel");
            string option = ReadFromConsole();
            if (option == "x")
            {
                return null;
            }
            else if (option == "g" || option == "c" || option == "e" || option == "o" || option == "l")
            {
                return option;
            }
            else
            {
                return AskCardType();
            }
        }

        private static string ReadFromConsole()
        {
            var option = Console.ReadKey().KeyChar.ToString();
            Console.Write("\b");
            return option;
        }

        public Task<bool> DoDoubleTrow()
        {
            Console.WriteLine("Do you want to throw the second dice? ");
            Console.WriteLine("[y]es, [n]o");
            var option = ReadFromConsole();
            if (option == "y")
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public async Task ExecuteRound()
        {
            foreach (var card in this.Cards)
            {
                if (card.Activation.Contains(Game.DiceService.CurrentPips))
                {
                    var result = await card.DoEffect();
                    if (result)
                    {
                        Game.OnCardActivated(card, this);
                    }
                }
            }
        }

        public Task<ACard> PickCardOfOwnCollection()
        {
            Console.WriteLine("Please pick a card? ");
            var index = 1;
            foreach (var card in this.Cards)
            {
                Console.WriteLine($"Card[{index}] {card.Name}");
                index++;
            }
            var option = int.Parse(ReadFromConsole());
            try
            {
                return Task.FromResult(Cards.ToArray()[option]);
            }
            catch (Exception)
            {
                return PickCardOfOwnCollection();
            }
        }

        public Task<IPlayer> PickPlayer()
        {
            Console.WriteLine("Please pick a player? ");
            var index = 1;
            foreach(var player in   Game.Players.Where(x=> !x.Equals(this)))
            {
                Console.WriteLine($"Player[{index}]");
                index++;
            }
            var option = int.Parse(ReadFromConsole());
            try
            {
                return Task.FromResult(Game.Players.Where(x => !x.Equals(this)).ToArray()[option]);
            }
            catch(Exception)
            {
                return PickPlayer();
            }
        }

        public Task<bool> RethrowDice()
        {
            Console.WriteLine("Do you want to throw again? ");
            Console.WriteLine("[y]es, [n]o");
            var option = ReadFromConsole();
            if (option == "y")
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> WantsToTradeACard()
        {
            Console.WriteLine("Do you want to trade a card? ");
            Console.WriteLine("[y]es, [n]o");
            var option = ReadFromConsole();
            if (option == "y")
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<ACard> PickCardOfOtherCollection(IPlayer otherPlayer)
        {
            Console.WriteLine("Please pick a card? ");
            var index = 1;
            foreach (var card in otherPlayer.Cards)
            {
                Console.WriteLine($"Card[{index}] {card.Name}");
                index++;
            }
            var option = int.Parse(ReadFromConsole());
            try
            {
                return Task.FromResult(otherPlayer.Cards.ToArray()[option]);
            }
            catch (Exception)
            {
                return PickCardOfOtherCollection(otherPlayer);
            }
        }
    }
}
