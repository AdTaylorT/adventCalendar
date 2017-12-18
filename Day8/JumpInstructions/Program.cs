using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JumpInstructions
{
    class Program
    {
        public static Dictionary<string, int> registers;
        public enum RegisterOperation { inc, dec };

        /*
         * b inc 5 if a > 1
         * a inc 1 if b < 5
         * c dec -10 if a >= 10
         * c inc -20 if c == 10
        */
        static void Main(string[] args)
        {
            string input = "b inc 5 if a > 1";
            ParseLine(input);
            WaitToClose();
        }

        public static string ParseLine(string line)
        {
            StringSplitOptions opt = new StringSplitOptions();
            RegisterOperation ro = RegisterOperation.inc;
            
            if (line.Contains("dec"))
            {
                ro = RegisterOperation.dec;
            }

            // parse the line for the actions to take
            string[] args = {"inc", "dec", "if"};
            string[] res = line.Split(args, opt);
            for(int i = 0; i < res.Length; i++)
            {
                res[i] = res[i].Trim();
            }

            // add new "register"
            if (! registers.ContainsKey(res[0]))
            {
                registers.Add(res[0], 0);
            }
            
            Array.ForEach<string>(res, x => Console.WriteLine(x));
            return "yers";
        }

        public static void WaitToClose()
        {
            Console.WriteLine("Closing ...");
            Console.ReadKey();
        }
    }
}
