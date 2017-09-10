using System;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service.DiceService
{
    public class FixedDice : IDiceService 
    {
        public int FixedNumber { get; set; }

        public int CurrentPips => FixedNumber;

        public Task<int> GeneratePips(IPlayer currentPlayer)
        {
            return Task.FromResult(FixedNumber);
        }
    }
}