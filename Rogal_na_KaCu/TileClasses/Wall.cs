using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.TileClasses
{
    class Wall:Tile
    {
        public Wall(int id, int posX, int posY) : base(id, posX, posY)
        {
            passable = false;
        }
    }
}
