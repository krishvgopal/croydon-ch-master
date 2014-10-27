using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHubData
{
    public class MatchList
    {
        public string SourceName    { get; set; }
        public string SourceRef     { get; set; }
        public string ParType       { get; set; }
        public string SourceAccRef  { get; set; }
        public string FullName      { get; set; }
        public string FullAddress   { get; set; }
        public string NINO          { get; set; }
        public string DOB           { get; set; }
        public string MatchId       { get; set; }
        public string InfoString    { get; set; }

        public MatchList() { }
        public MatchList(System.Data.SqlClient.SqlDataReader value)
        {
            SourceName          = value["SourceName"].ToString();
            SourceRef           = value["SourceRef"].ToString();
            ParType             = value["ParType"].ToString();
            SourceAccRef        = value["SourceAccRef"].ToString();
            FullName            = value["FullName"].ToString();
            FullAddress         = value["FullAddress"].ToString();
            NINO                = value["NINO"].ToString();
            DOB                 = value["DOB"].ToString();
            MatchId             = value["MatchId"].ToString();

            if (value["MatchUser"] != DBNull.Value)
            {
                InfoString += "Matched By : " + value["MatchUser"].ToString() + Environment.NewLine ;
            }
            if (value["MatchDate"] != DBNull.Value)
            {
                InfoString += "Matched At : " + value["MatchDate"].ToString();
            }
        }
    }
    public class MisMatchList
    {
        public string SourceName        { get; set; }
        public string SourceRef         { get; set; }
        public string ParType           { get; set; }
        public string SourceAccRef      { get; set; }
        public string FullName          { get; set; }
        public string FullAddress       { get; set; }
        public string NINO              { get; set; }
        public string DOB               { get; set; }
        //public string MatchToName       { get; set; }
        //public string MatchToDOB        { get; set; }
        //public string MatchToNINO       { get; set; }
        //public string MatchToOrgName    { get; set; }
        public string MatchId           { get; set; }
        public string MatchScore        { get; set; }
        public string MatchedElements   { get; set; }
        public string InfoString        { get; set; }

        
        public MisMatchList() { }
        public MisMatchList(System.Data.SqlClient.SqlDataReader value)
        {
            SourceName      = value["SourceName"].ToString();
            SourceRef       = value["SourceRef"].ToString();
            ParType         = value["ParType"].ToString();
            SourceAccRef    = value["SourceAccRef"].ToString();
            FullName        = value["FullName"].ToString();
            FullAddress     = value["FullAddress"].ToString();
            NINO            = value["NINO"].ToString();
            DOB             = value["DOB"].ToString();
            //MatchToName     = value["MatchToName"].ToString();
            //MatchToDOB      = value["MatchToDOB"].ToString();
            //MatchToNINO     = value["MatchToNINO"].ToString();
            //MatchToOrgName  = value["MatchToOrgName"].ToString();
            MatchScore      = value["MatchScore"].ToString();
            MatchedElements = value["MatchedElements"].ToString();
            MatchId         = value["MatchId"].ToString();

            if (value["MatchUser"] != DBNull.Value)
            {
                InfoString += "Matched By : " + value["MatchUser"].ToString() + Environment.NewLine ;
            }
            if (value["MatchDate"] != DBNull.Value)
            {
                InfoString += "Matched At : " + value["MatchDate"].ToString() + Environment.NewLine;
            }
            if (value["MatchUser"] != DBNull.Value)
            {
                InfoString += "Matched On : " + value["MatchedElements"].ToString() + Environment.NewLine ;
            }
            if (value["MatchDate"] != DBNull.Value)
            {
                InfoString += "Matching Score : " + value["MatchScore"].ToString();
            }
        }
    }
}
