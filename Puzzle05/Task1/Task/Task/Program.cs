using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    class Program
    {
        public static Dictionary<int, List<char>> _initial = new Dictionary<int, List<char>>();
        public static Dictionary<int, Stack<char>> _stacks = new Dictionary<int, Stack<char>>();

        static void Main(string[] args)
        {

            var lines = File.ReadAllLines("input.txt");
            var initalStacks = lines.Where(o => o.Contains("[")).ToArray();
            var firstline = lines.First();

            for (int i = 1; i < firstline.Length; i = i + 4)
            {
                var index = i / 4 + 1;
                _initial.Add(index, new List<char>());
            }

            foreach (var line in initalStacks)
            {
                if (line == string.Empty)
                    break;

                for (int i = 1; i < line.Length; i = i + 4)
                {
                    var index = i / 4 + 1;
                    if (line[i] != ' ')
                        _initial[index].Add(line[i]);
                }
            }

            foreach (var list in _initial)
            {
                _stacks.Add(list.Key, new Stack<char>(list.Value.ToArray().Reverse()));
            }


            var moves = lines.Where(o => o.Contains("move")).ToArray();
            foreach (var move in moves)
            {
                var splits = move.Split(' ');

                var quantity = Int32.Parse(splits[1]);
                var from = Int32.Parse(splits[3]);
                var to = Int32.Parse(splits[5]);
                PlayMoves(quantity, from, to);
            }
            var word = new StringBuilder();

            foreach (var stack in _stacks)
            {
                word.Append(stack.Value.Peek());
            }
            
            Console.WriteLine("Result is {0}", word.ToString());
            Console.ReadKey();
        }

        private static void PlayMoves(int quantity, int from, int to)
        {
            
            for (int i = 0; i < quantity; i++)
            {
                var item = _stacks[from].Pop();
                _stacks[to].Push(item);
            }
        }
    }
}
