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
                var secondToken = line.Substring(length / 2, length / 2);

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
    }
}
