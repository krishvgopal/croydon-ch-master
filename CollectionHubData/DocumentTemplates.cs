using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class DocumentTemplates
    {
        public int    CHT_ID { get; set; }
        public string CHT_Name { get; set; }
        public string CHT_Notes { get; set; }
        public string CHT_Content { get; set; }

        public DocumentTemplates() { }
        public DocumentTemplates(System.Data.SqlClient.SqlDataReader value)
        {
            CHT_ID = Convert.ToInt32(value["CHT_ID"]);
            CHT_Name = Convert.ToString(value["CHT_Name"]);
            CHT_Notes = Convert.ToString(value["CHT_Notes"]);
            CHT_Content = Convert.ToString(value["CHT_Content"]);
        }
    }

}
