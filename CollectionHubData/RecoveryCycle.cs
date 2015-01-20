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


    public class TreatmentCycle
    {
        public int TreatmentCycleId { get; set; }
        public string TreatmentCycleName { get; set; }

        public TreatmentCycle() { }
        public TreatmentCycle(System.Data.SqlClient.SqlDataReader value)
        {
            if (value.HasColumn("cycle") && value.HasColumn("Id"))
            {
                TreatmentCycleName = value["cycle"].ToString();
                TreatmentCycleId = Convert.ToInt32(value["id"]);
            }
            else
            {
                TreatmentCycleName = value["name"].ToString();
                TreatmentCycleId = Convert.ToInt32(value["id"]);    
            }
        }
    }


    //  ID	NAME	DESCRIPTION	STEPS	DURATION


}
