using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class DebtAddress
    {
        public DebtAddress()
        { }
        public DebtAddress(System.Data.SqlClient.SqlDataReader value)
        {
            Pin = value["PIN"].ToString(); 
            Source = value["Source"].ToString();
            UPRN = value["UPRN"].ToString();
            FullAddress = value["FullAddress"].ToString();
            FullName = value["FullName"].ToString();
            RespOfficer = value["RespOfficer"].ToString();
            DebtOS = Convert.ToDecimal(value["DebtOS"]);
            TotalDebt = Convert.ToDecimal( value["TotalDebt"] );
        }

        public string Source { get; set; }
        public string Pin { get; set; }
        public string UPRN { get; set; }
        public string FullAddress { get; set; }
        public string FullName { get; set; }
        public string RespOfficer { get; set; }
        public Decimal DebtOS { get; set; }
        public Decimal TotalDebt { get; set; }
    }
}
