using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;
using System;
using System.Reflection;

namespace Machikoro.Logic.Service.CardService
{
    public class CardService : ICardService
    {
        private IGame game;

        public CardService(IGame game)
        {
            this.game = game;
        }

        public async Task<bool> BuyCard(Player buyForPlayer, Type card) 
        {
            if (!card.GetTypeInfo().IsSubclassOf(typeof(ACard)))
            {
                throw new TypeInitializationException(nameof(card), new Exception("Only Buy objects of type Acard"));
            }

            // TODO: check card in stock
            ACard cardInst = (ACard)Activator.CreateInstance(card, buyForPlayer);
            var transferResult = await game.BankService.TransferMoney(cardInst.Cost, null, buyForPlayer);
            if (transferResult)
            {   
                buyForPlayer.Cards.Add(cardInst);
                return true;
            }

            return false;
        }
    }
}