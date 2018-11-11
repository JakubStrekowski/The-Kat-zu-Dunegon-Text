using Rogal_na_KaCu.TileClasses;
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
                    Tile neighbor0;
                    neighbor0 = currentMap.GiveNeighbor(positionX, positionY, 0);
                    if (neighbor0.passable)
                    {
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionY = positionY - 1;
                        if (isNearBorder())
                        {
                            currentMap.MoveFocus(this);
                        }

                        if (neighbor0 is Stairs)
                        {
                            currentMap.gameMaster.NextLevel();
                            currentMap.gameMaster.GenerateRandom(currentMap.gameMaster.floorNumber);
                            currentMap.SendLog("You went down the stairs");
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                            currentMap.MoveFocus(this);
                        }
                        if (neighbor0 is Coin)
                        {
                            currentMap.GotGold();
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                        }
                    }
                    else
                    {
                        if(neighbor0 is Enemy)
                        {
                            Enemy enm = (Enemy)neighbor0;
                            enm.GetDmg(attack);
                            currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
                        }

                    }
                    break;
                case 1:
                    targetPositionY = targetPositionY + 1;
                    Tile neighbor1 = currentMap.GiveNeighbor(positionX, positionY, 1);
                    if (neighbor1.passable)
                    {
                        
                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionY = positionY + 1;
                        if (isNearBorder())
                        {
                            currentMap.MoveFocus(this);
                        }
                        if (neighbor1 is Stairs)
                        {
                            currentMap.gameMaster.NextLevel();
                            currentMap.gameMaster.GenerateRandom(currentMap.gameMaster.floorNumber);
                            currentMap.SendLog("You went down the stairs");
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                            currentMap.MoveFocus(this);
                        }
                        if (neighbor1 is Coin)
                        {
                            currentMap.GotGold();
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                        }

                    }
                    else
                    {
                        if (neighbor1 is Enemy)
                        {
                            Enemy enm = (Enemy)neighbor1;
                            enm.GetDmg(attack);
                            currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
                        }
                        
                    }
                    break;
                case 2:
                    Tile neighbor2;
                    targetPositionX = targetPositionX + 1;
                    neighbor2 = currentMap.GiveNeighbor(positionX, positionY, 2);
                    if (neighbor2.passable)
                    {
                            currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                            positionX = positionX + 1;
                            if (isNearBorder())
                            {
                                currentMap.MoveFocus(this);
                            }
                        if (neighbor2 is Stairs)
                        {
                            currentMap.gameMaster.NextLevel();
                            currentMap.gameMaster.GenerateRandom(currentMap.gameMaster.floorNumber);
                            currentMap.SendLog("You went down the stairs");
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                            currentMap.MoveFocus(this);
                        }
                        if (neighbor2 is Coin)
                        {
                            currentMap.GotGold();
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                        }
                    }
                    else
                    {
                        if (neighbor2 is Enemy)
                        {
                            Enemy enm = (Enemy)neighbor2;
                            enm.GetDmg(attack);
                            currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
                        }
                    }
                    break;
                case 3:
                    Tile neighbor3;
                    targetPositionX = targetPositionX - 1 ;
                    neighbor3 = currentMap.GiveNeighbor(positionX, positionY, 3);
                    if (neighbor3.passable)
                    {

                        currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                        positionX = positionX - 1;
                        if (isNearBorder())
                        {
                            currentMap.MoveFocus(this);
                        }

                        if (neighbor3 is Stairs)
                        {
                            currentMap.gameMaster.NextLevel();
                            currentMap.gameMaster.GenerateRandom(currentMap.gameMaster.floorNumber);
                            currentMap.SendLog("You went down the stairs");
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                            currentMap.MoveFocus(this);
                        }
                        if( neighbor3 is Coin)
                        {
                            currentMap.GotGold();
                            standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                        }
                    }
                    else
                    {
                        if (neighbor3 is Enemy)
                        {
                            Enemy enm = (Enemy)neighbor3;
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
        protected virtual void GetPotion()
        {
            
        }
        public virtual void UseItem(int id)
        {
            equipment[id].UseEffect(this);
            
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
