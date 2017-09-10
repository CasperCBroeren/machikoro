using System.Linq;
using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards.Large
{
    public class TvStation : ACard
    {
        public TvStation(IPlayer owner) : base(owner)
        {
            this.Name = "TV Station";
            this.Activation = new int[] {6};
            this.CardType = CardType.Commercial;
            this.Cost = 7;
        }
        
        public override async Task<bool> DoEffectAsync()
        {
            var otherPlayer = await Owner.PickPlayer();
            if (otherPlayer != null)
            {
                await CurrentGame.BankService.TransferMoney(2, Owner, otherPlayer);
                return true;
            }
            return false;
        }
    }
}