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
        public static List<Monkey> _monkeysList = new List<Monkey>();
       

        static void Main(string[] args)
        {

        
                _monkeysList.Clear();

                var lines = File.ReadAllLines("input.txt");
            Monkey _currentMonkey = null;


            foreach (var line in lines)
            {

                if (line == String.Empty)
                    _monkeysList.Add(_currentMonkey);

                if (line.Contains("Monkey"))
                {
                    _currentMonkey = new Monkey();
                    var splits = line.Split(' ');
                    _currentMonkey.Name = splits[1].Replace(":", "");
                }

                if (line.Contains("Starting items:"))
                {
                    var lineTrimed = line.Replace("Starting items:", "");
                    var splited = lineTrimed.Split(",");
                    foreach (var token in splited)
                    {
                        var toParse = token.Replace(" ", "");
                        _currentMonkey._roundItems.Add(long.Parse(toParse.Trim()));
                    }
                }

                if (line.Contains("Operation:"))
                {
                    var splited = line.Split("=");
                    var test = splited[1];

                    if (test.Contains("*"))
                    {
                        int ott = 0;
                        var splited2 = test.Split('*');
                        Int32.TryParse(splited2[1], out ott);

                        _currentMonkey._operationtype = "*";
                        if (ott > 0)
                        {
                            _currentMonkey._operationParameter = ott;
                        }
                    }

                    if (test.Contains("+"))
                    {
                        int ott = 0;
                        var splited2 = test.Split('+');
                        Int32.TryParse(splited2[1], out ott);

                        _currentMonkey._operationtype = "+";
                        if (ott > 0)
                        {
                            _currentMonkey._operationParameter = ott;
                        }
                    }

                }

                if (line.Contains("Test:"))
                {
                    var splited = line.Split("divisible by");
                    var test = splited[1];
                    _currentMonkey._divisbleBy = long.Parse(test.Trim());
                }

                if (line.Contains("If true:"))
                {
                    var splited = line.Split("throw to monkey");
                    var test = splited[1];
                    _currentMonkey._firstToThrow = test.Trim();
                }

                if (line.Contains("If false:"))
                {
                    var splited = line.Split("throw to monkey");
                    var test = splited[1];
                    _currentMonkey._secondToThrow = test.Trim();
                }

            }
            _monkeysList.Add(_currentMonkey);

            long divisor = 1;
            foreach (var s in _monkeysList)
            {
                divisor = divisor * s._divisbleBy;
            }
            foreach (var s in _monkeysList)
            {
                s._modulo = divisor;
            }


            for (int i = 0; i < 10000; i++)
            {
                foreach (var m in _monkeysList)
                {
                    m.PlayRound();
                }
            }
          
            var top2 = _monkeysList.Select(o => o._played).OrderByDescending(o=>o).Take(2);

            var first = top2.First();
            var second = top2.Last();

            Console.WriteLine("Result is {0}", first * second);
            Console.ReadKey();
        }


        public class Monkey
        {
            public string Name;
            public List<long> _roundItems;
            public long _currentItem;
            public long _divisbleBy;
            public long? _operationParameter;
            public String _operationtype;

            public String _firstToThrow;
            public String _secondToThrow;
            public long _played;
            public long _modulo;
     
            public Monkey() 
            {
                _roundItems = new List<long>();
            }


            public long DoOperation()
            {
                 _currentItem = _roundItems[0];
                _roundItems.RemoveAt(0);
                
                long result = 0;
                if (_operationtype == "+")
                {
                    result = _operationParameter != null ? _currentItem + _operationParameter.Value : _currentItem + _currentItem;
                    result = result % _modulo ;
                }

                if (_operationtype == "*")
                {
                    result = _operationParameter != null ? _currentItem * _operationParameter.Value : _currentItem * _currentItem;
                    result = result % _modulo;
                }

                return result;
            }

            public void DoTest(long result)
            {

      
                var remainder = result % _divisbleBy;
                if (remainder == 0)
                {
                    var monkey = _monkeysList.Where(o => o.Name == _firstToThrow).FirstOrDefault();
                    monkey._roundItems.Add(result);
                }
                else
                {
                    var monkey = _monkeysList.Where(o => o.Name == _secondToThrow).FirstOrDefault();
                    monkey._roundItems.Add(result);
                }
            }

            public void PlayRound()
            {
                while (_roundItems.Count > 0)
                {
                    var result = DoOperation();
                    DoTest(result);
                    _played++;
                }
            }
        }

    }

}
