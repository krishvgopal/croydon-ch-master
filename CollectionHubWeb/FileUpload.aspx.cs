using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Files.Count > 0)
        {
            string fileName = Page.Request.Files[0].FileName;

            Request.Files[0].SaveAs(@"C:\Temp\" + fileName);

            // Request.Files[0].InputStream 
        }
    }
}