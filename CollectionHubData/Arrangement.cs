using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class Arrangement
    {
        public DateTime?    CreatedDate             { get; set; }
        public decimal      AggreedAmount           { get; set; }
        public int          Frequency               { get; set; }
        public string       FrequencyDescription    { get; set; }
        public decimal      InstallmentAmount       { get; set; }
        public int          NumberOfInstallments    { get; set; }
        public string       AgmStatus               { get; set; }

        public Arrangement() {}
        public Arrangement(System.Data.SqlClient.SqlDataReader value)
        {
            AggreedAmount           = Convert.ToDecimal(value["agm_agreed_amount"].ToString());
            FrequencyDescription    = Convert.ToString(value["FrequencyDesc"]);
            InstallmentAmount       = Convert.ToDecimal(value["agm_installment_amount"].ToString());
            NumberOfInstallments    = Convert.ToInt32(value["agm_number_installments"].ToString());
            AgmStatus               = Convert.ToString(value["agm_status"]);

            var sqlDateTimeCreatedDate = value["agm_Created_Date"];
            CreatedDate = (sqlDateTimeCreatedDate == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTimeCreatedDate);
        }
    }
}
