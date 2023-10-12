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

            //var list = new List<RobotProduction>();
            //var production = new RobotProduction();
            //var res = new Resources();
            //res._ore = 26;
            //res._clay = 26;
            //res._obsidian = 26;

            //var req = new RobotRequirements();
            //req._oreRobot = 4;
            //req._clayRobot = 2;
            //req._obsidianRobot = new Tuple<int, int>(3, 14);
            //req._geodeRobot = new Tuple<int, int>(2, 7);

        

            // CalculateAllPosibleProductions(list, production, res, req);

            //Console.WriteLine("Result is {0}", first * second);
            Console.ReadKey();
        }

      

    


    public class RobotFactory
        {
            public int _oreRobots;
            public int _clayRobots;
            public int _obsidianRobots;
            public int _geodeRobots;


            public RobotFactory(String line, String name) 
            {
               var splits = line.Split('.');
               var match = Regex.Match(splits[0].Split(':')[1], @"(\d+)[^\d]+ore");
               var oreRobotCost = Int32.Parse(match.Value.Replace("ore", "").Trim());

               var match1 = Regex.Match(splits[1], @"(\d+)[^\d]+ore");
               var clayRobotCost = Int32.Parse(match1.Value.Replace("ore", "").Trim());

                var match2 = Regex.Match(splits[2], @"(\d+)[^\d]+ore");
                var obsidianRobotCostOre = Int32.Parse(match2.Value.Replace("ore", "").Trim());
                var match3 = Regex.Match(splits[2], @"(\d+)[^\d]+clay");
                var obsidianRobotCostClay = Int32.Parse(match3.Value.Replace("clay", "").Trim());

                var match4 = Regex.Match(splits[3], @"(\d+)[^\d]+ore");
                var geodeRobotCostOre = Int32.Parse(match4.Value.Replace("ore", "").Trim());
                var match5 = Regex.Match(splits[3], @"(\d+)[^\d]+obsidian");
                var geodeRobotCostObsidian = Int32.Parse(match5.Value.Replace("obsidian", "").Trim());


                var req = new RobotRequirements();
                req._oreRobot = oreRobotCost;
                req._clayRobot = clayRobotCost;
                req._obsidianRobot = new Tuple<int, int>(obsidianRobotCostOre, obsidianRobotCostClay);
                req._geodeRobot = new Tuple<int, int>(geodeRobotCostOre, geodeRobotCostObsidian);
                _oreRobots = 1;

                for (int i = 0; i < 24; i++)
                {
                    var list = new List<RobotProduction>();
                    var items = new Resources()
                    {
                        _ore = _oreRobots,
                        _clay = _clayRobots,
                        _obsidian = _obsidianRobots,
                        _geode = _geodeRobots
                    };

                   CalculateAllPosibleProductions(list, new RobotProduction(), items, req);
                    
                   
                }
            }

            public struct RobotProduction
            {
                public int _oreRobots;
                public int _clayRobots;
                public int _obsidianRobots;
                public int _geodeRobots;

                public RobotProduction Clone()
                {
                    var r = new RobotProduction();
                    r._oreRobots = _oreRobots;
                    r._clayRobots = _clayRobots;
                    r._obsidianRobots = _obsidianRobots;
                    r._geodeRobots = _geodeRobots;
                    return r;
                }


            }


            public struct Resources
            {
                public int _ore;
                public int _clay;
                public int _obsidian;
                public int _geode;

                public Resources Clone()
                {
                    var r = new Resources();
                    r._ore = _ore;
                    r._clay = _clay;
                    r._obsidian = _obsidian;
                    r._geode = _geode;
                    return r;
                }
            }
            public struct RobotRequirements
            {
                public int _oreRobot;
                public int _clayRobot;
                public Tuple<int, int> _obsidianRobot;
                public Tuple<int, int> _geodeRobot;
            }




            private static void CalculateAllPosibleProductions(List<RobotProduction> allPossibleProductions, RobotProduction rp, Resources res, RobotRequirements robotRequirements)
            {
                for (int i = 0; i < 4; i++)
                {

                    if (i == 0)
                    {
                        var canProduceOre = res._ore >= robotRequirements._oreRobot;
                        if (canProduceOre)
                        {
                            var robotProductions = rp.Clone();
                            var resources = res.Clone();
                            resources._ore = resources._ore - robotRequirements._oreRobot;
                            robotProductions._oreRobots = robotProductions._oreRobots + 1;
                            CalculateAllPosibleProductions(allPossibleProductions, robotProductions, resources, robotRequirements);
                        }
                    }

                    if (i == 1)
                    {
                        var canProduceClay = res._ore >= robotRequirements._clayRobot;
                        if (canProduceClay)
                        {
                            var robotProductions = rp.Clone();
                            var resources = res.Clone();
                            resources._ore = resources._ore - robotRequirements._clayRobot;
                            robotProductions._clayRobots = robotProductions._clayRobots + 1;
                            CalculateAllPosibleProductions(allPossibleProductions, robotProductions, resources, robotRequirements);
                        }
                    }
                    if (i == 2)
                    {
                        var canProduceObsidian = res._ore >= robotRequirements._obsidianRobot.Item1 && res._clay >= robotRequirements._obsidianRobot.Item2;
                        if (canProduceObsidian)
                        {
                            var robotProductions = rp.Clone();
                            var resources = res.Clone();
                            resources._ore = resources._ore - robotRequirements._obsidianRobot.Item1;
                            resources._clay = resources._clay - robotRequirements._obsidianRobot.Item2;
                            robotProductions._obsidianRobots = robotProductions._obsidianRobots + 1;
                            CalculateAllPosibleProductions(allPossibleProductions, robotProductions, resources, robotRequirements);
                        }
                    }
                    if (i == 3)
                    {
                        var canProduceGeode = res._ore >= robotRequirements._geodeRobot.Item1 && res._obsidian >= robotRequirements._geodeRobot.Item2;
                        if (canProduceGeode)
                        {
                            var robotProductions = rp.Clone();
                            var resources = res.Clone();

                            resources._ore = resources._ore - robotRequirements._geodeRobot.Item1;
                            resources._obsidian = resources._obsidian - robotRequirements._geodeRobot.Item2;
                            robotProductions._geodeRobots = robotProductions._geodeRobots + 1;
                            CalculateAllPosibleProductions(allPossibleProductions, robotProductions, resources, robotRequirements);
                        }
                    }
                }

                allPossibleProductions.Add(rp);
            }

        }
    }
}
