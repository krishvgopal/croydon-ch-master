using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class BatchProcessFieldsFromRun
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
        
        public BatchProcessFieldsFromRun() { }
        public BatchProcessFieldsFromRun(System.Data.SqlClient.SqlDataReader value)
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

        //public int bf_id { get; set; }
        //public int bf_bp_id { get; set; }
        //public string FieldLabel { get; set; }
        //public string DataType { get; set; }
        //public string DefaultValue { get; set; }
        //public string HelpText { get; set; }
        //public bool DisplayOnly { get; set; }
        //public string FieldData { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int CreatedBy { get; set; }
        //public int bf_field_mandatory { get; set; }
        //public string bf_fieldname { get; set; }
        //public bool bf_field_system { get; set; }

        //public BatchProcessFieldsFromRun() { }
        //public BatchProcessFieldsFromRun(System.Data.SqlClient.SqlDataReader value)
        //{
        //    bf_id       = Convert.ToInt32(value["bf_id"]);
        //    bf_bp_id    = Convert.ToInt32(value["bf_bp_id"]);
        //    FieldLabel = Convert.ToString(value["FieldLabel"]);
        //    DataType = Convert.ToString(value["DataType"]);
        //    DefaultValue = Convert.ToString(value["DefaultValue"]);
        //    HelpText = Convert.ToString(value["HelpText"]);
        //    DisplayOnly = Convert.ToBoolean(value["DisplayOnly"]);
        //    FieldData = Convert.ToString(value["FieldData"]);
        //    CreatedBy = Convert.ToInt32(value["CreatedBy"]);
        //    bf_field_mandatory = Convert.ToInt32(value["bf_field_mandatory"]);
        //    bf_fieldname = Convert.ToString(value["bf_fieldname"]);
        //    bf_field_system = Convert.ToBoolean(value["bf_field_system"]);
        //}
    }
}
