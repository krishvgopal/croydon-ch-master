using System;
using CollectionHubData;

public partial class Common_SiteHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var ud = (UserData)Session["USERDATA"];
        var id = ud.UserName;
        
        if (ud.WindowsId != null)
        {
            if (ud.WindowsId.Length > 0)
            {
                id += "<br/><i>" + ud.WindowsId + "</i>";
            }
        }
        userName.Text = id;
    }
}