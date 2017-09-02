using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;
using Machikoro.Logic.Service;
using Machikoro.Logic.Service.BankService;

namespace Machikoro.Logic.GameItems
{
    public class Game : IGame
    {
        public IDiceService DiceService { get; private set; }
        public IBankService BankService { get; private set; }
        public ICardService CardService { get; private set; }
        
        private int _pips; 
        public List<Player> Players { get;  }
        public Player CurrentPlayer { get; set; }
        public int Pips
        {
            get => _pips;
        }
        public int StartCoinCount { get; } = 3;

        public void SetDependencies(IDiceService diceService,
            IBankService bankService,
            ICardService cardService)
        {
            DiceService = diceService;
            BankService = bankService;
            CardService = cardService;
        }

        public Game()
        {
           this.Players = new List<Player>();
        }

        public async Task<bool> ExecuteRound()
        {
            SetNextPlayer();
            
            await ThrowDice();
            
            foreach (var player in Players)
            {
                await player.ExecuteRound();   
            }
            
            return true;
        }

        public async Task ThrowDice()
        {
            _pips = await this.DiceService.GeneratePips(this.CurrentPlayer);
            OnDiceThrown(CurrentPlayer, _pips);
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

        public void OnDiceThrown(Player player, int pips)
        {
            DiceThrown?.Invoke(player, pips);
        }
        
        public void OnCardActivated(ACard card, Player player)
        {
            CardActivated?.Invoke(card, player);
        }

        public void OnCoinsDeducted(Player deductedFrom, int amount, Player taker)
        {
            CoinsDeducted?.Invoke(deductedFrom, amount, taker);
        }
        
        public void OnCoinsReceived(Player receiver, int amount, Player sender)
        {
            CoinsReceived?.Invoke(receiver, amount, sender);
        }
    }
}