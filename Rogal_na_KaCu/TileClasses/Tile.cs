using Rogal_na_KaCu.TileClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    public abstract class Tile
    {
        static Dictionary<int,Tile> staticTiles;
        public bool passable;
        public int representedByID;
        public Map currentMap;

        public Tile(int id,Map mp)
        {
            currentMap = mp;
            representedByID = id;
        }

        public static Tile GetStaticTile(int tileID)
        {
            if (staticTiles == null) staticTiles = new Dictionary<int, Tile>();
            if (!staticTiles.ContainsKey(tileID))
            {
                if (tileID == 0)
                {
                    EmptyTile.NewFloor(staticTiles);
                }
                if (tileID == 1)
                {
                    Wall.NewWall(staticTiles);
                }
            }
            return staticTiles[tileID];
        }
    }
}
