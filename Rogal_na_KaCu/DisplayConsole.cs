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
        int mapFirstXPosition = 26;
        int mapFirstYPosition = 4;

        public DisplayConsole()
        {
            string line;
            charDictionary = new string[40];
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
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(mapFirstXPosition, mapFirstYPosition);
            int rowCount = 0;
            foreach (Tile[] row in mapObject.tileMap)
            {
                if (row != null)
                for(int i = 0; i < row.Length; i++)
                {
                        Console.SetCursorPosition(mapFirstXPosition+i, mapFirstYPosition+rowCount);
                        PrintTile(tileDictionary[row[i].representedByID].charID, tileDictionary[row[i].representedByID].colorID);
                }
                rowCount++;
                Console.SetCursorPosition(mapFirstXPosition, mapFirstYPosition+rowCount);
            }
            Console.SetCursorPosition(prevX, prevY);
        }

        public void RefreshFromMapAtPosition(Map mapObject, int posX, int posY)
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.SetCursorPosition(posX+1+mapFirstXPosition, posY+mapFirstYPosition);
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
            Console.Write('\b'+charDictionary[charID]);
            Console.ForegroundColor = ConsoleColor.White; 
        }

        public void DrawFrame() //max column length = 119
        {
            int currentRow=0;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, currentRow);
            string line;
            System.IO.StreamReader frameFile = new System.IO.StreamReader("display/frameUI.txt");
            while ((line = frameFile.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            frameFile.Close();
        }

        private void PrintNumberOfTimes(int number, int character)
        {
            for(int i = 0; i < number; i++)
            {
                Console.Write(charDictionary[character]);
            }
        }
    }
}
