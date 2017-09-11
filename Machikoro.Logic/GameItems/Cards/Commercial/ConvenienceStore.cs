using Machikoro.Logic.GameItems.Cards.Epic;
using System.Linq;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Commercial
{
    public class ConvenienceStore :  ACard
    {
        public ConvenienceStore(IPlayer owner) : base(owner)
        {
            this.Name = "Convenience store";
            this.Activation = new int[] {4};
            this.CardType = CardType.Commercial;
            this.Cost = 2;
            this.SubType = CardSubType.Shop;
        }
        
        public override Task<bool> DoEffect()
        {
            if (Owner == CurrentGame.CurrentPlayer)
            {
                var addOne = Owner.Cards.Any(x => x is ShoppingMall) ? 1 : 0;
                CurrentGame.BankService.TransferMoney(3 + addOne, Owner, null);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}