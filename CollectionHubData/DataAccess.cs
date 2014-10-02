using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CollectionHubData
{
    public class DataAccess
    {
        //private const string CONNECTION_STRING = "Data Source=192.168.1.17;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=HubAdmin;Password=Croydon#";
        private const string CONNECTION_STRING = "Data Source=HIT-DEV-02\\SQL14;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=sa;Password=bakeryCakes1";
        
        
        public bool SetRecoveryCycle(int debtId, int recoveryCycleId, int userId, DateTime recoveryDateTime)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_RECOVERY_HISTORY_CREATE", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId",    debtId));
                    sqlCommand.Parameters.Add(new SqlParameter("CycleId",   recoveryCycleId));
                    sqlCommand.Parameters.Add(new SqlParameter("UserId",    userId));
                    sqlCommand.Parameters.Add(new SqlParameter("StartDate", recoveryDateTime.ToString("yyyy/MM/dd")));

                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) returnvalue = true;
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool CreateDebtGroup(string debtIdString, int userId, int partyPin, string source)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CHP_DEBTGROUP_CREATE]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtIdList", debtIdString));
                    sqlCommand.Parameters.Add(new SqlParameter("Source", source));
                    sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));
                    sqlCommand.Parameters.Add(new SqlParameter("Party_pin", partyPin));

                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) returnvalue = true;
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool RemoveDebtFromGroup(int debtId)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CHP_DEBTGROUP_CREATE]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));
                    sqlCommand.Parameters.Add(new SqlParameter("ERROR_MESSAGE", System.Data.SqlDbType.Text).Direction);
                    sqlCommand.Parameters["ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;

                    var count = sqlCommand.ExecuteNonQuery();

                    if (sqlCommand.Parameters["ERROR_MESSAGE"].Value.ToString().Length == 0) { returnvalue = true; }
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool CreateNote(int debtId, int userId, string noteText)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_DEBT_NOTE_CREATE", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("debtId", debtId));
                    sqlCommand.Parameters.Add(new SqlParameter("userId", userId));
                    sqlCommand.Parameters.Add(new SqlParameter("noteText", noteText));

                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) { returnvalue = true; }
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool CreateDebtAttribute(int debtId, int userId, int attributeId, bool isCurrent, string attributeValue)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_DEBT_ATTRIBUTE_CREATE", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("debtId", debtId));
                    sqlCommand.Parameters.Add(new SqlParameter("userId", userId));
                    sqlCommand.Parameters.Add(new SqlParameter("AttributeId", attributeId));
                    sqlCommand.Parameters.Add(new SqlParameter("AttributeValue", attributeValue));
                    sqlCommand.Parameters.Add(new SqlParameter("IsCurrent", isCurrent));

                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) { returnvalue = true; }
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool CreatePersonAttribute(int sourceRef, int userId, int attributeId, bool isCurrent, string attributeValue)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_PERSON_ATTRIBUTE_CREATE", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("SourcePin", sourceRef));
                    sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));
                    sqlCommand.Parameters.Add(new SqlParameter("AttributeId", attributeId));
                    sqlCommand.Parameters.Add(new SqlParameter("AttributeValue", attributeValue));
                    sqlCommand.Parameters.Add(new SqlParameter("IsCurrent", isCurrent));

                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) { returnvalue = true; }
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool SetPersonAttributeCurrent(int personAttributeId)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_SET_PERSON_ATTRIBUTE_CURRENT", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("personAttributeId", personAttributeId));

                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) { returnvalue = true; }
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool CreateArrangement(int agm_pin, int agm_cd_id, DateTime? agm_start_date, int agm_frequency,
                                      int agm_day_of_month, int agm_day_of_week, decimal agm_start_amount,
                                      decimal agm_installment_amount, int agm_number_installment, int agm_payment_method,
                                      decimal agm_agreed_amount, decimal agm_totaldebt_amount, decimal agm_last_amount,
                                      int agm_Created_By, DateTime? agm_agreement_date, DateTime? agm_payment_date, DateTime? agm_starting_from_date)
        {

            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CHP_Arrangement_INSERT]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("agm_pin",                   agm_pin));
                    sqlCommand.Parameters.Add(new SqlParameter("agm_cd_id",                 agm_cd_id));
                    sqlCommand.Parameters.Add(new SqlParameter("agm_start_date",            agm_start_date));          // DatePicker
                    sqlCommand.Parameters.Add(new SqlParameter("agm_frequency",             agm_frequency));           // Procedure
                    sqlCommand.Parameters.Add(new SqlParameter("agm_day_of_month",          agm_day_of_month));        // Dropdown 1..31
                    sqlCommand.Parameters.Add(new SqlParameter("agm_day_of_week",           agm_day_of_week));         // 1..7
                    sqlCommand.Parameters.Add(new SqlParameter("agm_start_amount",          agm_start_amount));        // figure
                    sqlCommand.Parameters.Add(new SqlParameter("agm_installment_amount",    agm_installment_amount));  // calced
                    sqlCommand.Parameters.Add(new SqlParameter("agm_number_installments",   agm_number_installment )); // calced
                    sqlCommand.Parameters.Add(new SqlParameter("agm_payment_method",        agm_payment_method ));     // procedure
                    sqlCommand.Parameters.Add(new SqlParameter("agm_agreed_amount",         agm_agreed_amount ));      // same as total debt amount - max value total debt amount - can be less
                    sqlCommand.Parameters.Add(new SqlParameter("agm_totaldebt_amount",      agm_totaldebt_amount));    // populate with main debt outstanding balance
                    sqlCommand.Parameters.Add(new SqlParameter("agm_last_amount",           agm_last_amount));         // remainder
                    sqlCommand.Parameters.Add(new SqlParameter("agm_Created_By",            agm_Created_By));          // user id of creater
                    sqlCommand.Parameters.Add(new SqlParameter("agm_agreement_date",        agm_agreement_date));
                    sqlCommand.Parameters.Add(new SqlParameter("agm_payment_date",          agm_payment_date));
                    sqlCommand.Parameters.Add(new SqlParameter("agm_starting_from_date",    agm_starting_from_date)); 
                    
                    // @ERROR_MESSAGE			
                    var count = sqlCommand.ExecuteNonQuery();
                    //if (count > 0) { returnvalue = true; }
                }
                sqlDataConnection.Close();
            }

            return false;
            
            //agm_pin, agm_cd_id, agm_start_date, agm_frequency, agm_day_of_month, agm_day_of_week, agm_start_amount, agm_installment_amount, agm_number_installment, agm_payment_method, agm_agreed_amount, agm_totaldebt_amount, agm_last_amount, agm_Created_By            
            //ERROR_MESSAGE			
            //@agm_pin				
            //@agm_cd_id				
            //@agm_start_date			
            //@agm_frequency			
            //@agm_day_of_month		
            //@agm_day_of_week		
            //@agm_start_amount		
            //@agm_installment_amount	
            //@agm_number_installments
            //@agm_payment_method		
            //@agm_agreed_amount		
            //@agm_totaldebt_amount	
            //@agm_last_amount		
            //@agm_Created_By			
            //@ERROR_MESSAGE			
        }

        public string GetDashboardDataPercentByYear(int sourceId, int historic)
        {
            var returnValue = "";
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_DASHBOARD_PERCENT_BY_FYEAR", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("SOURCE", sourceId));
                sqlCommand.Parameters.Add(new SqlParameter("HISTORY", historic));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        // Example data format >> { y: '2006', a: 100, b: 90 },
                        string newLine = "{ \"y\": \"" + dataReader["FYear"] + "\", " +
                                           "\"a\": \"" + cleanValue(dataReader["CTAX"]) + "\", " +
                                           "\"b\": \"" + cleanValue(dataReader["HSG"]) + "\", " +
                                           "\"c\": \"" + cleanValue(dataReader["BEN"]) + "\", " +
                                           "\"d\": \"" + cleanValue(dataReader["PR"]) + "\"}," + Environment.NewLine;

                        returnValue += newLine;
                    }
                    returnValue = returnValue.Substring(0, returnValue.Length - 3);
                    returnValue = "[" + returnValue + "]";
                }
            }
            sqlDataConnection.Close();

        

            return returnValue;
        }
        public string GetDashboardDataAmountByYear(int sourceId, int historic)
        {
            var returnValue = "";
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_DASHBOARD_AMOUNT_BY_FYEAR", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("SOURCE", sourceId));
                sqlCommand.Parameters.Add(new SqlParameter("HISTORY", historic));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    var marker = new decimal(0.0);
                    while (dataReader.Read())
                    {
                        // GET THE LARGEST NUMBER OF THE DATA ROW (NEEDED FOR CHART MAX VALUES)
                        var thisMarker = getMarker(dataReader);
                        // CARRY OVER THIS MARKER IF HIGHER THAN PREVIOUS
                        if (thisMarker > marker) {marker = thisMarker;}
                        // EXAMPLE DATA FORMAT >> { "y": "2006", "a": "100", "b": "90" }
                        string newLine = "{ \"y\": \"" + dataReader["FYear"] + "\", " +
                                           "\"a\": \"" + cleanValue(dataReader["CTAX"]) + "\", " +
                                           "\"b\": \"" + cleanValue(dataReader["HSG"]) + "\", " +
                                           "\"c\": \"" + cleanValue(dataReader["BEN"]) + "\", " +
                                           "\"d\": \"" + cleanValue(dataReader["PR"]) + "\"}," + Environment.NewLine;

                        returnValue += newLine;
                    }
                    returnValue = returnValue.Substring(0, returnValue.Length - 3);
                    returnValue = "[" + returnValue + "]";
                }
            }
            sqlDataConnection.Close();

            return returnValue;
        }
        public string GetDashboardDataBalanceByYear(int sourceId, int historic)
        {
            var returnValue = "";
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_DASHBOARD_BALANCE_BY_FYEAR", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("SOURCE", sourceId));
                sqlCommand.Parameters.Add(new SqlParameter("HISTORY", historic));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    var marker = new decimal(0.0);
                    while (dataReader.Read())
                    {
                        // GET THE LARGEST NUMBER OF THE DATA ROW (NEEDED FOR CHART MAX VALUES)
                        var thisMarker = getMarker(dataReader);
                        // CARRY OVER THIS MARKER IF HIGHER THAN PREVIOUS
                        if (thisMarker > marker) { marker = thisMarker; }
                        // EXAMPLE DATA FORMAT >> { "y": "2006", "a": "100", "b": "90" }
                        string newLine = "{ \"y\": \"" + dataReader["FYear"] + "\", " +
                                           "\"a\": \"" + cleanValue(dataReader["CTAX"]) + "\", " +
                                           "\"b\": \"" + cleanValue(dataReader["HSG"]) + "\", " +
                                           "\"c\": \"" + cleanValue(dataReader["BEN"]) + "\", " +
                                           "\"d\": \"" + cleanValue(dataReader["PR"]) + "\"}," + Environment.NewLine;

                        returnValue += newLine;
                    }
                    returnValue = returnValue.Substring(0, returnValue.Length - 3);
                    returnValue = "[" + returnValue + "]";
                }
            }
            sqlDataConnection.Close();

            return returnValue;
        }

        public List<ArrangementFrequencyItem> GetFrequencyList()
        {
            var returnValue = new List<ArrangementFrequencyItem>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_PAYMENT_FREQ_LIST", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new ArrangementFrequencyItem(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<DebtItem> GetDebts(int pin)
        {
            var returnValue = new List<DebtItem>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_GETPERSONDEBTS_byPIN", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("pin", pin));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var newResult = new DebtItem(dataReader);
                        returnValue.Add(newResult);
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<ArrangementPaymentMethods> GetPaymenyMethodList()
        {
            var returnValue = new List<ArrangementPaymentMethods>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_PAYMENT_METHODS_LIST", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new ArrangementPaymentMethods(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<DebtNote> GetDebtNotes(int debtId)
        {
            var returnValue = new List<DebtNote>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_DEBT_NOTES_LIST", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new DebtNote(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<FullNameFullAddressSearchResults> SearchAddress(string firstName, string lastName)
        {
            var returnValue = new List<FullNameFullAddressSearchResults>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("FULLNAME_FULLADDRESS_SEARCH", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("FIRSTNAME", firstName));
                sqlCommand.Parameters.Add(new SqlParameter("LASTNAME", lastName));
                //sqlCommand.Parameters.Add(new SqlParameter("MIDNAME", DBNull.Value));
                //sqlCommand.Parameters.Add(new SqlParameter("ORGNAME", DBNull.Value));
                //sqlCommand.Parameters.Add(new SqlParameter("DOB", DBNull.Value));
                //sqlCommand.Parameters.Add(new SqlParameter("NINO", DBNull.Value));
                //sqlCommand.Parameters.Add(new SqlParameter("ADDRNUMBER", DBNull.Value));
                //sqlCommand.Parameters.Add(new SqlParameter("ADDRNAME", DBNull.Value));
                //sqlCommand.Parameters.Add(new SqlParameter("ADDRPOSTCODE", DBNull.Value));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var newResult = new FullNameFullAddressSearchResults(dataReader);
                        returnValue.Add(newResult);
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<SampleData> GetSampleData(string fixedValue, int count)
        {
            var returnValue = new List<SampleData>();

            for (var i = 0; i < count; i++)
            {
                returnValue.Add(new SampleData(fixedValue + "-" + i.ToString()));
            }

            return returnValue;
        }
        public List<RecoveryCycleItem> GetRecoveryCycleHistory(int debtId)
        {
            var returnValue = new List<RecoveryCycleItem>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_RECOVERY_HISTORY_SELECT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new RecoveryCycleItem(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<RecoveryCycle> GetRecoveryCycles()
        {
            var returnValue = new List<RecoveryCycle>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_RECOVERY_CYCLE_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new RecoveryCycle(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<DebtItem> GetFrequencyListGetDebts(int pin)
        {
            var returnValue = new List<DebtItem>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_GETPERSONDEBTS_byPIN", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("pin", pin));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var newResult = new DebtItem(dataReader);
                        returnValue.Add(newResult);
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<DebtAttribute> GetDebtAttribute(int debtId)
        {
            var returnValue = new List<DebtAttribute>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_DEBT_ATTRIBUTES_LIST", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("debtId", debtId));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new DebtAttribute(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<PersonAttribute> GetPersonAttribute(int partyPin)
        {
            var returnValue = new List<PersonAttribute>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_PERSON_ATTRIBUTES_LIST", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("partyPin", partyPin));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new PersonAttribute(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<PersonAttribute> GetCurrentAttribute(int partyPin)
        {
            var returnValue = new List<PersonAttribute>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_CURRENT_ATTRIBUTES_LIST", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("partyPin", partyPin));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new PersonAttribute(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<AttributeItem> GetAttributeList(bool listDebtAttributes, bool listPersonAttributes)
        {
            var returnValue = new List<AttributeItem>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_GET_ATTRIBUTES", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("listDebtAttributes", listDebtAttributes));
                    sqlCommand.Parameters.Add(new SqlParameter("listPersonAttributes", listPersonAttributes));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new AttributeItem(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<Payment> GetPaymentsByDebtId(int debtId)
        {
            var returnValue = new List<Payment>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_PAYMENTS_byDebtId", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new Payment(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<DebtParties> GetPartiesByDebtId(int debtId)
        {
            var returnValue = new List<DebtParties>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_PARTIES_ByDebt", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new DebtParties(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<LinkedAddress> GetLinkedAddresses(int partyPin)
        {
            var returnValue = new List<LinkedAddress>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CH_ADDRESS_LIST]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("Pin", partyPin));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new LinkedAddress(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;   
        }
        public List<Arrangement> GetArrangements(int debtId)
        {
            var returnValue = new List<Arrangement>();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_ARRANGEMENTS_BY_DEBTID", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnValue.Add(new Arrangement(dataReader));
                        }
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        
        public DebtSearchResult DebtSearch(decimal amountFrom, decimal amountTo, int debtStreamCount, int includesStreamCode, int lastPaymentCode, int debtAgeCode)
        {
            var returnValue = new DebtSearchResult();
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_TOTALS_COUNT", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("AmountFrom", amountFrom));
                    sqlCommand.Parameters.Add(new SqlParameter("AmountTo", amountTo));
                    sqlCommand.Parameters.Add(new SqlParameter("DebtStreamCount", debtStreamCount));
                    sqlCommand.Parameters.Add(new SqlParameter("IncludesStreamCode", includesStreamCode));
                    sqlCommand.Parameters.Add(new SqlParameter("LastPaymentCode", lastPaymentCode));
                    sqlCommand.Parameters.Add(new SqlParameter("DebtAgeCode", debtAgeCode));
                    sqlCommand.Parameters.Add(new SqlParameter("List", 1));

                    sqlCommand.Parameters.Add(new SqlParameter("Records", SqlDbType.Int));
                    sqlCommand.Parameters["Records"].Direction = ParameterDirection.Output;
                    
                    sqlCommand.Parameters.Add(new SqlParameter("Amount",  SqlDbType.Float));
                    sqlCommand.Parameters["Amount"].Direction  = ParameterDirection.Output;

                    var dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            if (dataReader[0] != DBNull.Value)
                            {
                                returnValue.RecordCount = Convert.ToInt32(dataReader[0].ToString());
                            }
                            if (dataReader[1] != DBNull.Value)
                            {
                                returnValue.TotalValue = Convert.ToDecimal(dataReader[1].ToString());
                            }
                        }
                    }

                    dataReader.NextResult();

                    if (dataReader.HasRows)
                    {
                        var results = new List<DebtSearchResultItem>();
                        
                        while (dataReader.Read())
                        {
                            results.Add(new DebtSearchResultItem(dataReader));
                        }

                        returnValue.Results = results;
                    }
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public DebtAddress GetAddressForDebt(string pin)
        {
            var returnValue = new DebtAddress();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_GetNameAddress_byPIN", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("pin", pin));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var newResult = new DebtAddress(dataReader);
                        returnValue = newResult;
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public UserData AuthenticateUser(string loginName, string passwordHash)
        {
            UserData returnValue = null;
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_AUTHENTICATE_USER", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("LoginName", loginName));
                sqlCommand.Parameters.Add(new SqlParameter("PasswordHash", passwordHash));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue = new UserData(dataReader);
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }

        private decimal getMarker(SqlDataReader dataReader)
        {
            decimal marker = 0;
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (dataReader.GetFieldType(i) == Type.GetType("System.Int16") ||
                    dataReader.GetFieldType(i) == Type.GetType("System.Int32") ||
                    dataReader.GetFieldType(i) == Type.GetType("System.Int64") ||
                    dataReader.GetFieldType(i) == Type.GetType("System.Double") ||
                    dataReader.GetFieldType(i) == Type.GetType("System.Decimal") ||
                    dataReader.GetFieldType(i) == Type.GetType("System.Byte"))
                {
                    if (dataReader[i] != DBNull.Value)
                    {
                        if (marker < Convert.ToDecimal(dataReader[i]))
                        {
                            marker = Convert.ToDecimal(dataReader[i]);
                        }
                    }
                }
            }

            return marker;
        }
        private string cleanValue(object value)
        {
            if (value != null)
            {
                if (value.ToString().Length > 0)
                {
                    decimal returnValue = Convert.ToDecimal(value);
                    if (returnValue == 0)
                    {
                        return "0";
                    }
                    else
                    {
                        return returnValue.ToString("#.##");
                    }
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
    }
}