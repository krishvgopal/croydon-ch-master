using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class AutomaticTrayItems
    {
        public int       ActionId        { get; set; }
        public int       DebtId          { get; set; }
        public string    DebtReference   { get; set; }
        public DateTime? DebtDate        { get; set; }
        public string    ItemName        { get; set; }    
        public string    ItemDescription { get; set; }
        public decimal?  DebtOnDate      { get; set; }
        public string    Pin             { get; set; }
        public string    Uprn            { get; set; }
        
        public AutomaticTrayItems(){}
        public AutomaticTrayItems(IDataRecord value)
        {
            this.ActionId           = Convert.ToInt32(value["A_ID"]);
            this.DebtId             = Convert.ToInt32(value["A_DEBT_ID"]);
            this.DebtReference      = Convert.ToString(value["A_DEBT_REF"]);
            this.ItemDescription    = Convert.ToString(value["A_DESC"]);
            this.ItemName           = Convert.ToString(value["A_NAME"]);
            this.Pin                = Convert.ToString(value["cd_pin"]);
            this.Uprn               = Convert.ToString(value["cd_UPRN"]);

            object decDebtOnDate    = value["A_DEBTONDATE"];
            DebtOnDate = (decDebtOnDate == System.DBNull.Value)
                ? (decimal?)null
                : Convert.ToDecimal(decDebtOnDate);

            object sqlDateTime      = value["A_DEBTDATE"];
            DebtDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);
        }
    }
}
