using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    class Program
    {


        public static Dictionary<int, int> _dict = new Dictionary<int, int>();

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

            var result = _dict[20] * 20 + _dict[60] * 60 + _dict[100] * 100 + _dict[140] * 140 +
                _dict[180] * 180 + _dict[220] * 220;


            Console.WriteLine("Result is {0}", result);
            Console.ReadKey();
        }

     
    }

}
