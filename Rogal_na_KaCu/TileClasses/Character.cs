using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogal_na_KaCu.TileClasses;

namespace Rogal_na_KaCu
{
   abstract class Character:Tile
    {
        protected int hp;
        protected int attack;
        protected int armor;
        protected Map currentMap;
        public Tile standingOnTile;

        void GetDmg() { }
        void Attack() { }
        void Die() { }

        public Character(int id, int posX, int posY) : base(id, posX, posY)
        {
            standingOnTile = TileFactory.Get(0, posX, posY);
            passable = false;
        }

        public virtual void SetCurrentMap(Map crMap)
        {
            currentMap = crMap;
        }

       
        
    }
}
