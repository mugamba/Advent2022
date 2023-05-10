using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task
{
    class Program
    {
        public static Dir _tree = null;
        public static Dir _currentDir = null;
        public static long _globalsum = 0;


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            
            
            foreach (var line in lines)
            {
                if (line.Contains("$ cd"))
                {

                    var split = line.Split(" ");
                    var cdfunc = split[2];

                    if (cdfunc == "..")
                    {
                        _currentDir = _currentDir.Parent;
                    }
                    else
                    {
                        var temp = new Dir();
                        temp.Name = cdfunc;
                        if (cdfunc == "/")
                        {
                            _tree = temp;
                            temp.Parent = null;
                            _currentDir = temp;
                        }
                        else
                        {
                            temp.Parent = _currentDir;
                            _currentDir.Dirs.Add(temp);
                            _currentDir = temp;
                        }
                    }
                }

                if (!(line.StartsWith("$") || line.StartsWith("dir")))
                {
                    var filesize = line.Split(" ");
                    _currentDir.Files.Add(long.Parse(filesize[0]));
                }

            }
            TraverseTree(_tree);

            Console.WriteLine("Result is {0}", _globalsum);
            Console.ReadKey(); 
        }

        public static void TraverseTree(Dir current)
        {
            if (current.DirSize <= 100000)
            {
                _globalsum += current.DirSize;
            }

            foreach (var child in current.Dirs)
            {
                TraverseTree(child);
            }
        }
    }


    


    public class Dir
    {

        public Dir()
        {
            Dirs = new List<Dir>();
            Files = new List<long>(); 
        }

        public String Name { get; set; }
        public List<Dir> Dirs { get; set; }
        public List<long> Files { get; set; }
        public Dir Parent { get; set; }

        public long DirSize
        {
            get { return (Files == null ? 0 : Files.Sum()) + (Dirs == null ? 0 : Dirs.Select(o => o.DirSize).Sum()); }
        }
    }
}
