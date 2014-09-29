using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class LinkedAddress
    {
        public string       Source      { get; set; }
        public string       Address     { get; set; }
        public DateTime?    FromDate    { get; set; }
        public DateTime?    UntilDate   { get; set; }

        public LinkedAddress() { }
        public LinkedAddress(System.Data.SqlClient.SqlDataReader value)
        {
            Source      = value["Source"].ToString().Trim();
            Address     = value["FullAddress"].ToString();

            var sqlDateTimeLastPaid = value["FromDate"];
            FromDate = (sqlDateTimeLastPaid == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeLastPaid);

            var sqlDateTimeUntilDate = value["UntilDate"];
            UntilDate = (sqlDateTimeUntilDate == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeUntilDate);
        }
    }
}
