using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Rogal_na_KaCu
{
    class GameHandler
    {
        int floorNumber;
        Map currentMap;
        DisplayConsole display;
        Input input;
        List<Enemy> enemiesOnMap;
        Hero hero;
        public GameHandler(DisplayConsole display)
        {
            this.display = display;
            input = new Input();
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
            int[][] intMap = new int[25][];
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("maps/"+name);
            int rowCounter = 0;
            while(rowCounter!=25)
            if((line = file.ReadLine()) != null)
            {
                string[] strRow = line.Split(' ');
                int[] intRow = new int[25];
                int counter = 0;
                foreach (string st in strRow)
                {
                    intRow[counter] = int.Parse(st);
                    if (intRow[counter] == 2)
                    {
                        hero = new Hero(2,counter, rowCounter);
                    }
                    counter++;
                }
                    while (counter < 25)
                    {
                        intRow[counter] = 0;
                        counter++;
                    }
                intMap[rowCounter] = intRow;
                rowCounter++;
            }
                else
                {
                    while (rowCounter < 25)
                    {
                        intMap[rowCounter] = new int[25];
                        int counter = 0;
                        while (counter < 25)
                        {
                            intMap[rowCounter][counter] = 0;
                            counter++;
                        }
                        rowCounter++;
                    }
                    
                }
            file.Close();
            Map newMap = new Map(intMap,display);
            currentMap = newMap;
            hero.SetCurrentMap(newMap);
            return newMap;
        }

        public void PlayInMap()
        {
            bool heroAlife = true;
            while (heroAlife)
            {
                ResolveInput(input.TakeInput());
            }
        }

        public void ResolveInput(String inputCommand)
        {
            switch (inputCommand) {
                case "ArrowUp":
                    
                    //
                    {
                        hero.Move(0);
                    }
                    break;

            }
        }
    }
}
