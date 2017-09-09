using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Catering
{
    public class Cafe : ACard
    {
        public Cafe(Player owner) : base(owner)
        {
            this.Name = "Cafe";
            this.Activation = new int[] {3};
            this.CardType = CardType.Catering;
            this.Cost = 2;
        }
        
        public override Task<bool> DoEffect()
        {
            if (Owner != CurrentGame.CurrentPlayer)
            { 
                CurrentGame.BankService.TransferMoney(1, Owner, CurrentGame.CurrentPlayer);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}