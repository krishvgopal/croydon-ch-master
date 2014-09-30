using System;

namespace CollectionHubData
{
    public class DebtSearchResultItem
    {
        public string       Source          { get; set; }
        public string       SourceRef       { get; set; }
        public string       CnPin           { get; set; }
        public string       UrPin           { get; set; }
        public string       LastName        { get; set; }
        public string       FullAddress     { get; set; }
        public string       FullName        { get; set; }
        public string       DebtStream      { get; set; }
        public int          NumberOfDebts   { get; set; }
        public decimal      TotalAmount     { get; set; }
        public DateTime?    LastPaid        { get; set; }
        public DateTime?    LatestDebt      { get; set; }

        public DebtSearchResultItem() { }
        public DebtSearchResultItem(System.Data.SqlClient.SqlDataReader value)
        {
            Source          = value["Source"].ToString().Trim();
            CnPin           = value["Cn_Pin"].ToString();
            //UrPin           = value["uprn"].ToString().Trim();
            LastName        = value["Lastname"].ToString();
            FullName        = value["FullName"].ToString();
            FullAddress     = value["FullAddress"].ToString();
            DebtStream      = value["Debt Streams"].ToString();
            NumberOfDebts   = Convert.ToInt32(value["Debts"]);
            TotalAmount     = Convert.ToDecimal(value["Amount"]);

            var sqlDateTimeLastPaid = value["Last Paid"];
            LastPaid = (sqlDateTimeLastPaid == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeLastPaid);

            var sqlDateTimeLatestDebt = value["Latest Debt"];
            LatestDebt = (sqlDateTimeLatestDebt == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeLatestDebt);
        }
    }
}
