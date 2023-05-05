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

            var exceptionList = new List<Int32>();


            foreach (var line in lines)
            {
                var splits = line.Split(",");
                var firstToken = splits[0];
                var secondToken = splits[1];

                var splitsFirst = firstToken.Split("-");
                var splitsSecond = secondToken.Split("-");

                var ffone = Int32.Parse(splitsFirst[0]);
                var fftwo = Int32.Parse(splitsFirst[1]);

                var ssone = Int32.Parse(splitsSecond[0]);
                var sstwo = Int32.Parse(splitsSecond[1]);

                var firstRange = Enumerable.Range(ffone, fftwo - ffone + 1).ToList();
                var secondRange = Enumerable.Range(ssone, sstwo - ssone + 1).ToList();

                if (firstRange.All(o => firstRange.Intersect(secondRange).Contains(o)) ||
                    secondRange.All(o => secondRange.Intersect(firstRange).Contains(o)))
                    counter++;

            }

            Console.WriteLine("Result is {0}", counter);
            Console.ReadKey();
        }
    }
}
