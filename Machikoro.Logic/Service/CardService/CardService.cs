using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards;
using System;
using System.Reflection;
using System.Collections.Generic;
using Machikoro.Logic.GameItems.Cards.Goods;
using Machikoro.Logic.GameItems.Cards.Catering;
using Machikoro.Logic.GameItems.Cards.Commercial;
using Machikoro.Logic.GameItems.Cards.Epic;
using Machikoro.Logic.GameItems.Cards.Large;

namespace Machikoro.Logic.Service.CardService
{
    public class CardService : ICardService
    {
        private Dictionary<string, int> cardStock = new Dictionary<string, int>();
        
        private IGame game;

        public CardService(IGame game)
        {
            this.game = game;
            cardStock.Add(nameof(GrainField), 10);
            cardStock.Add(nameof(LiveStockFarm), 6);
            cardStock.Add(nameof(Forest), 6);
            cardStock.Add(nameof(Mine), 6);
            cardStock.Add(nameof(Orchard), 6);

            cardStock.Add(nameof(Cafe), 6);
            cardStock.Add(nameof(Restaurant), 6);

            cardStock.Add(nameof(Bakery),10);
            cardStock.Add(nameof(ConvenienceStore), 6);
            cardStock.Add(nameof(VegetableFruitMarket), 6);
            cardStock.Add(nameof(CheeseFactory), 6);
            cardStock.Add(nameof(FurnitureFactory), 6);

            cardStock.Add(nameof(Stadium), 4);
            cardStock.Add(nameof(BusinessComplex), 9);
            cardStock.Add(nameof(TvStation), 4);

            cardStock.Add(nameof(TrainStation), 4);
        }

        public int AmountInStock(Type card)
        {
            return cardStock[card.GetTypeInfo().Name];
        }

        public async Task<bool> BuyCard(IPlayer buyForPlayer, Type card)
        {
            if (!card.GetTypeInfo().IsSubclassOf(typeof(ACard)))
            {
                throw new TypeInitializationException(nameof(card), new Exception("Only Buy objects of type Acard"));
            }

            if (cardStock[card.GetTypeInfo().Name] < 1)
            {
                return false;
            }


            ACard cardInst = (ACard)Activator.CreateInstance(card, buyForPlayer);
            var transferResult = await game.BankService.TransferMoney(cardInst.Cost, null, buyForPlayer);
            if (transferResult)
            {   
                buyForPlayer.Cards.Add(cardInst);
                cardStock[card.GetTypeInfo().Name] -= 1;
                return true;
            }

            return false;
        }

        public Task TradeCardFromOwner(ACard ownCard, ACard otherCard)
        {
            var playerA = ownCard.Owner;
            var playerB = otherCard.Owner;

            playerA.Cards.Remove(ownCard);
            playerB.Cards.Remove(otherCard);

            playerA.Cards.Add(otherCard);
            playerB.Cards.Add(ownCard);

            game.OnCardTraded(ownCard, otherCard);
            return Task.CompletedTask;
        }
    }
}