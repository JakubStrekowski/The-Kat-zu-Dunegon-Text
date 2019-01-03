using Rogal_na_KaCu.EnemyBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Zombie : Enemy
    {
        bool directionHorizontal; //does zombie move left-right or up-down
        public bool DirectionHorizontal
        {
            get { return directionHorizontal; }
        }
        Random rnd;
        ZombieMoveState movementState;
        public ZombieMoveState MovementState
        {
            set { movementState = value; }
        }
        public Zombie(int id, int posX, int posY,Map mp) : base(id, posX, posY, mp)
        {
            name = "Zombie";
            rnd = new Random(posX*posY);
            hp = 3;
            attack = 1;
            
            int random = rnd.Next( 2);
            if (random == 0)
            {
                directionHorizontal = true;
            }
            else
            {
                directionHorizontal = false;
            }
            if (rnd.Next(2) == 0) movementState = new MoveDownState();
            else movementState = new MoveUpState();
        }

        public override void MovementBehaviour()
        {
            movementState.ZombieMove(this);
        }
        

    }
}
