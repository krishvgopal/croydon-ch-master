using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CollectionHubData;
using System.Web.Services;

public partial class DataService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard.aspx");
    }

    // SetPersonAttributeCurrent

    [WebMethod]
    public static bool SetPersonAttributeCurrent(int personAttributeId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SetPersonAttributeCurrent(personAttributeId);

        return returnData;
    }

    [WebMethod]
    public static List<PersonAttribute> GetCurrentAttribute(int partyPin)
    {
        var returnData = new List<PersonAttribute>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetCurrentAttribute(partyPin);

        return returnData;
    }
    [WebMethod]
    public static List<PersonAttribute> GetPersonAttribute(int sourcePin)
    {
        var returnData = new List<PersonAttribute>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetPersonAttribute(sourcePin);

        return returnData;
    }
    [WebMethod]
    public static List<DebtAttribute> GetDebtAttribute(int debtId)
    {
        var returnData = new List<DebtAttribute>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebtAttribute(debtId);

        return returnData;
    }
    [WebMethod]
    public static bool CreateDebtAttribute(int debtId, int userId, int attributeId, bool isCurrent, string attributeValue)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateDebtAttribute(debtId, userId, attributeId, isCurrent, attributeValue);

        return returnData;
    }
    [WebMethod]
    public static bool CreatePersonAttribute(int sourceRef, int userId, int attributeId, bool isCurrent, string attributeValue)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreatePersonAttribute(sourceRef, userId, attributeId, isCurrent, attributeValue);

        return returnData;
    }
    [WebMethod]
    public static List<AttributeItem> GetAttributeList(bool listDebtAttributes, bool listPersonAttributes)
    {
        var returnData = new List<AttributeItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetAttributeList(listDebtAttributes, listPersonAttributes);

        return returnData;
    }
    [WebMethod]
    public static bool CreateNote(int debtId, int userId, string noteText)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateNote(debtId, userId, noteText);

        return returnData;
    }
    [WebMethod]
    public static List<DebtNote> GetDebtNotes(int debtId)
    {
        var returnData = new List<DebtNote>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebtNotes(debtId);

        return returnData;
    }
    [WebMethod]
    public static bool RemoveDebtFromGroup(int debtId, int userId, int partyPin, string source)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.RemoveDebtFromGroup(debtId);

        return returnData;
    }
    [WebMethod]
    public static bool CreateDebtGroup(string debtIdString, int userId, int partyPin, string source)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateDebtGroup(debtIdString, userId, partyPin, source);

        return returnData;
    }
    [WebMethod]
    public static List<Payment> GetPaymentsByDebtId(int debtId)
    {
        var returnData = new List<Payment>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetPaymentsByDebtId(debtId);

        return returnData;
    }
    [WebMethod]
    public static bool SetRecoveryCycle(int debtId, int recoveryCycleId, int userId, DateTime recoveryDateTime)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SetRecoveryCycle(debtId, recoveryCycleId, userId, recoveryDateTime);

        return returnData;
    }
    [WebMethod]
    public static List<RecoveryCycle> GetRecoveryCycles()
    {
        var returnData = new List<RecoveryCycle>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetRecoveryCycles();

        return returnData;
    }
    [WebMethod]
    public static List<RecoveryCycleItem> GetRecoveryCycleHistory(int debtId)
    {
        var returnData = new List<RecoveryCycleItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetRecoveryCycleHistory(debtId);

        return returnData;
    }
    [WebMethod]
    public static List<DebtItem> GetDebts(string source, string sourceRef)
    {
        var returnData = new List<DebtItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebts(sourceRef , source);

        return returnData;
    }
    [WebMethod]
    public static List<FullNameFullAddressSearchResults> SearchFullNameFullAddress(string firstName, string lastName)
    {
        var returnData = new List<FullNameFullAddressSearchResults>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SearchAddress(firstName, lastName);

        return returnData;
    }
    [WebMethod]
    public static List<SampleData> GetSampleData(string fixedValue, int count)
    {
        var returnData = new List<SampleData>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetSampleData(fixedValue, count);

        return returnData;
    }
}