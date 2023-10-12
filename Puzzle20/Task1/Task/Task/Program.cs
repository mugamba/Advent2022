using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
       
        public static List<int> _initialList = new List<int>();
        public static List<int> _sorted = new List<int>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var counter = 0;
            foreach (var line in lines)
            {
                var p = Int32.Parse(line);
                _initialList.Add(p);
                _sorted.Add(p);
                counter++;
            }

            for (int i = 0; i < _initialList.Count; i++)
            {

              var oldIndex =  _sorted.IndexOf(_initialList[i]);

              var initialElement = _initialList[i];

                var newPosition = (oldIndex + (initialElement % _initialList.Count));
                
                var indexPos = 0;

                if (newPosition < 0)
                     indexPos = (_initialList.Count + (newPosition - 1));
                else
                     indexPos = (newPosition % _initialList.Count);

                if (indexPos > oldIndex)
                {
                    var offset = (initialElement < 0) ? 1 : 0;
                     
                    var element = _sorted.ElementAt(oldIndex);
                    _sorted.RemoveAt(oldIndex);
                    _sorted.Insert(indexPos, element);

                    Console.WriteLine(String.Join(",", _sorted.ToArray()));
                }
                else
                {
               
                    //1, 2, -2, -3, 0, 3, 4
                    var element = _sorted.ElementAt(oldIndex);
                    
                    _sorted.RemoveAt(oldIndex);
                    _sorted.Insert(indexPos, element);
                    Console.WriteLine(String.Join(",", _sorted.ToArray()));
                }
            }


            var oldIndex1 = _sorted.IndexOf(0);
            var newindex1 = _sorted[(oldIndex1 + (1000 % _sorted.Count)) % _sorted.Count];


            var oldIndex2= _sorted.IndexOf(0);
            var newindex2 = _sorted[(oldIndex2 + (2000 % _sorted.Count)) % _sorted.Count];



            var oldIndex3 = _sorted.IndexOf(0);
            var newindex3 = _sorted[(oldIndex3 + (3000 % _sorted.Count)) % _sorted.Count];


            Console.WriteLine(newindex1+newindex2+newindex3);
            Console.ReadKey();
        }

    }
}
