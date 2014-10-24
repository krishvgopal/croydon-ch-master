using System;

namespace CollectionHubData
{
   public class BatchJobs 
    {
        public int      Id              { get; set; }
        public string   BatchName       { get; set; }
        public string   DebtSource      { get; set; }
        public DateTime CreatedDate     { get; set; }
        public string   UserName        { get; set; }

        public BatchJobs() { }
        public BatchJobs(System.Data.SqlClient.SqlDataReader value)
        {
            Id           = Convert.ToInt32(value["bp_id"]);
            BatchName    = Convert.ToString(value["batch_name"]);
            DebtSource   = Convert.ToString(value["bp_debt_source"]);
            CreatedDate  = Convert.ToDateTime(value["bp_created_date"]);
            UserName     = Convert.ToString(value["UserName"]);
        }
    }
}
