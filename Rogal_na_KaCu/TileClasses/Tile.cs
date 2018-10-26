using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    public abstract class Tile
    {
        public int positionX;
        public int positionY;
        public bool passable;
        public int representedByID;

        public Tile(int id, int posX, int posY)
        {
            representedByID = id;
            positionX = posX;
            positionY = posY;
        }
    }
}
