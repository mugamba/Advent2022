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

        public static Dictionary<Tuple<int, int>, Tuple<long, long>> _occurence = new Dictionary<Tuple<int, int>, Tuple<long, long>>();
        public static long _toPlay = 0;
        public static long _multiplyHeight = 0;
        public static long _heightCalculated = 0;
        public static int _lineFeed = 0;

        /*They are all on the ground floor*/
        public static HashSet<long>[] _columns = new HashSet<long>[7]
        {   new HashSet<long>() { 0 },
            new HashSet<long>() { 0 },
            new HashSet<long>() { 0 },
            new HashSet<long>() { 0 },
            new HashSet<long>() { 0 },
            new HashSet<long>() { 0 },
            new HashSet<long>() { 0 }
        };
        public static IList<long> _maxelements =new List<long>() { 0, 0, 0, 0, 0, 0, 0};
        public static IList<long> _lowElements = new List<long>() { 0, 0, 0, 0, 0, 0, 0 };
    
        static void Main(string[] args)
        {
            var line = File.ReadAllLines("input.txt").FirstOrDefault().ToCharArray();
            var _lineFeed = 0;
            var toBreak = false;

            for (long i = 0; i < 1000000000000; i++)
            {
                var top = _maxelements.Max();
                var falingPiece = GetFalingPiece(i, top);
                var toFall = true;
                if (toBreak)
                    break;

                while (toFall)
                {
                    if (line[_lineFeed] == '>')
                    {
                        if (CanMoveRight(falingPiece))
                        {
                            falingPiece = MoveRight(falingPiece);
                        }
                    }
                    if (line[_lineFeed] == '<')
                    {
                        if (CanMoveLeft(falingPiece))
                        {
                            falingPiece = MoveLeft(falingPiece);
                        }
                    }

                   

                    if (CanMoveDown(falingPiece))
                    {
                        falingPiece = MoveDown(falingPiece);
                    }
                    else                    
                    {
                        /*stoped*/
                        AddFalingPieceToHashMap(falingPiece, _lineFeed, i);

                        /*we get multiple occurencies*/
                        if (_heightCalculated != 0)
                        {
                            toBreak = true;
                        }
                     
                        toFall = false;
                    }

                    _lineFeed = (_lineFeed + 1) % line.Length;

                }
            }

           _columns = new HashSet<long>[7]
            {   new HashSet<long>() { 0 },
                new HashSet<long>() { 0 },
                new HashSet<long>() { 0 },
                new HashSet<long>() { 0 },
                new HashSet<long>() { 0 },
                new HashSet<long>() { 0 },
                new HashSet<long>() { 0 }
            };
         _maxelements = new List<long>() { 0, 0, 0, 0, 0, 0, 0 };
         _lowElements = new List<long>() { 0, 0, 0, 0, 0, 0, 0 };


            for (long i = 1; i < _toPlay; i++)
            {
                var top = _maxelements.Max();
                var falingPiece = GetFalingPiece(i, top);
                var toFall = true;

                while (toFall)
                {
                    if (line[_lineFeed] == '>')
                    {
                        if (CanMoveRight(falingPiece))
                        {
                            falingPiece = MoveRight(falingPiece);
                        }
                    }
                    if (line[_lineFeed] == '<')
                    {
                        if (CanMoveLeft(falingPiece))
                        {
                            falingPiece = MoveLeft(falingPiece);
                        }
                    }

                    _lineFeed = (_lineFeed + 1) % line.Length;

                    if (CanMoveDown(falingPiece))
                    {
                        falingPiece = MoveDown(falingPiece);
                    }
                    else
                    {
                        /*stoped*/
                        AddFalingPieceToHashMap(falingPiece, _lineFeed, i);

                      

                        toFall = false;
                    }
                }
            }

          var test =   _maxelements.Max() + _heightCalculated;


          
        }

        private static void AddFalingPieceToHashMap(List<Tuple<long, long>> falingPiece, int linefeed, long element)
        {
            falingPiece.ForEach(o => _columns[o.Item1].Add(o.Item2));
            _maxelements[0] =_columns[0].Max();
            _maxelements[1] = _columns[1].Max();
            _maxelements[2] = _columns[2].Max();
            _maxelements[3] = _columns[3].Max();
            _maxelements[4] = _columns[4].Max();
            _maxelements[5] = _columns[5].Max();
            _maxelements[6] = _columns[6].Max();


            if (_maxelements[0] == _maxelements[1] 
                && _maxelements[0] == _maxelements[2] 
                && _maxelements[0] == _maxelements[3] 
                && _maxelements[0] == _maxelements[4]
                && _maxelements[0] == _maxelements[5]
                && _maxelements[0] == _maxelements[6]
                )
            {

                int falingPieceNumber =(int) (element % 5);
                var key = new Tuple<int, int>(linefeed, falingPieceNumber);
                var cuurentHeight = _maxelements[0];

                if (_occurence.ContainsKey(new Tuple<int, int>(linefeed, falingPieceNumber)))
                {

                    var iteratorPatern = element - _occurence[new Tuple<int, int>(linefeed, falingPieceNumber)].Item2;
                    var heightdiffernece = cuurentHeight - _occurence[new Tuple<int, int>(linefeed, falingPieceNumber)].Item1;

                    _multiplyHeight = (1000000000000 - _occurence[new Tuple<int, int>(linefeed, falingPieceNumber)].Item2) / iteratorPatern;
                    _toPlay = (1000000000000 - _occurence[new Tuple<int, int>(linefeed, falingPieceNumber)].Item2) % iteratorPatern;
                    _heightCalculated = _occurence[new Tuple<int, int>(linefeed, falingPieceNumber)].Item1 + _multiplyHeight * heightdiffernece;
                    return;
                }
                else
                     _occurence.Add(new Tuple<int, int>(linefeed, falingPieceNumber), new Tuple<long, long>(cuurentHeight, element));
              
            }
        }

        private static Boolean CanMoveDown(List<Tuple<long, long>> falingPiece)
        {
            var min = falingPiece.Select(o => o.Item2).Min();
            if (min - 1 == 0)
                return false;

           var list = falingPiece.Select(o => new Tuple<long,long>(o.Item1, o.Item2 - 1)).ToList();
            return list.All(o => !_columns[o.Item1].Contains(o.Item2));

        }

        private static List<Tuple<long,long>> MoveDown(List<Tuple<long,long>> falingPiece)
        {
            return falingPiece.Select(o => new Tuple<long,long>(o.Item1, o.Item2-1)).ToList();
        }

        private static List<Tuple<long,long>> MoveLeft(List<Tuple<long,long>> falingPiece)
        {
            return falingPiece.Select(o => new Tuple<long,long>(o.Item1 - 1, o.Item2)).ToList();
        }

        private static List<Tuple<long,long>> MoveRight(List<Tuple<long,long>> falingPiece)
        {
            return falingPiece.Select(o => new Tuple<long,long>(o.Item1 + 1, o.Item2)).ToList();
        }

        private static bool CanMoveRight(List<Tuple<long,long>> falingPiece)
        {
           var max = falingPiece.Select(o => o.Item1).Max();
           if (max + 1 > 6)
                return false;

            var list = falingPiece.Select(o => new Tuple<long,long>(o.Item1+1, o.Item2)).ToList();
            return list.All(o => !_columns[o.Item1].Contains(o.Item2));

        }

        private static bool CanMoveLeft(List<Tuple<long,long>> falingPiece)
        {
            var min = falingPiece.Select(o => o.Item1).Min();
            if (min - 1 < 0)
                return false;

            var list = falingPiece.Select(o => new Tuple<long,long>(o.Item1 -1, o.Item2)).ToList();
            return list.All(o => !_columns[o.Item1].Contains(o.Item2));
        }

        public static List<Tuple<long, long>> GetFalingPiece(long iteration, long toppiece)
        {
            /*@@@@*/
            if (iteration % 5 == 0)
            { 
                var points = new List<Tuple<long, long>> ();
                points.Add(new Tuple<long,long>(2, toppiece + 4));
                points.Add(new Tuple<long, long>(3, toppiece + 4));
                points.Add(new Tuple<long, long>(4, toppiece + 4));
                points.Add(new Tuple<long, long>(5, toppiece + 4));
                return points;
            }
            /*.@.*/
            /*@@@*/
            /*.@.*/
            if (iteration % 5 == 1)
            {
                var points = new List<Tuple<long, long>>();
                points.Add(new  Tuple<long,long>(3, toppiece + 4));
                points.Add(new  Tuple<long,long>(2, toppiece + 5));
                points.Add(new  Tuple<long,long>(3, toppiece + 5));
                points.Add(new  Tuple<long,long>(4, toppiece + 5));
                points.Add(new  Tuple<long, long>(3, toppiece + 6));
                return points;
            }

            /*..@*/
            /*..@*/
            /*@@@*/
            if (iteration % 5 == 2)
            {
                var points = new List<Tuple<long, long>>();
                points.Add(new  Tuple<long,long>(2, toppiece + 4));
                points.Add(new  Tuple<long,long>(3, toppiece + 4));
                points.Add(new  Tuple<long,long>(4, toppiece + 4));
                points.Add(new  Tuple<long,long>(4, toppiece + 5));
                points.Add(new Tuple<long, long>(4, toppiece + 6));
                return points;
            }

            /*@*/
            /*@*/
            /*@*/
            /*@*/
            if (iteration % 5 == 3)
            {
                var points = new List<Tuple<long, long>>();
                points.Add(new Tuple<long, long>(2, toppiece + 4));
                points.Add(new Tuple<long, long> (2, toppiece + 5));
                points.Add(new Tuple<long, long>(2, toppiece + 6));
                points.Add(new Tuple<long, long>(2, toppiece + 7));
                return points;
            }

            /*...*/
            /*@@.*/
            /*@@.*/
            if (iteration % 5 == 4)
            {
                var points = new List<Tuple<long, long>>();
                points.Add(new Tuple<long, long>(2, toppiece + 4));
                points.Add(new Tuple<long, long>(2, toppiece + 5));
                points.Add(new Tuple<long, long>(3, toppiece + 4));
                points.Add(new Tuple<long, long>(3, toppiece + 5));
                return points;
            }


            return null;
        }
    }
}
