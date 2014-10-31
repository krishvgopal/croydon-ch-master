using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class BatchRunHistory 
    {
        public int           B_ID        { get; set; }
        public string        UserName    { get; set; }
        public string        BatchName   { get; set; }
        public DateTime?     DateCreated { get; set; }
        public int           Records     { get; set; }
        public string        BatchStatus { get; set; }
        public double        DebtAtStart { get; set; }
        public double        DebtOSNow   { get; set; }

        public BatchRunHistory() {}
        public BatchRunHistory(System.Data.SqlClient.SqlDataReader value)
        {
            B_ID            = Convert.ToInt32(value["B_ID"]);
            UserName        = Convert.ToString(value["User name"]);
            BatchName       = Convert.ToString(value["Batch name"]);
            Records         = Convert.ToInt32(value["Records"]);
            BatchStatus     = Convert.ToString(value["Batch Status"]);

            var sqlDateTimeLastPaid = value["Date Created"];
            DateCreated = (sqlDateTimeLastPaid == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeLastPaid);

            if (value["Debt at start"] != DBNull.Value)
            {
                DebtAtStart = Convert.ToDouble(value["Debt at start"]);
            }
            else
            {
                DebtAtStart = 0;
            }

            if (value["Debt OS now"] != DBNull.Value)
            {
                DebtOSNow = Convert.ToDouble(value["Debt OS now"]);
            }
            else
            {
                DebtOSNow = 0;
            }
        }
    }

    public class BatchProcessResult
    {
        public string RowIdentifier { get; set; }
        public string RecordID      { get; set; }
        public string PIN           { get; set; } 
        public string UPRN          { get; set; }
        public string Source        { get; set; }
        public string DebtAccount   { get; set; }
        public string FullName      { get; set; }
        public string FullAddress   { get; set; }
        public string ThisDebt      { get; set; }
        public string ThisDebtOS    { get; set; }             
        public string DebtAsAt      { get; set; }
        public string DebtOS        { get; set; }
        public string Reduction     { get; set; }
        public bool   Included      { get; set; }

        public BatchProcessResult() {}
        public BatchProcessResult(System.Data.SqlClient.SqlDataReader value)
        {
            RowIdentifier   = Convert.ToString(value["RowIdentifier"]);
            RecordID        = Convert.ToString(value["RecordID"]);
            PIN             = Convert.ToString(value["PIN"]);
            UPRN            = Convert.ToString(value["UPRN"]);
            Source          = Convert.ToString(value["Source"]);
            DebtAccount     = Convert.ToString(value["DebtAccount"]);
            FullName        = Convert.ToString(value["FullName"]);
            FullAddress     = Convert.ToString(value["FullAddress"]);
            ThisDebt        = Convert.ToString(value["ThisDebt"]);
            ThisDebtOS      = Convert.ToString(value["ThisDebtOS"]);
            DebtAsAt        = Convert.ToString(value["DebtAsAt"]);
            DebtOS          = Convert.ToString(value["DebtOS"]);
            Reduction       = Convert.ToString(value["Reduction"]);
            Included        = Convert.ToBoolean(value["Included"]);
        }
    }
}
