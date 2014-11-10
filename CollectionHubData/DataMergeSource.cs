using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class DataMergeSource
    {
        public string ViewUserName      { get; set; }
        public string ViewDescription   { get; set; }
        public string ViewName          { get; set; }

        public DataMergeSource() { }
        public DataMergeSource(System.Data.SqlClient.SqlDataReader value)
        {
            ViewUserName        = Convert.ToString(value["ViewUserName"]);
            ViewDescription     = Convert.ToString(value["ViewDescription"]);
            ViewName            = Convert.ToString(value["ViewName"]);   
        }
    }
}
