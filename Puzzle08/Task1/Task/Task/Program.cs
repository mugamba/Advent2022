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

        public static Dictionary<Tuple<int, int>, int> _visibleList = new Dictionary<Tuple<int, int>, int>();

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



            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                    if (i == 0 || j == 0 || i == _x - 1 || j == _y - 1)
                        _visibleList.Add(new Tuple<int, int>(i, j), _treeMap[i, j]);




             for (int i = 1; i < _x-1; i++)
                for (int j = 1; j < _y-1; j++)
                {
                    if (CheckUp(i, j) || CheckDown(i, j) || CheckLeft(i, j) || CheckRight(i, j))
                    {
                        _visibleList.Add(new Tuple<int, int>(i, j), _treeMap[i, j]);

                    }

                }


            Console.WriteLine("Number of visibles {0}", _visibleList.Count);

        }

        private static bool CheckRight(int i, int j)
        {
            for (int k = i + 1; k < _x; k++)
            {
               
                if (_treeMap[i, j] <= _treeMap[k, j])
                    return false;
            }

            return true;

        }

        private static bool CheckLeft(int i, int j)
        {
            for (int k = i; k > 0; k--)
            {
                if (_treeMap[i, j] <= _treeMap[k-1, j])
                    return false;
            }

            return true;
        }

        private static bool CheckDown(int i, int j)
        {
            for (int k = j+1; k < _y; k++)
            {
                if (_treeMap[i, j] <= _treeMap[i, k])
                    return false;
            }

            return true;
        }

        private static bool CheckUp(int i, int j)
        {
            for (int k = j; k > 0; k--)
            { 
                if (_treeMap[i, j] <= _treeMap[i, k-1])
                    return false;
            }

            return true;
        }
    }

}
