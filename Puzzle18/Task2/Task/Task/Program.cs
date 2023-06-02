using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Task
{
    class Program
    {
        public static List<Cube> _cubeList = new List<Cube>();
        public static List<Cube> _getAllIntersection = new List<Cube>();
        public static List<Cube> _airDrops = new List<Cube>();
        public static List<Cube> _outside = new List<Cube>();



        static void Main(string[] args)
        {

            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
            {
                var splits = line.Split(',');
                _cubeList.Add(new Cube(int.Parse(splits[0].Trim()), int.Parse(splits[1].Trim()), int.Parse(splits[2].Trim())));

            }

            var intersection = 0;
            foreach (var cube in _cubeList)
            {
                intersection = intersection + GetIntersectedCount(cube);
            }

            foreach (var cube in _cubeList)
            {
                GetIntersectedList(cube);
            }

            var count = _getAllIntersection.Where(o => _cubeList.Contains(o));




            var airDrops = _getAllIntersection.Where(o => IsAirDrop(o, new List<Cube>())).ToList();

            var a = _getAllIntersection.Select(o=>o.X).Min();
            var ma = _getAllIntersection.Select(o => o.X).Max();

            var airdropsIntersection = 0;
            foreach (var cube in airDrops)
            {
                airdropsIntersection = airdropsIntersection + GetIntersectedCount(cube);
            }




            var number =  _cubeList.Count * 6 - intersection -  airdropsIntersection;


            Console.WriteLine("Result is {0}", number);
            Console.ReadKey();
        }

        
        public class Cube
        {
            public int X;
            public int Y;
            public int Z;
          
            public Cube(int x, int y, int z) 
            {
                X = x;
                Y = y;
                Z = z;
            }

            public override bool Equals(Object cube)
            {
                var c = cube as Cube;
                if (c == null)
                    return false;

                return this.X == c.X && this.Y == c.Y && this.Z == c.Z;
            }

        }


        public static int GetIntersectedCount(Cube selected)
        {

            var counter = 0;
            if (_cubeList.Contains(new Cube(selected.X - 1, selected.Y, selected.Z)))
                counter++;

            if (_cubeList.Contains(new Cube(selected.X + 1, selected.Y, selected.Z)))
                counter++;

            if (_cubeList.Contains(new Cube(selected.X, selected.Y + 1, selected.Z)))
                counter++;

            if (_cubeList.Contains(new Cube(selected.X, selected.Y - 1, selected.Z)))
                counter++;

            if (_cubeList.Contains(new Cube(selected.X, selected.Y, selected.Z+1)))
                counter++;

            if (_cubeList.Contains(new Cube(selected.X, selected.Y, selected.Z-1)))
                counter++;

            return counter;
        }

        public static void GetIntersectedList(Cube selected)
        {
            var cube1 = new Cube(selected.X - 1, selected.Y, selected.Z);
            if (!_getAllIntersection.Contains(cube1)
                
                && !_cubeList.Contains(cube1)
                )
                _getAllIntersection.Add(cube1);


            var cube2 = new Cube(selected.X + 1, selected.Y, selected.Z);
            if (!_getAllIntersection.Contains(cube2)

                && !_cubeList.Contains(cube2)
                )
                _getAllIntersection.Add(cube2);

            var cube3 = new Cube(selected.X, selected.Y-1, selected.Z);
            if (!_getAllIntersection.Contains(cube3)

               && !_cubeList.Contains(cube3)
               )
                _getAllIntersection.Add(cube3);

            var cube4 = new Cube(selected.X, selected.Y+1, selected.Z);
            if (!_getAllIntersection.Contains(cube4)

               && !_cubeList.Contains(cube4)
               )
                _getAllIntersection.Add(cube4);


            var cube5 = new Cube(selected.X, selected.Y, selected.Z-1);
            if (!_getAllIntersection.Contains(cube5)

               && !_cubeList.Contains(cube5)
               )
                _getAllIntersection.Add(cube5);

            var cube6 = new Cube(selected.X, selected.Y, selected.Z+1);
            if (!_getAllIntersection.Contains(cube6)

               && !_cubeList.Contains(cube6)
               )
                _getAllIntersection.Add(cube6);


        }

        public static Boolean IsAirDrop(Cube selected, List<Cube> visited)
        {
            Boolean moveMinusX = true;
            Boolean movePlusX = true;
            Boolean moveMinusY = true;
            Boolean movePlusY = true;
            Boolean moveMinusZ = true;
            Boolean movePlusZ = true;

            Cube move = null;

            visited.Add(selected);
            if (selected.X <= 0 || selected.Y <= 0 || selected.Z <= 0
                || selected.X >= 21 || selected.Y >= 21 || selected.Z >= 21)
            {
                _outside.AddRange(visited);
                return false;
            }

            move = new Cube(selected.X - 1, selected.Y, selected.Z);
            if (!_cubeList.Contains(move) && !visited.Contains(move))
            {
                moveMinusX = IsAirDrop(move, visited.ToList());
                if (!moveMinusX)
                    return false;
            }

            move = new Cube(selected.X + 1, selected.Y, selected.Z);
            if (!_cubeList.Contains(move) && !visited.Contains(move))
            {
                movePlusX = IsAirDrop(move, visited.ToList());
                if (!movePlusX)
                    return false;

            }

            move = new Cube(selected.X, selected.Y-1, selected.Z);
            if (!_cubeList.Contains(move) && !visited.Contains(move))
            {
                moveMinusY = IsAirDrop(move, visited.ToList());
                if (!moveMinusY)
                    return false;

            }

            move = new Cube(selected.X, selected.Y+1, selected.Z);
            if (!_cubeList.Contains(move) && !visited.Contains(move))
            {
                movePlusY = IsAirDrop(move, visited.ToList());
                if (!movePlusY)
                    return false;
            }

            move = new Cube(selected.X, selected.Y, selected.Z-1);
            if (!_cubeList.Contains(move) && !visited.Contains(move))
            {
                moveMinusZ = IsAirDrop(move, visited.ToList());
                if (!moveMinusZ)
                    return false;
            }

            move = new Cube(selected.X, selected.Y, selected.Z+1);
            if (!_cubeList.Contains(move) && !visited.Contains(move))
            {
                movePlusZ = IsAirDrop(move, visited);
                if (!movePlusZ)
                    return false;
            }

            return moveMinusX && movePlusX && moveMinusY && movePlusY && moveMinusZ && movePlusZ;
        }


        public static Boolean BorderZMinus(Cube selected)
        {
            for (int i = 0; i < 25; i++)
            {
                if (_cubeList.Contains(new Cube(selected.X, selected.Y, selected.Z - i)))
                    return true;
            }
            return false;
        
        }

        public static Boolean BorderZPlus(Cube selected)
        {
            for (int i = 0; i < 25; i++)
            {
                if (_cubeList.Contains(new Cube(selected.X, selected.Y, selected.Z + i)))
                    return true;
            }
            return false;

        }

        public static Boolean BorderXMinus(Cube selected)
        {
            for (int i = 0; i < 25; i++)
            {
                if (_cubeList.Contains(new Cube(selected.X - i, selected.Y, selected.Z)))
                    return true;
            }
            return false;

        }

        public static Boolean BorderXPlus(Cube selected)
        {
            for (int i = 0; i < 25; i++)
            {
                if (_cubeList.Contains(new Cube(selected.X + i, selected.Y, selected.Z)))
                    return true;
            }
            return false;

        }

        public static Boolean BorderYMinus(Cube selected)
        {
            for (int i = 0; i < 25; i++)
            {
                if (_cubeList.Contains(new Cube(selected.X, selected.Y - i, selected.Z)))
                    return true;
            }
            return false;

        }

        public static Boolean BorderYPlus(Cube selected)
        {
            for (int i = 0; i < 25; i++)
            {
                if (_cubeList.Contains(new Cube(selected.X, selected.Y + i, selected.Z)))
                    return true;
            }
            return false;

        }


    }

}
