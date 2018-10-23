using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.TileClasses
{
    public static class TileFactory
    {
          public static Tile Get(int id, int posX, int posY)
           {
                switch (id)
                {
                    case 0:
                            return new EmptyTile(id, posX, posY);
                    case 1:
                        return new Wall(id, posX,posY);
                    case 2:
                        return new Hero(id, posX, posY);
                    case 3:
                    default:
                        return new EmptyTile(id, posX, posY);
            }
           }
      
    }
}
