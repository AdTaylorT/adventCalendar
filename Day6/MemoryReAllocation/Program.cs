using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryReAllocation
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

        public static void Day1()
        {
            int[] memoryBanks = new int[] { 4,1,15,12,0,9,9,5,5,8,7,3,14,5,12,3 };
            int[] currentProfile = (int[])memoryBanks.Clone();
            int countOfRedis = 0;

            Dictionary<int[], int> memoryDistributionHist = new Dictionary<int[], int>(new MyEqualityComparer());
            while (!memoryDistributionHist.ContainsKey(currentProfile))
            {
                memoryDistributionHist.Add(currentProfile, 1);
                int largestMemBank = memoryBanks.Max();
                int indexOfLargestMemBank = Array.IndexOf(memoryBanks, largestMemBank);

                int index = (memoryBanks.Length > indexOfLargestMemBank + 1) ? indexOfLargestMemBank + 1 : 0;
                int redistributionValue = largestMemBank;
                memoryBanks[indexOfLargestMemBank] = 0;
                while (redistributionValue > 0)
                {
                    memoryBanks[index] += 1;
                    redistributionValue--;
                    index = (memoryBanks.Length > index + 1) ? index + 1 : 0;
                }
                currentProfile = (int[])memoryBanks.Clone(); ;
                countOfRedis++;
            }
            Console.WriteLine("Number of re-dis: " + countOfRedis);
        }

        public static void Day2()
        {

        }

        public class MyEqualityComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                if (x.Length != y.Length)
                {
                    return false;
                }
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(int[] obj)
            {
                int result = 17;
                for (int i = 0; i < obj.Length; i++)
                {
                    unchecked
                    {
                        result = result * 23 + obj[i];
                    }
                }
                return result;
            }

        }
    }
}
