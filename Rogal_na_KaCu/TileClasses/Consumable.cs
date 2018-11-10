using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogal_na_KaCu.TileClasses;

namespace Rogal_na_KaCu
{
   abstract public class Consumable:Tile
    {
        public Tile standingOnTile;

        public void UseEffect() { }

        public Consumable(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            standingOnTile = TileFactory.Get(0, posX, posY, mp);
            passable = false;
        }
    }
}
