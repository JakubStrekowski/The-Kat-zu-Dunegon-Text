using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Skeleton:Enemy
    {
        public override void MovementBehaviour()
        {
            throw new NotImplementedException();
        }
        public Skeleton(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            hp = 2;
            attack = 1;

        }
    }
}
