using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class ArrangementPaymentMethods
    {
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        //public int ArrangementId        { get; set; }

        public ArrangementPaymentMethods() { }
        public ArrangementPaymentMethods(System.Data.SqlClient.SqlDataReader value)
        {
            PaymentMethodName = value["Value"].ToString();
            PaymentMethodCode = value["Code"].ToString();
        }
    }
}