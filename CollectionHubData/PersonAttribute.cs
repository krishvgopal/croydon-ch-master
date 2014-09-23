using System;

namespace CollectionHubData
{
    public class PersonAttribute
    {
        public int      PersonAttributeId   { get; set; }
        public int      SourcePin           { get; set; }
        public int      UserId              { get; set; }
        public bool     IsCurrent           { get; set; }
        public int      AttributeId         { get; set; }
        public string   AttributeValue      { get; set; }
        public DateTime? CreatedDate        { get; set; }
        public string   AttributeText       { get; set; }

        public PersonAttribute(){}
        public PersonAttribute(System.Data.SqlClient.SqlDataReader value)
        {
            PersonAttributeId   = Convert.ToInt32(value["PersonAttributeId"]);
            SourcePin       = Convert.ToInt32(value["SourcePin"]);
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
