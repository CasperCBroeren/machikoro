using Machikoro.Logic.GameItems.Cards.Epic;
using System.Linq;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Catering
{
    public class Cafe : ACard
    {
        public Cafe(IPlayer owner) : base(owner)
        {
            this.Name = "Cafe";
            this.Activation = new int[] {3};
            this.CardType = CardType.Catering;
            this.Cost = 2;
            this.SubType = CardSubType.Restauration;
        }
        
        public override Task<bool> DoEffect()
        {
            if (Owner != CurrentGame.CurrentPlayer)
            {
                var addOne = Owner.Cards.Any(x => x is ShoppingMall) ? 1 : 0;
                CurrentGame.BankService.TransferMoney(1 + addOne, Owner, CurrentGame.CurrentPlayer);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}