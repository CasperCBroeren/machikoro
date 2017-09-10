using System.Linq;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Large
{
    public class Stadium : ACard
    {
        public Stadium(IPlayer owner) : base(owner)
        {
            this.Name = "Stadium";
            this.Activation = new int[] {6};
            this.CardType = CardType.Commercial;
            this.Cost = 6;
        }
        
        public override Task<bool> DoEffectAsync()
        {
            foreach (var otherPlayer in CurrentGame.Players.Where(x=> !x.Equals(Owner)))
            {
                CurrentGame.BankService.TransferMoney(2, Owner, otherPlayer); 
            }
            
            return Task.FromResult(true);
        }
    }
}