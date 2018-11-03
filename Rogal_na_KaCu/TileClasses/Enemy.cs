﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
   public abstract class Enemy:Character
    {
        public int id;
        public int speed;
        public int giveGold;
        public abstract void MovementBehaviour();

        public Enemy(int id, int posX, int posY,Map mp) : base(id, posX, posY, mp)
        {
        }

        public bool moveDirection(int direction) //0 up, 1 down, 2 right, 3 left
        {
            int targetPositionX = positionX;
            int targetPositionY = positionY;
            Thread.Sleep(200);
            switch (direction)
            {
                case 0:
                    targetPositionY = targetPositionY - 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 0).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionY = positionY - 1;
                        return true;
                    }
                    break;
                case 1:
                    targetPositionY = targetPositionY + 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 1).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionY = positionY + 1;
                        return true;
                    }
                    break;
                case 2:
                    targetPositionX = targetPositionX + 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 2).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionX = positionX + 1;
                        return true;
                    }
                    break;
                case 3:
                    targetPositionX = targetPositionX - 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 3).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionX = positionX - 1;
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
