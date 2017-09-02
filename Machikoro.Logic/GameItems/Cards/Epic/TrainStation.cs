using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Epic
{
    public class TrainStation : ACard
    {
        public TrainStation(Player owner) : base(owner)
        {
            this.Name = "Train station";
            this.Activation = new int[] {0};
            this.CardType = CardType.Epic;
            this.Cost = 4;
        }
        
        public override Task<bool> DoEffect()
        {
            
            return Task.FromResult(true);
        }
    }
}