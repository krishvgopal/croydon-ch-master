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
        public DateTime?      DateCreated { get; set; }
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
}
