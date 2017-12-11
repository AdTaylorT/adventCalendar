using System;
using System.Collections.Generic;
using System.IO;

namespace InstructionSet
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
            if (day == '2')
            {
                Day2();
            }
            Console.ReadKey();
        }

        // TODO: improve this algorithm to read a block until a return character is read, 
        // that way we are reading from the file and not loading the whole list into memory.
        public static void Day1()
        {
            List<int> instructions = new List<int>();
            using (StreamReader sr = new StreamReader("..\\..\\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    instructions.Add(int.Parse(sr.ReadLine()));
                }
            }

            int countOfSteps = 0;
            int index = 0;
            try
            {
                while (index < instructions.Count)
                {
                    int jumpAhead = instructions[index]++;
                    int newIndex = index + jumpAhead;
                    index = newIndex;
                    countOfSteps++;
                }
            }catch(Exception e)
            {

            }
            finally
            {
                Console.WriteLine(countOfSteps);
            }
        }

        public static void Day2()
        {
            List<int> instructions = new List<int>();
            using (StreamReader sr = new StreamReader("..\\..\\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    instructions.Add(int.Parse(sr.ReadLine()));
                }
            }

            int countOfSteps = 0;
            int index = 0;
            try
            {
                while (index < instructions.Count)
                {
                    int jumpAhead = instructions[index];
                    int var = (jumpAhead >= 3) ? instructions[index]-- : instructions[index]++ ;

                    index += jumpAhead;
                    countOfSteps++;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                Console.WriteLine(countOfSteps);
            }
        }
    }
}
