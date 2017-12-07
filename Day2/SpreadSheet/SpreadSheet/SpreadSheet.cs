using System;

namespace SS
{
    public class SpreadSheet
    {
        private Row[] MyRows { get; set; }

        public SpreadSheet(Row[] rows)
        {
            MyRows = rows;
        } 

        public int CalculateSheetCheckSum()
        {
            int sum = 0;

            foreach( Row currentRow in MyRows )
            {
                sum += currentRow.CalculateRowCheckSum();
            }

            return sum;
        }
    }
}
