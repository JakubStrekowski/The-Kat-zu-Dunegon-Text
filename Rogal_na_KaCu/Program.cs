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

            DisplayConsole display = new DisplayConsole();
            display.DrawFrame();
            GameHandler gameMaster = new GameHandler(display);
            Map firstMap=gameMaster.LoadMap("1.txt");
            display.DisplayMap(firstMap,0,0);
            gameMaster.PlayInMap();
            Console.ReadKey();
        }
    }
}
