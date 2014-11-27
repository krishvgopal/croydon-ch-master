using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class AttributeCurrentStatusType
    {
        public int      AttributeTypeId { get; set; }
        public string   TypeName        { get; set; }

        public AttributeCurrentStatusType() { }
        public AttributeCurrentStatusType(System.Data.SqlClient.SqlDataReader value)
        {
            AttributeTypeId = Convert.ToInt32(value["AttributeTypeId"]);
            TypeName        = value["TypeName"].ToString();
        }
    }
}
