using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class PersonContactNote
    {
        public int      PersonNoteId    { get; set; }
        public DateTime CreatedDate     { get; set; }
        public string   ReasonText      { get; set; }
        public string   Content         { get; set; }
        public string   CreatedBy       { get; set; }

        public PersonContactNote() { }
        public PersonContactNote(System.Data.SqlClient.SqlDataReader value)
        {
            PersonNoteId    = Convert.ToInt32(value["PN_ID"]);
            CreatedDate     = Convert.ToDateTime(value["PN_CREATEDDATE"]);
            ReasonText      = Convert.ToString(value["PN_REASONTEXT"]);
            Content         = Convert.ToString(value["CONTENT"]);
            CreatedBy       = Convert.ToString(value["USERNAME"]);
        } 
    }
}
