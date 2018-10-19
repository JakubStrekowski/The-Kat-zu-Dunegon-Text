using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GameHandler gameMaster = new GameHandler();
            DisplayConsole display = new DisplayConsole();
            Map firstMap=gameMaster.LoadMap("1.txt");
            display.DisplayWindow(firstMap);
            Console.ReadKey();
        }
    }
}
