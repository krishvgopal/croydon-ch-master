using System;

namespace CollectionHubData
{
    public class RecoveryCycleItem
    {
        public int    RecoveryCycleItemId { get; set; }
        public string RecoveryCycle { get; set; }
        public string Stage { get; set; }
        public string StageType { get; set; }
        public string Status { get; set; }
        public string Days { get; set; }
        public DateTime? TargetDate { get; set; }
        public string TargetDateFormatted { get { if (TargetDate.HasValue) { return TargetDate.Value.ToShortDateString(); } else { return string.Empty; } } }

        public RecoveryCycleItem() { }
        public RecoveryCycleItem(System.Data.SqlClient.SqlDataReader value)
        {
            RecoveryCycleItemId = Convert.ToInt32(value["rh_id"].ToString());
            RecoveryCycle       = value["rc_name"].ToString();
            Stage               = value["ac_action_name"].ToString();
            Status              = value["rh_status"].ToString();
            Days                = value["ca_days"].ToString();
            StageType           = value["ActionTypeName"].ToString();

            object sqlDateTime = value["rh_scheduled_date"];
            TargetDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);
        }
    }
}