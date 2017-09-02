using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Commercial
{
    public class Bakery :  ACard
    {
        public Bakery(Player owner) : base(owner)
        {
            this.Name = "Bakery";
            this.Activation = new int[] {2, 3};
            this.CardType = CardType.Commercial;
            this.Cost = 1;
        }
        
        public override Task<bool> DoEffect()
        {
            if (Owner == CurrentGame.CurrentPlayer)
            {
                CurrentGame.BankService.TransferMoney(1, Owner, null);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}