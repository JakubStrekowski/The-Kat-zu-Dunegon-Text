﻿using System;
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
                    return Tile.GetStaticTile(0);
                case 1:
                    return Tile.GetStaticTile(1);
                case 2:
                    return new Hero(id, posX, posY, mp);
                case 3:
                    return new Skeleton(id, posX, posY, mp);
                case 4:
                    return new Stairs(id, posX, posY, mp);
                case 5:
                    return new Coin(id, posX, posY, mp); 
                case 6:
                    return new Zombie(id, posX, posY, mp);
                case 7:
                    return new HealthPotion(id, posX, posY, mp);
                case 8:
                    return new Rat(id, posX, posY, mp);
                case 9:
                    return new Slime(id, posX, posY, mp);
                case 10:
                    return new KatzuAvatarEyes(id, posX, posY, mp);
                case 11:
                    return new KatzuAvatar(id, posX, posY, mp);
                case 12:
                    return new KatzuAvatarArms(id, posX, posY, mp);
                case 13:
                    return new KatzuAvatarBody(id, posX, posY, mp);
                default:
                    return Tile.GetStaticTile(0);
            }
        }

    }
}
