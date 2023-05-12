using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    class Program
    {

      
        public static List<Tuple<int, int>> _tailPositiotions = new List<Tuple<int, int>>();
        public static Tuple<int, int> _currentHead = null;
        public static Tuple<int, int> _currentTail = null;



        static void Main(string[] args)
        {
         
            var lines = File.ReadAllLines("input.txt");

            _currentHead = new Tuple<int, int>(0, 0);
            _currentTail = new Tuple<int, int>(0, 0);
            _tailPositiotions.Add(_currentTail);

            foreach (var line in lines)
            {

                var splits = line.Split(' ');
                var direction = splits[0];
                var steps = Int32.Parse(splits[1]);

                if (direction == "U")
                {
                    for (int i = 0; i < steps; i++)
                    {
                        _currentHead = MoveUp(_currentHead);
                        _currentTail = DoTailMove(_currentHead, _currentTail);

                        if (!_tailPositiotions.Contains(_currentTail))
                            _tailPositiotions.Add(_currentTail);
                    }

                }

                if (direction == "D")
                {
                    for (int i = 0; i < steps; i++)
                    {
                        _currentHead = MoveDown(_currentHead);
                        _currentTail = DoTailMove(_currentHead, _currentTail);

                        if (!_tailPositiotions.Contains(_currentTail))
                            _tailPositiotions.Add(_currentTail);

                    }

                }


                if (direction == "R")
                {
                    for (int i = 0; i < steps; i++)
                    {
                        _currentHead = MoveRight(_currentHead);
                        _currentTail = DoTailMove(_currentHead, _currentTail);

                        if (!_tailPositiotions.Contains(_currentTail))
                            _tailPositiotions.Add(_currentTail);

                    }

                }


                if (direction == "L")
                {
                    for (int i = 0; i < steps; i++)
                    {
                        _currentHead = MoveLeft(_currentHead);
                        _currentTail = DoTailMove(_currentHead, _currentTail);

                        if (!_tailPositiotions.Contains(_currentTail))
                            _tailPositiotions.Add(_currentTail);

                    }

                }

            }

            Console.WriteLine("Result is {0}", _tailPositiotions.Count);
            Console.ReadKey();
        }

        public static Tuple<int, int> MoveUp(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1, toMove.Item2 + 1);
            return newPosition;
        }

        public static  Tuple<int, int> MoveDown(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1, toMove.Item2 - 1);
            return newPosition;
        }

        public static Tuple<int, int> MoveLeft(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1 - 1, toMove.Item2);
            return newPosition;
        }

        public static  Tuple<int, int> MoveRight(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1 + 1, toMove.Item2);
            return newPosition;
        }

        public static Tuple<int, int> MoveDownRight(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1 + 1, toMove.Item2 - 1);
            return newPosition;
        }

        public static Tuple<int, int> MoveDownLeft(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1 - 1, toMove.Item2 - 1);
            return newPosition;
        }

        public static Tuple<int, int> MoveUpLeft(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1 - 1, toMove.Item2 + 1);
            return newPosition;
        }

        public static Tuple<int, int> MoveUpRight(Tuple<int, int> toMove)
        {
            var newPosition = new Tuple<int, int>(toMove.Item1 + 1, toMove.Item2 + 1);
            return newPosition;
        }


        public static Tuple<int, int> DoTailMove(Tuple<int, int> head, Tuple<int, int> tail)
        {
            if (head == null || tail == null)
                return tail;

            if (head.Item2 == tail.Item2 && head.Item1 - 2 == tail.Item1)
                return MoveRight(tail);

            if (head.Item2 == tail.Item2 && head.Item1 + 2 == tail.Item1)
                return MoveLeft(tail);

            if (head.Item1 == tail.Item1 && head.Item2 - 2 == tail.Item2)
                return MoveUp(tail);

            if (head.Item1 == tail.Item1 && head.Item2 + 2 == tail.Item2)
                return MoveDown(tail);


            if (head.Item2 - 1 == tail.Item2  && head.Item1 - 2 == tail.Item1)
                return MoveUpRight(tail);

            if (head.Item2 - 2 == tail.Item2 && head.Item1 - 1 == tail.Item1)
                return MoveUpRight(tail);

            if (head.Item2 - 1 == tail.Item2 && head.Item1 + 2 == tail.Item1)
                return MoveUpLeft(tail);

            if (head.Item2 - 2 == tail.Item2 && head.Item1 + 1 == tail.Item1)
                return MoveUpLeft(tail);

            if (head.Item2 + 1 == tail.Item2 && head.Item1 - 2 == tail.Item1)
                return MoveDownRight(tail);

            if (head.Item2 + 2 == tail.Item2 && head.Item1 - 1 == tail.Item1)
                return MoveDownRight(tail);


            if (head.Item2 + 1 == tail.Item2 && head.Item1 + 2 == tail.Item1)
                return MoveDownLeft(tail);

            if (head.Item2 + 2 == tail.Item2 && head.Item1 + 1 == tail.Item1)
                return MoveDownLeft(tail);






            return tail;
        }

    }

}
