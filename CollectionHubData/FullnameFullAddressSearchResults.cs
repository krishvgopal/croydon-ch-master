using System;
using System.Data.SqlClient;

namespace CollectionHubData
{
    public class FullNameFullAddressSearchResults
    {
        public DateTime? DateofBirth            { get; set; }
        public string Source                    { get; set; }
        public string Pin                       { get; set; }
        public string Urpin                     { get; set; }
        public string Title                     { get; set; }
        public string FirstName                 { get; set; }
        public string MiddleName                { get; set; }
        public string LastName                  { get; set; }
        public string OrganisationName          { get; set; }
        public string NationalInsuranceNumber   { get; set; }
        public string FlatNumber                { get; set; }
        public string Building                  { get; set; }
        public string HouseNumber               { get; set; }
        public string StreetName                { get; set; }
        public string PostCode                  { get; set; }
        public string Descriptive               { get; set; }
        public string FullName                  { get; set; }
        public string FullAddress               { get; set; }
        public decimal TotalDebt                { get; set; }
        public decimal DebtOutstanding          { get; set; }
        public string ResponsibleOfficer        { get; set; }
        public string CN_Pin                    { get; set; }
        public string DebtAddresses             { get; set; }
        public string CommAddresses             { get; set; }
        public DateTime? FromDate               { get; set; }
        public DateTime? UntilDate              { get; set; }
        public string DebtorAge                 { get; set; }

        public FullNameFullAddressSearchResults() { }
        public FullNameFullAddressSearchResults(SqlDataReader value)
        {
            Pin                 = value["PIN"].ToString();
            Urpin               = value["UPRN"].ToString();
            Source              = value["SOURCE"].ToString();
            FullName            = value["FULLNAME"].ToString();
            FullAddress         = value["FULLADDRESS"].ToString();
            DebtOutstanding     = value["DEBTOS"] != DBNull.Value ? Convert.ToDecimal(value["DEBTOS"]) : 0;
            TotalDebt           = value["TOTALDEBT"] != DBNull.Value ? Convert.ToDecimal(value["TOTALDEBT"]) : 0;
            DebtorAge           = Convert.ToString(value["DOBAGE"]);
            ResponsibleOfficer  = value["RESPOFFICER"].ToString();
            LastName            = value["LASTNAME"].ToString();
            CN_Pin              = value["CN_PIN"].ToString();
            DebtAddresses       = value["DebtAddresses"].ToString();
            CommAddresses       = value["CommAddresses"].ToString();

            object sqlDateTime0 = value["FromDate"];
            FromDate = (sqlDateTime0 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime0);

            object sqlDateTime1 = value["UntilDate"];
            UntilDate = (sqlDateTime1 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime1);
        }
    }
}
