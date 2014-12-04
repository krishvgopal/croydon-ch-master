using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using CollectionHubData;
using System.Web.Services;
using System.Text;

[ScriptService]
public partial class DataService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard.aspx");
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<PersonAttribute> GetCurrentAttribute(int partyPin)
    {
        var returnData = new List<PersonAttribute>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetCurrentAttribute(partyPin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<PersonAttribute> GetPersonAttribute(int sourcePin)
    {
        var returnData = new List<PersonAttribute>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetPersonAttribute(sourcePin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DebtAttribute> GetDebtAttribute(int debtId)
    {
        var returnData = new List<DebtAttribute>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebtAttribute(debtId);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DebtParties> GetPartiesByDebt(int debtId)
    {
        var returnData = new List<DebtParties>();
        var dataAccess = new DataAccess();

        returnData = dataAccess.GetPartiesByDebtId(debtId);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DebtParties> GetPartiesByPin(string pin)
    {
        var returnData = new List<DebtParties>();
        var dataAccess = new DataAccess();

        returnData = dataAccess.GetPartiesByPin(pin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<AttributeItem> GetAttributeList(bool listDebtAttributes, bool listPersonAttributes)
    {
        var returnData = new List<AttributeItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetAttributeList(listDebtAttributes, listPersonAttributes);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DebtNote> GetDebtNotes(int debtId)
    {
        var returnData = new List<DebtNote>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebtNotes(debtId);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Payment> GetPaymentsByDebtId(int debtId, string source, string sourceAccountReference)
    {
        var returnData = new List<Payment>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetPaymentsByDebtId(debtId, source, sourceAccountReference);

        return returnData;
    }

    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Payment> GetPaymentsByPin(string pin)
    {
        var returnData = new List<Payment>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetPaymentsByPin(pin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<TreatmentCycle> GetTreatmentCycles(int debtId)
    {
        var returnData = new List<TreatmentCycle>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetTreatmentCycles(debtId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<RecoveryCycleItem> GetRecoveryCycleHistory(int debtId)
    {
        var returnData = new List<RecoveryCycleItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetRecoveryCycleHistory(debtId);

        return returnData;
    }
    [WebMethod(CacheDuration = 120)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<ArrangementFrequencyItem> GetFrequencyList()
    {
        var dataAccess = new CollectionHubData.DataAccess();
        var returnData = dataAccess.GetFrequencyList();

        return returnData;
    }
    [WebMethod(CacheDuration = 120)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<ArrangementPaymentMethods> GetPaymenyMethodList()
    {
        var dataAccess = new CollectionHubData.DataAccess();
        var returnData = dataAccess.GetPaymenyMethodList();

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CreateArrangement(   int agm_pin,    // PIN
                                            int agm_cd_id,  // DEBTID
                                            DateTime? agm_start_date, 
                                            int agm_frequency,
                                            int agm_day_of_month, 
                                            int agm_day_of_week, 
                                            decimal agm_start_amount,
                                            decimal agm_installment_amount, 
                                            int agm_number_installment, int agm_payment_method,
                                            decimal agm_agreed_amount, 
                                            decimal agm_totaldebt_amount, 
                                            decimal agm_last_amount,
                                            int agm_Created_By, 
                                            DateTime? agm_agreement_date, 
                                            DateTime? agm_payment_date, 
                                            DateTime? agm_starting_from_date
        )
    {

        var dataAccess = new CollectionHubData.DataAccess();
        var returnData = dataAccess.CreateArrangement(agm_pin, agm_cd_id, agm_start_date, agm_frequency, agm_day_of_month, agm_day_of_week, agm_start_amount, agm_installment_amount, agm_number_installment, agm_payment_method, agm_agreed_amount, agm_totaldebt_amount, agm_last_amount, agm_Created_By, agm_agreement_date, agm_payment_date, agm_starting_from_date);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DebtItem> GetDebts(int pin)
    {
        var returnData = new List<DebtItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebts(pin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DebtItem> GetDebts(int pin, bool showCleared)
    {
        var returnData = new List<DebtItem>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebts(pin);
        if (!showCleared) { returnData.RemoveAll(o => o.DebtOutstanding.Equals(0)); }
       
        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<FullNameFullAddressSearchResults> SearchFullNameFullAddress(string firstName, string lastName, string nino, string dob, string address, string street, string postCode, bool currentAddressOnly, string sourceCode)
    {
        var returnData = new List<FullNameFullAddressSearchResults>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SearchAddress(firstName, lastName, nino, dob, address, street, postCode, currentAddressOnly, sourceCode);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<SampleData> GetSampleData(string fixedValue, int count)
    {
        var returnData = new List<SampleData>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetSampleData(fixedValue, count);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static DebtSearchResult SearchDebts(decimal amountFrom, decimal amountTo, int debtStreamCount, int includesStreamCode, int lastPaymentCode, int debtAgeCode)
    {
        var dataAccess = new DataAccess();
        var returnData = dataAccess.DebtSearch(amountFrom, amountTo, debtStreamCount, includesStreamCode, lastPaymentCode, debtAgeCode);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<LinkedAddress> GetLinkedAddress(int sourcePin)
    {
        var returnData = new List<LinkedAddress>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetLinkedAddresses(sourcePin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Arrangement> GetArrangements(int debtId)
    {
        var returnData = new List<Arrangement>();
        var dataAccess = new DataAccess();

        returnData = dataAccess.GetArrangements(debtId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Arrangement> GetArrangementsByPin(string pin)
    {
        var returnData = new List<Arrangement>();
        var dataAccess = new DataAccess();

        returnData = dataAccess.GetArrangementsByPin(pin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool SetPersonAttributeStatus(int userId, int statusId, int personAttributeId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SetPersonAttributeStatus(userId, statusId, personAttributeId);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<AttributeCurrentStatusType> GetAttributesCurrentStatuses()
    {
        var returnData = new List<AttributeCurrentStatusType>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetAttributesCurrentStatuses();

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CreateDebtAttribute(int debtId, int userId, int attributeId, bool isCurrent, string attributeValue)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateDebtAttribute(debtId, userId, attributeId, isCurrent, attributeValue);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CreatePersonAttribute(int sourceRef, int userId, int attributeId, bool isCurrent, string attributeValue)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreatePersonAttribute(sourceRef, userId, attributeId, isCurrent, attributeValue);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CreateNote(int debtId, int userId, string noteText)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateNote(debtId, userId, noteText);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool RemoveDebtGroup(int debtId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.RemoveDebtGroup(debtId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CreateDebtGroup(string debtIdString, int userId, int partyPin)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateDebtGroup(debtIdString, userId, partyPin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool SetRecoveryCycle(int debtId, int recoveryCycleId, int userId, DateTime recoveryDateTime)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SetRecoveryCycle(debtId, recoveryCycleId, userId, recoveryDateTime);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool SetPersonAttributeCurrent(int personAttributeId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SetPersonAttributeCurrent(personAttributeId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<MatchList> GetMatchListByPin(int pin)
    {
        var returnData = new List<MatchList>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetMatchListByPin(pin);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<MisMatchList> GetMisMatchListByPin(int pin)
    {
        var returnData = new List<MisMatchList>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetMisMatchListByPin(pin);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<PersonDetails> GetPersonDetails(int pin, string uprn)
    {
        var returnData = new List<PersonDetails>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetPersonDetails(pin, uprn);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public static String GetDashboardDataPercentByYear(int sourceId, int historic)
    {
        var dataAccess = new DataAccess();
        var returnData = dataAccess.GetDashboardDataPercentByYear(sourceId, historic);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public static String GetDashboardDataAmountByYear(int sourceId, int historic)
    {
        var dataAccess = new DataAccess();
        var returnData = dataAccess.GetDashboardDataAmountByYear(sourceId, historic);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchProcessHistory> GetBatchProcessHistory()
    {
        var returnData = new List<BatchProcessHistory>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessHistory();

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchProcess> GetBatchProcess(int bp_id)
    {
        var returnData = new List<BatchProcess>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcess(bp_id);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchProcessJobs> GetBatchProcessJobs()
    {
        var returnData = new List<BatchProcessJobs>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessJobs();

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchProcessFields> GetBatchProcessFields(int bp_id)
    {
        var returnData = new List<BatchProcessFields>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessFields(bp_id);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool RemoveMatch(int matchId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.RemoveMatch(matchId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CreateMatch(int matchId, string pin, string userId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateMatch(matchId, pin, userId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool SaveTemplateContent(int chtId, int userId, string content, string notes)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SaveTemplateContent(chtId, userId, content, notes);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
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
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchRecords> GetBatchRunRecords(int batchRunId)
    {
        List<BatchRecords> returnData = new List<BatchRecords>();
 
        var dataAccess = new CollectionHubData.DataAccess();
        returnData = dataAccess.GetBatchRunRecords(batchRunId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool SaveBatchIncludeStatus(int recordIdentifier, Boolean include)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.SaveBatchIncludeStatus(recordIdentifier, include);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool DeactivateBatch(int batchId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.DeactivateBatch(batchId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool ActivateBatch(int batchId, string batchName)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.ActivateBatch(batchId, batchName);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetBatchName(int batchId)
    {
        var returnData = String.Empty;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchName(batchId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchRunHistory> GetBatchRunHistory() 
    {
        var returnData = new List<BatchRunHistory>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchRunHistory();

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchProcessResult> GetBatchProcessResults(int batchProcessId)
    {
        var returnData = new List<BatchProcessResult>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessResults(batchProcessId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static BatchProcessParentHeader GetBatchProcessParentHeader(int batchRunId)
    {
        var returnData = new BatchProcessParentHeader();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessParentHeader(batchRunId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static BatchProcessParentFields GetBatchProcessParentFields(int batchRunId)
    {
        var returnData = new BatchProcessParentFields();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessParentFields(batchRunId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<BatchProcessFieldsFromRun> GetBatchProcessFieldsFromRun(int batchRunId)
    {
        var returnData = new List<BatchProcessFieldsFromRun>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetBatchProcessFieldsFromRun(batchRunId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DocumentTemplates> GetDocumentTemplates(int userId)
    {
        var returnData = new List<DocumentTemplates>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDocumentTemplates(userId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int CreateNewDocumentTemplate(int userId, string documentName, string viewName)
    {
        var returnData = -1;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateNewDocumentTemplate(userId, documentName, viewName);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static DocumentTemplate GetDocumentTemplate(int templateId)
    {
        var returnData = new DocumentTemplate();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDocumentTemplate(templateId);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DataMergeSource> GetDataMergeOptions()
    {
        var returnData = new List<DataMergeSource>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDataMergeOptions();

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DataMergeFields> GetDataMergeFields(string viewName)
    {
        var returnData = new List<DataMergeFields>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDataMergeFields(viewName);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> GetTreatmentGroups(int debtId)
    {
        var returnData = new List<string>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetTreatmentGroups(debtId);

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<TreatmentActionItems> GetTreatmentsForGroup(string actionType, int debtId)
    {
        var returnData = new List<TreatmentActionItems>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetTreatmentsForGroup(actionType, debtId);

        return returnData;
    }
    [WebMethod(CacheDuration = 60)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<DebtStream> GetDebtStreams()
    {
        var returnData = new List<DebtStream>();
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.GetDebtStreams();

        return returnData;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CreateAdHocItem(int userId, int actionItemId, int debtId)
    {
        var returnData = false;
        var dataAccess = new CollectionHubData.DataAccess();

        returnData = dataAccess.CreateAdHocItem(userId, actionItemId, debtId);

        return returnData;
    }
}