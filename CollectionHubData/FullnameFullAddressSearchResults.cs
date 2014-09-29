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
        public float TotalDebt          { get; set; }
        public float DebtOutstanding    { get; set; }
        public string ResponsibleOfficer { get; set; }

        public FullNameFullAddressSearchResults() { }
        public FullNameFullAddressSearchResults(SqlDataReader value)
        {
            Pin = value["PIN"].ToString();
            Urpin = value["UPRN"].ToString();
            Source = value["SOURCE"].ToString();
            FullName = value["FULLNAME"].ToString();
            FullAddress = value["FULLADDRESS"].ToString();
            DebtOutstanding = Convert.ToInt32(value["DEBTOS"]);
            ResponsibleOfficer = value["RESPOFFICER"].ToString();
            LastName = value["LASTNAME"].ToString();
        }
    }
}
