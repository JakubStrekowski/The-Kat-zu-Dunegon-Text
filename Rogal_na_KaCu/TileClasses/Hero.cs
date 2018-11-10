using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    public class Hero : Character
    {
        List<Consumable> equipment;
        public int currentCenterPositionX;
        public int currentCenterPositionY;
        public Weapon currentWeapon;
        public Armor currentArmor;
        public bool isAlife;

        public Hero(int id, int posX, int posY,Map mp): base(id, posX,posY, mp)
        {
            isAlife = true;
            name = "Jan";
            hp = 6;
            passable = false;
            attack = 1;
            armor = 0;
            int currentCenterPositionX=posX;
            int currentCenterPositionY=posY;
        }

        public void SetName(string name)
        {
            this.name = name;
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
                    else
                    {
                        if(currentMap.GiveNeighbor(positionX, positionY, 0) is Enemy)
                        {
                            Enemy enm = (Enemy)currentMap.GiveNeighbor(positionX, positionY, 0);
                            enm.GetDmg(attack);
                            currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
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
                    else
                    {
                        if (currentMap.GiveNeighbor(positionX, positionY, 1) is Enemy)
                        {
                            Enemy enm = (Enemy)currentMap.GiveNeighbor(positionX, positionY, 1);
                            enm.GetDmg(attack);
                            currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
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
                    else
                    {
                        if (currentMap.GiveNeighbor(positionX, positionY, 2) is Enemy)
                        {
                            Enemy enm = (Enemy)currentMap.GiveNeighbor(positionX, positionY, 2);
                            enm.GetDmg(attack);
                            currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
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
                    else
                    {
                        if (currentMap.GiveNeighbor(positionX, positionY, 3) is Enemy)
                        {
                            Enemy enm = (Enemy)currentMap.GiveNeighbor(positionX, positionY, 3);
                            enm.GetDmg(attack);
                            currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
                        }
                    }
                    break;
            }
            Thread.Sleep(200);
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

        public override void GetDmg(int value)
        {
            hp = hp - (value - armor);
            currentMap.SendUIInfo(2, hp.ToString());
            if (hp <= 0)
            {
                isAlife = false;
                currentMap.HeroDied();
            }
            
        }

        public String ReturnWeaponName()
        {
            if (currentWeapon == null)
            {
                return "Dagger";
            }
            else
            {
                return currentWeapon.name;
            }
        }

        public String ReturnArmorName()
        {
            if (currentArmor == null)
            {
                return "No Armor";
            }
            else
            {
                return currentArmor.name;
            }
        }
    }
}
