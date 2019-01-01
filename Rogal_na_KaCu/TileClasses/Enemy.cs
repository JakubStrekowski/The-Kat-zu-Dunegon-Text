using System;
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
        private bool alreadyMoved;
        public bool AlreadyMoved
        {
            get { return alreadyMoved; }
        }
        public abstract void MovementBehaviour();

        public Enemy(int id, int posX, int posY,Map mp) : base(id, posX, posY, mp)
        {
            alreadyMoved = false;
        }

        public bool moveDirection(int direction) //0 up, 1 down, 2 right, 3 left
        {
            int targetPositionX = positionX;
            int targetPositionY = positionY;
            alreadyMoved = true;
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
                    else
                    {
                        if (currentMap.GiveNeighbor(positionX, positionY, 0) is Hero)
                        {
                            Hero enm = (Hero)currentMap.GiveNeighbor(positionX, positionY, 0);
                            enm.GetDmg(attack);
                            currentMap.SendLog(name + " hit you for " + attack.ToString() + " damage!");
                        }
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
                    else
                    {
                        if (currentMap.GiveNeighbor(positionX, positionY, 1) is Hero)
                        {
                            Hero enm = (Hero)currentMap.GiveNeighbor(positionX, positionY, 1);
                            enm.GetDmg(attack);
                            currentMap.SendLog(name + " hit you for " + attack.ToString() + " damage!");
                        }
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
                    else
                    {
                        if (currentMap.GiveNeighbor(positionX, positionY, 2) is Hero)
                        {
                            Hero enm = (Hero)currentMap.GiveNeighbor(positionX, positionY, 2);
                            enm.GetDmg(attack);
                            currentMap.SendLog(name + " hit you for " + attack.ToString() + " damage!");
                        }
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
                    else
                    {
                        if (currentMap.GiveNeighbor(positionX, positionY, 3) is Hero)
                        {
                            Hero enm = (Hero)currentMap.GiveNeighbor(positionX, positionY, 3);
                            enm.GetDmg(attack);
                            currentMap.SendLog(name + " hit you for "+ attack.ToString() + " damage!");
                        }
                    }
                    break;
            }
            return false;
        }

        public void ReenableMove()
        {
            alreadyMoved = false;
        }
    }
}
