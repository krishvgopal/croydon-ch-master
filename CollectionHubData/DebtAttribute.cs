using System;

namespace CollectionHubData
{
    public class DebtAttribute
    {
        public int      DebtAttributeId { get; set; }
        public int      DebtId          { get; set; }
        public int      UserId          { get; set; }
        public bool     IsCurrent       { get; set; }
        public int      AttributeId     { get; set; }
        public string   AttributeValue  { get; set; }
        public DateTime? CreatedDate    { get; set; }
        public string    AttributeText  { get; set; }

        public DebtAttribute(){}
        public DebtAttribute(System.Data.SqlClient.SqlDataReader value)
        {
            DebtAttributeId = Convert.ToInt32(value["DebtAttributeId"]);
            DebtId          = Convert.ToInt32(value["DebtId"]);
            UserId          = Convert.ToInt32(value["UserId"]);
            IsCurrent       = Convert.ToBoolean(value["IsCurrent"]);
            AttributeId     = Convert.ToInt32(value["AttributeId"]);
            AttributeValue  = Convert.ToString(value["AttributeValue"]);
            AttributeText   = Convert.ToString(value["AttributeText"]);
            
            object sqlDateTime = value["CreatedDate"];
            CreatedDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);
        }
    }
}