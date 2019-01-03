using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.EnemyBehaviour
{
    class MoveDownState : ZombieMoveState
    {
        public override void ZombieMove(Zombie context)
        {
            if (context.DirectionHorizontal)
            {
                if (!context.moveDirection(3))
                {
                    context.MovementState = new MoveUpState();
                }
            }
            else
            {
                if (!context.moveDirection(1))
                {
                    context.MovementState = new MoveUpState();
                }
            }
        }
    }
}
