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

        public Hero(int id, int posX, int posY): base(id, posX,posY)
        {
            hp = 6;
            passable = false;
            attack = 1;
            armor = 1;
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
                        
                    }
                    break;
                case 1:
                    targetPositionY = targetPositionY + 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 1).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionY = positionY + 1;

                    }
                    break;
                case 2:
                    targetPositionX = targetPositionX + 1;
                    if (currentMap.GiveNeighbor(positionX, positionY, 2).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionX = positionX + 1;

                    }
                    break;
                case 3:
                    targetPositionX = targetPositionX - 1 ;
                    if (currentMap.GiveNeighbor(positionX, positionY, 3).passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionX = positionX - 1;

                    }
                    break;
            }
        }
    }
}
