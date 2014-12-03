using System;

namespace CollectionHubData
{
    public class PersonAttribute
    {
        public int      PersonAttributeId   { get; set; }
        public int      SourcePin           { get; set; }
        public int      UserId              { get; set; }
        public int?      IsCurrent           { get; set; }
        public int      AttributeId         { get; set; }
        public string   AttributeValue      { get; set; }
        public DateTime? CreatedDate        { get; set; }
        public string   AttributeText       { get; set; }
        public string   Streams                { get; set; }
        public DateTime? FromDate           { get; set; }
        public DateTime? ToDate             { get; set; }
        public string StatusText { get; set; }

        public PersonAttribute(){}
        public PersonAttribute(System.Data.SqlClient.SqlDataReader value)
        {
            PersonAttributeId   = Convert.ToInt32(value["PersonAttributeId"]);
            SourcePin       = Convert.ToInt32(value["CHPIN"]);
            UserId          = Convert.ToInt32(value["UserId"]);
            
            AttributeId     = Convert.ToInt32(value["AttributeId"]);
            AttributeValue  = Convert.ToString(value["AttributeValue"]);
            AttributeText   = Convert.ToString(value["AttributeText"]);

            object isCurrentObject = value["IsCurrent"];
            IsCurrent = (isCurrentObject == System.DBNull.Value)
                ? (int?)null
                : Convert.ToInt32(isCurrentObject);

            object isCurrentObjectType = value["TypeName"];
            StatusText = (isCurrentObjectType == System.DBNull.Value)
                ? "Unknown"
                : Convert.ToString(isCurrentObjectType);

            var allStreams = "";

            if (value["Stream1"] != DBNull.Value) { allStreams = allStreams + value["Stream1"].ToString() + ","; }
            if (value["Stream2"] != DBNull.Value) { allStreams = allStreams + value["Stream2"].ToString() + ","; }
            if (value["Stream3"] != DBNull.Value) { allStreams = allStreams + value["Stream3"].ToString() + ","; }
            if (value["Stream4"] != DBNull.Value) { allStreams = allStreams + value["Stream4"].ToString() + ","; }
            if (value["Stream5"] != DBNull.Value) { allStreams = allStreams + value["Stream5"].ToString() + ","; }

            if (allStreams.Length > 0) { Streams = allStreams.Substring(0, allStreams.Length - 1); }
            
            object sqlDateTimeFromDate = value["fromdate"];
            FromDate = (sqlDateTimeFromDate == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeFromDate);

            object sqlDateTimeToDate = value["todate"];
            ToDate = (sqlDateTimeToDate == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeToDate);

            object sqlDateTime = value["CreatedDate"];
            CreatedDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);
        }
    }
}
