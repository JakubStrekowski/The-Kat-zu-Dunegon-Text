using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
   abstract class Enemy:Character
    {
        public int id;
        public int speed;
        public int giveGold;
        void Die() { }
        protected abstract void MovementBehaviour();
    }
}
