using System;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service.DiceService
{
    public class FixedDice : IDiceService 
    {
        public int FixedNumber { get; set; }

        public int CurrentPips => FixedNumber;

        public int PipsDice1 => FixedNumber;

        public int? PipsDice2 => null;

        public Task<int> GeneratePips(IPlayer currentPlayer)
        {
            return Task.FromResult(FixedNumber);
        }
    }
}