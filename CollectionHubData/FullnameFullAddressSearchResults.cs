using System;
using System.Data.SqlClient;

namespace CollectionHubData
{
    public class FullNameFullAddressSearchResults
    {
        public FullNameFullAddressSearchResults()
        {
        }

        public FullNameFullAddressSearchResults(SqlDataReader value)
        {
            pin                     = value["PIN"].ToString();
            urpin                   = value["UPRN"].ToString();
            source                  = value["SOURCE"].ToString();
            fullName                = value["FULLNAME"].ToString();
            fullAddress             = value["FULLADDRESS"].ToString();
            debtOutstanding         = Convert.ToInt32(value["DEBTOS"]);
            responsibleOfficer      = value["RESPOFFICER"].ToString();
            lastName                = value["LASTNAME"].ToString();
            //title                   = value["TITLE"].ToString();
            //firstname               = value["FIRSTNAME"].ToString();
            //middleName              = value["MIDNAME"].ToString();
            //organisationName        = value["ORGNAME"].ToString();
            //dateofBirth             = value["DOB"];
            //nationalInsuranceNumber = value["NINO"].ToString();
            //flatNumber              = value["FLATNUM"].ToString();
            //building                = value["BUILDING"].ToString();
            //houseNumber             = value["HOUSENUM"].ToString();
            //streetName              = value["STREET"].ToString();
            //postCode                = value["POSTCODE"].ToString();
            //descriptive             = value["DESCRIPTIVE"].ToString();
            //totalDebt               = value["TOTALDEBT"];
        }

        private string source;
        public string Source
        {
          get { return source; }
          set { source = value; }
        }
        private string pin;
        public string Pin
        {
          get { return pin; }
          set { pin = value; }
        }
        private string urpin;
        public string Urpin
        {
          get { return urpin; }
          set { urpin = value; }
        }
        private string title;
        public string Title
        {
          get { return title; }
          set { title = value; }
        }
        private string firstname;
        public string FirstName
        {
          get { return firstname; }
          set { firstname = value; }
        }
        private string middleName;
        public string MiddleName
        {
          get { return middleName; }
          set { middleName = value; }
        }
        private string lastName;
        public string LastName
        {
          get { return lastName; }
          set { lastName = value; }
        }
        private string organisationName;
        public string OrganisationName
        {
          get { return organisationName; }
          set { organisationName = value; }
        }
        private Nullable<DateTime> dateofBirth;
        public Nullable<DateTime> DateofBirth
        {
          get { return dateofBirth; }
          set { dateofBirth = value; }
        }
        private string nationalInsuranceNumber;
        public string NationalInsuranceNumber
        {
          get { return nationalInsuranceNumber; }
          set { nationalInsuranceNumber = value; }
        }
        private string flatNumber;
        public string FlatNumber
        {
          get { return flatNumber; }
          set { flatNumber = value; }
        }
        private string building;
        public string Building
        {
          get { return building; }
          set { building = value; }
        }
        private string houseNumber;
        public string HouseNumber
        {
          get { return houseNumber; }
          set { houseNumber = value; }
        }
        private string streetName;
        public string StreetName
        {
          get { return streetName; }
          set { streetName = value; }
        }
        private string postCode;
        public string PostCode
        {
          get { return postCode; }
          set { postCode = value; }
        }
        private string descriptive;
        public string Descriptive
        {
          get { return descriptive; }
          set { descriptive = value; }
        }
        private string fullName;
        public string FullName
        {
          get { return fullName; }
          set { fullName = value; }
        }
        private string fullAddress;
        public string FullAddress
        {
          get { return fullAddress; }
          set { fullAddress = value; }
        }
        private float totalDebt;
        public float TotalDebt
        {
          get { return totalDebt; }
          set { totalDebt = value; }
        }
        private float debtOutstanding;
        public float DebtOutstanding
        {
          get { return debtOutstanding; }
          set { debtOutstanding = value; }
        }
        private string responsibleOfficer;
        public string ResponsibleOfficer
        {
          get { return responsibleOfficer; }
          set { responsibleOfficer = value; }
        }
    }
}
