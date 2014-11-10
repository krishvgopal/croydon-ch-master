using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class MergeFieldItem
    {
        public int      ColumnId    { get; set; }
        public int      FieldCode   { get; set; }
        public int      FieldLength { get; set; }
        public string   FieldType   { get; set; }
        public string   FieldName   { get; set; }
        public string   FieldInfo   { get; set; }

        public MergeFieldItem() { }
        public MergeFieldItem(System.Data.SqlClient.SqlDataReader value)
        {
            ColumnId    = Convert.ToInt32(value["ColId"]);
            FieldCode   = Convert.ToInt32(value["FieldCode"]);
            FieldLength = Convert.ToInt32(value["Length"]);
            FieldType   = Convert.ToString(value["FieldType"]);
            FieldName   = Convert.ToString(value["FieldName"]);
            FieldInfo   = Convert.ToString(value["Info"]);
        }
    }
}
