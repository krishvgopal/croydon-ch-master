using System.Web;
using CollectionHubData;
using System;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var loginHeader = Request.ServerVariables["AUTH_USER"].ToLower();

        if (loginHeader != null)
        {
            if (loginHeader.Length > 0)
            {
                var test = loginHeader;

                var da = new DataAccess();
                var ud = da.AuthenticateUser(loginHeader);

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
    }
    protected void submitForm_Click(object sender, EventArgs e)
    {
        var da = new DataAccess();
        var ud = da.AuthenticateUser(authLoginName.Text, authPassword.Text);

        //authLoginName.Text = 

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