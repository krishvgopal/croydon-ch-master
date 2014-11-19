using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class MergeValue
    {
        public string FieldValue { get; set; }
        public string FieldName { get; set; }

        public MergeValue() { }
        public MergeValue(System.Data.SqlClient.SqlDataReader value)
        {
            this.FieldName  = value["FieldName"].ToString();
            this.FieldValue = value["FieldValue"].ToString();
        }
    }
}
