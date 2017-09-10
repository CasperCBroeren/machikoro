using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Goods
{
    public class GrainField : ACard
    {
        public GrainField(IPlayer owner) : base(owner)
        {
            this.Name = "Grain field";
            this.Activation = new int[] {1};
            this.CardType = CardType.Goods;
            this.Cost = 1;
            this.SubType = CardSubType.FruitVegetableGrain;
        }

        public override Task<bool> DoEffect()
        { 
            CurrentGame.BankService.TransferMoney(1, Owner, null);
            
            return Task.FromResult(true);
        }
    }
}