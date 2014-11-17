using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class TreatmentItems
    {
        public string  ItemName { get; set; }
        public string ItemNotes { get; set; }
        public int Days { get; set; }
        public int? WeekDay { get; set; }
        public int? TemplateId { get; set; }

        public TreatmentItems() { }
        public TreatmentItems(System.Data.SqlClient.SqlDataReader value)
        {
            this.ItemName   = value["ITEM NAME"].ToString();
            this.ItemNotes  = value["ITEM NOTES"].ToString();
            this.Days       = Convert.ToInt32(value["DAYS"]);
            this.TemplateId = Convert.ToInt32(value["TemplateId"]);

            if (value["WEEK DAY"] != DBNull.Value)
            {
                WeekDay     = Convert.ToInt32(value["WEEK DAY"]);
            }
            if (value["TEMPLATEID"] != DBNull.Value)
            {
                WeekDay     = Convert.ToInt32(value["TEMPLATEID"]);
            }
        }
    }
}
