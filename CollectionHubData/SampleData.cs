using System;

namespace CollectionHubData
{
    public class SampleData
    {
        public SampleData() { }
        public SampleData(string fixedValue)
        {
            FixedValue = fixedValue;
            ColumnA = Guid.NewGuid().ToString().Substring(1, 5);
            ColumnB = Guid.NewGuid().ToString().Substring(1, 10);
            ColumnC = Guid.NewGuid().ToString().Substring(1, 15);
            ColumnD = Guid.NewGuid().ToString().Substring(1, 5);
        }

        public string FixedValue { get; set; }
        public string ColumnA { get; set; }
        public string ColumnB { get; set; }
        public string ColumnC { get; set; }
        public string ColumnD { get; set; }
    }
}
