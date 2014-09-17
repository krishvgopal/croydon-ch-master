using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class Payment
    {
        public int          PaymentId {get;set;}
        public string       PaymentSource { get; set; }
        public string       PaymentSourceAccountReference {get;set;}
        public string       PaymentDebtReference { get; set; }
        public string       PaymentPartyPin { get; set; }
        public decimal      PaymentAmount {get;set;}
        public DateTime?    PaymentDate {get;set;}
        public DateTime?    PaymentCreatedDate { get; set; }

        public Payment() { }
        public Payment(System.Data.SqlClient.SqlDataReader value)
        {
            PaymentId                       = Convert.ToInt32(value["pay_id"].ToString());
            PaymentSource                   = value["pay_source"].ToString();
            PaymentSourceAccountReference   = value["pay_source_accref"].ToString();
            PaymentDebtReference            = value["pay_debt_ref"].ToString();
            PaymentPartyPin                 = value["pay_party_pin"].ToString();
            PaymentAmount                   = Convert.ToDecimal(value["pay_amount"].ToString());

            object sqlDateTime = value["pay_date"];
            PaymentDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime);

            object sqlDateTime2 = value["pay_created_date"];
            PaymentCreatedDate = (sqlDateTime == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime2);
        }
    }
}
