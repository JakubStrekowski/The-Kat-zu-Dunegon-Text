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
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.UpArrow: {
                        return "ArrowUp";
                    }
                default: return "None";
            }
        }
    }
}
