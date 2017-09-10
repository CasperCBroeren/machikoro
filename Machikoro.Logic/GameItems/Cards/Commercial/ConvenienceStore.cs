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
        }
        
        public override Task<bool> DoEffectAsync()
        {
            if (Owner == CurrentGame.CurrentPlayer)
            {
                CurrentGame.BankService.TransferMoney(3, Owner, null);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}