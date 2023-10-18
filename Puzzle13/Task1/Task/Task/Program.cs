using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    class Program
    {
        public static List<Tuple<string, string>> _list = new List<Tuple<string, string>>();
        public static Dictionary<int, int> _indices = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            // Console.WriteLine("Shortest path {0}:", _distanceFromStart[_end]);
            for (int i = 0; i < lines.Length; i = i + 3)
            {
                _list.Add(new Tuple<string, string>(lines[i], lines[i + 1]));
            }

            int k = 1;
            foreach (var tuple in _list)
            {
                _indices.Add(k, CompareElements(tuple.Item1, tuple.Item2));
                k++;
            }


            

            Console.WriteLine("Result is {0}", _indices.Where(o => o.Value == 1).Select(o => o.Key).Sum());
            Console.ReadKey();
        }

        private static Int32 CompareElements(string left, String right)
        {
            int resultLeft = 0;
            int resultRight = 0;
            if (Int32.TryParse(left, out resultLeft) && Int32.TryParse(right, out resultRight))
            {
                if (resultLeft == resultRight)
                    return 0;

                if (resultLeft < resultRight)
                    return 1;

                if (resultLeft > resultRight)
                    return -1;

            }
            List<string> leftOperands = SplitElements(left);
            List<string> rightOperands = SplitElements(right);

            var test = leftOperands.Count > rightOperands.Count ? rightOperands.Count : leftOperands.Count;

            for (int i = 0; i < test; i++)
            {
                if (CompareElements(leftOperands[i], rightOperands[i]) == 0)
                    continue;

                else
                    return CompareElements(leftOperands[i], rightOperands[i]);
            }


            if (leftOperands.Count == rightOperands.Count)
                return 0;

            if (leftOperands.Count < rightOperands.Count)
                return 1;
            else
                return -1;
        }

        private static List<string> SplitElements(string left)
        {
            var openedBeforelast = 0;
            for (int i = 0; i < left.Length - 1; i++)
            {
                if (left[i] == '[')
                    openedBeforelast += 1;
                if (left[i] == ']')
                    openedBeforelast -= 1;
            }

            if (openedBeforelast > 0 && left.EndsWith("]") && left.StartsWith("["))
                left = left.Substring(1, left.Length - 2);

            List<string> strings = new List<string>();

            var openedincoma = 0;
            var builder = new StringBuilder();
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] == '[')
                    openedincoma += 1;
                if (left[i] == ']')
                    openedincoma -= 1;

                if (left[i] == ',' && openedincoma == 0)
                {

                    strings.Add(builder.ToString());
                    builder.Clear();
                    continue;
                }

                builder.Append(left[i]);

                if (i == left.Length - 1)
                    strings.Add(builder.ToString());
            }

            return strings;
        }

    }

}
