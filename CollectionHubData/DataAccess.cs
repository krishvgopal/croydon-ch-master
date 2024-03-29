﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CollectionHubData
{
    public class DataAccess
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["CONNECTION_STRING_CONFIG"].ToString() ;
        }
        public bool RemoveMatch(int matchId)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("P_CYCLE_ACTION_INSERT", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId",    debtId));
                    sqlCommand.Parameters.Add(new SqlParameter("CycleId",   recoveryCycleId));
                    sqlCommand.Parameters.Add(new SqlParameter("UserId",    userId));
                    
                    var count = sqlCommand.ExecuteNonQuery();

                    if (count > 0) returnvalue = true;
                }
                sqlDataConnection.Close();
            }
            return returnvalue;
        }

        public bool SetDebtResponsibleUser(int debtId, int userId)
        {
            var returnvalue = false;
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("P_SET_DEBT_RESP_USER", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));
                    sqlCommand.Parameters.Add(new SqlParameter("RESPUSER", userId));

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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
        public bool CreateArrangement(  int agm_pin, int agm_cd_id, DateTime? agm_start_date, int agm_frequency, int agm_day_of_month, 
                                        int agm_day_of_week, decimal agm_start_amount, decimal agm_installment_amount, int agm_number_installment, 
                                        int agm_payment_method, decimal agm_agreed_amount, decimal agm_totaldebt_amount, decimal agm_last_amount, 
                                        int agm_Created_By, DateTime? agm_agreement_date, DateTime? agm_payment_date, DateTime? agm_starting_from_date)
        {
            var returnValue = false;
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_Arrangement_INSERT", sqlDataConnection))
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
                    
                    var count = sqlCommand.ExecuteNonQuery();
                    if (count > 0) { returnValue = true; }
                }
                sqlDataConnection.Close();
            }
            return returnValue;   			
        }

        #region DASHBOARD GRAPH PROCEDURES

        public string GetDashboardDataPercentByYear(int sourceId, int historic)
        {
            var returnValue = "";
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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

        //public List<DebtItem> GetNoteDebts(int noteId)
        //{
        //    var returnValue = new List<DebtItem>();
        //    var sqlDataConnection = new SqlConnection(GetConnectionString());

        //    sqlDataConnection.Open();
        //    using (var sqlCommand = new SqlCommand("CHP_NOTE_DEBT_LIST", sqlDataConnection)) //  // CHP_PERSON_NOTES_DATA
        //    {
        //        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //        sqlCommand.Parameters.Add(new SqlParameter("noteId", noteId));

        //        var dataReader = sqlCommand.ExecuteReader();

        //        if (dataReader.HasRows)
        //        {
        //            while (dataReader.Read())
        //            {
        //                var newResult = new DebtItem(dataReader);
        //                returnValue.Add(newResult);
        //            }
        //        }
        //    }

        //    sqlDataConnection.Close();

        //    return returnValue;
        //}

        //  CHP_NOTE_DEBT_LIST
        //public List<DebtNote>           GetDebtNotes(int debtId)
        //{
        //    var returnValue = new List<DebtNote>();
        //    using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
        //    {
        //        sqlDataConnection.Open();
        //        using (var sqlCommand = new SqlCommand("CH_DEBT_NOTES_LIST", sqlDataConnection))
        //        {
        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //            sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));

        //            var dataReader = sqlCommand.ExecuteReader();

        //            if (dataReader.HasRows)
        //            {
        //                while (dataReader.Read())
        //                {
        //                    returnValue.Add(new DebtNote(dataReader));
        //                }
        //            }
        //        }
        //        sqlDataConnection.Close();
        //    }
        //    return returnValue;
        //}
        
        public List<ArrangementPaymentMethods>          GetPaymenyMethodList()
        {
            var returnValue = new List<ArrangementPaymentMethods>();
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
        public List<FullNameFullAddressSearchResults> SearchAddress(string organisationName, string firstName, string lastName, string nino, string dob, string address, string street, string postcode, bool currentAddressOnly, string streamSource)
        {
            var returnValue = new List<FullNameFullAddressSearchResults>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("FULLNAME_FULLADDRESS_SEARCH", sqlDataConnection))
            {
                var searchCurrent = currentAddressOnly ? "YES" : "NO";

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("OrgName", organisationName));
                sqlCommand.Parameters.Add(new SqlParameter("FIRSTNAME", firstName));
                sqlCommand.Parameters.Add(new SqlParameter("LASTNAME", lastName));
                sqlCommand.Parameters.Add(new SqlParameter("DOB", dob));
                sqlCommand.Parameters.Add(new SqlParameter("NINO", nino));
                sqlCommand.Parameters.Add(new SqlParameter("ADDRNUMBER", address));
                sqlCommand.Parameters.Add(new SqlParameter("ADDRNAME", street));
                sqlCommand.Parameters.Add(new SqlParameter("ADDRPOSTCODE", postcode));
                sqlCommand.Parameters.Add(new SqlParameter("SOURCECODE", streamSource));
                sqlCommand.Parameters.Add(new SqlParameter("CURRENT", searchCurrent));
                //sqlCommand.Parameters.Add(new SqlParameter("MIDNAME", DBNull.Value));
                //sqlCommand.Parameters.Add(new SqlParameter("ORGNAME", DBNull.Value));

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONS_LIST", sqlDataConnection))
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
        public List<RecoveryCycleItem>  GetRecoveryCycleHistory(int debtId, int statusId)
        {
            var returnValue = new List<RecoveryCycleItem>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));
                sqlCommand.Parameters.Add(new SqlParameter("Status", statusId));

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
        public List<RecoveryCycleItem>  GetRecoveryCycleHistory(int debtId, int statusId, int nextStep)
        {
            var returnValue = new List<RecoveryCycleItem>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));
                sqlCommand.Parameters.Add(new SqlParameter("Status", statusId));
                sqlCommand.Parameters.Add(new SqlParameter("NextAction", nextStep));

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
        public List<TreatmentCycle>     GetTreatmentCycles(int debtId)
        {
            var returnValue = new List<TreatmentCycle>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_CYCLES_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("DebtID", debtId));
                
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new TreatmentCycle(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<DebtItem>           GetFrequencyListGetDebts(int pin)
        {
            var returnValue = new List<DebtItem>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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

        // 
        public List<PersonContactNote> GetPersonContactNotes(int pin)
        {
            var returnValue = new List<PersonContactNote>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_PERSON_NOTES_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("pin", pin));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var newResult = new PersonContactNote(dataReader);
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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

        public List<Payment>            GetPaymentsByDebtId(int debtId, string source, string sourceAccReference)
        {
            var returnValue = new List<Payment>();
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_PAYMENTS_byDebtId", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));
                    sqlCommand.Parameters.Add(new SqlParameter("Source", source));
                    sqlCommand.Parameters.Add(new SqlParameter("SourceAccRef", sourceAccReference));

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
        public List<Payment>            GetPaymentsByPin(string pin)
        {
            var returnValue = new List<Payment>();
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_PAYMENTS_byPin", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("PartyPIN", pin));
                    
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
        public List<DebtParties>        GetPartiesByPin(string pin)
        {
            var returnValue = new List<DebtParties>();
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_PARTIES_ByPIN", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));

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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CH_ADDRESS_LIST", sqlDataConnection))
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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
        public List<Arrangement>        GetArrangementsByPin(string pin)
        {
            var returnValue = new List<Arrangement>();
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
            {
                sqlDataConnection.Open();
                using (var sqlCommand = new SqlCommand("CHP_ARRANGEMENTS_BYPIN", sqlDataConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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

        public PersonHeaderRecord GetPersonHeader(string pin, string uprn)
        {
            var returnValue         = new PersonHeaderRecord();
            var sqlDataConnection   = new SqlConnection(GetConnectionString());

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
                        var newResult = new PersonHeaderRecord(dataReader);
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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public UserData AuthenticateUser(string windowsId)
        {
            UserData returnValue = null;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_AUTHENTICATE_WINDOWSID", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("windowsId", windowsId));

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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public List<BatchProcess>       GetBatchProcess(int bp_id)
        {
            var returnValue = new List<BatchProcess>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public List<BatchProcessJobs>   GetBatchProcessJobs()
        {
            var returnValue = new List<BatchProcessJobs>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public List<BatchRunHistory>    GetBatchRunHistory()
        {
            var returnValue = new List<BatchRunHistory>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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

            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
                }
                sqlDataConnection.Close();
            }
            return returnValue;
        }
        public List<BatchRecords> GetBatchRunRecords(int batchRunId)
        {
            List<BatchRecords> returnValue = new List<BatchRecords>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public bool     DeactivateBatch(int batchRunId)
        {
            bool returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public bool     ActivateBatch(int batchRunId, string batchName)
        {
            bool returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public string   GetBatchName(int batchId)
        {
            string returnValue = String.Empty;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public int CreateNewDocumentTemplate(int userId, string documentName, string viewName, int debtTypeId)
        {
            var returnValue = -1;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TEMPLATES_ADD", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));
                sqlCommand.Parameters.Add(new SqlParameter("Name", documentName));
                sqlCommand.Parameters.Add(new SqlParameter("viewName", viewName));
                sqlCommand.Parameters.Add(new SqlParameter("debtTypeId", debtTypeId));
                // 

                SqlParameter returnParameter = sqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                var dataReader = sqlCommand.ExecuteScalar();

                returnValue = (int)returnParameter.Value;
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public DocumentTemplate GetDocumentTemplate(int templateId)
        {
            var returnValue = new DocumentTemplate();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONTYPE2_ADD", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("ActionID", templateId));

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

        public DocumentTemplate GetDocumentTemplateByTemplateId(int templateId)
        {
            var returnValue = new DocumentTemplate();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            using (var sqlDataConnection = new SqlConnection(GetConnectionString()))
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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public List<DataMergeSource> GetDataMergeOptionsByDebtType(int debtTypeId)
        {
            var returnValue = new List<DataMergeSource>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_CORRES_VIEWS_LIST_BY_DEBT_TYPE", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CH_DEBT_TYPE_ID", debtTypeId));
                
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
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public List<string>             GetTreatmentGroups(int debtId)
        {
            var returnValue = new List<string>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONTYPE_GROUPS", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("debtId", debtId));

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
        public bool                     CreateAdHocItem(int userId, int actionItemId, int debtId)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTION_INSERT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("debtId", debtId));
                sqlCommand.Parameters.Add(new SqlParameter("userId", userId));
                sqlCommand.Parameters.Add(new SqlParameter("actionItemId", actionItemId));

                //SqlParameter returnParameter = sqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
                //returnParameter.Direction = ParameterDirection.ReturnValue;

                var countObject = sqlCommand.ExecuteScalar();
                var count = 0;

                if (countObject != null) { count = (int) countObject; }
                if (count > 0) { returnValue = true; }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public bool                     SetPersonAttributeStatus(int userId, int statusId, int personAttributeId)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_PERSON_ATTRIBUTE_UPDATE", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("PersonAttributeId", personAttributeId));
                sqlCommand.Parameters.Add(new SqlParameter("IsCurrentId", statusId));
                sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));

                var count = sqlCommand.ExecuteNonQuery();
                
                if (count > 0) { returnValue = true; }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<AttributeCurrentStatusType> GetAttributesCurrentStatuses()
        {
            var returnValue = new List<AttributeCurrentStatusType>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_ATTRIBUTES_STATUS_TYPES_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new AttributeCurrentStatusType(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<TreatmentActionItems>       GetTreatmentsForGroup(string groupName, int debtId)
        {
            var returnValue = new List<TreatmentActionItems>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONTYPE_LIST_BY_GROUP", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("GroupName", groupName));
                sqlCommand.Parameters.Add(new SqlParameter("DebtId", debtId));
               
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new TreatmentActionItems(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public List<MergeValue>         GetMergeValues(int userId, string viewName, string pin, string uprn, int debtId)
        {
            var returnValue = new List<MergeValue>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CH_TEMPLATES_MERGE_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("UserID", userId));
                sqlCommand.Parameters.Add(new SqlParameter("ViewName", viewName));
                sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));
                sqlCommand.Parameters.Add(new SqlParameter("uprn", uprn));
                sqlCommand.Parameters.Add(new SqlParameter("debtid", debtId));
                
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
        public List<ActionStatus>       GetActionStatuses()
        {
            var returnValue = new List<ActionStatus>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTION_STATUS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new ActionStatus(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public bool                     SaveDocument(int userId, int templateId, string documentName, string documentContent, byte[] documentBody, int actionId, string pin, string uprn, int debtId)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

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
        public List<DebtStream>         GetDebtStreams()
        {
            var returnValue = new List<DebtStream>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_GET_DEBT_STREAMS", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new DebtStream(dataReader));
                    }
                }
            }
            sqlDataConnection.Close();
            return returnValue;
        }
        public bool                     SaveDebtAction(int userId, int actionId, string content, byte[] documentBody, string pin, string uprn)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTION_2_INSERT_UPDATE", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("CONTENT_TEXT", content));
                sqlCommand.Parameters.Add(new SqlParameter("CONTENT", documentBody));
                sqlCommand.Parameters.Add(new SqlParameter("ACTIONID", actionId));
                sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));
                sqlCommand.Parameters.Add(new SqlParameter("UPRN", uprn));
                sqlCommand.Parameters.Add(new SqlParameter("USERID", userId));

                var returnParameter = sqlCommand.ExecuteScalar();
                returnValue = true;  
            }
            sqlDataConnection.Close();

            return returnValue;
        }
        public bool                     CompleteDebtAction(int actionId, int outputType)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONTYPE2_OUTPUT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("ACTIONID", actionId));
                sqlCommand.Parameters.Add(new SqlParameter("OUTPUT", outputType));

                var returnParameter = sqlCommand.ExecuteScalar();
                returnValue = true;

            }
            sqlDataConnection.Close();

            return returnValue;
        }
        public CorrespondenceItem       GetActionCorrespondenceItem(int actionId)
        {
            CorrespondenceItem returnValue = null;
            var sqlDataConnection   = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_ACTIONTYPE2_VIEW_EDIT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("ACTIONID", actionId));

                var dataReader = sqlCommand.ExecuteReader(); //CommandBehavior.SequentialAccess
                
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue = new CorrespondenceItem(dataReader);
                    }
                }
            }
            sqlDataConnection.Close();

            return returnValue;
        }
        public int                      CreateDebtorNote(int userId, int pin, int uprn)
        {
            int returnValue = 0;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_PERSON_NOTES_INSERT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("USERID", userId));
                sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));
                sqlCommand.Parameters.Add(new SqlParameter("UPRN", uprn));
                //sqlCommand.Parameters.Add(new SqlParameter("DebtID", debtId));

                SqlParameter returnParameter = sqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                var callReturn = sqlCommand.ExecuteScalar();

                returnValue = (int)returnParameter.Value;
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public DebtorNote               GetDebtorNote(int noteId)
        {
            var returnValue = new DebtorNote();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_PERSON_NOTES_DATA", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("NoteID", noteId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var newResult = new DebtorNote(dataReader);
                        returnValue = newResult;
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        } 
        public List<TreatmentCycle>     GetTreatmentCyclesList()
        {
            var returnValue = new List<TreatmentCycle>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("ch_treatment_cycles_list", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new TreatmentCycle(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<DebtorNoteCategory> GetDebtNoteCategories()
        {
            var returnValue = new List<DebtorNoteCategory>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("LOOKUP_NOTE_CATEGORY", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var newResult = new DebtorNoteCategory(dataReader);
                        returnValue.Add(newResult);
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public bool                     SaveDebtorNote(int noteId, int userId, int pin, string theirRef, string reason, string content, string newLandLine, string newMobile, string newEmail)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_PERSON_NOTES_UPDATE", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("NoteID"     , noteId));
                sqlCommand.Parameters.Add(new SqlParameter("UserID"     , userId));
                sqlCommand.Parameters.Add(new SqlParameter("PIN"        , pin));
                //sqlCommand.Parameters.Add(new SqlParameter("DebtID"     , debtId));
                //sqlCommand.Parameters.Add(new SqlParameter("Category"   , categoryId));
                sqlCommand.Parameters.Add(new SqlParameter("TheirRef"   , theirRef));
                sqlCommand.Parameters.Add(new SqlParameter("Reason"     , reason));
                sqlCommand.Parameters.Add(new SqlParameter("Content"    , content));
                sqlCommand.Parameters.Add(new SqlParameter("NewFixed"   , newLandLine));
                sqlCommand.Parameters.Add(new SqlParameter("NewMobile"  , newMobile));
                sqlCommand.Parameters.Add(new SqlParameter("NewEMail"   , newEmail));

                var rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected>0)
                {
                    returnValue = true;
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<UserData>           GetSystemUsers(bool showInvalid)
        {
            List<UserData> returnValue = new List<UserData>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_GET_SYSTEM_USERS", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("ShowNonValid", showInvalid));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add( new UserData(dataReader) );
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<SearchWorkResult>   SearchWorkCases(string sourceList, int? amountFrom, int? amountTo, int? cycleId, int? daysSinceIssued)
        {
            var returnValue         = new List<SearchWorkResult>();
            var sqlDataConnection   = new SqlConnection(GetConnectionString());

            if (cycleId == 0) cycleId = null;
            
            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_DEBT_SEARCH", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("cd_source", sourceList));
                sqlCommand.Parameters.Add(new SqlParameter("amount_from", amountFrom));
                sqlCommand.Parameters.Add(new SqlParameter("amount_to", amountTo));
                sqlCommand.Parameters.Add(new SqlParameter("CycleId", cycleId));
                sqlCommand.Parameters.Add(new SqlParameter("DaysSinceDebt", daysSinceIssued));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new SearchWorkResult(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<KeyValuePair<int, string>> GetAutomaticOutstandingGroups(int userId)
        {
            var returnValue = new List<KeyValuePair<int, string>>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_GET_AUTOMATIC_OUTSTANDING_GROUPS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new KeyValuePair<int, string>(Convert.ToInt32(dataReader["AG_ID"]), dataReader["AG_NAME"].ToString()));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<AutomaticTrayItems> GetAutomaticOutstandingItems(int userId, int corresType, int processCode)
        {
            var returnValue         = new List<AutomaticTrayItems>();
            var sqlDataConnection   = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_GET_AUTOMATIC_OUTSTANDING_ITEMS_LIST", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("UserId", userId));
                sqlCommand.Parameters.Add(new SqlParameter("CorresType", corresType));
                sqlCommand.Parameters.Add(new SqlParameter("ProcessCode", processCode));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new AutomaticTrayItems(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<KeyValuePair<int, string>> GetDebtTypes()
        {
            var returnValue = new List<KeyValuePair<int, string>>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_GET_DEBT_TYPES", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new KeyValuePair<int, string>(Convert.ToInt32(dataReader["LK_CODE"]), dataReader["LK_VALUE"].ToString()));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public KeyValuePair<int, string> GetTemplateForActionItem(int actionItemId)
        {
            var returnValue = new KeyValuePair<int, string>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_GET_CONTENT_FOR_ACTION_ITEM", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("AID", actionItemId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue= new KeyValuePair<int, string>(Convert.ToInt32(dataReader["A_ITEM_ID"]), dataReader["CHT_CONTENT"].ToString());
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public KeyValuePair<int, string> GetViewForActionItem(int actionItemId)
        {
            var returnValue = new KeyValuePair<int, string>();
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("P_GET_VIEW_FOR_ACTION_ITEM", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("AID", actionItemId));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue = new KeyValuePair<int, string>(Convert.ToInt32(dataReader["CHT_DEBT_TYPE_ID"]), dataReader["LK_OBJECTNAME"].ToString());
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public List<UprnAddress> SearchAddresses(string flatNumber, string building, string house, string street, string place, string postcode)
        {
            var returnValue         = new List<UprnAddress>();
            var sqlDataConnection   = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("PERSON_ATTRIBUTE_ADDRESS_SEARCH", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("FlatNum",   flatNumber));
                sqlCommand.Parameters.Add(new SqlParameter("Building",  building));
                sqlCommand.Parameters.Add(new SqlParameter("HouseNum",  house));
                sqlCommand.Parameters.Add(new SqlParameter("Street",    street));
                sqlCommand.Parameters.Add(new SqlParameter("Place",     place));
                sqlCommand.Parameters.Add(new SqlParameter("PostCode",  postcode));

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnValue.Add(new UprnAddress(dataReader));
                    }
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public bool SetAddress(int pin, int uprn, int userId)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("PERSON_ATTRIBUTE_ADDRESS_SELECT_INSERT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));
                sqlCommand.Parameters.Add(new SqlParameter("UPRN", uprn));
                sqlCommand.Parameters.Add(new SqlParameter("USERID", userId));

                var rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    returnValue = true;
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
        public bool SaveNewAddress(int pin, int userId, string careOf, string description, string flatNo, string houseNo, string building, string streetName, string placeName, string postcode)
        {
            var returnValue = false;
            var sqlDataConnection = new SqlConnection(GetConnectionString());

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("PERSON_ATTRIBUTE_NEW_ADDRESS_INSERT", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("PIN", pin));
                sqlCommand.Parameters.Add(new SqlParameter("USERID", userId));
                sqlCommand.Parameters.Add(new SqlParameter("CareOf", careOf));
                sqlCommand.Parameters.Add(new SqlParameter("AddrInfo", description));
                sqlCommand.Parameters.Add(new SqlParameter("FlatNum", flatNo));
                sqlCommand.Parameters.Add(new SqlParameter("Building", building));
                sqlCommand.Parameters.Add(new SqlParameter("HouseNum", houseNo));
                sqlCommand.Parameters.Add(new SqlParameter("Street", streetName));
                sqlCommand.Parameters.Add(new SqlParameter("Place", placeName));
                sqlCommand.Parameters.Add(new SqlParameter("PostCode", postcode));

                var rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    returnValue = true;
                }
            }

            sqlDataConnection.Close();

            return returnValue;
        }
    }
}