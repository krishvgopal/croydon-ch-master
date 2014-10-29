using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class BatchRecords
    {
        public string RecordID         { get; set; }
        public string PIN              { get; set; }
        public string Source           { get; set; }
        public string DebtAccount      { get; set; }
        public string FullName         { get; set; }
        public string FullAddress      { get; set; }
        public string ThisDebt         { get; set; }
        public string ThisDebtOS       { get; set; }
        public string DebtCount        { get; set; }
        public string AllDebtAmount    { get; set; }
        public string AllDebtOS        { get; set; }
        public DateTime? FromDate       { get; set; }
        public DateTime? UntilDate      { get; set; }
        public string UPRN              { get; set; }
        public string RowIdentifier     { get; set; }
        public Boolean Included         { get; set; }

        public BatchRecords() { }
        public BatchRecords(System.Data.SqlClient.SqlDataReader value) 
        {
            RecordID        = Convert.ToString(value["RecordID"]);
            PIN             = Convert.ToString(value["PIN"]);
            UPRN            = Convert.ToString(value["UPRN"]);
            Source          = Convert.ToString(value["Source"]);
            DebtAccount     = Convert.ToString(value["DebtAccount"]);
            FullName        = Convert.ToString(value["FullName"]);
            FullAddress     = Convert.ToString(value["FullAddress"]);
            ThisDebt        = Convert.ToString(value["ThisDebt"]);
            ThisDebtOS      = Convert.ToString(value["ThisDebtOS"]);
            DebtCount       = Convert.ToString(value["DebtCount"]);
            AllDebtAmount   = Convert.ToString(value["AllDebtAmount"]);
            AllDebtOS       = Convert.ToString(value["AllDebtOS"]);
            RowIdentifier   = Convert.ToString(value["RowIdentifier"]);
            Included        = Convert.ToBoolean(value["Included"]);

            var sqlDateTimeLatestDebt1 = value["FromDate"];
            FromDate = (sqlDateTimeLatestDebt1 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeLatestDebt1);

            var sqlDateTimeLatestDebt2 = value["UntilDate"];
            UntilDate = (sqlDateTimeLatestDebt2 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeLatestDebt2);
        }
    }                               
}
