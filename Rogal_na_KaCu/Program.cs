using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Program
    {
        

    private static int index = 0;
        static void Main(string[] args)
        {
            SoundPlayer soundMenu = new SoundPlayer("8bit 2.wav");
            soundMenu.PlayLooping();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DisplayConsole display = DisplayConsole.Instance;
            List<string> menuItems = new List<string>()
            {
                "Start Game",
                "Plot",
                "Credits",
                "Quit Game"
            };

            Console.CursorVisible = false;
            while (true)
            {
                string line;
                System.IO.StreamReader frameFile = new System.IO.StreamReader("display/dragon.txt");
                while ((line = frameFile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                frameFile.Close();

                string selectedMenu = DrowMainMenu(menuItems);
                if (selectedMenu == "Start Game")
                {
                    while (Console.KeyAvailable)
                        Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("Enter hero name: ");
                    string name = Console.ReadLine();
                    while (name.Length > 16||name.Length==0)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter hero name: (shorter than 16 characters)");
                        name = Console.ReadLine();
                    }
                    Console.Clear();
                    soundMenu = new SoundPlayer("piano.wav");
                    soundMenu.PlayLooping();
                    display.DrawFrame();
                    
                    GameHandler gameMaster = new GameHandler(display);
                    gameMaster.CreateHero(name);
                    gameMaster.GenerateRandom(gameMaster.floorNumber);
                    gameMaster.PlayInMap();
                    Console.ReadKey();
                }
                else if (selectedMenu == "Plot")
                {
                    display.DrowStory();
                }
                else if (selectedMenu == "Credits")
                {
                    display.DrowCredits();
                }
                else if (selectedMenu == "Quit Game")
                {
                    Environment.Exit(0);
                }
                Console.Clear();
            };
        }
        
        public static string DrowMainMenu(List<string> item)
        {
            Console.CursorVisible = false;
            for (int i = 0; i < item.Count; i++)
            {
                if (i == index)
                {
                    Console.SetCursorPosition(36, 9 + i);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(item[i]);
                }
                else
                {
                    Console.SetCursorPosition(36, 9 + i);
                    Console.WriteLine(item[i]);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            if (consoleKey.Key == ConsoleKey.DownArrow)
            {
                if (index == item.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else if (consoleKey.Key == ConsoleKey.UpArrow)
            {
                if (index == 0)
                {
                    index = item.Count - 1;
                }
                else
                {
                    index--;
                }
            }
            else if (consoleKey.Key == ConsoleKey.Enter)
            {
                return item[index];
            }
            else
            {
                return "";
            }
            Console.Clear();
            return "";
        }
    }
}
