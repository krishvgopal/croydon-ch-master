using System.Collections.Generic;

namespace CollectionHubData
{
    public class DebtSearchResult
    {
        public List<DebtSearchResultItem> Results { get; set; }
        public int          RecordCount { get; set; }
        public decimal      TotalValue  { get; set; }

        public DebtSearchResult()
        {
            Results = null;
            RecordCount = -1;
            TotalValue = -1;
        }
        public DebtSearchResult(List<DebtSearchResultItem> debtSearchResultItems, int recordCount, decimal totalValue)
        {
            Results     = debtSearchResultItems;
            TotalValue  = totalValue;
            RecordCount = recordCount;
        }
    }
}
