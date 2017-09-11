using Machikoro.Logic.GameItems.Cards.Epic;
using System.Linq;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Commercial
{
    public class Bakery :  ACard
    {
        public Bakery(IPlayer owner) : base(owner)
        {
            this.Name = "Bakery";
            this.Activation = new int[] {2, 3};
            this.CardType = CardType.Commercial;
            this.Cost = 1;
            this.SubType = CardSubType.Shop;
        }
        
        public override Task<bool> DoEffect()
        {
            if (Owner == CurrentGame.CurrentPlayer)
            {
                var addOne = Owner.Cards.Any(x => x is ShoppingMall) ? 1 : 0;
                CurrentGame.BankService.TransferMoney(1 + addOne, Owner, null);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}