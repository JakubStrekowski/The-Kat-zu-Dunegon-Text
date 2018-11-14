using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace Rogal_na_KaCu
{
   public class DisplayConsole
    {
        string[] charDictionary;
        TileInfo[] tileDictionary;
        int mapFirstXPosition = 26;
        int mapFirstYPosition = 4;
        int maxRow = 15;
        int maxColumn=66;
        int mapCurrentFirstX = 0;
        int mapCurrentFirstY = 0;
        private int mapLevel;
        private string heroName;
        private int heroHp;
        private string weaponName;
        private string armorName;
        private int gold;
        private int enemiesKilled;
        private string[] items;
        private List<string> LogsList;
        private int logCurrentPosition = 0;
        private int logSize = 5;
        private int logLastID = 0;
        private bool writeGreyLog = false;
        string[] crown;
        string[] gameMenu;
        string[] deathMenu;

        public DisplayConsole()
        {
            gold = 0;
            enemiesKilled = 0;
            heroName = "";
            weaponName = "";
            armorName = "";
            crown = new string[34];
            deathMenu = new string[19];
            gameMenu = new string[7];
            LogsList = new List<string>();
            items = new string[6];
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
            for(int i = 0; i < 6; i++)
            {
                items[i] = "Empty";
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

            System.IO.StreamReader gameMenuFile = new System.IO.StreamReader("display/gameMenuUI.txt");
            counter = 0;
            while ((line = gameMenuFile.ReadLine()) != null)
            {
                gameMenu[counter] = line;
                gameMenu[counter].Replace('\n', '\0');
                counter++;
            }
            gameMenuFile.Close();

            System.IO.StreamReader deathMenuFile = new System.IO.StreamReader("display/YouDied.txt");
            counter = 0;
            while ((line = deathMenuFile.ReadLine()) != null)
            {
                deathMenu[counter] = line;
                deathMenu[counter].Replace('\n', '\0');
                counter++;
            }
            deathMenuFile.Close();

            System.IO.StreamReader crownMenuFile = new System.IO.StreamReader("display/crown.txt");
            counter = 0;
            while ((line = crownMenuFile.ReadLine()) != null)
            {
                crown[counter] = line;
                crown[counter].Replace('\n', '\0');
                counter++;
            }
            crownMenuFile.Close();
        }

        public void DrowStory()
        {
            Console.Clear();
            string line;
            Console.ForegroundColor = ConsoleColor.White;
            System.IO.StreamReader frameFile = new System.IO.StreamReader("display/story.txt");
            while ((line = frameFile.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            frameFile.Close();
            Console.Read();
        }

        
        public void DrowCredits()
        {

            Console.Clear();
            string line;
            Console.ForegroundColor = ConsoleColor.White;
            System.IO.StreamReader frameFile = new System.IO.StreamReader("display/credits.txt");
            while ((line = frameFile.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            frameFile.Close();
            Console.Read();
        }

        public void DisplayMap(Map mapObject, int centerX, int centerY)
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            int beginX;
            int beginY;
            CleanMap();
            if (centerX - 33 >= 0)
            {
                beginX = centerX - 33;
            }
            else
            {
                beginX = 0;
            }
            if (centerY - 7 >= 0)
            {
                beginY = centerY - 7;
                if (centerY + 8 > mapObject.tileMap.Length)
                {
                    beginY = mapObject.tileMap.Length - maxRow;
                }
            }
            else
            {
                beginY = 0;
            }
            mapCurrentFirstX = beginX;
            mapCurrentFirstY = beginY;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(mapFirstXPosition, mapFirstYPosition);
            int rowCount = beginY;
            for(;rowCount<mapObject.tileMap.Length;rowCount++)
            {
                if (rowCount == maxRow+beginY)
                {
                    break;
                }

                if (mapObject.tileMap[rowCount] != null)
                for(int i = beginX; i < maxColumn+beginX; i++)
                {

                        if (i >= mapObject.tileMap[rowCount].Length)
                        {
                                Console.SetCursorPosition(mapFirstXPosition + i - beginX, mapFirstYPosition + rowCount - beginY - 1);
                                PrintTile(tileDictionary[1].charID, tileDictionary[1].colorID);
                        }
                        else
                        {
                            Console.SetCursorPosition(mapFirstXPosition + i - beginX, mapFirstYPosition + rowCount - beginY - 1);
                            PrintTile(tileDictionary[mapObject.tileMap[rowCount][i].representedByID].charID, tileDictionary[mapObject.tileMap[rowCount][i].representedByID].colorID);
                        }
                        
                    }
                Console.SetCursorPosition(mapFirstXPosition, mapFirstYPosition+rowCount-1);
            }
            Console.SetWindowPosition(0, 0);
            Console.SetCursorPosition(prevX, prevY);
        }

        public void RefreshFromMapAtPosition(Map mapObject, int posX, int posY)
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.SetCursorPosition(posX+1+mapFirstXPosition-mapCurrentFirstX, posY+mapFirstYPosition-mapCurrentFirstY-1);
            Console.Write("\b");
            PrintTile(tileDictionary[mapObject.tileMap[posY][posX].representedByID].charID, tileDictionary[mapObject.tileMap[posY][posX].representedByID].colorID);
            Console.SetCursorPosition(prevX, prevY);
        }

        public void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.SetCursorPosition(mapFirstXPosition + 1, mapFirstYPosition + 3);
            for(int j = 0; j < 7; j++) { 
            for(int i = 0; i < 68; i++)
                {
                    Console.Write('\b');
                 }
                Console.SetCursorPosition(mapFirstXPosition + 1, mapFirstYPosition + 3+j);
                Console.Write(gameMenu[j]);
            }
            Console.SetCursorPosition(prevX, prevY);
        }

        public void DisplayDeathMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            ResetLogVariables();
            Console.Clear();
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.SetCursorPosition(0, 0);
            for(int j = 0; j < 19; j++)
            {
                Console.WriteLine(deathMenu[j]);
            }
        }

        public void DisplayCrown()
        {
            SoundPlayer snd = new SoundPlayer("the end.wav");
            snd.PlayLooping();
            Console.ForegroundColor = ConsoleColor.Yellow;
            ResetLogVariables();
            Console.Clear();
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < 34; j++)
            {
                Console.WriteLine(crown[j]);
            }
        }
        
        private void ResetLogVariables()
        {
            LogsList.Clear();
            logCurrentPosition = 0;
            logLastID = 0;
            writeGreyLog = false;
    }

        private void PrintTile(int charID, int color)
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

        private void CleanMap()
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            Console.SetCursorPosition(mapFirstXPosition, mapFirstYPosition);
            for(int rowCount = 0; rowCount < maxRow; rowCount++)
            {
                for(int columnCount = 0; columnCount < maxColumn; columnCount++)
                {
                    PrintTile(0, 0);
                }
            }
        }

        public void SetStatUI(int which, string value)
        {
            /*
             *  0 floor number
             *  1 hero name
             *  2 hero hp
             *  3 weapon name
             *  4 armor name
             *  5 items name list
             *  6 gold
             *  7 enemies count
             */
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            switch (which)
            {
                case 0:
                    
                    int startPosition0 = 60;
                    for(int i = 0; i < mapLevel.ToString().Length; i++)
                    {
                        Console.SetCursorPosition(startPosition0, 1);
                        Console.Write('\b');
                    }
                    mapLevel = Int32.Parse(value);
                    Console.SetCursorPosition(startPosition0, 1);
                    Console.Write(value);
                    break;

                case 1:
                    
                    int startPosition1 = 10;
                    for (int i = 0; i < heroName.Length; i++)
                    {

                        Console.SetCursorPosition(startPosition1, 3);
                        Console.Write('\b');
                    }
                    
                    heroName = value;
                    int writePosition1 = 12 - (heroName.Length / 2);
                    Console.SetCursorPosition(writePosition1, 3);
                    Console.Write(value);
                    break;

                case 2:
                    
                    int startPosition2 = 6;
                    for (int i = 0; i < heroHp.ToString().Length; i++)
                    {

                        Console.SetCursorPosition(startPosition2,5);
                        Console.Write('\b');
                    }
                    heroHp = Int32.Parse(value);
                    Console.SetCursorPosition(startPosition2, 5);
                    Console.Write(value);
                    break;

                case 3:
                    
                    int startPosition3 = 6;
                    for (int i = 0; i < weaponName.Length; i++)
                    {

                        Console.SetCursorPosition(startPosition3, 6);
                        Console.Write('\b');
                    }
                    weaponName = value;
                    Console.SetCursorPosition(startPosition3, 6);
                    Console.Write(value);
                    break;

                case 4:
                    
                    int startPosition4 = 6;
                    for (int i = 0; i < armorName.Length; i++)
                    {

                        Console.SetCursorPosition(startPosition4, 8);
                        Console.Write('\b');
                    }
                    armorName = value;
                    Console.SetCursorPosition(startPosition4, 8);
                    Console.Write(value);
                    break;

                case 5:
                    
                    break;
                case 6:
                    int startPosition5 = 110;
                    for (int i = 0; i < value.Length; i++)
                    {

                        Console.SetCursorPosition(startPosition5, 7);
                        Console.Write('\b');
                    }
                    gold = Int32.Parse(value);
                    Console.SetCursorPosition(startPosition5, 7);
                    Console.Write(value);
                    break;
                case 7:
                    int startPosition6 = 110;
                    for (int i = 0; i < value.Length; i++)
                    {

                        Console.SetCursorPosition(startPosition6, 5);
                        Console.Write('\b');
                    }
                    enemiesKilled = Int32.Parse(value);
                    Console.SetCursorPosition(startPosition6, 5);
                    Console.Write(value);
                    break;
                default: break;
            }
            Console.SetCursorPosition(prevX, prevY);
        }

        

        public void RefreshItem(int idInList,string newName)
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            int startPosition8 = 2;
            int counter = 0;
            for(int j=0;j<6;j++)
            {
                for (int i = 0; i < 19; i++)
                {

                    Console.SetCursorPosition(startPosition8, 5 + counter);
                    Console.Write('\b');
                }
                counter++;
            }
            if (idInList != -1)
            {
                items[idInList] = newName;
            }
            for (int i=0;i<6;i++)
            {
                if (i < 6&&idInList!=-1)
                {
                    Console.SetCursorPosition(startPosition8, 5 + counter);
                    int iToPrint = i + 1;
                    Console.Write("  " + iToPrint +".  "+items[i]);
                    for(int j = 0; j < 19 - (items[i].Length + 6); j++)
                    {
                        Console.Write(" ");
                    }
                    counter++;
                }
                else
                {
                    Console.SetCursorPosition(startPosition8, 5 + counter);
                    int iToPrint = i + 1;
                    Console.Write("  "+ iToPrint  + ".  Empty");
                    for (int j = 0; j < 19 - (11); j++)
                    {
                        Console.Write(" ");
                    }
                    counter++;
                }
            }
            Console.SetCursorPosition(prevX, prevY);
        }

        public void AddLog(String log)
        {
            if (logCurrentPosition == 0)
            {
                writeGreyLog = !writeGreyLog;
            }
            LogsList.Add(logLastID.ToString() + ".     " + log);
            logLastID = (logLastID + 1) % 1000;
            PrintLastLog();
            logCurrentPosition = (logCurrentPosition + 1) % logSize;
            if (LogsList.Count > 99)
            {
                LogsList.RemoveAt(0);
            }
        }
        
        private void PrintLastLog()
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            CleanLogLine(logCurrentPosition);
            for(int i=0;i< LogsList[LogsList.Count-1].Length; i++)
            {
                Console.SetCursorPosition(10, 21 + logCurrentPosition);
                Console.Write('\b');
            }
            if (writeGreyLog)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.Write(LogsList[LogsList.Count-1]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(prevX, prevY);
        }

        private void CleanLogLine(int whichLine)
        {
            int prevX = Console.CursorLeft;
            int prevY = Console.CursorTop;
            for (int i = 0;i<90; i++)
            {
                Console.SetCursorPosition(10+i, 21 + logCurrentPosition);
                Console.Write('\b');
                Console.Write(" ");
            }
            Console.SetCursorPosition(prevX, prevY);
        }
    }
}
