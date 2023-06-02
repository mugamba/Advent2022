using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Task
{
    class Program
    {
        public static List<Cube> _cubeList = new List<Cube>();
       

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

            var number = _cubeList.Count*6 - intersection;


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

    }

}
