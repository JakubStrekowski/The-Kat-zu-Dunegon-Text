using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    public class Armor:Tile
    {
        public string name;
        int defenceValue;

        public Armor(int id, int posX, int posY,Map mp) : base(id, posX, posY,mp)
        {
            passable = true;
        }
    }
}
