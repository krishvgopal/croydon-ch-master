using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class PersonDetails
    {
        public int PIN { get; set; }
        public string SOURCE_REF { get; set; }
        public DateTime DOB { get; set; }
        public string NINO { get; set; }
        public string PAR_TYPE { get; set; }
        public string FULLNAME { get; set; }
        public string FULLADDRESS { get; set; }
        public string AccountRef { get; set; }
        public DateTime ACC_START_DATE { get; set; }
        public DateTime ACC_END_DATE { get; set; }
        public string CTAXACCLIVE { get; set; }
        public string UPRN { get; set; }

        public PersonDetails() { }
        public PersonDetails(System.Data.SqlClient.SqlDataReader value)
        {
            PIN = Convert.ToInt32(value["PIN"]);
            //SOURCE_REF = Convert.ToString(value["SOURCE_REF"]);
            NINO = Convert.ToString(value["NINO"]);
            //PAR_TYPE = Convert.ToString(value["PAR_TYPE"]);
            FULLNAME = Convert.ToString(value["FULLNAME"]);
            FULLADDRESS = Convert.ToString(value["FULLADDRESS"]);
            //AccountRef = Convert.ToString(value["AccountRef"]);
            //CTAXACCLIVE = Convert.ToString(value["CTAXACCLIVE"]);
            UPRN = Convert.ToString(value["UPRN"]);
        }
    }

}
