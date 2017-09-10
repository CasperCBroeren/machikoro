using System.Linq;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Commercial
{
    public class CheeseFactory : ACard
    {
        public CheeseFactory(IPlayer owner) : base(owner)
        {
            this.Name = "Cheese Factory";
            this.Activation = new int[] { 7 };
            this.CardType = CardType.Commercial;
            this.Cost = 5;
        }

        public override Task<bool> DoEffectAsync()
        {
            if (Owner == CurrentGame.CurrentPlayer)
            {
                var amountOfCoins = 3 * Owner.Cards.Count(x => x.CardType == CardType.Goods && x.SubType == CardSubType.LiveStock);
                CurrentGame.BankService.TransferMoney(amountOfCoins, Owner, null);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
