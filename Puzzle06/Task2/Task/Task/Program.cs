using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    class Program
    {
        
        public static List<char> _slidingWindow = new List<char>();
        public static int _counter = 0;

        static void Main(string[] args)
        {

            var lines = File.ReadAllLines("input.txt");
            var firstline = lines.First();

            for (int i = 13; i < firstline.Length; i++)
            {
                _slidingWindow.Clear();

                for (int j = 13; j >= 0; j--)
                {

                    _slidingWindow.Add(firstline[i - j]);
                   
                }

                if (_slidingWindow.Distinct().Count() == 14)
                {
                    _counter = i;
                    break;
                }
                
            }

           
            
            Console.WriteLine("Result is {0}", _counter+1);
            Console.ReadKey();
        }

       
    }
}

}
