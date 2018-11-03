﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
   abstract class Consumable:Tile
    {

       public void UseEffect() { }

        public Consumable(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {

        }
    }
}
