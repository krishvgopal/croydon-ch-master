using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class DebtParties
    {
        public int      DebtPartyId { get; set; }
        public string   DebtPartyPin { get; set; }
        public string   Pin { get; set; }
        public string   PartyType { get; set; }
        public string   PrimaryFlag { get; set; }
        public string   PartyFullName { get; set; }

        public DebtParties() {}
        public DebtParties(System.Data.SqlClient.SqlDataReader value)
        {
            DebtPartyId     = Convert.ToInt32(value["dp_id"]);
            DebtPartyPin    = Convert.ToString(value["dp_party_pin"]);
            Pin             = Convert.ToString(value["dp_party_pin"]);
            PartyType       = Convert.ToString(value["PartyType"]);
            PrimaryFlag     = Convert.ToString(value["PrimaryFlag"]);
            PartyFullName   = Convert.ToString(value["FULLNAME"]);
        }
    }
}
