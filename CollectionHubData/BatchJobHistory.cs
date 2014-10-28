using System;

namespace CollectionHubData
{
    public class BatchProcessHistory
    {
        public int          Id                  { get; set; }
        public int          BpId                { get; set; }
        public int          UserId              { get; set; }
        public DateTime     ProcessDate         { get; set; }
        public int          RecordsAffected     { get; set; }
        public int          PmId                { get; set; }
        public string       PmName              { get; set; }
        public string       PmDescription       { get; set; }
        public string       UserName            { get; set; }
        public string       BatchName           { get; set; }
        public string       DebtSource          { get; set; }

        public BatchProcessHistory() { }
        public BatchProcessHistory(System.Data.SqlClient.SqlDataReader value)
        {
            Id              = Convert.ToInt32(value["bph_id"]);
            UserId          = Convert.ToInt32(value["bph_user_id"]);
            ProcessDate     = Convert.ToDateTime(value["bph_process_date"]);
            RecordsAffected = Convert.ToInt32(value["bph_records_affected"]);
            PmId            = Convert.ToInt32(value["bph_pm_id"]);
            PmName          = Convert.ToString(value["pm_name"]);
            PmDescription   = Convert.ToString(value["pm_description"]);
            UserName        = Convert.ToString(value["UserName"]);
            BpId            = Convert.ToInt32(value["bph_bp_id"]);
            BatchName       = Convert.ToString(value["bp_batch_name"]);
            DebtSource      = Convert.ToString(value["bp_debt_source"]);
        }
    }
    public class BatchProcessFields
    {
        public int      bf_id           { get; set; }
        public string   FieldLabel      { get; set; }
        public string   DataType        { get; set; }
        public string   DefaultValue    { get; set; }
        public bool     ReadOnly        { get; set; }
        public DateTime CreatedDate     { get; set; }
        public string   CreatedBy       { get; set; }
        public string   HelpText        { get; set; }
        public string   FieldData       { get; set; }
        public bool     IsMandatory     { get; set; }
        public int      BpBpId          { get; set; }
        public string   FieldName       { get; set; }
        public bool     IsSystem        { get; set; }
        
        public BatchProcessFields() { }
        public BatchProcessFields(System.Data.SqlClient.SqlDataReader value)
        {
            bf_id           = Convert.ToInt32(value["bf_id"]);
            FieldLabel      = Convert.ToString(value["FieldLabel"]);
            DataType        = Convert.ToString(value["DataType"]);
            DefaultValue    = Convert.ToString(value["DefaultValue"]);
            ReadOnly        = Convert.ToBoolean(value["DisplayOnly"]);
            HelpText        = Convert.ToString(value["HelpText"]);
            CreatedBy       = Convert.ToString(value["CreatedBy"]);
            FieldData       = Convert.ToString(value["FieldData"]);
            IsMandatory     = Convert.ToBoolean(value["bf_field_mandatory"]);
            BpBpId          = Convert.ToInt32(value["bf_bp_id"]);
            FieldName       = Convert.ToString(value["bf_fieldname"]);
            IsSystem        = Convert.ToBoolean(value["bf_field_system"]);    
        } 

    } 
} 
