using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Rogal_na_KaCu
{
    class DisplayConsole
    {
        string[] charDictionary;
        TileInfo[] tileDictionary;
        public DisplayConsole()
        {
            string line;
            charDictionary = new string[20];
            tileDictionary = new TileInfo[20];
            System.IO.StreamReader dictFile = new System.IO.StreamReader("display/intToCharTranslator.txt");
            while ((line = dictFile.ReadLine()) != null)
            {
                string[] words = line.Split(' ');
                int number = int.Parse(words[0]);
                charDictionary[number] =(words[1]);
            }
            charDictionary[0] = " ";
            dictFile.Close();

            System.IO.StreamReader tileDictFile = new System.IO.StreamReader("display/tileToInt.txt");
            int counter = 0;
            while ((line = tileDictFile.ReadLine()) != null)
            {
                tileDictionary[counter] = new TileInfo();
                string[] words = line.Split(' ');
                int charID = int.Parse(words[0]);
                int colorID = int.Parse(words[1]);
                tileDictionary[counter].charID = charID;
                tileDictionary[counter].colorID = colorID;
                counter++;
            }
        }

        public void DisplayWindow(Map mapObject)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Tile[] row in mapObject.tileMap)
            {
                if(row!=null)
                for(int i = 0; i < row.Length; i++)
                {
                        PrintTile(tileDictionary[row[i].representedByID].charID, tileDictionary[row[i].representedByID].colorID);
                }
                RefreshAtPosition(mapObject, 2, 0);
                Console.Write('\n');
            }
        }

        public void RefreshAtPosition(Map mapObject, int posX, int posY)
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.SetCursorPosition(posX+1, posY);
            Console.Write("\b");
            PrintTile(tileDictionary[mapObject.tileMap[posY][posX].representedByID].charID, tileDictionary[mapObject.tileMap[posY][posX].representedByID].colorID);
            Console.SetCursorPosition(prevX, prevY);
        }
        

        public void PrintTile(int charID, int color)
        {
            switch (color)
            {
                case 0: Console.ForegroundColor = ConsoleColor.White;break;
                case 1: Console.ForegroundColor = ConsoleColor.Gray; break;
                case 2: Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case 3: Console.ForegroundColor = ConsoleColor.Red; break;
                case 4: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case 5: Console.ForegroundColor = ConsoleColor.Green; break;
                case 6: Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case 7: Console.ForegroundColor = ConsoleColor.Blue; break;
                case 8: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case 9: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 10: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case 11: Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 12: Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                case 13: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 14: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 15: Console.ForegroundColor = ConsoleColor.Black; break;
            }
            Console.Write(charDictionary[charID]);
            Console.ForegroundColor = ConsoleColor.White; 
        }
    }
}
