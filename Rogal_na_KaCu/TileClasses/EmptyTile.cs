using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.TileClasses
{
    public class EmptyTile : Tile
    {
        private EmptyTile(int id, Map mp) : base(id, mp)
        {
            passable = true;
        }

        public static void NewFloor(Dictionary<int, Tile> staticTiles)
        {
            staticTiles.Add(0, new EmptyTile(0, null));
        }
    }
}
