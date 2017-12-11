using System;
using System.Collections.Generic;
using System.IO;
using SS;

namespace applet
{
    class Driver
    {
        static void Main(string[] args)
        {
            List<Row> row = new List<Row>();
            
            try
            {
                using (StreamReader sr = new StreamReader("input.tsv"))
                {
                    for(int row_index = 0; !sr.EndOfStream; row_index++)
                    {
                        String[] line_values = sr.ReadLine().Split("\t");
                        List<int> column_val = new List<int>();
                        foreach (string column_s in line_values)
                        {
                            int column_int = int.Parse(column_s);
                            column_val.Add(column_int);
                        }
                        Row thisRow = new Row(column_val.ToArray());
                        row.Add(thisRow);
                    }
                }
            }
            catch( IOException e)
            {
                Console.WriteLine(e);
            }
            catch( Exception e)
            {
                Console.WriteLine(e);
            }
            
            SpreadSheet spreadSheet = new SpreadSheet(row.ToArray());
            int checkSum = spreadSheet.CalculateSheetCheckSum();

            int sumOfQuotients = spreadSheet.CalculateSumOfRowDivision();
            Console.WriteLine(checkSum);
            Console.WriteLine(sumOfQuotients);
            Console.WriteLine("press any key to close");
            Console.ReadKey();
        }
    }
}