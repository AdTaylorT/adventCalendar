using System;
using System.Collections.Generic;
using System.IO;

namespace Circus
{
    public class MyNode
    {
        public int rawWeight;
        public int weight;
        public string name;
        public Dictionary<string, MyNode> children = new Dictionary<string, MyNode>();
        public bool markedForDeletion = false;

        public MyNode(string name)
        {
            this.name = name;
        }

        public MyNode(string name, int weight)
        {
            this.name = name;
            this.weight = weight;
            this.rawWeight = weight;
        }

        public MyNode(string name, int weight, Dictionary<string, MyNode> children)
        {
            this.name = name;
            this.weight = weight;
            this.rawWeight = weight;
            this.children = children;
        }

        public void SetWeight(int weight)
        {
            this.weight = weight;
        }

        public void ClearChildren()
        {
            children.Clear();
        }

        public void SetChildren(List<MyNode> children)
        {
            children.ForEach(x => this.children.Add(x.name, x));
        }

        public void SetChildren(Dictionary<string, MyNode> children)
        {
            foreach (KeyValuePair<string, MyNode> child in children)
            {
                this.children.Add(child.Key, child.Value);
            }
        }

        public bool HasChildren() => (this.children.Count > 0);
        
        public override string ToString()
        {
            string child_string = "";
            foreach(KeyValuePair<string, MyNode> entry in children)
            {
                child_string += '\t'+entry.ToString()+'\n';
            }

            return "name: " + name + " rawweight: " + rawWeight + " weight: " + weight + " children: " + '\n' + child_string;
        }
    }

    class Program
    {
        public static Dictionary<string, MyNode> myNodes = new Dictionary<string, MyNode>();
        public static int delta = 0;
        public static int myCorrectedWeight = 0;

        public static void setCorrectedWeight(int weight)
        {
            if (myCorrectedWeight == 0) myCorrectedWeight = weight;
        }

        static void Main(string[] args)
        {
            string testinput = "";
            using (StreamReader sr = new StreamReader("..\\..\\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    testinput += (sr.ReadLine() + '\n');
                }
            }
            /*
            testinput =
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
cntj (57)";*/

            foreach (string line in testinput.Split('\n'))
            {
                // if adding a node with children, check if that node already exists in the dictionary
                // move that node to be a child node.

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
            List<string> childList = new List<string>();
            foreach (KeyValuePair<string, MyNode> entry in myNodes)
            {
                foreach (KeyValuePair<string, MyNode> child in entry.Value.children)
                {
                    childList.Add(child.Key);
                }
            }

            foreach (string name in childList)
            {
                myNodes.Remove(name);
            }

            foreach(KeyValuePair<string, MyNode> enter in myNodes)
            {
                Console.WriteLine(enter.Key);
            }
        }

        /*
         * actual notes
         * each tower must have the same weight at the top of the tower
         * their will be one tower whose weight (the sum of it's children) 
         * will not be the same as the other two.
         * -- this is the case we are looking for
         * ** starting from the top we can add the weight of the children to the parent so long as they are all equal
         * *** remove a node once it's children have been found to be equal
         * **abrbitrarily pick a node ( why not the first node)
         * *** go as high as possible ( while it has children )
         * // foreach(kv)
         * // call method goToTop(){
         * // do math and remove from myNodes
         * */
        public static void Day2()
        {
            foreach( KeyValuePair<string, MyNode> kvp in myNodes)
            {
                if (kvp.Value.HasChildren())
                {
                    GetToTheTop(kvp.Value);
                }
                else
                {
                    kvp.Value.markedForDeletion = true;
                }
            }
            Console.WriteLine(delta);
            Console.WriteLine(myCorrectedWeight);

            List<string> deleteNodes = new List<string>();
            foreach (KeyValuePair<string, MyNode> kvp in myNodes)
            {
                if (kvp.Value.markedForDeletion)
                {
                    deleteNodes.Add(kvp.Key);
                }
            }
            foreach(string deleteName in deleteNodes)
            {
                myNodes.Remove(deleteName);
            }
            foreach (KeyValuePair<string, MyNode> kvp in myNodes)
            {
                Console.WriteLine(WeightOfTower(kvp.Value));
            }

            Console.WriteLine(delta);
            Console.WriteLine(myCorrectedWeight);
            Console.WriteLine("done");
            
        }

        // Takes a parent node and correlates its children against the base list of actual nodes
        public static void GetToTheTop( MyNode parent )
        {
            // looks up each child in the main table 
            Dictionary<string, MyNode> realChildren = new Dictionary<string, MyNode>();
            foreach (KeyValuePair<string, MyNode> child in parent.children)
            {
                realChildren.Add(myNodes[child.Key].name, myNodes[child.Key]);
                myNodes[child.Key].markedForDeletion = true;
            }
            parent.ClearChildren();
            parent.SetChildren(realChildren);
            
            // Tests if there is a higher node and goes there if there is.
            foreach (KeyValuePair<string, MyNode> realChild in realChildren)
            {
                if (realChild.Value.HasChildren())
                {
                    GetToTheTop(realChild.Value);
                }
            }
        }

        public static int WeightOfTower(MyNode parent)
        {
            // you don't want the parents raw weight you want the childs that has the wrong value.
            int firstWeight = 0;
            int otherWeight = 0;
            int consensusWeight = 0;
            // at this point we are at the top of a tower looking at children who should have weight,
            // test each childs weight and return that value down a level
            foreach (KeyValuePair<string, MyNode> realChild in parent.children)
            {
                if (realChild.Value.HasChildren())
                {
                    realChild.Value.weight += ( WeightOfTower(realChild.Value) * realChild.Value.children.Count);
                }
                if (firstWeight == 0)
                {
                    firstWeight = realChild.Value.weight;
                    continue;
                }
                if(firstWeight == realChild.Value.weight && consensusWeight == 0)
                {
                    consensusWeight = firstWeight;
                }
                if(firstWeight != realChild.Value.weight )
                {
                    if(otherWeight == 0)
                    {
                        otherWeight = realChild.Value.weight;
                    }
                    else if(otherWeight == realChild.Value.weight && consensusWeight == 0)
                    {
                        consensusWeight = otherWeight;
                    }
                }
            }

            if(otherWeight != 0)
            {
                if(consensusWeight == otherWeight)
                {
                    foreach (KeyValuePair<string, MyNode> child in parent.children)
                    {
                        if(child.Value.weight == firstWeight)
                        {
                            setCorrectedWeight(child.Value.rawWeight);
                        }
                        
                    }
                    delta = consensusWeight - firstWeight;
                }
                else
                {
                    foreach (KeyValuePair<string, MyNode> child in parent.children)
                    {
                        if (child.Value.weight == otherWeight)
                        {
                            setCorrectedWeight(child.Value.rawWeight);
                        }
                    }
                    delta = consensusWeight - otherWeight;
                }
                //1144 is too high
                //584
                //131
                //454
                //3240
                //325
                //744
            }

            return consensusWeight;
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
                if(starter.Length > 1)
                {
                    name_and_weight = starter;

                    string[] naw = name_and_weight.Split(' ');
                    name = naw[0];
                    string weight_string = naw[1];
                    weight = int.Parse(weight_string.Trim(' ', '(', ')', '\r'));

                }
            }

            MyNode newNode = new MyNode(name, weight);
            if (children.Count > 0) newNode.SetChildren(children);

            return newNode;
        }
    }
}