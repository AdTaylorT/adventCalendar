using System;
using System.Collections.Generic;
using System.IO;

namespace Circus
{
    public class MyNode
    {
        public int weight;
        public string name;
        public List<MyNode> children = new List<MyNode>();

        public MyNode(string name)
        {
            this.name = name;
        }

        public MyNode(string name, int weight)
        {
            this.name = name;
            this.weight = weight;
        }

        public MyNode(string name, int weight, List<MyNode> children)
        {
            this.name = name;
            this.weight = weight;
            this.children = children;
        }

        public override string ToString()
        {
            string child_string = "";
            foreach (MyNode child in children)
            {
                child_string += child.name +", ";
            }

            return "name: " + name + " weight: " + weight + " children: " + child_string;
        }

        
    }

    class Program
    {
        public static List<MyNode> myNodes = new List<MyNode>();

        static void Main(string[] args)
        {
            /*
            using (StreamReader sr = new StreamReader("..\\..\\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    instructions.Add(sr.ReadLine());
                }
            }*/
            string testinput =
@"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";
            foreach (string line in testinput.Split('\n'))
            {
                myNodes.Add(new MyNode( _initMyNode(line) ));
            }

            Console.Write("Which day?");
            char day = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (day == '1')
            {
                Day1();
            }
            if (day == '2')
            {
                Day2();
            }
            Console.ReadKey();
        }

        public static void Day1()
        {
            

        }

        public static void Day2()
        {
        }

        public void _initMyNode(string starter)
        {
            string name_and_weight = "";
            if (starter.Contains("->"))
            {
                string[] split = starter.Split('-', '>');
                name_and_weight = split[0];
                if (split[2].Contains(","))
                {
                    string[] c_split = split[2].Split(',');
                    foreach (string child in c_split)
                    {
                        children.Add(child.Trim());
                    }
                }

                string[] naw = name_and_weight.Split(' ');
                name = naw[0];
                string weight_string = naw[1];
                weight = int.Parse(weight_string.Trim(' ', '(', ')', '\r'));
            }
            else
            {
                name_and_weight = starter;

                string[] naw = name_and_weight.Split(' ');
                name = naw[0];
                string weight_string = naw[1];
                weight = int.Parse(weight_string.Trim(' ', '(', ')', '\r'));
            }
        }
    }
}