using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private Dictionary<string, int> Counts = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            string[] inputFiles = new string[] {  };
            Program calc = new Program();

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key after diag window initializes ");
                Console.ReadLine();
            }

            var sw = Stopwatch.StartNew();
            foreach (var file in inputFiles)
            {
                calc.CountProperties(file);
            }
            sw.Stop();

            foreach (var item in calc.Counts.Keys)
            {
                Console.WriteLine($"{item}: {calc.Counts[item]}");
            }

            Console.WriteLine($"Completed in {sw.ElapsedMilliseconds}ms");

            if (Debugger.IsAttached) { Debugger.Break(); }
        }

        public void CountProperties(string inputFile)
        {

            using (var fs = new StreamReader(inputFile))
            {
                string line = fs.ReadLine();
                while (!fs.EndOfStream)
                {
                    //Expected format is Value1Type, Value2Type, ID, TimeStamp
                    string[] values = line.Split('\t');
                    string authType = values[0];

                    if (this.Counts.ContainsKey(authType))
                    {
                        this.Counts[authType]++;
                    }
                    else
                    {
                        this.Counts.Add(authType, 1);
                    }

                    line = fs.ReadLine();
                }

            }
        }
    }
}
