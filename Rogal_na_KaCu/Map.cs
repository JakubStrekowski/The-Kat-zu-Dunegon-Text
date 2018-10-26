using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogal_na_KaCu.TileClasses;

namespace Rogal_na_KaCu
{
    class Map
    {
        DisplayConsole display;
        public Tile[][] tileMap;
        public Map()
        {

        }

        public Map(int[][] intMap,DisplayConsole display)
        {
            this.display = display;
            this.tileMap = new Tile[25][];
            int rowCounter = 0;
            foreach (int[] intRow in intMap)
            {
                this.tileMap[rowCounter] = new Tile[25];
                int columnCounter = 0;
                foreach(int integer in intRow)
                {
                    
                    this.tileMap[rowCounter][columnCounter] = TileFactory.Get(integer, columnCounter, rowCounter);
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
            display.RefreshAtPosition(this, sourceX, sourceY);
            display.RefreshAtPosition(this, targetX, targetY);

        }

        public void StepOnElement(int sourceX,int sourceY, int targetX, int targetY)
        {
            Character chara = (Character)tileMap[sourceY][sourceX];
            Tile temporary = tileMap[targetY][targetX];
            tileMap[targetY][targetX] = tileMap[sourceY][sourceX];
            tileMap[sourceY][sourceX] = chara.standingOnTile;
            chara.standingOnTile = temporary;
            display.RefreshAtPosition(this, sourceX, sourceY);
            display.RefreshAtPosition(this, targetX, targetY);
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
    }
}
