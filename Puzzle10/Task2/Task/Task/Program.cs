using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Task
{
    class Program
    {
        
        public static Dictionary<int, int> _dict = new Dictionary<int, int>();
        public static char[,] _inputImage = new char[40, 6];

        static void Main(string[] args)
        {
         
            var lines = File.ReadAllLines("input.txt");

            _dict.Add(1, 1);

            foreach (var line in lines)
            {
                if (line.StartsWith("add"))
                {

                    var lastValue = _dict.Last();
                    _dict.Add(lastValue.Key + 1, lastValue.Value);
                    var toAdd = Int32.Parse(line.Split(' ')[1]);
                    _dict.Add(lastValue.Key + 2, lastValue.Value + toAdd);

                }
                if (line.StartsWith("noop"))
                {
                    var lastValue = _dict.Last();
                    _dict.Add(lastValue.Key + 1, lastValue.Value);
                }
            }

            for (int i = 0; i < 240; i++)
            {
                var k = i % 40;
                var j = i / 40;

                var cycle = i+1;
                var sprite = ReturnSprite(cycle);

                if (sprite.Contains(k))
                    _inputImage[k, j] = '#';
                else
                    _inputImage[k, j] = '.';
            }

            PrintImage(_inputImage);
            Console.ReadKey();
        }

        public static List<int> ReturnSprite(int i)
        {
            return new List<int>() { _dict[i] - 1, _dict[i], _dict[i] + 1 };
        }

        public static void PrintImage(char[,] image)
        {
            var x = image.GetLength(0);
            var y = image.GetLength(1);
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    Console.Write(image[i, j]);
                }
                Console.WriteLine();
            }
        }
    }

}
