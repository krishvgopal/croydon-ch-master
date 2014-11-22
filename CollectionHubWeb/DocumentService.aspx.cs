﻿using System;

using System.IO;

using System.Web.Services;

using CollectionHubData;
using Aspose.Words;

public partial class DocumentService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Default.aspx");
    }

    [WebMethod]
    public static string MergeDocument(int documentTemplateId, int templateId, int userId, string pin, string uprn)
    {
        var da = new DataAccess();
        var dt = da.GetDocumentTemplate(templateId);
        var mv = da.GetMergeValues(userId, dt.CHT_ViewTable, pin, uprn);

        var html = dt.CHT_Content;

        foreach (MergeValue m in mv)
        {
            // TODO: THIS SHOULD BE LOOKING FOR ALL [] MATCHES TO REMOVE UNUSED MARKERS
            html = html.Replace(m.FieldName, m.FieldValue);
        }

        return html;
    }

    [WebMethod]
    public static bool SaveDocument(string documentContent, string documentName, int documentTemplateId, int actionId, int userId, string pin, string uprn, int debtId)
    {
        var returnValue     = false;
        
        var stream = new MemoryStream();
        
        var d = new Document();
        var b = new DocumentBuilder(d);

        b.InsertHtml(documentContent);
        d.Save(stream, SaveFormat.Doc);
        //d.Save("new_doc_" + Guid.NewGuid().ToString() + ".doc" );
        
        stream.Position = 0;

        var documentBody = stream.ToArray();

        stream.Close();

        var da = new DataAccess();

        returnValue = da.SaveDocument(userId, documentTemplateId, documentName, documentContent, documentBody, actionId, pin, uprn, debtId);

        stream.Dispose();
        d = null;

        return returnValue;
    }
}