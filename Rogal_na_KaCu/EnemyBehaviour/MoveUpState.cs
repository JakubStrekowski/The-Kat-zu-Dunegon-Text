using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.EnemyBehaviour
{
    class MoveUpState : ZombieMoveState
    {
        public override void ZombieMove(Zombie context)
        {
            if (context.DirectionHorizontal)
            {
                if (!context.moveDirection(2))
                {
                    context.MovementState = new MoveDownState();
                }
            }
            else
            {
                if (!context.moveDirection(0))
                {
                    context.MovementState = new MoveDownState();
                }
            }
        }
    }
}
