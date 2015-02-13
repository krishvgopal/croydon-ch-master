using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class UprnAddress
    {
        public int      UprnId              { get; set; }
        public string   FlatNumber          { get; set; }
        public string   BuildingNumber      { get; set; }
        public string   Place               { get; set; }
        public string   Street              { get; set; }
        public string   PostCode            { get; set; }
        public string   CareOf              { get; set; }
        public string   AddressDescription  { get; set; }
        public string   LineAddress         { get; set; }

        public UprnAddress() { }
        public UprnAddress(System.Data.SqlClient.SqlDataReader value)
        {
            UprnId      = Convert.ToInt32(value["ch_uprn"].ToString());
            LineAddress = value["single line address"].ToString().Trim();
        }
    }
}
