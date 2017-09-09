using System.Linq;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Commercial
{
    public class FurnitureFactory : ACard
    {
        public FurnitureFactory(Player owner) : base(owner)
        {
            this.Name = "Furniture Factory";
            this.Activation = new int[] { 8 };
            this.CardType = CardType.Commercial;
            this.Cost = 3;
        }

        public override Task<bool> DoEffect()
        {
            if (Owner == CurrentGame.CurrentPlayer)
            {
                var amountOfCoins = 3 * Owner.Cards.Count(x => x.CardType == CardType.Goods && x.SubType == CardSubType.BuildingMaterial);
                CurrentGame.BankService.TransferMoney(amountOfCoins, Owner, null);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
