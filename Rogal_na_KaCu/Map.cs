using System;
using System.Collections;
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
        int mapRowLimit;
        int mapColumnLimit;
        public EnemyIterator enemies;


        public Map(int[][] intMap,DisplayConsole display,GameHandler gm, int rowAmmount, int ColumnAmmount)
        {
            dontShow = false;
            gameMaster = gm;
            mapRowLimit = rowAmmount;
            mapColumnLimit = ColumnAmmount;
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
                    
                    columnCounter++;
                }
                rowCounter++;
            }
            enemies = new EnemyIterator(tileMap);
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
            if (hero.positionX > mapColumnLimit - 33)
            {
                relativeCenterX= mapColumnLimit - 33;
            }
            if (hero.positionY < 8)
            {
                relativeCenterY = 8;
            }
            else
            {
                relativeCenterY =hero.positionY;
            }
            if (hero.positionY > mapRowLimit - 8)
            {
                relativeCenterY = mapRowLimit - 8;
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
                gameMaster.enemiesKilled++;
                display.SetStatUI(7, gameMaster.enemiesKilled.ToString());
            }
            tileMap[posY][posX] = temporary;
            display.RefreshFromMapAtPosition(this, posX, posY);
        }

        public void GetPotion(int posX,int posY)
        {
            Consumable consumable = (Consumable)tileMap[posX][posY];
            Tile temporary = consumable.standingOnTile;
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
            if (gameMaster.hero.positionX > mapColumnLimit - 33)
            {
                relativeCenterX = mapColumnLimit - 33;
            }

            if (gameMaster.hero.positionY < 8)
            {
                relativeCenterY= 8;
            }
            else
            {
                relativeCenterY = gameMaster.hero.positionY;
            }
            if (gameMaster.hero.positionY > mapRowLimit-8)
            {
                relativeCenterY = mapRowLimit - 8;
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
            gameMaster.AddGold(goldAmnt);
            SendLog("You found " + goldAmnt + " gold!");
        }

        public void RefreshItem(int idFromList, string newName)
        {
            display.RefreshItem(idFromList, newName);
        }

        public void HeroWon()
        {
            dontShow = true;
            gameMaster.SetWhatInControl(4);
            display.DisplayCrown();
        }

        public class EnemyIterator : IEnumerable<Tile>
        {
            public Tile[][] values;

            public EnemyIterator(Tile[][] tileMap)
            {
                values = tileMap;
            }

            public IEnumerator<Tile> GetEnumerator()
            {
                for(int i = 0; i < values.Length; i++)
                {
                    for(int j = 0; j < values[i].Length; j++)
                    {
                        if(values[i][j] is Enemy)
                        {
                            yield return values[i][j];
                        }
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

    }
}
