using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
   abstract class Character:Tile
    {
        int hp;
        int attack;
        int armor;

        void GetDmg() { }
        void Attack() { }
        void Die() { }

    }
}
