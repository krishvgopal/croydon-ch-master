using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using CollectionHubData;
using System.Web.Services;
using System.Text;

public partial class DataService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard.aspx");
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
    public static List<DebtParties> GetPartiesByDebt(int debtId)
    {
        var returnData = new List<DebtParties>();
        var dataAccess = new DataAccess();

        returnData = dataAccess.GetPartiesByDebtId(debtId);

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
    public static List<DebtNote> GetDebtNotes(int debtId)
    {
        var returnData = new List<DebtNote>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebtNotes(debtId);

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
    public static List<ArrangementFrequencyItem> GetFrequencyList()
    {
        var dataAccess = new CollectionHubData.DataAccess();
        var returnData = dataAccess.GetFrequencyList();

        return returnData;
    }
    [WebMethod]
    public static List<ArrangementPaymentMethods> GetPaymenyMethodList()
    {
        var dataAccess = new CollectionHubData.DataAccess();
        var returnData = dataAccess.GetPaymenyMethodList();

        return returnData;
    }
    [WebMethod]
    public static List<DebtItem> GetDebts(int pin)
    {
        var returnData = new List<DebtItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebts(pin);

        return returnData;
    }
    [WebMethod]
    public static List<DebtItem> GetDebts(int pin, bool showCleared)
    {
        var returnData = new List<DebtItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebts(pin);
        if (!showCleared) { returnData.RemoveAll(o => o.DebtOutstanding.Equals(0)); }
       
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
    [WebMethod]
    public static DebtSearchResult SearchDebts(decimal amountFrom, decimal amountTo, int debtStreamCount, int includesStreamCode, int lastPaymentCode, int debtAgeCode)
    {
        var dataAccess = new DataAccess();
        var returnData = dataAccess.DebtSearch(amountFrom, amountTo, debtStreamCount, includesStreamCode, lastPaymentCode, debtAgeCode);

        return returnData;
    }
    [WebMethod]
    public static List<LinkedAddress> GetLinkedAddress(int sourcePin)
    {
        var returnData = new List<LinkedAddress>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetLinkedAddresses(sourcePin);

        return returnData;
    }
    [WebMethod]
    public static List<Arrangement> GetArrangements(int debtId)
    {
        var returnData = new List<Arrangement>();
        var dataAccess = new DataAccess();

        returnData = dataAccess.GetArrangements(debtId);

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
    public static bool CreateNote(int debtId, int userId, string noteText)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateNote(debtId, userId, noteText);

        return returnData;
    }
    [WebMethod]
    public static bool RemoveDebtGroup(int debtId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.RemoveDebtGroup(debtId);

        return returnData;
    }
    [WebMethod]
    public static bool CreateDebtGroup(string debtIdString, int userId, int partyPin)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateDebtGroup(debtIdString, userId, partyPin);

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
    public static bool SetPersonAttributeCurrent(int personAttributeId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SetPersonAttributeCurrent(personAttributeId);

        return returnData;
    }
    [WebMethod]
    public static List<MatchList> GetMatchListByPin(int pin)
    {
        var returnData = new List<MatchList>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetMatchListByPin(pin);

        return returnData;
    }
    [WebMethod]
    public static List<MisMatchList> GetMisMatchListByPin(int pin)
    {
        var returnData = new List<MisMatchList>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetMisMatchListByPin(pin);

        return returnData;
    }
    [WebMethod]
    public static List<PersonDetails> GetPersonDetails(int pin, string uprn)
    {
        var returnData = new List<PersonDetails>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetPersonDetails(pin, uprn);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public static String GetDashboardDataPercentByYear(int sourceId, int historic)
    {
        var dataAccess = new DataAccess();
        var returnData = dataAccess.GetDashboardDataPercentByYear(sourceId, historic);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public static String GetDashboardDataAmountByYear(int sourceId, int historic)
    {
        var dataAccess = new DataAccess();
        var returnData = dataAccess.GetDashboardDataAmountByYear(sourceId, historic);

        return returnData;
    }
    [WebMethod]
    public static List<BatchProcessHistory> GetBatchProcessHistory()
    {
        var returnData = new List<BatchProcessHistory>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessHistory();

        return returnData;
    }
    [WebMethod]
    public static List<BatchProcess> GetBatchProcess(int bp_id)
    {
        var returnData = new List<BatchProcess>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcess(bp_id);

        return returnData;
    }
    [WebMethod]
    public static List<BatchProcessJobs> GetBatchProcessJobs()
    {
        var returnData = new List<BatchProcessJobs>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessJobs();

        return returnData;
    }
    [WebMethod]
    public static List<BatchProcessFields> GetBatchProcessFields(int bp_id)
    {
        var returnData = new List<BatchProcessFields>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessFields(bp_id);

        return returnData;
    }
    [WebMethod]
    public static bool RemoveMatch(int matchId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.RemoveMatch(matchId);

        return returnData;
    }
    [WebMethod]
    public static bool CreateMatch(int matchId, string pin, string userId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateMatch(matchId, pin, userId);

        return returnData;
    }
    [WebMethod]
    public static int SaveBatchParameters(int batchId, int userId, string base64String)
    {
        int returnData = 0;
        byte[] data = Convert.FromBase64String(base64String);
        string decodedString = Encoding.UTF8.GetString(data);

        var dataAccess = new CollectionHubData.DataAccess();
        returnData = dataAccess.SaveBatchJob(batchId, decodedString, userId);

        return returnData;
    }
    [WebMethod]
    public static List<BatchRecords> GetBatchRunRecords(int batchRunId)
    {
        List<BatchRecords> returnData = new List<BatchRecords>();
 
        var dataAccess = new CollectionHubData.DataAccess();
        returnData = dataAccess.GetBatchRunRecords(batchRunId);

        return returnData;
    }
    [WebMethod]
    public static bool SaveBatchIncludeStatus(int recordIdentifier, Boolean include)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SaveBatchIncludeStatus(recordIdentifier, include);

        return returnData;
    }
    [WebMethod]
    public static bool DeactivateBatch(int batchId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.DeactivateBatch(batchId);

        return returnData;
    }
    [WebMethod]
    public static bool ActivateBatch(int batchId, string batchName)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.ActivateBatch(batchId, batchName);

        return returnData;
    }
    [WebMethod]
    public static string GetBatchName(int batchId)
    {
        var returnData = String.Empty;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchName(batchId);

        return returnData;
    }
}