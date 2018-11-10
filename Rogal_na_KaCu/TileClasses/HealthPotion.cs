using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
  public  class HealthPotion : Consumable
    {
        public int heal;
               
        public HealthPotion(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp) {

        }
        public void UseEffect(Character value) {
            value.hp = +heal;           
        }
        
    }
}
