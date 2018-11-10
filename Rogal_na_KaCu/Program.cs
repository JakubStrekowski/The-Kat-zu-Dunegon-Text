using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Program
    {

        private static int index = 0;
        static void Main(string[] args)
        {


            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DisplayConsole display = new DisplayConsole();
            List<string> menuItems = new List<string>()
            {
                "Rozpocznij grę",
                "Fabuła",
                "Twórcy",
                "Wyjście"
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
                if (selectedMenu == "Rozpocznij grę")
                {

                    Console.Clear();
                    Console.WriteLine("Podaj imie bohatera: ");
                    string name = Console.ReadLine();
                    while (name.Length > 16)
                    {
                        Console.Clear();
                        Console.WriteLine("Podaj imie bohatera: (Krótsze niz 16 znaków)");
                        name = Console.ReadLine();
                    }
                    Console.Clear();
                    display.DrawFrame();
                    
                    GameHandler gameMaster = new GameHandler(display);
                    gameMaster.CreateHero(name);
                    //Map firstMap = gameMaster.LoadMap("1.txt");
                    gameMaster.GenerateRandom();
                    gameMaster.PlayInMap();
                    Console.ReadKey();
                }
                else if (selectedMenu == "Fabuła")
                {
                    display.DrowStory();
                }
                else if (selectedMenu == "Twórcy")
                {
                    display.DrowCredits();
                }
                else if (selectedMenu == "Wyjście")
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
                    Console.SetCursorPosition(36, 3 + i);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(item[i]);
                }
                else
                {
                    Console.SetCursorPosition(36, 3 + i);
                    Console.WriteLine(item[i]);

                }
                Console.ResetColor();
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
