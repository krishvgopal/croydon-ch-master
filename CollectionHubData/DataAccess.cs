using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CollectionHubData
{
    public class DataAccess
    {
        private const string CONNECTION_STRING = "Data Source=192.168.1.10;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=HubAdmin;Password=Croydon#;Connection Timeout=60";
        //private const string CONNECTION_STRING = "Data Source=HIT-DEV-02\\SQL14;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=sa;Password=bakeryCakes1;Connection Timeout=30";

        

        public bool RemoveMatch(int matchId)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CH_NAMES_UNLINK]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("matchId", matchId));

                    var count = sqlCommand.ExecuteNonQuery();
                    if (count > 0) returnvalue = true;
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool CreateMatch(int matchId, string pin, string userId)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CH_NAMES_LINK]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("matchId", matchId));
                    sqlCommand.Parameters.Add(new SqlParameter("pin", pin));
                    sqlCommand.Parameters.Add(new SqlParameter("userId", userId));

                    var count = sqlCommand.ExecuteNonQuery();
                    if (count > 0) returnvalue = true;
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        
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
        
        public bool CreateDebtGroup(string debtIdString, int userId, int partyPin)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CHP_DEBTGROUP_CREATE]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtIdList", debtIdString));
                    sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));
                    sqlCommand.Parameters.Add(new SqlParameter("cn_pin", partyPin));

                    var count = sqlCommand.ExecuteNonQuery();
                    if (count > 0) returnvalue = true;
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        public bool RemoveDebtGroup(int debtId)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CHP_DEBTGROUP_DELETE]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));

                    sqlCommand.Parameters.Add(new SqlParameter("ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 200));
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

        #region DASHBOARD GRAPH PROCEDURES

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

        #endregion

        #region SINGLE DEBT VIEW

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
        public List<DebtItem>           GetDebts(int pin)
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
        public List<DebtNote>           GetDebtNotes(int debtId)
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
        
        public List<ArrangementPaymentMethods>          GetPaymenyMethodList()
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
        public List<FullNameFullAddressSearchResults>   SearchAddress(string firstName, string lastName)
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
       
        public List<RecoveryCycleItem>  GetRecoveryCycleHistory(int debtId)
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
        public List<RecoveryCycle>      GetRecoveryCycles()
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
        public List<DebtItem>           GetFrequencyListGetDebts(int pin)
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

        #region ATTRIBUTES

        public List<DebtAttribute>      GetDebtAttribute(int debtId)
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
        public List<PersonAttribute>    GetPersonAttribute(int partyPin)
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
        public List<PersonAttribute>    GetCurrentAttribute(int partyPin)
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
        public List<AttributeItem>      GetAttributeList(bool listDebtAttributes, bool listPersonAttributes)
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
        
        #endregion

        public List<Payment>            GetPaymentsByDebtId(int debtId)
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
        public List<DebtParties>        GetPartiesByDebtId(int debtId)
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
        public List<LinkedAddress>      GetLinkedAddresses(int partyPin)
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
        public List<Arrangement>        GetArrangements(int debtId)
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
        public List<MatchList>          GetMatchListByPin(int pin)
        {
            var returnValue = new List<MatchList>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_MatchList_byPIN", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("Pin", pin));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new MatchList(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<MisMatchList>       GetMisMatchListByPin(int pin)
        {
            var returnValue = new List<MisMatchList>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_MisMatchList_byPIN", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("Pin", pin));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new MisMatchList(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        
        public List<PersonDetails>      GetPersonDetails(int pin, string uprn)
        {
            var returnValue = new List<PersonDetails>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open(); // CHP_GetNameAddress_byPIN // CHP_PersonDetails_byPIN
            using (var sqlCommand = new SqlCommand("CHP_GetNameAddress_byPIN", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));
                sqlCommand.Parameters.Add(new SqlParameter("UPRN", uprn));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new PersonDetails(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public DebtSearchResult         DebtSearch(decimal amountFrom, decimal amountTo, int debtStreamCount, int includesStreamCode, int lastPaymentCode, int debtAgeCode)
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
        public DebtAddress              GetAddressForDebt(string pin, string uprn)
        {
            var returnValue = new DebtAddress();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_GetNameAddress_byPIN", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("pin", pin));
                sqlCommand.Parameters.Add(new SqlParameter("uprn", uprn));

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

        #endregion

        #region AUTHENTICATION

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

        #endregion

        #region UTILITIES

        private string getStringValue(string valueString, int targetId)
        {
            string r = String.Empty;
            string[] p = valueString.Split('&');
            foreach (var pair in p)
            {
                string[] kv = pair.Split('=');
                if(kv.Length == 2)
                {
                    var k = kv[0];
                    var v = kv[1];
                    k = k.Replace("auto_datepicker_","").Replace("auto_","");
                    if(k == targetId.ToString())
                    {
                        r = v.ToString();
                    }
                }
            }
            return r;
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
        public List<SampleData> GetSampleData(string fixedValue, int count)
        {
            var returnValue = new List<SampleData>();

            for (var i = 0; i < count; i++)
            {
                returnValue.Add(new SampleData(fixedValue + "-" + i.ToString()));
            }

            return returnValue;
        }

        #endregion      

        #region BATCHES

        public bool SaveBatchIncludeStatus(int recordIdentifier, bool includeInBatch)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("[CH_SET_BATCH_INCLUDE_STATUS]", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("recordIdentifier", recordIdentifier));
                    sqlCommand.Parameters.Add(new SqlParameter("includeInBatch", includeInBatch));

                    var count = sqlCommand.ExecuteNonQuery();
                    if (count > 0) returnvalue = true;
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }

        public List<BatchProcessHistory> GetBatchProcessHistory()
        {
            var returnValue = new List<BatchProcessHistory>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_BATCH_PROCESS_HISTORY", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchProcessHistory(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<BatchProcess> GetBatchProcess(int bp_id)
        {
            var returnValue = new List<BatchProcess>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_BATCH_PROCESS_byId", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("bp_id", bp_id));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchProcess(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<BatchProcessJobs> GetBatchProcessJobs()
        {
            var returnValue = new List<BatchProcessJobs>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_BATCH_PROCESS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchProcessJobs(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<BatchProcessFields> GetBatchProcessFields(int bp_id)
        {
            var returnValue = new List<BatchProcessFields>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_BATCH_PROCESS_Fields", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("bp_id", bp_id));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchProcessFields(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<BatchRunHistory> GetBatchRunHistory()
        {
            var returnValue = new List<BatchRunHistory>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_BATCH_RUNS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchRunHistory(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<BatchProcessResult> GetBatchProcessResults(int batchProcessId)
        {
            var returnValue = new List<BatchProcessResult>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_BATCH_RESULTS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("BatchId", batchProcessId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchProcessResult(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public BatchProcessParentHeader GetBatchProcessParentHeader(int batchRunId)
        {
            var returnValue = new BatchProcessParentHeader();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_BATCH_GET_PARENT_HEADER", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("BatchRunId", batchRunId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue = new BatchProcessParentHeader(dataReader);
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public BatchProcessParentFields GetBatchProcessParentFields(int batchRunId)
        {
            var returnValue = new BatchProcessParentFields();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_BATCH_GET_PARENT_FIELDS", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("BatchRunId", batchRunId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue = new BatchProcessParentFields(dataReader);
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<BatchProcessFieldsFromRun> GetBatchProcessFieldsFromRun(int batchid)
        {
            var returnValue = new List<BatchProcessFieldsFromRun>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_BATCH_PROCESS_Fields_FromRun", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("BatchID", batchid));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchProcessFieldsFromRun(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public int SaveBatchJob(int batchId, string parameterString, int userId)
        {
            int returnValue = 0;

            if (parameterString.EndsWith("=="))
            {
                byte[] data = Convert.FromBase64String(parameterString);
                parameterString = Encoding.UTF8.GetString(data);
            }

            BatchProcess batchProcess = GetBatchProcess(batchId)[0];
            List<BatchProcessFields> processFields = GetBatchProcessFields(batchId);

            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand(batchProcess.Procedure, sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var field in processFields)
                    {
                        if (field.IsSystem)
                        {
                            if (field.FieldName.ToLower() == "userid")
                            {
                                sqlCommand.Parameters.Add(new SqlParameter(field.FieldName, userId));
                            }
                            if (field.FieldName.ToLower() == "ProcessID".ToLower())
                            {
                                sqlCommand.Parameters.Add(new SqlParameter(field.FieldName, batchId));
                            }
                        }
                        else
                        {
                            var suppliedValue = getStringValue(parameterString, field.bf_id);

                            if (suppliedValue.Length == 0 && field.IsMandatory)
                            {
                                throw new Exception("Required value was not supplied");
                            }

                            if (suppliedValue.Length > 0)
                            {
                                var discovered = false;

                                if (field.DataType.ToLower() == "datetime")
                                {
                                    DateTime dateValue = new DateTime();
                                    if (DateTime.TryParse(suppliedValue, out dateValue))
                                    {
                                        sqlCommand.Parameters.Add(new SqlParameter(field.FieldName, dateValue));
                                        discovered = true;
                                    }
                                }
                                if (field.DataType.ToLower() == "boolean")
                                {
                                    Boolean boolValue = false;
                                    if (Boolean.TryParse(suppliedValue, out boolValue))
                                    {
                                        sqlCommand.Parameters.Add(new SqlParameter(field.FieldName, boolValue));
                                        discovered = true;
                                    }
                                }
                                if (field.DataType.ToLower() == "currency")
                                {
                                    Decimal decimalValue = 0;
                                    if (Decimal.TryParse(suppliedValue, out decimalValue))
                                    {
                                        sqlCommand.Parameters.Add(new SqlParameter(field.FieldName, decimalValue));
                                        discovered = true;
                                    }
                                }
                                if (field.DataType.ToLower() == "int")
                                {
                                    Int32 intValue = 0;
                                    if (Int32.TryParse(suppliedValue, out intValue))
                                    {
                                        sqlCommand.Parameters.Add(new SqlParameter(field.FieldName, intValue));
                                        discovered = true;
                                    }
                                }
                                if (field.DataType.ToLower() == "string" || field.DataType.ToLower() == "textarea" || field.DataType.ToLower() == "multiselect")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter(field.FieldName, suppliedValue));
                                    discovered = true;
                                }
                                if (!discovered) { throw new Exception("DataType Could Not Be Translated"); }
                            }
                        }
                    }
                    returnValue = (int)sqlCommand.ExecuteScalar();
                    //if (returnedId > 0)
                    //{
                    //    returnValue = getBatchRecords(returnedId);
                    //}
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<BatchRecords> GetBatchRunRecords(int batchRunId)
        {
            List<BatchRecords> returnValue = new List<BatchRecords>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_BATCH_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("BatchId", batchRunId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new BatchRecords(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public bool DeactivateBatch(int batchRunId)
        {
            bool returnValue = false;
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_BATCH_RUNS_DEACTIVATE", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("BatchId", batchRunId));

                var count = sqlCommand.ExecuteNonQuery();
                if (count > 0) { returnValue = true; }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public bool ActivateBatch(int batchRunId, string batchName)
        {
            bool returnValue = false;
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("[CH_BATCH_RUNS_ACTIVATE]", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@B_ID", batchRunId));
                sqlCommand.Parameters.Add(new SqlParameter("BatchName", batchName));

                var count = sqlCommand.ExecuteNonQuery();
                if (count > 0) { returnValue = true; }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public string GetBatchName(int batchId)
        {
            string returnValue = String.Empty;
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("[CH_BATCH_RUNS_GETNAME]", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("BatchId", batchId));
                sqlCommand.Parameters.Add(new SqlParameter("BatchName", SqlDbType.NVarChar, 200));
                sqlCommand.Parameters["BatchName"].Direction = ParameterDirection.Output;

                sqlCommand.ExecuteScalar();
                returnValue = sqlCommand.Parameters["BatchName"].Value.ToString();
            }

            sqlDataConnection.Close();

            return returnValue;
        }

        #endregion

        #region DOCUMENT TEMPLATES

        public List<DocumentTemplates> GetDocumentTemplates(int userId)
        {
            var returnValue = new List<DocumentTemplates>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TEMPLATES_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("UserID", userId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new DocumentTemplates(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public int CreateNewDocumentTemplate(int userId, string documentName, string viewName)
        {
            var returnValue = -1;
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TEMPLATES_ADD", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));
                sqlCommand.Parameters.Add(new SqlParameter("Name", documentName));
                sqlCommand.Parameters.Add(new SqlParameter("viewName", viewName));

                SqlParameter returnParameter = sqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                var dataReader = sqlCommand.ExecuteScalar();

                //sqlCommand.ExecuteNonQuery();

                returnValue = (int)returnParameter.Value;
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public DocumentTemplate GetDocumentTemplate(int templateId)
        {
            var returnValue = new DocumentTemplate();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TEMPLATES_SELECT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CHT_iD", templateId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue = new DocumentTemplate(dataReader);
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public bool SaveTemplateContent(int chtId, int userId, string content, string notes)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(CONNECTION_STRING))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_TEMPLATES_UPDATE", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("CHT_ID", chtId));
                    sqlCommand.Parameters.Add(new SqlParameter("Content", content));
                    sqlCommand.Parameters.Add(new SqlParameter("UserID", userId));
                    sqlCommand.Parameters.Add(new SqlParameter("Notes", notes));
                    //sqlCommand.Parameters.Add(new SqlParameter("viewName", viewName));

                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) { returnvalue = true; }
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }
        
        #endregion

        #region MERGE FIELDS

        public List<MergeFieldItem> GetMergeFieldItems(string tableName)
        {
            var returnValue = new List<MergeFieldItem>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("SYS_FIELD_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("Tablename", tableName));
                sqlCommand.Parameters.Add(new SqlParameter("AZSequence", 1));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new MergeFieldItem(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<DataMergeSource> GetDataMergeOptions()
        {
            var returnValue = new List<DataMergeSource>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_CORRES_VIEWS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new DataMergeSource(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<DataMergeFields> GetDataMergeFields(string viewName)
        {
            var returnValue = new List<DataMergeFields>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("SYS_FIELD_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("tablename", viewName));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new DataMergeFields(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        #endregion
        public List<string> GetTreatmentGroups()
        {
            var returnValue = new List<string>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TREATMENT_GROUP_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(dataReader.GetString(0));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<TreatmentItems> GetTreatmentsForGroup(string groupName)
        {
            var returnValue = new List<TreatmentItems>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_Treatment_List", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("GroupName", groupName));
               
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add( new TreatmentItems(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<MergeValue> GetMergeValues(int userId, string viewName, string pin, string uprn)
        {
            var returnValue = new List<MergeValue>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TEMPLATES_MERGE_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("UserID", userId));
                sqlCommand.Parameters.Add(new SqlParameter("ViewName", viewName));
                sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));
                sqlCommand.Parameters.Add(new SqlParameter("uprn", uprn));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new MergeValue(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public bool SaveDocument(int userId, int templateId, string documentName, string documentContent, byte[] documentBody, int actionId, string pin, string uprn, int debtId)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TEMPLATES_ACTION_SAVE", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_TEMPLATEID",    templateId));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_TITLE",         documentName));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_CONTENT",       documentContent));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_ACTION",        actionId));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_PIN",           pin));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_UPRN",          uprn));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_DEBTID",        debtId));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_USERID",        userId));
                sqlCommand.Parameters.Add(new SqlParameter("CH_DOCUMENT_BODY",          documentBody));

                SqlParameter returnParameter = sqlCommand.Parameters.Add("RetVal",      SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                sqlCommand.ExecuteScalar();

                if ((int)returnParameter.Value > 0) { returnValue = true; }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
    }
}