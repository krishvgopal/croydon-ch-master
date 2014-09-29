using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CollectionHubData
{
    public class DataAccess
    {
        //private const string CONNECTION_STRING = "Data Source=192.168.1.19;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=sa;Password=Croydon#";
        private const string CONNECTION_STRING = "Data Source=192.168.1.17;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=HubAdmin;Password=Croydon#";
        //private const string CONNECTION_STRING = "Data Source=192.168.2.160;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=sa;Password=Croydon#";
        //private const string CONNECTION_STRING = "Data Source=192.168.1.66;Initial Catalog=COLHUBCOPY;Persist Security Info=True;User ID=sa;Password=Croydon#";

        public DebtAddress GetAddressForDebt(string sourceRef, string source)
        {
            var returnValue = new DebtAddress();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_GetNameAddress", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("source_ref", sourceRef));
                sqlCommand.Parameters.Add(new SqlParameter("source", source));

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
        public List<DebtItem> GetDebts(string sourceRef, string source)
        {
            var returnValue = new List<DebtItem>();
            var sqlDataConnection = new SqlConnection(CONNECTION_STRING);

            sqlDataConnection.Open();
            using (var sqlCommand = new SqlCommand("CHP_GETPERSONDEBTS", sqlDataConnection))
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("source_ref", sourceRef));
                sqlCommand.Parameters.Add(new SqlParameter("source", source));

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
                            returnValue.RecordCount = Convert.ToInt32(dataReader[0].ToString());
                            returnValue.TotalValue = Convert.ToDecimal(dataReader[1].ToString());
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


    }
}