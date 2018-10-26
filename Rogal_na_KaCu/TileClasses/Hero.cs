using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Hero : Character
    {
        List<Consumable> equipment;
        public int currentCenterPositionX;
        public int currentCenterPositionY;

        public Hero(int id, int posX, int posY): base(id, posX,posY)
        {
            hp = 6;
            passable = false;
            attack = 1;
            armor = 1;
            int currentCenterPositionX=posX;
            int currentCenterPositionY=posY;
        }

        public void Move(int direction) //0 up, 1 down, 2 right, 3 left
        {

            int targetPositionX = positionX;
            int targetPositionY = positionY;
            switch (direction)
            {
                case 0:
                    targetPositionY = targetPositionY - 1;
                    if (currentMap.GiveNeighbor(positionX,positionY, 0).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionY = positionY - 1;
                        if (isNearBorder())
                        {
                            currentMap.MoveFocus(this);
                        }
                    }
                    break;
                case 1:
                    targetPositionY = targetPositionY + 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 1).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionY = positionY + 1;
                        if (isNearBorder())
                        {
                            currentMap.MoveFocus(this);
                        }
                    }
                    break;
                case 2:
                    targetPositionX = targetPositionX + 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 2).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionX = positionX + 1;
                        if (isNearBorder())
                        {
                            currentMap.MoveFocus(this);
                        }
                    }
                    break;
                case 3:
                    targetPositionX = targetPositionX - 1 ;
                    if (currentMap.GiveNeighbor(positionX, positionY, 3).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionX = positionX - 1;
                        if (isNearBorder())
                        {
                            currentMap.MoveFocus(this);
                        }
                    }
                    break;
            }
        }

        public bool isNearBorder()
        {
            if (positionX - currentCenterPositionX > 28 || positionX - currentCenterPositionX < -28 || positionY - currentCenterPositionY < -5 || positionY - currentCenterPositionY > 5)
            {
                return true;
            }
            else return false;
        }

        public override void SetCurrentMap(Map crMap)
        {
            base.SetCurrentMap(crMap);
            currentMap.relativeCenterX = currentCenterPositionX;
            currentMap.relativeCenterY = currentCenterPositionY;
        }
    }
}
