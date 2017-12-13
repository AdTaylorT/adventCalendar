using System;
using System.Collections.Generic;
using System.IO;

namespace Circus
{
    //TODO custom equality comparer

    public class MyNode
    {
        public int weight;
        public string name;
        public Dictionary<string, MyNode> children = new Dictionary<string, MyNode>();

        public MyNode(string name)
        {
            this.name = name;
        }

        public MyNode(string name, int weight)
        {
            this.name = name;
            this.weight = weight;
        }

        public MyNode(string name, int weight, Dictionary<string, MyNode> children)
        {
            this.name = name;
            this.weight = weight;
            this.children = children;
        }

        public void SetWeight(int weight)
        {
            this.weight = weight;
        }
        public void SetChildren(List<MyNode> children)
        {
            children.ForEach(x => this.children.Add(x.name, x));
        }

        public bool HasChildren() => (this.children.Count > 0);

        //TODO this is complicated with dictionaries
        // or is it?
        public override string ToString()
        {
            string child_string = "";
            foreach(KeyValuePair<string, MyNode> entry in children)
            {
                child_string += entry.ToString();
            }

            return "name: " + name + " weight: " + weight + " children: " + child_string;
        }
    }


    // TODO what is this?
    public class MyEqualityComparer : IEqualityComparer<Dictionary<string, MyNode>>
    {
        public bool Equals(Dictionary<string, MyNode> x, Dictionary<string, MyNode> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }
            

            

            return true;
        }

        public int GetHashCode(Dictionary<string, MyNode> obj)
        {
            int result = 17;            
            foreach(KeyValuePair<string, MyNode> kvp in obj)
            {
                unchecked
                {
                    result = result * 23 + kvp.Key.GetHashCode();
                }
            }

            return result;
        }
    }

    class Program
    {
        public static Dictionary<string, MyNode> myNodes = new Dictionary<string, MyNode>();

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
                MyNode currentNode = ParseInput(line);
                myNodes.Add(currentNode.name, currentNode);
                
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
            foreach(KeyValuePair<string, MyNode> entry in myNodes)
            {
                Console.WriteLine(entry.ToString());
            }
        }

        public static void Day2()
        {
        }

        // input
        // ktlj (57)
        // fwft (72) -> ktlj, cntj, xhth
        public static MyNode ParseInput(string starter)
        {
            string name = "";
            int weight = 0;
            List<MyNode> children = new List<MyNode>();
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
                        children.Add(new MyNode(child.Trim()));
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

            MyNode newNode = new MyNode(name, weight);
            if (children.Count > 0) newNode.SetChildren(children);

            return newNode;
        }
    }
}