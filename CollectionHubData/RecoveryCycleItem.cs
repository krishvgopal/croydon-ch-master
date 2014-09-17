using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class RecoveryCycleItem
    {
        public int    RecoveryCycleItemId { get; set; }
        public string RecoveryCycle	{get; set;}
        public string Stage	{get; set;}
        public string StageType	{get; set;}
        public string Status {get; set;}
        public string Days { get; set; }
        public DateTime? TargetDate { get; set; }

        public RecoveryCycleItem() { }
        public RecoveryCycleItem(System.Data.SqlClient.SqlDataReader value)
        {
            RecoveryCycleItemId = Convert.ToInt32(value["rh_id"].ToString());
            RecoveryCycle       = value["rc_name"].ToString();
            Stage               = value["ac_action_name"].ToString();
            Status              = value["rh_status "].ToString();
            Days                = value["ca_days"].ToString();
            StageType           = value["ac_action_type"].ToString();

            object sqlDateTime = value["rh_scheduled_date"];
            TargetDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);
        }
    }
}
//public string Complete { get; set; }
//public string Method {get; set;}
//public int rh_id {get; set;}
//public int rh_cd_id {get; set;}
//public int rh_rc_id {get; set;} 
//public int rh_ac_id {get; set;}
//public string rh_status {get; set;}
//public DateTime? rh_scheduled_date {get; set;}
//public DateTime? rh_created_date {get; set;}
//public string rh_created_user {get; set;}
//public string rc_name {get; set;}
//public string ac_action_name {get; set;}
//public string ac_action_type {get; set;}
//public string ca_days { get; set; }
// Complete = value["cd_id"].ToString();