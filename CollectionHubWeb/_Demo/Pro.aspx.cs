﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using NLog;

public partial class Scripts_Pro : System.Web.UI.Page
{
    //private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        foreach (String key in Request.QueryString.AllKeys)
        {
            System.Diagnostics.Debug.WriteLine("{0}:{1}", key, Request.QueryString[key]);
            sb.AppendLine(key + ":" + Request.QueryString[key]);
        }

        //System.Diagnostics.Debug.WriteLine("______________________________");
        //System.Diagnostics.Debug.WriteLine("Form Values");
        //foreach (var key in Request.Form.AllKeys)
        //{
        //    //logger.Debug("Key:[{0}],Value:[{1}]", key, Request.Form[key]);
        //    System.Diagnostics.Debug.WriteLine("Key:[{0}],Value:[{1}]", key, Request.Form[key]);
        //}

        //string json = "";

        Debug.Write(sb.ToString());

       // DataTableParameters x = new DataTableParameters(sb.ToString());

        var counter = 1;

        if (Application["count"] == null)
        {
            Application["count"] = 1;
        }
        else
        {
            Application["count"] = Convert.ToInt32(Application["count"]) + 1;
            counter = Convert.ToInt32(Application["count"]);
        }

        Response.Clear();

        var sr = new StreamReader(@"C:\_Repositories\HubSolutionsCollectionHub\CollectionHubWeb\Scripts\test.json");
        var json = sr.ReadToEnd();
        sr.Close();

        json = json.Replace("~d~", "\"" + counter.ToString() + "\"" );

        Response.Write(json);

        
            //Application["data"] = json;
        //}
        //else
        //{
        //    json = Application["data"].ToString();
        //}

       // Response.Write(json);
        //Response.Flush();
    }
}







