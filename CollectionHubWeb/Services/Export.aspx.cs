using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using CollectionHubData;

public partial class Services_Export : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["debtExport"] != null)
        {
            GenerateExcelExport(Convert.ToInt32(Request["debtExport"]));
            return;
        }
    }
    public void GenerateExcelExport(int pin)
    {
        // Instantiate a new Workbook
        var book = new Workbook();

        // Create stream we save to
        var stream = new MemoryStream();

        // Clear all the worksheets
        book.Worksheets.Clear();

        // Add a new Sheet "Data";
        var sheet = book.Worksheets.Add("Data");

        // Get the collection returned
        var debtList = new DataAccess().GetDebts(pin);

        // We pick a few columns not all to import to the worksheet
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

        // Reset position in stream to start
        stream.Position = 0;

        //Save the Excel file
        book.Save(stream, SaveFormat.CSV);
        //book.Save(@"C:\Temp\ImportedCustomObjects.xls");

        // Get byte[] of document for saving
        var document = stream.ToArray();

        // Send to browser
        Response.Clear();
        Response.ContentType = "application/msexcel";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + pin + "_List.csv");
        Response.OutputStream.Write(document, 0, document.Length);
        Response.Flush();
        Response.End();
    }
}