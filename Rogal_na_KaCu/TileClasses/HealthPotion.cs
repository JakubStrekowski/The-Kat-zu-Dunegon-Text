using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class HealthPotion : Consumable
    {
        public HealthPotion(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp) {

        }
        public void useEffect() { }
    }
}
