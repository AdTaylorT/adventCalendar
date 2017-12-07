using System;
using SS;

namespace applet
{
    class Driver
    {
        static void Main(string[] args)
        {
            Row firstRow = new Row(new[] { 5, 1, 9, 5 });
            Row secondRow = new Row(new[] { 7, 5, 3 });
            Row thirdRow = new Row(new[] { 2, 4, 6, 8 });
            SpreadSheet spreadSheet = new SpreadSheet(new[] { firstRow, secondRow, thirdRow });
            SpreadSheet huh = new SpreadSheet(new[] {
                (Row)new[] { 5, 1, 9, 5 },
                (Row)new[] { 7, 5, 3 },
                (Row)new[] { 2, 4, 6, 8 },
            });
            int checkSum = huh.CalculateSheetCheckSum();
            Console.WriteLine(checkSum);
            Console.WriteLine("press any key to close");
            Console.ReadKey();
        }
    }

}