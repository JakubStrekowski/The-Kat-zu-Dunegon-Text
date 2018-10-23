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
            switch (direction)
            {
                case 0:
                    Console.WriteLine(positionX + " " + (positionY) + " " + currentMap.GiveNeighbor(positionX, positionY, 0).passable + currentMap.GiveNeighbor(positionX, positionY, 0).representedByID);
                    if (currentMap.GiveNeighbor(positionX,positionY, 0).passable)
                    {
                        currentMap.SwitchElements(positionX, positionY, positionX, positionY - 1);
                        positionY = positionY - 1;
                        
                    }
                    break;
            }
        }
    }
}
