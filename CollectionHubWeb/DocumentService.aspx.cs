using System;

using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

using CollectionHubData;
using Aspose.Words;

public partial class DocumentService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["documentId"] != null)
        {
            var documentId = Convert.ToInt32(Request.QueryString["documentId"]);
            var returnByte = DownloadItemById(documentId);

            Response.Clear();
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", "attachment; filename=test.doc");
            Response.OutputStream.Write(returnByte, 0, returnByte.Length);
            Response.Flush();
            Response.End();
        }
        if (Request.QueryString["sessionDocument"] != null)
        {
            var documentId = Request.QueryString["sessionDocument"].ToString();
            var returnByte = openFileAsBytes("C:\\Temp\\Process_Batch_" + documentId + ".doc");

            Response.Clear();
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", "attachment; filename=test.doc");
            Response.OutputStream.Write(returnByte , 0, returnByte.Length);
            Response.Flush();
            Response.End();
        }
        else
        {
            Response.Redirect(@"~/Default.aspx");
        }
    }
    private static byte[] openFileAsBytes(string filePath)
    {
        var fs      = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var length  = Convert.ToInt32(fs.Length);
        var data    = new byte[length];

        fs.Read(data, 0, length);
        fs.Close();
        
        return data;
    }
    //public string ExecuteBatchDocuments(int templateId, int userId, string pin, string uprn)
    //{
    //    var da = new DataAccess();
    //    var dt = da.GetDocumentTemplate(templateId);
    //    var mv = da.GetMergeValues(userId, dt.CHT_ViewTable, pin, uprn);

    //    var html = dt.CHT_Content;

    //    foreach (MergeValue m in mv)
    //    {
    //        // TODO: THIS SHOULD BE LOOKING FOR ALL [] MATCHES TO REMOVE UNUSED MARKERS
    //        html = html.Replace(m.FieldName, m.FieldValue);
    //    }

    //    //return html;

    //    var returnValue = false;

    //    var stream = new MemoryStream();

    //    var d = new Document();
    //    var b = new DocumentBuilder(d);

    //    b.InsertHtml(documentContent);
    //    d.Save(stream, SaveFormat.Doc);
    //    //d.Save("C:\\Temp\\new_doc_" + Guid.NewGuid().ToString() + ".doc" );

    //    stream.Position = 0;

    //    var documentBody = stream.ToArray();

    //    stream.Close();

    //    //var da = new DataAccess();

    //    returnValue = da.SaveDocument(userId, documentTemplateId, documentName, documentContent, documentBody, actionId, pin, uprn, debtId);

    //    stream.Dispose();
    //    d = null;

    //    //  return returnValue;

    //}
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ProcessAdd(int itemId, int groupId, int userId, string pin, string uprn, int debtId)
    {
        var da = new DataAccess();

        var dt = da.GetDocumentTemplate(itemId);
        var mv = da.GetMergeValues(userId, dt.CHT_ViewTable, pin, uprn, debtId);

        var html = dt.CHT_Content;

        foreach (MergeValue m in mv)
        {
            // TODO: THIS SHOULD BE LOOKING FOR ALL [] MATCHES TO REMOVE UNUSED MARKERS
            html = html.Replace(m.FieldName, m.FieldValue);
        }

        return html;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string MergeDocument(int documentTemplateId, int templateId, int userId, string pin, string uprn, int debtId)
    {
        var da = new DataAccess();
        var dt = da.GetDocumentTemplate(templateId);
        var mv = da.GetMergeValues(userId, dt.CHT_ViewTable, pin, uprn, debtId);

        var html = dt.CHT_Content;

        foreach (MergeValue m in mv)
        {
            // TODO: THIS SHOULD BE LOOKING FOR ALL [] MATCHES TO REMOVE UNUSED MARKERS
            html = html.Replace(m.FieldName, m.FieldValue);
        }

        return html;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool SaveDocument(string documentContent, string documentName, int documentTemplateId, int actionId, int userId, string pin, string uprn, int debtId)
    {
        var returnValue     = false;
        
        var stream = new MemoryStream();
        
        var d = new Document();
        var b = new DocumentBuilder(d);

        b.InsertHtml(documentContent);
        d.Save(stream, SaveFormat.Doc);
        //d.Save("C:\\Temp\\new_doc_" + Guid.NewGuid().ToString() + ".doc" );
        
        stream.Position = 0;

        var documentBody = stream.ToArray();

        stream.Close();

        var da = new DataAccess();

        returnValue = da.SaveDocument(userId, documentTemplateId, documentName, documentContent, documentBody, actionId, pin, uprn, debtId);

        stream.Dispose();
        d = null;

        return returnValue;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool ProcessSave(string documentContent, int actionId, int userId, string pin, string uprn)
    {
        var returnValue = false;

        var stream  = new MemoryStream();
        var d       = new Document();
        var b       = new DocumentBuilder(d);

        b.InsertHtml(documentContent);
        d.Save(stream, SaveFormat.Doc);
        //d.Save("C:\\Temp\\new_doc_" + Guid.NewGuid().ToString() + ".doc");
        
        stream.Position = 0;

        var documentBody = stream.ToArray();

        stream.Close();

        var da      = new DataAccess();

        returnValue = da.SaveDebtAction(userId, actionId, documentContent, documentBody, pin, uprn);

        stream.Dispose();
        d = null;

        return returnValue;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool ProcessPrint(int actionId)
    {
        var returnValue = false;

        //var stream  = new MemoryStream();
        //var d       = new Document();
        //var b       = new DocumentBuilder(d);

        //b.InsertHtml(documentContent);
        //d.Save(stream, SaveFormat.Doc);
        
        //stream.Position = 0;

        //var documentBody = stream.ToArray();

        //stream.Close();

        var da = new DataAccess();

        // public bool CompleteDebtAction(int actionId, int outputType)

        returnValue = da.CompleteDebtAction(actionId, 1);

        //stream.Dispose();
        //d = null;

        return returnValue;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string OpenItemById(int itemId)
    {
        var da = new DataAccess();
        CorrespondenceItem returnValue = da.GetActionCorrespondenceItem(itemId);
        return returnValue.ContentText;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static byte[] DownloadItemById(int itemId)
    {
        var da = new DataAccess();
        CorrespondenceItem returnValue = da.GetActionCorrespondenceItem(itemId);
        return returnValue.ContentArray;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ProcessAutomaticItems(string[] actionItems, int userId, int pin, int uprn, int debtId)
    {
        var da              = new DataAccess();
        var parentStream    = new MemoryStream();
        var parentDocument  = new Document();

        // enumerate all the items
        foreach (string s in actionItems)
        {
            var stream          = new MemoryStream();
            var document        = new Document();
            var documentBuilder = new DocumentBuilder(document);

            // This action Id
            var actionId = Convert.ToInt32(s);

            // Get the template
            var html = da.GetTemplateForActionItem(actionId).Value;

            // Get the view
            var view = da.GetViewForActionItem(actionId).Value;

            // Execute View & merge with results
            var mv = da.GetMergeValues(userId, view, pin.ToString(), uprn.ToString(), debtId);

            // Merge Results
            foreach (MergeValue m in mv)
            {
                // TODO: THIS SHOULD BE LOOKING FOR ALL [] MATCHES TO REMOVE UNUSED MARKERS

                // Get alternate delimeters, e.g. not [ but «
                var alternateFieldName = m.FieldName.Replace("[", "«").Replace("]", "»");

                // Merge values to HTML
                html = html.Replace(m.FieldName, m.FieldValue);
                html = html.Replace(alternateFieldName, m.FieldValue);
            }

            // Add HTML to document
            documentBuilder.InsertHtml(html);

            // Save to stream object
            document.Save(stream, SaveFormat.Doc);

            // Save to filesystem for testing
            document.Save("C:\\Temp\\ActionItem_Document_" + Guid.NewGuid().ToString() + ".doc");

            // Reset position in stream to start
            stream.Position = 0;

            // Get byte[] of document for saving
            var documentBody = stream.ToArray();

            // Attempt to save the action item in the db
            var returnValue = da.SaveDebtAction(userId, actionId, html, documentBody, pin.ToString(), uprn.ToString());

            // Setup correct page section start so we append correctly to parent
            document.FirstSection.PageSetup.SectionStart = SectionStart.NewPage;

            // Global parent document to hold all sub documents
            parentDocument.AppendDocument(document, ImportFormatMode.KeepSourceFormatting);

            // Save overall parent document
            parentDocument.Save(parentStream, SaveFormat.Doc);

            // Close open stream as we have the byte array
            stream.Close();

            // Kill the stream
            stream.Dispose();

            // Kill the document
            // document = null;
        }

        // Get Id for the document
        var parentDocumentId = Guid.NewGuid().ToString();

        // Save to filesystem for testing
        parentDocument.Save("C:\\Temp\\Process_Batch_" + parentDocumentId + ".doc");

        // Reset position in parent stream to start
        parentStream.Position = 0;
        
        // Return batch to user
        return parentDocumentId;
    }
}