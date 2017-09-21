using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;
using Machikoro.Logic.GameItems.Cards.Epic;
using Machikoro.Logic.Service;
using Machikoro.Logic.GameItems.Cards.Goods;
using Machikoro.Logic.GameItems.Cards.Commercial;

namespace Machikoro.Logic.GameItems.Game
{
    public class RealGame : IGame
    {
        public IDiceService DiceService { get; private set; }
        public IBankService BankService { get; private set; }
        public ICardService CardService { get; private set; }

        public List<IPlayer> Players { get; }
        public IPlayer CurrentPlayer { get; set; }

        public int StartCoinCount { get; } = 5;

        public void StartGame(IDiceService diceService,
            IBankService bankService,
            ICardService cardService)
        {
            if (Players.Count < 2)
                throw new Exception("Please start the game with 2 or more players (but not more then 4)");
            if (Players.Count > 4)
                throw new Exception("Please start the game with 4 or less players (but no less then 2)");

            DiceService = diceService;
            BankService = bankService;
            CardService = cardService;

            foreach (var player in Players)
            {
                BankService.TransferMoney(StartCoinCount, player, null);
                CardService.BuyCard(player, typeof(GrainField));
                CardService.BuyCard(player, typeof(Bakery));
               
            }
            
        }

        public RealGame()
        {
            this.Players = new List<IPlayer>();
        }

        public async Task<bool> ExecuteRound(bool setNext = true)
        {
            if (setNext)
            {
                SetNextPlayer();
            }

            await ThrowDice();
            

            foreach (var player in Players)
            {
                await player.ExecuteRound();
            }

            await CurrentPlayer.BuyACardAtRound();
            this.CheckEndGame();

            if (this.CurrentPlayer.Cards.Any(x => x is ThemePark))
            {
                if (await this.CurrentPlayer.Cards.First(x => x is ThemePark).DoEffect())
                {
                    await ExecuteRound(false);
                }
            }
            return true;
        }

        public async Task ThrowDice()
        {
            await this.DiceService.GeneratePips(this.CurrentPlayer);
            OnDiceThrown(CurrentPlayer, this.DiceService.CurrentPips);
            if (CurrentPlayer.Cards.Any(x => x is RadioStation))
            {
                if (await CurrentPlayer.Cards.First(x => x is RadioStation).DoEffect())
                {
                    await this.DiceService.GeneratePips(this.CurrentPlayer);
                    OnDiceThrown(CurrentPlayer, this.DiceService.CurrentPips);
                }

            }
        }

        public bool CheckEndGame()
        {
            foreach (var player in this.Players)
            {
                if (player.Cards.Any(x => x is TrainStation)
                    && player.Cards.Any(x => x is RadioStation)
                    && player.Cards.Any(x => x is ShoppingMall)
                    && player.Cards.Any(x => x is ThemePark))
                {
                    OnGameEnded(player);
                    return true;
                }
            }
            return false;
        }

        private void SetNextPlayer()
        {
            if (this.CurrentPlayer == null)
            {
                this.CurrentPlayer = this.Players.First();
                return;
            }

            var newPlayer = this.Players.SkipWhile(x => !x.Equals(this.CurrentPlayer)).Skip(1);
            if (newPlayer != null && newPlayer.Any())
            {
                this.CurrentPlayer = newPlayer.First();
            }
            else
            {
                this.CurrentPlayer = this.Players.First();
            }
        }

        public event Events.DiceThrown DiceThrown;

        public event Events.CardBought CardBought;

        public event Events.CardSaleFailed CardSaleFailed;

        public event Events.CardActivated CardActivated;

        public event Events.CoinsDeducted CoinsDeducted;

        public event Events.CoinsReceived CoinsReceived;

        public event Events.CardTraded CardTraded;

        public event Events.GameEnded GameEnded;

        public void OnGameEnded(IPlayer winner)
        {
            GameEnded?.Invoke(winner);
        }

        public void OnDiceThrown(IPlayer player, int pips)
        {
            DiceThrown?.Invoke(player, pips);
        }

        public void OnCardActivated(ACard card, IPlayer player)
        {
            CardActivated?.Invoke(card, player);
        }

        public void OnCoinsDeducted(IPlayer deductedFrom, int amount, IPlayer taker)
        {
            CoinsDeducted?.Invoke(deductedFrom, amount, taker);
        }

        public void OnCoinsReceived(IPlayer receiver, int amount, IPlayer sender)
        {
            CoinsReceived?.Invoke(receiver, amount, sender);
        }

        public void OnCardTraded(ACard ownCard, ACard otherCard)
        {
            CardTraded?.Invoke(ownCard, otherCard);
        }

        public void OnCardBought(ACard card, IPlayer player)
        {
            CardBought?.Invoke(card, player);
        }

        public void OnCardSaleFailed(ACard card, IPlayer player, BuyFailedReason reason)
        {
            CardSaleFailed?.Invoke(card, player, reason);
        }

       
    }
}