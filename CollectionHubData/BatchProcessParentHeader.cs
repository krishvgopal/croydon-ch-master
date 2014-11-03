using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class BatchProcessParentHeader
    {
        public int BatchProcessId { get; set; }
        public string BatchName { get; set; }
        public string DebtSource { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string SelectionProcedure { get; set; }
        public string Description { get; set; }
        public string BatchProcedure { get; set; }

        public BatchProcessParentHeader() { }
        public BatchProcessParentHeader(System.Data.SqlClient.SqlDataReader value)
        {
            BatchProcessId  = Convert.ToInt32(value["bp_id"]);
            BatchName       = Convert.ToString(value["bp_batch_name"]);
            DebtSource      = Convert.ToString(value["bp_debt_source"]);
            CreatedBy       = Convert.ToInt32(value["bp_created_by"]);
            
            SelectionProcedure = Convert.ToString(value["bp_selection_procedure"]);
            Description     = Convert.ToString(value["bp_description"]);
            BatchProcedure  = Convert.ToString(value["bp_batch_procedure"]);

            if (value["bp_modified_by"] != DBNull.Value) { ModifiedBy = Convert.ToInt32(value["bp_modified_by"]); }
            
            //CreatedDate

        }
    }

    public class BatchProcessParentFields
    {
        public int BatchFieldUniqueId { get; set; }
        public int BatchFieldId { get; set; }
        public int BatchId { get; set; }
        public int BatchProcessId { get; set; }
        public string FieldValue { get; set; }

        public BatchProcessParentFields() { }
        public BatchProcessParentFields(System.Data.SqlClient.SqlDataReader value)
        {
            BatchFieldUniqueId = Convert.ToInt32(value["bfs_id"]);
            BatchFieldId = Convert.ToInt32(value["bf_id"]);
            BatchId = Convert.ToInt32(value["bfs_bp_id"]);
            BatchProcessId = Convert.ToInt32(value["bfs_batch_id"]);
            FieldValue = Convert.ToString(value["bfs_field_value"]);
        }
    }


}
