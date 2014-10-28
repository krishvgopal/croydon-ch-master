using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class BatchRecords
    {
        string RecordID         { get; set; }
        string PIN              { get; set; }
        string Source           { get; set; }
        string DebtAccount      { get; set; }
        string FullName         { get; set; }
        string FullAddress      { get; set; }
        string ThisDebt         { get; set; }
        string ThisDebtOS       { get; set; }
        string DebtCount        { get; set; }
        string AllDebtAmount    { get; set; }
        string AllDebtOS        { get; set; }
        string FromDate         { get; set; }
        string UntilDate        { get; set; }

        public BatchRecords() { }
        public BatchRecords(System.Data.SqlClient.SqlDataReader value) 
        {
            RecordID        = Convert.ToString(value["RecordID"]);
            PIN             = Convert.ToString(value["PIN"]);
            Source          = Convert.ToString(value["Source"]);
            DebtAccount     = Convert.ToString(value["DebtAccount"]);
            FullName        = Convert.ToString(value["FullName"]);
            FullAddress     = Convert.ToString(value["FullAddress"]);
            ThisDebt        = Convert.ToString(value["ThisDebt"]);
            ThisDebtOS      = Convert.ToString(value["ThisDebtOS"]);
            DebtCount       = Convert.ToString(value["DebtCount"]);
            AllDebtAmount   = Convert.ToString(value["AllDebtAmount"]);
            AllDebtOS       = Convert.ToString(value["AllDebtOS"]);
            FromDate        = Convert.ToString(value["FromDate"]);
            UntilDate       = Convert.ToString(value["UntilDate"]);

        }
    }                               
}
