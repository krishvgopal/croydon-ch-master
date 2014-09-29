using CollectionHubData;
using System;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var da               = new DataAccess();
        var debtAddress      = da.GetAddressForDebt(Request["source_ref"].ToString()
                                                  , Request["source"].ToString());

        pageFullName.Text           = debtAddress.FullName;
        pageFullAddress.Text        = debtAddress.FullAddress;
        pageTotalDebt.Text          = debtAddress.TotalDebt.ToString();
        pageDebtOutstanding.Text    = debtAddress.DebtOS.ToString();
        sourceValue.Value           = Request["source"].ToString();
        sourceRefValue.Value        = Request["source_ref"].ToString();
    }
}