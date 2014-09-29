using CollectionHubData;
using System;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submitForm_Click(object sender, EventArgs e)
    {
        var da = new DataAccess();
        var ud = da.AuthenticateUser(authLoginName.Text, authPassword.Text);

        if (ud != null)
        {
            Session["USERDATA"] = ud;
            Response.Redirect("~/Dashboard.aspx");
        }
        else
        {
            badAuth.Visible = true;
        }
    }
}