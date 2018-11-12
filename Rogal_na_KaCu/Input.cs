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
                case ConsoleKey.S:
                    {
                        return "S";
                    }
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    {
                        return "1";
                    }
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    {
                        return "2";
                    }
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    {
                        return "3";
                    }
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    {
                        return "4";
                    }
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    {
                        return "5";
                    }
                case ConsoleKey.NumPad6:
                case ConsoleKey.D6:
                    {
                        return "6";
                    }
                default: return "None";
            }
        }
    }
}
