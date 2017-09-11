using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards.Goods;
using Machikoro.Logic.GameItems.Game;
using Machikoro.Logic.GameItems.Player;
using Machikoro.Logic.Service.BankService;
using Machikoro.Logic.Service.CardService;
using Machikoro.Logic.Service.DiceService;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Machikoro.Test
{
    public class CardServicesTests
    {
        [Fact]
        public async Task BuyAllGrainFields()
        {
            var game = new TestGame();
            var bankService = new BankService(game);
            var cardService = new CardService(game);
            game.SetDependencies(new FixedDice() { FixedNumber= 2}, bankService, cardService);
            var player = new TestPlayer(game);

            for (int i = 0; i< 5;i++)
            {
               await cardService.BuyCard(player, typeof(GrainField));
            }
            game.BankService.BankBalance.ShouldBe(192);
            game.CardService.AmountInStock(typeof(GrainField)).ShouldBe(7);
        }
    }
}