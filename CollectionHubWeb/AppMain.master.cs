using CollectionHubData;
using System;
using NLog;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Info("APP_RUN");
        logger.Trace("MasterPage!Page_Load");
        UserData ud = null;

        if (Session["USERDATA"] != null)
        {
            logger.Trace("\tSession_Exists");
            ud = (UserData)Session["USERDATA"];
            logger.Trace("\tRetrieve_Session");
            UserSessionToken.Value = ud.UserId.ToString();
            logger.Trace("\tSession_Value=({0})", ud.UserId);
        }
        else
        {
            logger.Trace("\tRequire_Login");
            Response.Redirect("~/Index.aspx");
        }
    }
}

/*
    logger.Trace("Sample trace message");
    logger.Debug("Sample debug message");
    logger.Info("Sample informational message");
    logger.Warn("Sample warning message");
    logger.Error("Sample error message");
    logger.Fatal("Sample fatal error message");

    // alternatively you can call the Log() method 
    // and pass log level as the parameter.
    logger.Log(LogLevel.Info, "Sample informational message");
*/