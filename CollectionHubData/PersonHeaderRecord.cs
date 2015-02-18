using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class PersonHeaderRecord
    {
        public string Source            { get; set; }
        public string Pin               { get; set; }
        public string Uprn              { get; set; }
        public string Fullname          { get; set; }
        public string Nino              { get; set; }
        public string Dob               { get; set; }
        public string Fulladdress       { get; set; }
        public string TotalDebt         { get; set; }
        public string DebtOS            { get; set; }
        public string DebtBal           { get; set; }
        public string DebtCredit        { get; set; }
        public string CreditScore       { get; set; }
        public string CreditScoreDate   { get; set; }

        public PersonHeaderRecord() { }
        public PersonHeaderRecord(System.Data.SqlClient.SqlDataReader value)
        {
            Source      = value["SOURCE"].ToString();
            Pin         = value["PIN"].ToString();
            Uprn        = value["UPRN"].ToString();
            Fullname    = value["Fullname"].ToString();
            Nino        = value["NINO"].ToString();
            Dob         = value["DOB"].ToString();
            Fulladdress = value["FULLADDRESS"].ToString();
            TotalDebt   = value["TotalDebt"].ToString();
            DebtOS      = value["DebtOS"].ToString();
            DebtBal     = value["DebtBal"].ToString();
            DebtCredit  = value["DebtCredit"].ToString();
            CreditScore = value["CreditScore"].ToString();
            CreditScoreDate = value["CreditScoreDate"].ToString();
        }
    }
}
