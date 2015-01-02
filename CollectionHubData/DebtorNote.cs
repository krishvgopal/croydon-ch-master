using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class DebtorNote
    {
        public int NoteId               { get; set; }
        public int DebtId               { get; set; }
        public string Subject           { get; set; }
        public string SubjectAddress    { get; set; }
        public string Category          { get; set; }
        public string OurRef            { get; set; }
        public string TheirRef          { get; set; }
        public string NoteReason        { get; set; }
        public string NoteContent       { get; set; }
        public string FixedPhone        { get; set; }
        public string MobilePhone       { get; set; }
        public string EMail             { get; set; }

        public DebtorNote() { }
        public DebtorNote(IDataReader value)
        {
            NoteId = Convert.ToInt32(value["N_ID"]);
            DebtId = Convert.ToInt32(value["DebtId"]);

            Subject         = value["Subject Name"].ToString();
            SubjectAddress  = value["SubjectAddress"].ToString();
            Category        = value["Category"].ToString();
            OurRef          = value["Our Ref"].ToString();
            TheirRef        = value["Their Ref"].ToString();
            NoteReason      = value["Note Reason"].ToString();
            NoteContent     = value["Note Content"].ToString();
            FixedPhone      = value["Fixed Phone"].ToString();
            MobilePhone     = value["Mobile Phone"].ToString();
            EMail           = value["EMail"].ToString();
        }
    }

    public class DebtorNoteCategory
    {
        public string Code { get; set; }
        public string Value { get; set; }

        public DebtorNoteCategory() { }
        public DebtorNoteCategory(IDataReader value)
        {
            Code = value["code"].ToString();
            Value = value["value"].ToString();
        }
    }
}
