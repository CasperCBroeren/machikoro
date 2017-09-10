using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems.Cards;

namespace Machikoro.Logic.GameItems
{
    public class Player : IPlayer
    {
        public Player(IGame game)
        { 
            this.Game = game;
            game.Players.Add(this);
            this.Cards = new List<ACard>();
            game.BankService.TransferMoney(game.StartCoinCount, this, null);
            Id = Guid.NewGuid();
        }

        public async Task<bool> TradeACard()
        {
            var ownCard = await PickCardOfOwnCollection();
            if (ownCard != null)
            {
                var otherCard = await PickCardOfOtherCollection();
                await Game.CardService.TradeCardFromOwner(ownCard, otherCard);
                return true;
            }
            return false;
        }

        public Task<ACard> PickCardOfOtherCollection()
        {
            throw new NotImplementedException();
        }

        public Task<ACard> PickCardOfOwnCollection()
        {
            throw new NotImplementedException();
        }

        public Task<bool> BuyACardAtRound()
        {
            return Task.FromResult(false);
        }

        public Guid Id { get; }
        public string Name { get; set; }

        private int _coins = 0;
        public IGame Game { get;  }

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
                if (card.Activation.Contains(Game.DiceService.CurrentPips))
                {
                    var result = await card.DoEffectAsync();
                    if (result)
                    { 
                        Game.OnCardActivated(card, this);
                    }
                }
            }
          
        }

        public Task<IPlayer> PickPlayer()
        {
            return Task.FromResult(Game.Players.Where(x => x.Id != this.Id).FirstOrDefault());
        }
    }
}