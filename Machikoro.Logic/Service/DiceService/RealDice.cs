﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Machikoro.Logic.GameItems;
using Machikoro.Logic.GameItems.Cards.Epic;

namespace Machikoro.Logic.Service.DiceService
{
    public class RealDice : IDiceService
    {
        public Random Random { get; set; }
        public RealDice()
        {
            Random = new Random(System.Environment.TickCount);
        }

        public int CurrentPips => PipsDice1 + (PipsDice2 ?? 0);
        public int PipsDice1 { get; private set; }
        public int? PipsDice2 { get; private set; }

        public async Task<int> GeneratePips(IPlayer currentPlayer)
        {
            var pipsTotal = 5;
            PipsDice1 = Random.Next(pipsTotal) + 1;
            if (currentPlayer.Cards.Any(x => x is TrainStation))
            {
                if (await currentPlayer.Cards.First(x => x is TrainStation).DoEffect())
                {
                    PipsDice2 = Random.Next(pipsTotal) + 1;
                }
            }


            return CurrentPips;
        }
    }
}