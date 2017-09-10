using System.Threading.Tasks;
using System.Linq;

namespace Machikoro.Logic.GameItems.Cards.Commercial
{
    public class VegetableFruitMarket :  ACard
    {
        public VegetableFruitMarket(IPlayer owner) : base(owner)
        {
            this.Name = "Vegetable and fruit market";
            this.Activation = new int[] {11, 12};
            this.CardType = CardType.Commercial;
            this.Cost = 2;
        }
        
        public override Task<bool> DoEffectAsync()
        {
            if (Owner == CurrentGame.CurrentPlayer)
            {
                var amountOfCoins = 3 * Owner.Cards.Count(x => x.CardType == CardType.Goods && x.SubType == CardSubType.FruitVegetableGrain);
                CurrentGame.BankService.TransferMoney(amountOfCoins, Owner, null);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}