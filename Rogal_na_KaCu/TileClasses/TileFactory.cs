using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.TileClasses
{
    public static class TileFactory
    {
        public static Tile Get(int id, int posX, int posY, Map mp)
        {
            switch (id)
            {
                case 0:
                    return new EmptyTile(id, posX, posY, mp);
                case 1:
                    return new Wall(id, posX, posY, mp);
                case 2:
                    return new Hero(id, posX, posY, mp);
                case 3:
                    return new Skeleton(id, posX, posY, mp);
                case 4:
                    return new Stairs(id, posX, posY, mp);
                case 5:
                    return new EmptyTile(id, posX, posY, mp); //tbd
                case 6:
                    return new Zombie(id, posX, posY, mp);
                case 7:
                    return new HealthPotion(id, posX, posY, mp);
                case 8:
                    return new Rat(id, posX, posY, mp);
                case 9:
                    return new Slime(id, posX, posY, mp);
                default:
                    return new EmptyTile(id, posX, posY, mp);
            }
        }

    }
}
