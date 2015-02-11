using CollectionHubData;
using System;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cnpin.Value = Request["cn_pin"].ToString();
        uprn.Value  = Request["uprn"].ToString();

        var da          = new DataAccess();
        var debtAddress = da.GetAddressForDebt(Request["cn_pin"].ToString(), Request["uprn"].ToString());
        var pageIconUrl = "img/person_misc.png";
        var mapsUrl     = "https://www.google.co.uk/maps/place/" + debtAddress.FullAddress.UrlEncode();
        var addressUrl  = @"<a target='_blank' href=" + mapsUrl + ">" + debtAddress.FullAddress + "</a>";
        
        pageFullName.Text           = debtAddress.FullName;
        pageFullAddress.Text        = addressUrl;
        pageTotalDebt.Text          = debtAddress.TotalDebt.ToString("C");
        pageDebtOutstanding.Text    = debtAddress.DebtOS.ToString("C");

        var thisTitle = String.Empty;

        if (debtAddress.FullName != null)
        {
            thisTitle = debtAddress.FullName.ToLower();
            if (thisTitle.StartsWith("mr")) { pageIconUrl = "img/person_boy.png"; }
            if (thisTitle.StartsWith("miss") || thisTitle.StartsWith("mrs") || thisTitle.StartsWith("ms")){pageIconUrl = "img/person_girl.png";}
        }

        pageIcon.ImageUrl = pageIconUrl;
    }
}