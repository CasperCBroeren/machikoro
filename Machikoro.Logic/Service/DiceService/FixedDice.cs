using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service.DiceService
{
    public class FixedDice : IDiceService 
    {
        public int FixedNumber { get; set; }
        public Task<int> GeneratePips(Player currentPlayer)
        {
            return Task.FromResult(FixedNumber);
        }
    }
}