using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogal_na_KaCu
{
    class Input 
    {
        public Input()
        {

        }
        public String TakeInput() {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
            var input = Console.ReadKey();
            Console.Write('\b');
            switch (input.Key)
            {
                case ConsoleKey.UpArrow: {
                        return "ArrowUp";
                    }
                case ConsoleKey.DownArrow:
                    {
                        return "ArrowDown";
                    }
                case ConsoleKey.LeftArrow:
                    {
                        return "ArrowLeft";
                    }
                case ConsoleKey.RightArrow:
                    {
                        return "ArrowRight";
                    }
                case ConsoleKey.Q:
                    {
                        return "Q";
                    }
                case ConsoleKey.C:
                    {
                        return "C";
                    }
                case ConsoleKey.Escape:
                    {
                        return "Escape";
                    }
                case ConsoleKey.E:
                    {
                        return "E";
                    }
                default: return "None";
            }
        }
    }
}
