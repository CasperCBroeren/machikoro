using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;
using Machikoro.Logic.Service;

namespace Machikoro.Logic.GameItems.Game
{
    public class TestGame : IGame
    {
        public IDiceService DiceService { get; private set; }
        public IBankService BankService { get; private set; }
        public ICardService CardService { get; private set; }
         
        public List<IPlayer> Players { get;  }
        public IPlayer CurrentPlayer { get; set; }
    
        public int StartCoinCount { get; } = 3;

        public void SetDependencies(IDiceService diceService,
            IBankService bankService,
            ICardService cardService)
        {
            DiceService = diceService;
            BankService = bankService;
            CardService = cardService;
        }

        public TestGame()
        {
           this.Players = new List<IPlayer>();
        }

        public async Task<bool> ExecuteRound()
        {
            SetNextPlayer();
            
            await ThrowDice();
            
            foreach (var player in Players)
            {
                await player.ExecuteRound();   
            }
            await CurrentPlayer.BuyACardAtRound();
            return true;
        }

        public async Task ThrowDice()
        {
            await this.DiceService.GeneratePips(this.CurrentPlayer);
            OnDiceThrown(CurrentPlayer, this.DiceService.CurrentPips);
        }

        private void SetNextPlayer()
        {
            if (this.CurrentPlayer == null)
            {
                this.CurrentPlayer = this.Players.First();
                return;
            }
            
            var newPlayer = this.Players.SkipWhile(x => !x.Id.Equals(this.CurrentPlayer.Id)).Skip(1);
            if (newPlayer != null && newPlayer.Any())
            {
                this.CurrentPlayer = newPlayer.First();
            }
            else
            {
                this.CurrentPlayer = this.Players.First();
            }
        }

        public event Events.DiceThrown        DiceThrown;
        public event Events.CardActivated     CardActivated;
        public event Events.CoinsDeducted     CoinsDeducted;
        public event Events.CoinsReceived     CoinsReceived;
        public event Events.CardTraded        CardTraded;

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
    }
}