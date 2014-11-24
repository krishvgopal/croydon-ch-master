using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class DebtStream
    {
        public int Id { get; set; }
        public string StreamName { get; set; }
        public string Code { get; set; }
        public int LuId { get; set; }
        public int CreatedById { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DebtStream() { }
        public DebtStream(SqlDataReader value)
        {
            Id = Convert.ToInt32(value["S_ID"]);
            StreamName = Convert.ToString(value["S_NAME"]);
            Code = Convert.ToString(value["S_Code"]);
            LuId = Convert.ToInt32(value["S_LUID"]);
            CreatedById = Convert.ToInt32(value["S_CREATEDUID"]);

            object sqlDateTime2 = value["S_LUDATE"];
            LastUpdated = (sqlDateTime2 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime2);

            object sqlDateTime1 = value["S_CREATEDDATE"];
            CreatedDate = (sqlDateTime1 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime1);
        }
    }
}
