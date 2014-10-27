using System;

namespace CollectionHubData
{
    public class BatchProcess
    {
        public int      BpId            { get; set; }

        public string   BatchName       { get; set; }
        public string   DebtSource      { get; set; }
        public DateTime CreatedDate     { get; set; }
        public string   Procedure       { get; set; }
        public string   Description     { get; set; }
        public string   BatchProcedure  { get; set; }


        public BatchProcess() { }
        public BatchProcess(System.Data.SqlClient.SqlDataReader value)
        {
            BpId                = Convert.ToInt32(value["bp_id"]);
            BatchName           = Convert.ToString(value["batch_name"]);
            DebtSource          = Convert.ToString(value["bp_debt_source"]);
            CreatedDate         = Convert.ToDateTime(value["bp_created_date"]);
            Procedure           = Convert.ToString(value["bp_selection_procedure"]);
            BatchProcedure      = Convert.ToString(value["bp_batch_procedure"]);
            Description         = Convert.ToString(value["bp_description"]);

           // IsMandatory         = Convert.ToBoolean(value["bf_field_mandatory"]);
        }
    }
    public class BatchProcessJobs
    {
        public int bp_id { get; set; }
        public string batch_name { get; set; }
        public string bp_debt_source { get; set; }
        public DateTime bp_created_date { get; set; }
        public string UserName { get; set; }

        public BatchProcessJobs() { }
        public BatchProcessJobs(System.Data.SqlClient.SqlDataReader value)
        {
            bp_id = Convert.ToInt32(value["bp_id"]);
            batch_name = Convert.ToString(value["batch_name"]);
            bp_debt_source = Convert.ToString(value["bp_debt_source"]);
            UserName = Convert.ToString(value["UserName"]);
        }
    }
}
