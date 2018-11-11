﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu.TileClasses
{
    class KatzuAvatarArms : Enemy
    {
        public KatzuAvatarArms(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            name = "Katzu Avatar";
            hp = 1;
            attack = 2;
        }

        public override void MovementBehaviour()
        {
        }

        public override void Die()
        {
            currentMap.DestroyCharacter(positionX, positionY);
        }

        public override void GetDmg(int value)
        {
            currentMap.SendLog("You tried to attack Katzu body part, but nothing happened...");
        }
    }
}
