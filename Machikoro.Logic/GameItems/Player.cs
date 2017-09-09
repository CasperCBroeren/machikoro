using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;

namespace Machikoro.Logic.GameItems
{
    public class Player
    {
        public Player(Game game)
        { 
            this.Game = game;
            game.Players.Add(this);
            this.Cards = new List<ACard>();
            game.BankService.TransferMoney(game.StartCoinCount, this, null);
            Id = Guid.NewGuid();
        }
        
        public Guid Id { get; }
        public string Name { get; set; }

        private int _coins = 0;
        public readonly Machikoro.Logic.GameItems.Game Game;

        public int Coins
        {
            get => _coins;
            set => _coins = value < 0 ? 0 : value;
        }

        public List<ACard> Cards { get; }
 

        public async Task ExecuteRound()
        {
            foreach (var card in this.Cards)
            {
                if (card.Activation.Contains(Game.Pips))
                {
                    var result = await card.DoEffect();
                    if (result)
                    { 
                        Game.OnCardActivated(card, this);
                    }
                }
            }
        }
 
    }
}