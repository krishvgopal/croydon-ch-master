using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class SearchWorkResult
    {
        public int          DebtId                      { get; set; }
        public string       DebtSource                  { get; set; }
        public string       SourceAccountReference      { get; set; }
        public string       DebtReference               { get; set; }
        public decimal      DebtTotal                   { get; set; }
        public decimal      OutstandingBalance          { get; set; }
        public DateTime?    DebtDate                    { get; set; }
        public string       Pin                         { get; set; }
        public string       Uprn                        { get; set; }

        public SearchWorkResult() { }
        public SearchWorkResult(System.Data.SqlClient.SqlDataReader value)
        {
            DebtId                  = Convert.ToInt32(value["cd_id"].ToString());
            DebtSource              = value["cd_source"].ToString().Trim();
            SourceAccountReference  = value["cd_source_accref"].ToString().Trim();
            DebtReference           = value["cd_debt_ref"].ToString().Trim();
            Pin                     = value["cd_party_pin"].ToString();
            Uprn                    = value["cd_UPRN"].ToString();
            
            object sqlDebtOutstanding = value["cd_os_bal"];
            OutstandingBalance = (sqlDebtOutstanding == System.DBNull.Value)
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
        }
        //*  cd_id
        //*  cd_source
        //*  cd_source_accref
        //*  cd_debt_ref
        //*  cd_debt_total
        //*  cd_os_bal
        //  cd_party_pin
        //  cd_prop_ref
        //*  cd_debt_date
        //  cd_type
        //  cd_created_user
        //  cd_modified_date
        //  cd_modified_user
        //  cd_group_debt_id
        //  cd_cn_pin
        //  cd_PIN
        //  cd_UPRN
        //  cd_cycle_id
        //*  cd_resp_user
    }
}
