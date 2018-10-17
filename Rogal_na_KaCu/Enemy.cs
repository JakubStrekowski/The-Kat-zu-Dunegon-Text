using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
   abstract class Enemy
    {
        public int id;
        public int speed;
        public int giveGold;
        void Die() { }
        int MovementBehaviour() { }
    }
}
