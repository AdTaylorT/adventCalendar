namespace SS
{

    public class Row
    {
        private int[] MyColumns { get; set; }

        public Row(int[] columns)
        {
            MyColumns = columns;
        }

        public int CalculateRowCheckSum()
        {
            int min = int.MaxValue;
            int max = 0;

            foreach (int columnValue in MyColumns)
            {
                if (columnValue > max) max = columnValue;
                if (columnValue < min) min = columnValue;
            }

            return max - min;
        }

        public int CalculateSumOfRowsOnlyDivision()
        {
            for (int l_index = 0; l_index < MyColumns.Length-1; l_index++)
            {
                for (int r_index = MyColumns.Length-1; r_index > l_index; r_index--)
                {
                    int divedend = 1;
                    int divisor = 1;
                    if (MyColumns[r_index] > MyColumns[l_index])
                    {
                        divedend = MyColumns[r_index];
                        divisor = MyColumns[l_index];
                    }
                    else
                    {
                        divisor = MyColumns[r_index];
                        divedend = MyColumns[l_index];
                    }

                    if (divedend % divisor == 0)
                    {
                        return divedend / divisor;
                    }
                }
            }

            throw new System.Exception();
        }

        public static implicit operator Row(int[] row)
        {
            return new Row(row);
        }
    }
}