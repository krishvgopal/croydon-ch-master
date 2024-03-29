﻿using System;

namespace CollectionHubData
{
    public class DebtItem
    {
        public int          DebtId              { get; set; }
        public string       DebtSource          { get; set; }
        public string       DebtAccRef          { get; set; }
        public string       DebtReference       { get; set; }
        public decimal      DebtTotal           { get; set; }
        public decimal      DebtOutstanding     { get; set; }
        public string       PartyPin            { get; set; }
        public string       PropertyReference   { get; set; }
        public string       Status              { get; set; }
        public string       Type                { get; set; }
        public string       RecoveryCycle       { get; set; }
        public DateTime?    DebtDate            { get; set; }
        public DateTime?    LastActionDate      { get; set; }
        public string       GroupOrder          { get; set; }
        public bool         IsGroupRecord       { get; set; }
        public int?         GroupDebtId         { get; set; }
        public int          ResponsibleUserId   { get; set; }
        public string       ResponsibleUserName { get; set; }
        public string       DebtAddress         { get; set; }

        public DebtItem() { }
        public DebtItem(System.Data.SqlClient.SqlDataReader value)
        {
            DebtId              = Convert.ToInt32( value["cd_id"].ToString() );
            DebtSource          = value["cd_source"].ToString().Trim();
            DebtAccRef          = value["cd_source_accref"].ToString().Trim();
            DebtReference       = value["cd_debt_ref"].ToString().Trim();
            PartyPin            = value["cd_party_pin"].ToString();
            PropertyReference   = value["cd_prop_ref"].ToString();
            RecoveryCycle       = value["Recovery_Cycle"].ToString();
            Status              = value["cd_Status"].ToString();
            Type                = value["cd_Type"].ToString();
            GroupOrder          = value["OrderId"].ToString();
            IsGroupRecord       = Convert.ToBoolean(value["UnGroupFlag"]);
            this.DebtAddress    = value["DebtAddress"].ToString();
            
            object intCheck = value["cd_group_debt_id"];
            GroupDebtId = (intCheck == System.DBNull.Value)
                ? (int)-1
                : Convert.ToInt32(value["cd_group_debt_id"]);

            object sqlDebtOutstanding = value["cd_os_bal"];
            DebtOutstanding = (sqlDebtOutstanding == System.DBNull.Value)
                ? (Decimal)0 
                : Convert.ToDecimal(value["cd_os_bal"]);

            object sqlDebtTotal = value["cd_debt_total"];
            DebtTotal = (sqlDebtTotal == System.DBNull.Value)
                ? (Decimal)0
                : Convert.ToDecimal(value["cd_debt_total"]);

            object sqlDateTime = value["cd_debt_date"];
            DebtDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);

            object sqlDateTime2 = value["Last_Action_Date"];
            LastActionDate = (sqlDateTime2 == System.DBNull.Value)
                ? (DateTime?) null
                : Convert.ToDateTime(sqlDateTime2);

            object intOfficer = value["cd_resp_user"];
            ResponsibleUserId = (intOfficer == System.DBNull.Value)
                ? 0
                : Convert.ToInt32(intOfficer);

            object strUserName = value["UserName"];
            ResponsibleUserName = (strUserName == System.DBNull.Value)
                ? null
                : Convert.ToString(strUserName);
        }
    }
}