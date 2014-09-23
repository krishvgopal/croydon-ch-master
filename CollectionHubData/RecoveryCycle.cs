using System;

namespace CollectionHubData
{
    public class RecoveryCycle
    {
        public int      RecoveryCycleId { get; set; }
        public string   RecoveryCycleName { get; set; }

        public RecoveryCycle() { }
        public RecoveryCycle(System.Data.SqlClient.SqlDataReader value) 
        {
            RecoveryCycleName   = value["rc_name"].ToString();
            RecoveryCycleId     = Convert.ToInt32(value["rc_id"]);
        }
    }
}
