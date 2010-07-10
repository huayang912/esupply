using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.Data;
using System.Data.SqlClient;

namespace Dndp.Persistence.Dao
{
    //this is the delegate for sql helper
    public class SqlHelperDao
    {
        private String connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        private int commitRecordCount;
        public int CommitRecordCount
        {
            get
            {
                return commitRecordCount;
            }
            set
            {
                this.commitRecordCount = value;
            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            int executeRecord = 0;
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();

                //start a transaction
                transaction = connection.BeginTransaction();

                int startPosition = 0;
                string currExecuteCommand = null;
                int position = 0;
                while (startPosition < commandText.Length)
                {
                    position = GetPostion(commandText, startPosition);
                    if (position != -1)
                    {
                        currExecuteCommand = commandText.Substring(startPosition, position - startPosition + 1);
                        startPosition = position + 1;
                    }
                    else
                    {
                        currExecuteCommand = commandText.Substring(startPosition);
                        startPosition = commandText.Length;
                    }

                    executeRecord += SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, currExecuteCommand);
                }

                transaction.Commit();
                return executeRecord;
            }
            catch (SqlException ex)
            {
                 if (transaction != null) 
                {
                    transaction.Rollback();
                }

                throw ex;
            }
            finally
            {               
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public DataSet ExecuteDataset(string commandText)
        {
            return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
        }

        private int GetPostion(string commandText, int startPosition)
        {
            int position = 0;
            for (int i = 0; i < commitRecordCount && position != -1; i++)
            {
                position = commandText.IndexOf(';', startPosition);
                startPosition = position + 1;
            }

            return position;
        }
    }
}
