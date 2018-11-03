using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Rogal_na_KaCu
{
   public class GameHandler
    {
        int floorNumber;
        Map currentMap;
        DisplayConsole display;
        Input input;
        List<Enemy> enemiesOnMap;
        Hero hero;
        public GameHandler(DisplayConsole display)
        {
            enemiesOnMap = new List<Enemy>();
            this.display = display;
            input = new Input();
            floorNumber = 1;
        }

        void ResolveTurn() {
            foreach(Enemy enemy in enemiesOnMap)
            {
                enemy.MovementBehaviour();
            }
        }
        

        public Map LoadMap(string name="1.txt")
        {
            currentMap = new Map();
            int mapRowLimit=50;
            int mapColumnLimit = 100;
            int[][] intMap = new int[mapRowLimit][];
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("maps/"+name);
            int rowCounter = 0;
            while(rowCounter!= mapRowLimit)
            if((line = file.ReadLine()) != null)
            {
                string[] strRow = line.Split(' ');
                int[] intRow = new int[mapColumnLimit];
                int counter = 0;
                foreach (string st in strRow)
                {
                    intRow[counter] = int.Parse(st);
                    if (intRow[counter] == 2)
                    {
                        hero = new Hero(2,counter, rowCounter,currentMap);
                    }
                    counter++;
                }
                    while (counter < mapColumnLimit)
                    {
                        intRow[counter] = 0;
                        counter++;
                    }
                intMap[rowCounter] = intRow;
                rowCounter++;
            }
                else
                {
                    while (rowCounter < mapRowLimit)
                    {
                        intMap[rowCounter] = new int[mapColumnLimit];
                        int counter = 0;
                        while (counter < mapColumnLimit)
                        {
                            intMap[rowCounter][counter] = 0;
                            counter++;
                        }
                        rowCounter++;
                    }
                    
                }
            file.Close();
            Map newMap = new Map(intMap,display,this);
            currentMap = newMap;
            hero.SetCurrentMap(newMap);
            ChangeFloorNumber(1);
            display.SetStatUI(1, hero.name);
            display.SetStatUI(2, hero.hp.ToString());
            display.SetStatUI(3, hero.ReturnWeaponName());
            display.SetStatUI(4, hero.ReturnArmorName());
            return newMap;
        }

        public void PlayInMap()
        {
            bool heroAlife = true;
            while (heroAlife)
            {
                if (ResolveInput(input.TakeInput()))
                {
                    ResolveTurn();
                }
                
            }
        }

        public bool ResolveInput(String inputCommand)
        {
            switch (inputCommand) {
                case "ArrowUp":
                    {
                        hero.Move(0);
                    }
                    return true;
                case "ArrowDown":
                    {
                        hero.Move(1);
                    }
                    return true;
                case "ArrowRight":
                    {
                        hero.Move(2);
                    }
                    return true;
                case "ArrowLeft":
                    {
                        hero.Move(3);
                    }
                    return true;
                default: return false;
            }
        }

        private void ChangeFloorNumber(int value)
        {
            floorNumber = value;
            display.SetStatUI(0, floorNumber.ToString());
        }

        public void AddEnemyToList(Enemy toAdd)
        {
            enemiesOnMap.Add(toAdd);
        }
    }
}
