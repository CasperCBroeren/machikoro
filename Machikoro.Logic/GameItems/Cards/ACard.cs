﻿using System.Threading.Tasks;

namespace Machikoro.Logic.GameItems.Cards
{
    public abstract class ACard
    {
        public CardType CardType { get; protected set; }
        public int[] Activation { get; protected set; }
        public string Name { get; protected set; }
        public string Effect { get; protected set; }
        public int Cost { get; protected set; }
        public Player Owner { get; set; }

        public ACard(Player owner)
        {
            this.Owner = owner;
            owner.Cards.Add(this);
        }
        
        public abstract Task<bool> DoEffect();
        public Game CurrentGame => Owner.Game;
    }
}