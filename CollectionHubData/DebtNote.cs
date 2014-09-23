﻿using System;

namespace CollectionHubData
{
    public class DebtNote
    {
        public int      NoteId { get; set; }
        public int      DebtId { get; set; }
        public int      UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string   NoteText { get; set; }

        public DebtNote() { }
        public DebtNote(System.Data.SqlClient.SqlDataReader value)
        {
            NoteId      = Convert.ToInt32(value["NoteId"].ToString());
            DebtId      = Convert.ToInt32(value["DebtId"].ToString());
            UserId      = Convert.ToInt32(value["UserId"].ToString());
            CreatedDate = Convert.ToDateTime(value["CreatedDate"]);
            NoteText    = Convert.ToString(value["NoteText"].ToString());
        }
    }
}
