﻿using Rogal_na_KaCu.TileClasses;
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
        public Consumable[] equipment;
        public int currentCenterPositionX;
        public int currentCenterPositionY;
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
            equipment = new Consumable[6];
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
                        IfIsOther(neighbor0);
                    }
                    else
                    {
                        if(neighbor0 is Enemy)
                        {
                            IfIsEnemy(neighbor0);
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
                        IfIsOther(neighbor1);
                    }
                    else
                    {
                        if (neighbor1 is Enemy)
                        {
                            IfIsEnemy(neighbor1);
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
                        IfIsOther(neighbor2);
                    }
                    else
                    {
                        if (neighbor2 is Enemy)
                        {
                            IfIsEnemy(neighbor2);
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
                        IfIsOther(neighbor3);
                    }
                    else
                    {
                        IfIsEnemy(neighbor3);
                    }
                    break;
            }
            Thread.Sleep(200);
        }

        void IfIsOther(Tile neighbor)
        {
            if (neighbor is Stairs)
            {
                currentMap.gameMaster.NextLevel();
                currentMap.gameMaster.GenerateRandom(currentMap.gameMaster.floorNumber);
                currentMap.SendLog("You went down the stairs");
                standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                currentMap.MoveFocus(this);
            }
            if (neighbor is Coin)
            {
                currentMap.GotGold();
                standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
            }
            IfIsConsumable(neighbor);
        }

        void IfIsEnemy(Tile neighbor)
        {
            if (neighbor is Enemy)
            {
                Enemy enm = (Enemy)neighbor;
                enm.GetDmg(attack);
                currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
            }
        }

        void IfIsConsumable(Tile tile)
        {
            if (tile is Consumable)
            {
                bool freeSpace = false;
                int i;
                for (i = 0; i < 6; i++)
                {
                    if (equipment[i] == null)
                    {
                        freeSpace = true;
                        break;
                    }
                }
                if (freeSpace)
                {
                    Consumable cons = (Consumable)tile;
                    AddItem(i, cons);
                    currentMap.SendLog("You picked up " + cons.name + "!");
                    standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                }
                else
                {
                    Consumable cons = (Consumable)tile;
                    currentMap.SendLog("You have no room for " + cons.name + "!");
                }
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

        public virtual void UseItem(int id)
        {
            id = id - 1;
            if (id<6)
            {
                if (equipment[id] != null)
                {
                    equipment[id].UseEffect(this);
                    RemoveItem(id);
                }
            }
            else
            {
                currentMap.SendLog("You have no item in this slot!");
            }
        }

        private void AddItem(int id,Consumable item)
        {
            equipment[id] = item;
            currentMap.RefreshItem(id,item.name);
        }

        private void RemoveItem(int itemId)
        {
            equipment[itemId] = null;
            currentMap.RefreshItem(itemId, "Empty");
        }
        
    }
}
