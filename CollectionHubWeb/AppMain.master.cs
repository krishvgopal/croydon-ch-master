using CollectionHubData;
using System;

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
