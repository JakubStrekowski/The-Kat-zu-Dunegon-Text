using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogal_na_KaCu.TileClasses;

namespace Rogal_na_KaCu
{
    public class Map
    {
        DisplayConsole display;
        public int relativeCenterX=0;
        public int relativeCenterY=0;
        public Tile[][] tileMap;
        private bool dontShow;
        public GameHandler gameMaster;
        public Map()
        {

        }

        public Map(int[][] intMap,DisplayConsole display,GameHandler gm, int rowAmmount, int ColumnAmmount)
        {
            dontShow = false;
            gameMaster = gm;
            int mapRowLimit = rowAmmount;
            int mapColumnLimit = ColumnAmmount;
            this.display = display;
            this.tileMap = new Tile[mapRowLimit][];
            int rowCounter = 0;
            foreach (int[] intRow in intMap)
            {
                this.tileMap[rowCounter] = new Tile[mapColumnLimit];
                int columnCounter = 0;
                foreach(int integer in intRow)
                {
                    if (integer == 2)
                    {
                        tileMap[rowCounter][columnCounter] = gameMaster.hero;
                        gameMaster.hero.positionX = columnCounter;
                        gameMaster.hero.positionY = rowCounter;
                        gameMaster.hero.currentCenterPositionX = columnCounter;
                        gameMaster.hero.currentCenterPositionY = rowCounter;
                    }
                    else this.tileMap[rowCounter][columnCounter] = TileFactory.Get(integer, columnCounter, rowCounter, this);
                    if (integer == 6 || integer==3|| integer == 8|| integer == 9)
                    {
                        gameMaster.AddEnemyToList((Enemy)this.tileMap[rowCounter][columnCounter]);
                    }
                    if(integer == 7)
                    {
                        gameMaster.AddConsumableToList((HealthPotion)this.tileMap[rowCounter][columnCounter]);
                        
                    }
                    columnCounter++;
                }
                rowCounter++;
            }
        }

        public void SwitchElements(int sourceX,int sourceY,int targetX,int targetY)
        {
            Tile temporary = tileMap[targetY][targetX];
            tileMap[targetY][targetX] = tileMap[sourceY][sourceX];
            tileMap[sourceY][sourceX] = temporary;
            display.RefreshFromMapAtPosition(this, sourceX, sourceY);
            display.RefreshFromMapAtPosition(this, targetX, targetY);

        }

        public void StepOnElement(int sourceX,int sourceY, int targetX, int targetY)
        {
            Character chara = (Character)tileMap[sourceY][sourceX];
            Tile temporary = tileMap[targetY][targetX];
            tileMap[targetY][targetX] = tileMap[sourceY][sourceX];
            tileMap[sourceY][sourceX] = chara.standingOnTile;
            chara.standingOnTile = temporary;
            if (!dontShow)
            {
                if(targetX - relativeCenterX>-33&& targetX - relativeCenterX<33&&sourceY-relativeCenterY>-8&&sourceY-relativeCenterY<8)
                    if(sourceX - relativeCenterX > -33 && sourceX - relativeCenterX < 33 && targetY - relativeCenterY > -8 && targetY - relativeCenterY < 8)
                    {
                        display.RefreshFromMapAtPosition(this, sourceX, sourceY);
                        display.RefreshFromMapAtPosition(this, targetX, targetY);
                    }
                
            }
        }

        public Tile GiveNeighbor(int posX, int posY, int direction) //direction going: 0-up, 1-down, 2-right, 3-left
        {
            switch (direction)
            {

                case 0:
                    return tileMap[posY-1][posX];
                case 1: return tileMap[posY+1][posX];
                case 2: return tileMap[posY][posX+1];
                case 3: return tileMap[posY][posX-1];
                default:return tileMap[posY][posX];
            }
        }

        public void MoveFocus(Hero hero)
        {
            if (hero.positionX < 33)
            {
                relativeCenterX = 33;
            }
            else
            {
                relativeCenterX =hero.positionX;
            }
            if (hero.positionY < 8)
            {
                relativeCenterY = 8;
            }
            else
            {
                relativeCenterY =hero.positionY;
            }
            hero.currentCenterPositionX=hero.positionX;
            hero.currentCenterPositionY=hero.positionY;
            display.DisplayMap(this, relativeCenterX, relativeCenterY);
        }
        
        public void SendUIInfo(int valueID, String value)
        {
            display.SetStatUI(valueID, value);
        }

        public void DestroyCharacter(int posX, int posY)
        {
            Character chara = (Character)tileMap[posY][posX];
            Tile temporary = chara.standingOnTile;
            if(chara is Enemy)
            {
                gameMaster.RemoveEnemyFromList((Enemy)chara);
                gameMaster.enemiesKilled++;
                gameMaster.gold += 5;
            }
            tileMap[posY][posX] = temporary;
            display.RefreshFromMapAtPosition(this, posX, posY);
        }

        public void GetPotion(int posX,int posY)
        {
            Consumable consumable = (Consumable)tileMap[posX][posY];
            Tile temporary = consumable.standingOnTile;
            gameMaster.AddConsumableToList((HealthPotion)consumable);
            tileMap[posY][posX] = temporary;
            display.RefreshFromMapAtPosition(this,posX, posY);
        }
        public void SendLog(String message)
        {
            if(!dontShow)
            display.AddLog(message);
        }

        public void SetFocus()
        {
            if (gameMaster.hero.positionX < 33)
            {
                relativeCenterX = 33;
            }
            else
            {
                relativeCenterX = gameMaster.hero.positionX;
            }
            if (gameMaster.hero.positionY < 8)
            {
                relativeCenterY= 8;
            }
            else
            {
                relativeCenterY = gameMaster.hero.positionY;
            }
            gameMaster.hero.currentCenterPositionX = relativeCenterX;
            gameMaster.hero.currentCenterPositionY = relativeCenterY;
            display.DisplayMap(this, relativeCenterX, relativeCenterY);
        }

        public void HeroDied()
        {
            dontShow = true;
            gameMaster.SetWhatInControl(2);
            display.DisplayDeathMenu();
        }

        public void GotGold()
        {
            Random rnd = new Random();
            int goldAmnt = rnd.Next(5, 21);
            gameMaster.gold += goldAmnt;
            SendLog("You found " + goldAmnt + " gold!");
        }
    }
}
