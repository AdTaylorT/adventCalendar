namespace SS
{

    public class Row
    {
        private int[] MyColumns { get; set;}
        
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

        public static implicit operator Row(int[] row)
        {
            return new Row(row);
        }
    }
}