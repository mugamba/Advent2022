using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Task
{
    class Program
    {
        /*They are all on the ground floor*/
        public static HashSet<int>[] _columns = new HashSet<int>[7]
        {   new HashSet<int>() { 0 },
            new HashSet<int>() { 0 },
            new HashSet<int>() { 0 },
            new HashSet<int>() { 0 },
            new HashSet<int>() { 0 },
            new HashSet<int>() { 0 },
            new HashSet<int>() { 0 }
        };
        public static IList<int> _maxelements =new List<int>() { 0, 0, 0, 0, 0, 0, 0};
        public static IList<int> _lowElements = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };
    
        static void Main(string[] args)
        {
            var line = File.ReadAllLines("input.txt").FirstOrDefault().ToCharArray();
            var linefeed = 0;

          
            for (int i = 0; i < 2022; i++)
            {
                var top = _maxelements.Max();
                var falingPiece = GetFalingPiece(i, top);
                var toFall = true;

                while (toFall)
                {
                    if (line[linefeed] == '>')
                    {
                        if (CanMoveRight(falingPiece))
                        {
                            falingPiece = MoveRight(falingPiece);
                        }
                    }
                    if (line[linefeed] == '<')
                    {
                        if (CanMoveLeft(falingPiece))
                        {
                            falingPiece = MoveLeft(falingPiece);
                        }
                    }

                    linefeed = (linefeed + 1) % line.Length;

                    if (CanMoveDown(falingPiece))
                    {
                        falingPiece = MoveDown(falingPiece);
                    
                    }
                    else                    
                    {

                        /*stoped*/
                        AddFalingPieceToHashMap(falingPiece); 
                        toFall = false;

                    }    
                }
            }
            var test = _maxelements.Max();
            //_lowElements.Min();
        }

        private static void AddFalingPieceToHashMap(List<Point> falingPiece)
        {
            falingPiece.ForEach(o => _columns[o.X].Add(o.Y));
            _maxelements[0] =_columns[0].Max();
            _maxelements[1] = _columns[1].Max();
            _maxelements[2] = _columns[2].Max();
            _maxelements[3] = _columns[3].Max();
            _maxelements[4] = _columns[4].Max();
            _maxelements[5] = _columns[5].Max();
            _maxelements[6] = _columns[6].Max();


        }

        private static Boolean CanMoveDown(List<Point> falingPiece)
        {
            var min = falingPiece.Select(o => o.Y).Min();
            if (min - 1 == 0)
                return false;

           var list = falingPiece.Select(o => new Point(o.X, o.Y - 1)).ToList();
            return list.All(o => !_columns[o.X].Contains(o.Y));

        }

        private static List<Point> MoveDown(List<Point> falingPiece)
        {
            return falingPiece.Select(o => new Point(o.X, o.Y-1)).ToList();
        }

        private static List<Point> MoveLeft(List<Point> falingPiece)
        {
            return falingPiece.Select(o => new Point(o.X - 1, o.Y)).ToList();
        }

        private static List<Point> MoveRight(List<Point> falingPiece)
        {
            return falingPiece.Select(o => new Point(o.X + 1, o.Y)).ToList();
        }

        private static bool CanMoveRight(List<Point> falingPiece)
        {
           var max = falingPiece.Select(o => o.X).Max();
           if (max + 1 > 6)
                return false;

            var list = falingPiece.Select(o => new Point(o.X+1, o.Y)).ToList();
            return list.All(o => !_columns[o.X].Contains(o.Y));

        }

        private static bool CanMoveLeft(List<Point> falingPiece)
        {
            var min = falingPiece.Select(o => o.X).Min();
            if (min - 1 < 0)
                return false;

            var list = falingPiece.Select(o => new Point(o.X -1, o.Y)).ToList();
            return list.All(o => !_columns[o.X].Contains(o.Y));
        }

        public static List<Point> GetFalingPiece(int iteration, int toppiece)
        {
            /*@@@@*/
            if (iteration % 5 == 0)
            { 
                var points = new List<Point>();
                points.Add(new Point(2, toppiece + 4));
                points.Add(new Point(3, toppiece + 4));
                points.Add(new Point(4, toppiece + 4));
                points.Add(new Point(5, toppiece + 4));
                return points;
            }
            /*.@.*/
            /*@@@*/
            /*.@.*/
            if (iteration % 5 == 1)
            {
                var points = new List<Point>();
                points.Add(new Point(3, toppiece + 4));
                points.Add(new Point(2, toppiece + 5));
                points.Add(new Point(3, toppiece + 5));
                points.Add(new Point(4, toppiece + 5));
                points.Add(new Point(3, toppiece + 6));
                return points;
            }

            /*..@*/
            /*..@*/
            /*@@@*/
            if (iteration % 5 == 2)
            {
                var points = new List<Point>();
                points.Add(new Point(2, toppiece + 4));
                points.Add(new Point(3, toppiece + 4));
                points.Add(new Point(4, toppiece + 4));
                points.Add(new Point(4, toppiece + 5));
                points.Add(new Point(4, toppiece + 6));
                return points;
            }

            /*@*/
            /*@*/
            /*@*/
            /*@*/
            if (iteration % 5 == 3)
            {
                var points = new List<Point>();
                points.Add(new Point(2, toppiece + 4));
                points.Add(new Point(2, toppiece + 5));
                points.Add(new Point(2, toppiece + 6));
                points.Add(new Point(2, toppiece + 7));
                return points;
            }

            /*...*/
            /*@@.*/
            /*@@.*/
            if (iteration % 5 == 4)
            {
                var points = new List<Point>();
                points.Add(new Point(2, toppiece + 4));
                points.Add(new Point(2, toppiece + 5));
                points.Add(new Point(3, toppiece + 4));
                points.Add(new Point(3, toppiece + 5));
                return points;
            }


            return null;
        }
    }
}
