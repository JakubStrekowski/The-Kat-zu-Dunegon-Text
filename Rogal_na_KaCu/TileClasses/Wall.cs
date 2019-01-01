using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.TileClasses
{
    public class Wall:Tile
    {
        private Wall(int id,  Map mp) : base(id, mp)
        {
            passable = false;
        }

        public static void NewWall( Dictionary<int, Tile> staticTiles)
        {

            staticTiles.Add(1, new Wall(1,null));
        }
    }
}
