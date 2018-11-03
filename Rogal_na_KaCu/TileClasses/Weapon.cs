using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Weapon:Tile
    {
        public string name;
        int attackValue;

        public Weapon(int id, int posX, int posY) : base(id, posX, posY)
        {
            passable = true;
        }
    }
}
