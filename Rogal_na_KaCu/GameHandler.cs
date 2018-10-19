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
        public GameHandler()
        {
            
        }
        void ResolveTurn() {  }

        public Map LoadMap(string name="1.txt")
        {
            currentMap = new Map();
            int[][] intMap = new int[20][];
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("maps/"+name);
            int rowCounter = 0;
            while ((line = file.ReadLine()) != null)
            {
                
                string[] strRow = line.Split(' ');
                int[] intRow = new int[strRow.Length];
                int counter = 0;
                foreach (string st in strRow)
                {
                    intRow[counter] = int.Parse(st);
                    counter++;
                }
                intMap[rowCounter] = intRow;
                rowCounter++;
            }
            Map newMap = new Map(intMap);
            return newMap;
        }
    }
}
