using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Goods
{
    public class GrainField : ACard
    {
        public GrainField(Player owner) : base(owner)
        {
            this.Name = "GrainField";
            this.Activation = new int[] {1};
            this.CardType = CardType.Goods;
            this.Cost = 1; 
        }

        public override Task<bool> DoEffect()
        { 
            CurrentGame.BankService.TransferMoney(1, Owner, null);
            
            return Task.FromResult(true);
        }
    }
}