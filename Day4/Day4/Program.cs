using System;
using System.Collections;
using System.IO;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Which day?");
            char day = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (day == '1')
            {
                Day1();
            }
            if(day == '2')
            {
                Day2();
            }
            Console.ReadKey();
        }

        public static void Day1()
        {
            int sum = 0;
            using (StreamReader sr = new StreamReader("..\\..\\input.ssv"))
            {
                while ( !sr.EndOfStream )
                {
                    Hashtable hashtable = new Hashtable();
                    string[] words = sr.ReadLine().Split(' ');
                    for(int index = 0; index < words.Length; index++)
                    {
                        string word = words[index];
                        if (!hashtable.ContainsKey(word))
                        {
                            hashtable.Add(word, 1);
                        }
                        else
                        {
                            break;
                        }
                        if (index == words.Length - 1) sum++;
                    }
                }
            }
            Console.WriteLine("There were " + sum + " valid lines");
        }

        public static void Day2()
        {
            int sum = 0;
            using (StreamReader sr = new StreamReader("..\\..\\input.ssv"))
            {
                while (!sr.EndOfStream)
                {
                    Hashtable wordtable = new Hashtable();
                    string[] words = sr.ReadLine().Split(' ');
                    for (int index = 0; index < words.Length; index++)
                    {
                        char[] letters = words[index].ToCharArray();
                        Array.Sort(letters);

                        string s_word = new string(letters);
                        if (!wordtable.ContainsKey(s_word))
                        {
                            wordtable.Add(s_word, 1);
                        }
                        else
                        {
                            break;
                        }
                        if (index == words.Length - 1) sum++;
                    }
                }
            }
            Console.WriteLine("There were " + sum + " valid lines");
        }
    }
}
