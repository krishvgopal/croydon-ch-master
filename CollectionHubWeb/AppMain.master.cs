using CollectionHubData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserData ud = null;

        if (Session["USERDATA"] != null)
        {
            ud = (UserData)Session["USERDATA"];
            UserSessionToken.Value = ud.UserId.ToString();
        }
        else
        {
            Response.Redirect("~/Index.aspx");
        }
    }
}
