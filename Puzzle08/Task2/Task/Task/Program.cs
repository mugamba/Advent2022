using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    class Program
    {

        public static int[,] _treeMap;
        public static int _x;
        public static int _y;

        public static Dictionary<Tuple<int, int>, long> _visibleList = new Dictionary<Tuple<int, int>, long>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            _x = lines.Length;
            _y = lines.First().Length;
            _treeMap = new int[_x, _y];

            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                {
                    _treeMap[i, j] = int.Parse(lines[j].ToCharArray()[i].ToString());
                   
                }



            //for (int i = 0; i < _x; i++)
            //    for (int j = 0; j < _y; j++)
            //        if (i == 0 || j == 0 || i == _x - 1 || j == _y - 1)
            //            _visibleList.Add(new Tuple<int, int>(i, j), _treeMap[i, j]);




             for (int i = 1; i < _x-1; i++)
                for (int j = 1; j < _y-1; j++)
                {
                    var visibilty = CheckUp(i, j) * CheckDown(i, j) * CheckLeft(i, j) * CheckRight(i, j);
                    
                        _visibleList.Add(new Tuple<int, int>(i, j), visibilty);

                    

                }


            Console.WriteLine("Number of visibles {0}", _visibleList.Select(o=>o.Value).Max());

        }

        private static int CheckRight(int i, int j)
        {
            int counter = 0;
            for (int k = i + 1; k < _x; k++)
            {
                counter++;

                if (_treeMap[i, j] <= _treeMap[k, j])
                    return counter;

                
            }

            return counter;

        }

        private static int CheckLeft(int i, int j)
        {
            int counter = 0;
            for (int k = i; k > 0; k--)
            {
                counter++;

                if (_treeMap[i, j] <= _treeMap[k-1, j])
                    return counter;

                
            }



            return counter;
        }

        private static int CheckDown(int i, int j)
        {
            int counter = 0;
            for (int k = j+1; k < _y; k++)
            {
                counter++;

                if (_treeMap[i, j] <= _treeMap[i, k])
                    return counter;
            }

            return counter;
        }

        private static int CheckUp(int i, int j)
        {
            int counter = 0;
            for (int k = j; k > 0; k--)
            {
                counter++;

                if (_treeMap[i, j] <= _treeMap[i, k-1])
                    return counter;

               
            }

            return counter;
        }
    }

}
