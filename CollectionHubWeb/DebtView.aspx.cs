using System.Collections.Generic;
using CollectionHubData;
using System;
using Aspose.Cells;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["export"] != null)
        {
            GenerateExcelExport(Convert.ToInt32(Request["export"]));
            return;
        }

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

    public void GenerateExcelExport(int pin)
    {
        //Instantiate a new Workbook
        Workbook book = new Workbook();

        //Clear all the worksheets
        book.Worksheets.Clear();

        //Add a new Sheet "Data";
        Worksheet sheet = book.Worksheets.Add("Data");

        // Get the collection returned
        var debtList = new DataAccess().GetDebts(pin);

        //We pick a few columns not all to import to the worksheet
        sheet.Cells.ImportCustomObjects((System.Collections.ICollection)debtList,
        new string[] { "DebtSource", "DebtAddress", "DebtAccRef", "DebtReference", "DebtTotal", "DebtOutstanding", "ResponsibleUserName" },
        true,
        0,
        0,
        debtList.Count,
        true,
        "dd/mm/yyyy",
        false);

        //Auto-fit all the columns
        book.Worksheets[0].AutoFitColumns();
        //Save the Excel file
        book.Save(@"e:\Temp\ImportedCustomObjects.xls");

    }
}