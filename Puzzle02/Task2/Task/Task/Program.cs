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

            foreach (var line in lines)
            {
                list.Add(ParseLine(line));
            }
            Console.WriteLine("Result is {0}", list.Sum());
            Console.ReadKey();
        }

        private static int ParseLine(string line)
        {
            var splits = line.Split(' ');

            if (splits[0] == "A")
            {

                if (splits[1] == "X")
                    return 3 + 0;

                if (splits[1] == "Y")
                    return 1 + 3;

                if (splits[1] == "Z")
                    return 2 + 6;

            }

            if (splits[0] == "B")
            {

                if (splits[1] == "X")
                    return 1 + 0;

                if (splits[1] == "Y")
                    return 2 + 3;

                if (splits[1] == "Z")
                    return 3 + 6;

            }

            if (splits[0] == "C")
            {

                if (splits[1] == "X")
                    return 2 + 0;

                if (splits[1] == "Y")
                    return 3 + 3;

                if (splits[1] == "Z")
                    return 1 + 6;

            }

            return 0;

        }
    }
}
