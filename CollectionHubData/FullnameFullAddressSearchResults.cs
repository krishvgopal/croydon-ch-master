using System;
using System.Data.SqlClient;

namespace CollectionHubData
{
    public class FullNameFullAddressSearchResults
    {
        public DateTime? DateofBirth    { get; set; }
        public string Source            { get; set; }
        public string Pin               { get; set; }
        public string Urpin             { get; set; }
        public string Title             { get; set; }
        public string FirstName         { get; set; }
        public string MiddleName        { get; set; }
        public string LastName          { get; set; }
        public string OrganisationName  { get; set; }
        public string NationalInsuranceNumber { get; set; }
        public string FlatNumber        { get; set; }
        public string Building          { get; set; }
        public string HouseNumber       { get; set; }
        public string StreetName        { get; set; }
        public string PostCode          { get; set; }
        public string Descriptive       { get; set; }
        public string FullName          { get; set; }
        public string FullAddress       { get; set; }
        public decimal TotalDebt          { get; set; }
        public decimal DebtOutstanding    { get; set; }
        public string ResponsibleOfficer { get; set; }
        public string CN_Pin            { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? UntilDate { get; set; }
        public string DebtorAge { get; set; }

        public FullNameFullAddressSearchResults() { }
        public FullNameFullAddressSearchResults(SqlDataReader value)
        {
            Pin = value["PIN"].ToString();
            Urpin = value["UPRN"].ToString();
            Source = value["SOURCE"].ToString();
            FullName = value["FULLNAME"].ToString();
            FullAddress = value["FULLADDRESS"].ToString();

            if (value["DEBTOS"] != DBNull.Value)
            {
                DebtOutstanding = Convert.ToDecimal(value["DEBTOS"]);
            }
            else
            {
                DebtOutstanding = 0;
            }

            if (value["TOTALDEBT"] != DBNull.Value)
            {
                TotalDebt = Convert.ToDecimal(value["TOTALDEBT"]);
            }
            else
            {
                TotalDebt = 0;
            }

            object sqlDateTime0 = value["FromDate"];
            FromDate = (sqlDateTime0 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime0);

            object sqlDateTime1 = value["UntilDate"];
            UntilDate = (sqlDateTime1 == System.DBNull.Value)
                ? (DateTime?)null
                : Convert.ToDateTime(sqlDateTime1);

            DebtorAge = Convert.ToString(value["DOBAGE"]);
            
            ResponsibleOfficer = value["RESPOFFICER"].ToString();
            LastName = value["LASTNAME"].ToString();
            CN_Pin = value["CN_PIN"].ToString();
        }
    }
}
