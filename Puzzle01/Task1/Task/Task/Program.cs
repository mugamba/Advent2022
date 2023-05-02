using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var counter = 0;
            var list = new List<int>();

            var elf = 0;

            foreach (var line in lines)
            {


                if (line == String.Empty)
                {
                    list.Add(elf);
                    elf = 0;
                }
                else
                {
                    elf += Int32.Parse(line);
                }
            
            }


            var maxElf = list.Max();

         

            Console.WriteLine("Result is {0}", maxElf);
            Console.ReadKey();
        }
    }
}
