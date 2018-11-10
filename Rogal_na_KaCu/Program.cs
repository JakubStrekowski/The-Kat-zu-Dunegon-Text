﻿using System;
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
            GameHandler gameMaster = new GameHandler(display);
            gameMaster.CreateHero("Jacopo");
             Map firstMap=gameMaster.LoadMap("1.txt");
            /*
            DungeonGenerator dg = new DungeonGenerator(150, 100);
            dg.CreateDungeon(150, 100, 18);
            */
            /*
            gameMaster.GenerateRandom();*/
            gameMaster.PlayInMap();
            
            Console.ReadKey();
        }
    }
}
