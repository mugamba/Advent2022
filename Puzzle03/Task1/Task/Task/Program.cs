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

            var firstEntires = new List<string>();
            var secondEntires = new List<string>();
            var exceptionList = new List<Int32>();

            foreach (var line in lines)
            {
                var length = line.Length;
                var firstToken = line.Substring(0, length / 2);
                var secondToken = line.Substring(length/2, length / 2);

                firstEntires.Add(firstToken);
                secondEntires.Add(secondToken);
            }

            var alphabet = Enumerable.Range('a', 26).Select(o => ((char)o)).Union(Enumerable.Range('A', 26).Select(o => (char)o)).ToList();

            for (int i = 0; i < firstEntires.Count; i++)
            {
                var exception = firstEntires[i].ToCharArray().Intersect(secondEntires[i]).FirstOrDefault();
                exceptionList.Add(alphabet.IndexOf(exception) + 1);
            }

            Console.WriteLine("Result is {0}", exceptionList.Sum());
            Console.ReadKey();
        }

        private static int ParseLine(string line)
        {
            var splits = line.Split(' ');

            if (splits[0] == "A")
            {

                if (splits[1] == "X")
                    return 1 + 3;

                if (splits[1] == "Y")
                    return 2 + 6;

                if (splits[1] == "Z")
                    return 3 + 0;

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
                    return 1 + 6;

                if (splits[1] == "Y")
                    return 2 + 0;

                if (splits[1] == "Z")
                    return 3 + 3;

            }

            return 0;







        }
    }
}
