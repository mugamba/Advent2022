using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;

namespace Task
{
    class Program
    {

        public static char[,] _treeMap;
        public static int _x;
        public static int _y;

        public static SortedDictionary<Tuple<int, int>, int> _visited = new SortedDictionary<Tuple<int, int>, int>();
        public static SortedDictionary<Tuple<int, int>, int> _unvisited = new SortedDictionary<Tuple<int, int>, int>();
        public static SortedDictionary<Tuple<int, int>, int> _distanceFromStart = new SortedDictionary<Tuple<int, int>, int>();

        public static Tuple<int, int> _start;
        public static Tuple<int, int> _end;

        public static Dictionary<Tuple<int, int>, int> _allStartsDistances = new Dictionary<Tuple<int, int>, int>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            _x = lines.First().Length;
            _y = lines.Count();
            _treeMap = new char[_x, _y];


            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                {

                        _treeMap[i, j] = (lines[j].ToCharArray()[i]);
                }


            for (int i = 0; i < _x; i++)
                for (int j = 0; j < _y; j++)
                {

                    if (lines[j].ToCharArray()[i] == 'a')
                    {
                        _allStartsDistances.Add(new Tuple<int, int>(i, j), 0);
                    }

                    if (lines[j].ToCharArray()[i] == 'S')
                    {
                        _treeMap[i, j] = 'a';
                        _allStartsDistances.Add(new Tuple<int, int>(i, j), 0);
                    }

                    if (lines[j].ToCharArray()[i] == 'E')
                    {
                        _end = new Tuple<int, int>(i, j);
                        _treeMap[i, j] = 'z';
                    }
                }




            foreach (var a in _allStartsDistances)
            {
                _start = a.Key;
                _distanceFromStart.Clear();
                _unvisited.Clear();
                _visited.Clear();

                for (int i = 0; i < _x; i++)
                    for (int j = 0; j < _y; j++)
                    {
                        if (_start.Item1 == i && _start.Item2 == j)
                            _distanceFromStart.Add(new Tuple<int, int>(i, j), 0);
                        else
                            _distanceFromStart.Add(new Tuple<int, int>(i, j), int.MaxValue);

                        _unvisited.Add(new Tuple<int, int>(i, j), 0);
                    }


                while (_unvisited.Count > 0)
                {
                    Tuple<int, int> toVisit = null;
                    var list = _distanceFromStart.Where(o => !_visited.ContainsKey(o.Key) && _unvisited.ContainsKey(o.Key)).ToList();
                    toVisit = list.OrderBy(o => o.Value).First().Key;
                    VisitPoint(toVisit);
                    _visited.Add(toVisit, 0);
                    _unvisited.Remove(toVisit);
                }



                var min = _allStartsDistances.Where(o=>o.Value > 0).ToList().Min();

                _allStartsDistances[a.Key] = _distanceFromStart[_end];
               
            }



           var minDistance =  _allStartsDistances.Min(o => o.Value);

        }



        public static IList<Tuple<int, int>> VisitPoint(Tuple<int, int> point)
        {
            var charPoint = _treeMap[point.Item1, point.Item2];
            var charz = _treeMap[point.Item1, point.Item2];
            IList<Tuple<int, int>> list = new List<Tuple<int, int>>();


            if (point.Item1 - 1 >= 0)
            {
                var canmove = _treeMap[point.Item1 - 1, point.Item2] - charz <= 1;

            
                if (canmove)
                {
                    var newdistance = 1 + _distanceFromStart[new Tuple<int, int>(point.Item1, point.Item2)];
                    var neighbour = new Tuple<int, int>(point.Item1 - 1, point.Item2);

                    if (_distanceFromStart[neighbour] > newdistance)
                    {
                        _distanceFromStart[neighbour] = newdistance;
                        list.Add(neighbour);
                    }
                }
            }

            if (point.Item1 + 1 < _x)
            {
                var canmove = _treeMap[point.Item1 + 1, point.Item2] - charz <= 1;
           

                if (canmove)
                {
                    var newdistance = 1 + _distanceFromStart[new Tuple<int, int>(point.Item1, point.Item2)];
                    var neighbour = new Tuple<int, int>(point.Item1 + 1, point.Item2);
                    if (_distanceFromStart[neighbour] > newdistance)
                    {
                        _distanceFromStart[neighbour] = newdistance;
                        list.Add(neighbour);
                    }
                }

            }



            if (point.Item2 - 1 >= 0)
            {
                
                var canMove = _treeMap[point.Item1, point.Item2 - 1] - charz  <= 1;
                if (canMove)
                {
                    var newdistance = 1 + _distanceFromStart[new Tuple<int, int>(point.Item1, point.Item2)];
                    var neighbour = new Tuple<int, int>(point.Item1, point.Item2 - 1);
                    if (_distanceFromStart[neighbour] > newdistance)
                    {
                        _distanceFromStart[neighbour] = newdistance;
                        list.Add(neighbour);
                    }
                }
            }


            if (point.Item2 + 1 < _y)
            {
             
                var canmove = _treeMap[point.Item1, point.Item2+1] - charz  <= 1;
                if (canmove)
                {
                    var newdistance = 1 + _distanceFromStart[new Tuple<int, int>(point.Item1, point.Item2)];
                    var neighbour = new Tuple<int, int>(point.Item1, point.Item2 + 1);

                    if (_distanceFromStart[neighbour] > newdistance)
                    {
                        _distanceFromStart[neighbour] = newdistance;
                        list.Add(neighbour);
                    }
                }
            }


            return list;
        }

    }

}
