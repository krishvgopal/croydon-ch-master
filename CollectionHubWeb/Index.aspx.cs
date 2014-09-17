using CollectionHubData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submitForm_Click(object sender, EventArgs e)
    {
        DataAccess da   = new DataAccess();
        UserData ud     = da.AuthenticateUser(authLoginName.Text, authPassword.Text);

        if (ud != null)
        {
            Session["USERDATA"] = ud;
            Response.Redirect("~/Dashboard.aspx");
        }

        badAuth.Visible = true;
    }
}