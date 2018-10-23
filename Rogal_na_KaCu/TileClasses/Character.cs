using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
   abstract class Character:Tile
    {
        protected int hp;
        protected int attack;
        protected int armor;
        protected Map currentMap;

        void GetDmg() { }
        void Attack() { }
        void Die() { }

        public Character(int id, int posX, int posY) : base(id, posX, posY)
        {
            passable = false;
        }

        public void SetCurrentMap(Map crMap)
        {
            currentMap = crMap;
        }
        
    }
}
