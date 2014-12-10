using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class CorrespondenceItem
    {
        public string   Status          { get; set; }
        public string   ContentText     { get; set; }
        public string   Name            { get; set; }
        public int      ItemId          { get; set; }

        public CorrespondenceItem() { }
        public CorrespondenceItem(SqlDataReader value)
        {
            Status      = value["ACO_STATUS"].ToString();
            ContentText = value["ACO_CONTENT_TEXT"].ToString();
            Name        = value["ACO_NAME"].ToString();
        }  
    }
}
