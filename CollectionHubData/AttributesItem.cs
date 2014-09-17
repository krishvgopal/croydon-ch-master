using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class AttributeItem
    {
        public int      AttributeId { get; set; }
        public string   AttributeText { get; set; }
        public bool     IsDebtAttribute { get; set; }
        public bool     IsPersonAttribute { get; set; }

        public AttributeItem() { }
        public AttributeItem(System.Data.SqlClient.SqlDataReader value)
        {
            AttributeId         = Convert.ToInt32(value["AttributeId"].ToString());
            AttributeText       = value["AttributeText"].ToString();
            IsDebtAttribute     = Convert.ToBoolean(value["IsDebtAttribute"].ToString());
            IsPersonAttribute   = Convert.ToBoolean(value["IsPersonAttribute"].ToString());
        }
    }
}
