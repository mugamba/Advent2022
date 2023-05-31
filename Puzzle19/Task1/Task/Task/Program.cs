using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Task
{
    class Program
    {
        public static List<RobotFactory> _factories = new List<RobotFactory>();

        public static Dictionary<string, int> _dicitionary = new Dictionary<string, int>();
        

        static void Main(string[] args)
        {

            var lines = File.ReadAllLines("input.txt");
            var counter = 0;
            foreach (var line in lines)
            {
                _factories.Add(new RobotFactory(line, counter.ToString()));
                counter++;
            }
            foreach (var robot in _factories)
            {
                robot.DoTurn(0, new Boolean[] { false, false, false, false }, robot._items, robot._robots);
            }

            //Console.WriteLine("Result is {0}", first * second);
            Console.ReadKey();
        }


        public class RobotFactory
        {
            public int[] _items = new int[4];
            public int[] _robots = new int[4];
            public String _name;
        
            public List<Tuple<int, int>> _robotreqs = new List<Tuple<int, int>>();

            public RobotFactory(String line, String name) 
            {
               var splits = line.Split('.');
               var match = Regex.Match(splits[0].Split(':')[1], @"(\d+)[^\d]+ore");
               var oreRobotCost = Int32.Parse(match.Value.Replace("ore", "").Trim());

               var match1 = Regex.Match(splits[1], @"(\d+)[^\d]+ore");
               var clayRobotCost = Int32.Parse(match1.Value.Replace("ore", "").Trim());

                var match2 = Regex.Match(splits[2], @"(\d+)[^\d]+ore");
                var obsidinaRobotCostOre = Int32.Parse(match2.Value.Replace("ore", "").Trim());
                var match3 = Regex.Match(splits[2], @"(\d+)[^\d]+clay");
                var obsidinaRobotCostClay = Int32.Parse(match3.Value.Replace("clay", "").Trim());

                var match4 = Regex.Match(splits[3], @"(\d+)[^\d]+ore");
                var geodeRobotCostOre = Int32.Parse(match4.Value.Replace("ore", "").Trim());
                var match5 = Regex.Match(splits[3], @"(\d+)[^\d]+obsidian");
                var geodeRobotCostObsidian = Int32.Parse(match5.Value.Replace("obsidian", "").Trim());

                _robotreqs.Add(new Tuple<int, int>(oreRobotCost, 0));
                _robotreqs.Add(new Tuple<int, int>(clayRobotCost, 0));
                _robotreqs.Add(new Tuple<int, int>(obsidinaRobotCostOre, obsidinaRobotCostClay));
                _robotreqs.Add(new Tuple<int, int>(geodeRobotCostOre, geodeRobotCostObsidian));
                _robots[0] = 1;
                _name = name;
            }
             public void DoTurn(int step, Boolean[] production, int[] items, int[] robots)
             {
                if (_dicitionary.Count > 0)
                {
                    var max = _dicitionary.Select(o => o.Value).Max();
                    if (max != 0 && 24 - step < max)
                        return;
                }

                if (step == 24)
                {
                    _items = items;
                    _robots = robots;

                    if (_dicitionary.ContainsKey(_name) && _dicitionary[_name] < items[3])
                    {
                        _dicitionary.Add(_name, items[3]);
                    }
                    else
                    { 
                        if (!_dicitionary.ContainsKey(_name))
                            _dicitionary.Add(_name, items[3]);

                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (production[i])
                    {

                        if (i == 0)
                        {
                            items[0] = items[0] - _robotreqs[0].Item1;

                        }
                        if (i == 1)
                        {
                            items[0] = items[0] - _robotreqs[1].Item1;

                        }
                        if (i == 2)
                        {
                            items[0] = items[0] - _robotreqs[2].Item1;
                            items[1] = items[1] - _robotreqs[2].Item2;


                        }
                        if (i == 3)
                        {
                            items[0] = items[0] - _robotreqs[3].Item1;
                            items[2] = items[2] - _robotreqs[3].Item2;
                        }
                    }
                }

                var canProduceGeodeRobot = _robotreqs[3].Item1 <= items[0] && _robotreqs[3].Item2 <= items[2];
                var canProduceObsidianRobot = _robotreqs[2].Item1 <= items[0] && _robotreqs[2].Item2 <= items[1];
                var canProduceClayRobot = _robotreqs[1].Item1 <= items[0];
                var canProduceOreRobot = _robotreqs[0].Item1 <= items[0];

                
                for (int i = 0; i < 4; i++)
                {
                    items[i] += robots[i];
                }
                for (int i = 0; i < 4; i++)
                {
                    if (production[i])
                        robots[i] += 1;
                }

                for (int i = 0; i < 5; i++)
                {
                    if (canProduceGeodeRobot && i == 0)
                        DoTurn(step + 1, new Boolean[] { false, false, false, true }, items.ToArray(), robots.ToArray());

                    if (canProduceObsidianRobot && i==1)
                        DoTurn(step + 1, new Boolean[] { false, false, true, false }, items.ToArray(), robots.ToArray());

                    if (canProduceClayRobot && i==2)
                        DoTurn(step + 1, new Boolean[] { false, true, false, false }, items.ToArray(), robots.ToArray());

                    if (canProduceOreRobot && i==3)
                        DoTurn(step + 1, new Boolean[] { true, false, false, false }, items.ToArray(), robots.ToArray());

                    if (i==4)
                        DoTurn(step + 1, new Boolean[] { false, false, false, false }, items.ToArray(), robots.ToArray());
                }
             }

          
        }
    }
}
