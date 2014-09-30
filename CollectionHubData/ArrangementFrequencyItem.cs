using System;

namespace CollectionHubData
{
    public class ArrangementFrequencyItem
    {
        public string ArrangementCode   { get; set; }
        public string ArrangementName   { get; set; }
        //public int ArrangementId        { get; set; }

        public ArrangementFrequencyItem() { }
        public ArrangementFrequencyItem(System.Data.SqlClient.SqlDataReader value)
        {
            //ArrangementId   = Convert.ToInt32(value["AttributeId"].ToString());
            ArrangementName = value["Value"].ToString();
            ArrangementCode = value["Code"].ToString();
        }
    }
}
