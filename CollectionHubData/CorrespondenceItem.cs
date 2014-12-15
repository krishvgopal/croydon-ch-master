using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class CorrespondenceItem
    {
        public string   Status          { get; set; }
        public string   ContentText     { get; set; }
        public Stream   ContentStream   { get; set; }
        public byte[]   ContentArray    { get; set; }
        public string   Name            { get; set; }
        public int      ItemId          { get; set; }

        public CorrespondenceItem() { }
        public CorrespondenceItem(SqlDataReader value)
        {
            Status      = value["ACO_STATUS"].ToString();
            ContentText = value["ACO_CONTENT_TEXT"].ToString();
            Name        = value["ACO_NAME"].ToString();

            var length = value.GetBytes(1, 0L, null, 0, 0);
            var buffer = new byte[length];
            value.GetBytes( 1, 0L, buffer, 0, (int)length);
            ContentArray = buffer;
        }  
    }
}
