using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionHubData
{
    public class UserData
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SessionIdentity { get; set; }

        public UserData(System.Data.SqlClient.SqlDataReader value)
        {
            UserId          = Convert.ToInt32(value["UserId"]);
            LoginName       = value["LoginName"].ToString();
            UserName        = value["UserName"].ToString();
            PasswordHash    = value["PasswordHash"].ToString();
            SessionIdentity = Guid.NewGuid().ToString();
        }
        public UserData()
        { }
    }
}
