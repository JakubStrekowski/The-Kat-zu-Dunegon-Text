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
        string[] dictionary;

        public DisplayConsole()
        {
            string line;
            dictionary = new string[20];
            System.IO.StreamReader dictFile = new System.IO.StreamReader("display/intToCharTranslator.txt");
            while ((line = dictFile.ReadLine()) != null)
            {
                string[] words = line.Split(' ');
                int number = int.Parse(words[0]);
                dictionary[number] =(words[1]);
            }
            dictionary[0] = " ";
        }

        public void DisplayWindow(Map mapObject)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (int[] row in mapObject.intMap)
            {
                if(row!=null)
                for(int i = 0; i < row.Length; i++)
                {
                    Console.Write(dictionary[row[i]]);
                }
                Console.Write('\n');
            }
        }
    }
}
