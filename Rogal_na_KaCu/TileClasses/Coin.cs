using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.TileClasses
{
    class Coin:Tile
    {
        public Coin(int id, int posX, int posY, Map mp) : base(id, mp)
        {
            passable = true;
        }
    }
}
