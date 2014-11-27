using System;

namespace CollectionHubData
{
    public class RecoveryCycleItem
    {
        //public int    RecoveryCycleItemId { get; set; }
        //public string RecoveryCycle { get; set; }
        //public string Stage { get; set; }
        //public string StageType { get; set; }
        //public string Status { get; set; }
        //public string Days { get; set; }
        //public DateTime? TargetDate { get; set; }
        //public string TargetDateFormatted { get { if (TargetDate.HasValue) { return TargetDate.Value.ToShortDateString(); } else { return string.Empty; } } }


        public int    ID                { get; set; } 
        public string DebtID            { get; set; }
        public string DebtReference     { get; set; }      
        public string Action            { get; set; }                                                                                    
        public string ActionStatus      { get; set; }
        public DateTime? Scheduled      { get; set; }      
        public string ProcessMethod     { get; set; }
        public int    ActionGroup       { get; set; }
        public string Actionable        { get; set; }
        public string ColumnFormat      { get; set; }
        public string ActionText        { get; set; }

        public RecoveryCycleItem() { }
        public RecoveryCycleItem(System.Data.SqlClient.SqlDataReader value)
        {
            ID              = Convert.ToInt32(  value["ID"] );
            DebtID          = Convert.ToString( value["DEBTID"] );
            DebtReference   = Convert.ToString( value["ID"] );
            Action          = Convert.ToString( value["ACTION"] );
            ActionStatus    = Convert.ToString( value["ACTIONSTATUS"] );
            ProcessMethod   = Convert.ToString( value["ProcessMethod"] );
            ActionGroup     = Convert.ToInt32(  value["ActionGroup"] );
            Actionable      = Convert.ToString( value["Actionable"]);
            ColumnFormat    = Convert.ToString( value["ColumnFormat"]);
            ActionText      = Convert.ToString(value["On Click"]);

            object sqlDateTime = value["Scheduled"];
            Scheduled = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);
        }

        //public RecoveryCycleItem(System.Data.SqlClient.SqlDataReader value)
        //{
        //    RecoveryCycleItemId = Convert.ToInt32(value["rh_id"].ToString());
        //    RecoveryCycle       = value["rc_name"].ToString();
        //    Stage               = value["ac_action_name"].ToString();
        //    Status              = value["rh_status"].ToString();
        //    Days                = value["ca_days"].ToString();
        //    StageType           = value["ActionTypeName"].ToString();

        //    object sqlDateTime = value["rh_scheduled_date"];
        //    TargetDate = (sqlDateTime == System.DBNull.Value)
        //        ? (DateTime?)null
        //        : Convert.ToDateTime(sqlDateTime);
        //}
    }
}