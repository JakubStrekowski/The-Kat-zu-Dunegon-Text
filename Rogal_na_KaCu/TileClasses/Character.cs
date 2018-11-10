﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogal_na_KaCu.TileClasses;

namespace Rogal_na_KaCu
{
    public abstract class Character:Tile
    {
        public int hp;
        protected int attack;
        protected int armor;
        public string name;
        public Tile standingOnTile;

        public virtual void GetDmg(int value)
        {
            hp = hp - (value - armor);
            if (hp <= 0) Die();
        }
      
        void Attack() { }

        protected virtual void Die() {
            currentMap.DestroyCharacter(positionX, positionY);
        }
        
        public Character(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            standingOnTile = TileFactory.Get(0, posX, posY, mp);
            passable = false;
            
        }

        public virtual void SetCurrentMap(Map crMap)
        {
            currentMap = crMap;
        }

       
        
    }
}
