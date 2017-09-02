using Machikoro.Logic.GameItems;
using Machikoro.Logic.Service.BankService;
using Machikoro.Logic.Service.CardService;

namespace Machikoro.Test
{
    public class CardServicesTests
    {
        public void BuyAllGrainFields()
        {
            var game = new Game();
            var bankService = new BankService(game);
            var cardService = new CardService();
        }
    }
}