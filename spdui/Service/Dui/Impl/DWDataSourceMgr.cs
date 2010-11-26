using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Dui;
using Dndp.Persistence.Dao.Dui;
using Dndp.Utility;
using Dndp.Utility.CSV;
using System.Data.SqlClient;
using System.Data;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Dao.Security;
using Dndp.Persistence.Entity.Security;
using System.IO;
using System.Web;
//TODO: Add other using statements here.

namespace Dndp.Service.Dui.Impl
{
    [Transactional]
    public class DWDataSourceMgr : SessionBase, IDWDataSourceMgr
    {
        private IUserDao userDao;
        private IDWDataSourceDao DWDataSourceDao;
        private IDWDataSourceOperatorDao DWDataSourceOperatorDao;
        private IDWDataSourceMergeRuleDao DWDataSourceMergeRuleDao;
        private SqlHelperDao sqlHelperDao;

        public DWDataSourceMgr(IDWDataSourceDao DWDataSourceDao,
            IDWDataSourceOperatorDao DWDataSourceOperatorDao,
            IDWDataSourceMergeRuleDao DWDataSourceMergeRuleDao,
            SqlHelperDao sqlHelperDao,
            IUserDao userDao)
        {
            this.DWDataSourceDao = DWDataSourceDao;
            this.DWDataSourceOperatorDao = DWDataSourceOperatorDao;
            this.DWDataSourceMergeRuleDao = DWDataSourceMergeRuleDao;
            this.sqlHelperDao = sqlHelperDao;
            this.userDao = userDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateDWDataSource(DWDataSource entity)
        {
            //TODO: Add other code here.

            DWDataSourceDao.CreateDWDataSource(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DWDataSource LoadDWDataSource(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            DWDataSource ds = DWDataSourceDao.LoadDWDataSource(id);
            if (ds != null)
            {
                ds.DWDataSourceOperatorList = DWDataSourceOperatorDao.FindAllByDWDataSourceId(id);
                ds.DWDataSourceMergeRuleList = DWDataSourceMergeRuleDao.FindAllByDWDataSourceId(id);
            }

            return ds;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDWDataSource(DWDataSource entity)
        {
            //TODO: Add other code here.
            DWDataSourceDao.UpdateDWDataSource(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSource(int id)
        {
            DWDataSource ds = DWDataSourceDao.LoadDWDataSource(id);
            DWDataSourceOperatorDao.DeleteDWDataSourceOperatorByDSId(id);
            DWDataSourceDao.DeleteDWDataSource(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSource(DWDataSource entity)
        {
            DeleteDWDataSource(entity.Id);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSource(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            //delete temp table and history table
            foreach (int id in idList)
            {
                DeleteDWDataSource(id);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSource(IList<DWDataSource> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            //delete temp table and history table
            foreach (DWDataSource entity in entityList)
            {
                DeleteDWDataSource(entity);
            }
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public IList LoadAllDWDataSource()
        {
            return DWDataSourceDao.LoadAllDWDataSource();
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSourceOperator(IList<int> DWDataSourceOperatorIdList)
        {
            if ((DWDataSourceOperatorIdList == null) || (DWDataSourceOperatorIdList.Count == 0))
            {
                return;
            }

            DWDataSourceOperatorDao.DeleteDWDataSourceOperator(DWDataSourceOperatorIdList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDWDataSourceOperatorByDWDataSourceId(int dsId)
        {
            return DWDataSourceOperatorDao.FindAllByDWDataSourceId(dsId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DWDataSourceOperator> FindDWOperatorByDSIdAndAllowType(int dsId, string type)
        {
            return DWDataSourceOperatorDao.FindAllByDWDataSourceIdAndAllowType(dsId, type);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateDWDataSourceOperator(IList<int> userIdList, int dsId, string allowType)
        {
            //delete original operator
            IList<DWDataSourceOperator> dsOperatorList = FindDWOperatorByDSIdAndAllowType(dsId, allowType);

            if (dsOperatorList != null && dsOperatorList.Count > 0)
            {
                IList<int> dsOperatorIdList = new List<int>();
                foreach (DWDataSourceOperator dso in dsOperatorList)
                {
                    dsOperatorIdList.Add(dso.Id);
                }

                this.DeleteDWDataSourceOperator(dsOperatorIdList);
            }

            //update new operator
            DWDataSource ds = DWDataSourceDao.LoadDWDataSource(dsId);
            if (userIdList != null && userIdList.Count > 0)
            {
                foreach (int userId in userIdList)
                {
                    User user = userDao.LoadUser(userId);
                    DWDataSourceOperator dso = new DWDataSourceOperator();
                    dso.AllowType = allowType;
                    dso.TheDWDataSource = ds;
                    dso.TheUser = user;

                    DWDataSourceOperatorDao.CreateDWDataSourceOperator(dso);
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<User> FindUserByRole(int roleId)
        {
            return userDao.FindUserByRole(roleId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DWDataSource> FindDWDataSource(int userId)
        {
            return DWDataSourceDao.FindDWDataSource(userId) as IList<DWDataSource>;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DWDataSource> FindDWDataSourceByAllowType(int userId, string AllowType, string strType, string strDSName)
        {
            IList<DWDataSource> dwDataSourceList = DWDataSourceDao.FindDWDataSourceByAllowType(userId, AllowType) as IList<DWDataSource>;
            List<DWDataSource> FoundResult = new List<DWDataSource>();
            if (dwDataSourceList != null)
            {
                foreach (DWDataSource dwDataSource in dwDataSourceList)
                {
                    if ((!AllowType.Equals("ADMIN") || (dwDataSource.DeleteQuerySQL != null && dwDataSource.DeleteQuerySQL.Trim().Length > 0)))
                    {
                        if (strType.Equals("") || (dwDataSource.DSType.Equals(strType)))
                        {
                            if (strDSName.Equals("") 
                                || dwDataSource.Name.ToUpper().Contains(strDSName.ToUpper()) 
                                || dwDataSource.Description.ToUpper().Contains(strDSName.ToUpper()))
                            {
                                FoundResult.Add(dwDataSource);
                            }
                        }
                    }
                }
            }
            return FoundResult;
        }

        public IList<DWDataSource> FindDWDataSourceByTypeAndName(int userId, string strType, string strDSName)
        {
            IList<DWDataSource> FoundResult = DWDataSourceDao.FindDWDataSourceByTypeAndName(userId, strType, strDSName) as IList<DWDataSource>;
            return FoundResult;
        }

        public IList<DWDataSource> FindDWDataSourceByTypeAndName(string strType, string strDSName)
        {
            IList<DWDataSource> FoundResult = DWDataSourceDao.FindDWDataSourceByTypeAndName(strType, strDSName) as IList<DWDataSource>;
            return FoundResult;
        }


        public IList<DWDataSource> FindDWDataSourceByTypeAndName(string strType, string strDSName, User user, string allowType)
        {
            IList<DWDataSource> FoundResult = DWDataSourceDao.FindDWDataSourceByTypeAndName(strType, strDSName, user, allowType) as IList<DWDataSource>;
            return FoundResult;
        }

        public IList<string> FindDWDataSourceTypeList(int userId, string AllowType)
        {
            IList<string> dwDataSourceTypeList = DWDataSourceDao.FindDWDataSourceTypeList(userId, AllowType) as IList<string>;
            return dwDataSourceTypeList;
        }

        public IList<string> FindDWDataSourceTypeList(int userId)
        {
            IList<string> dwDataSourceTypeList = DWDataSourceDao.FindDWDataSourceTypeList(userId) as IList<string>;
            return dwDataSourceTypeList;
        }

        public IList<string> FindDWDataSourceTypeList()
        {
            IList<string> dwDataSourceTypeList = DWDataSourceDao.FindAllDWDataSourceTypeList() as IList<string>;
            return dwDataSourceTypeList;
        }

        public void DownloadQueryData(DWDataSource ds, HttpResponse response, string queryDate)
        {
            string rule = ds.QuerySQL.Replace("<$QueryDate$>", queryDate);

            SqlConnection cn = null;
            SqlDataReader sqlDataReader = null;
            TextWriter txtWriter = null;
            CSVWriter csvWriter = null;
            try
            {
                sqlDataReader = sqlHelperDao.ExecuteReader(rule, out cn);
                //DataTableReader dataReader = dataSet.CreateDataReader();

                //write csv header
                txtWriter = new StreamWriter(response.OutputStream, Encoding.GetEncoding("GB2312"));
                csvWriter = new CSVWriter(txtWriter);

                string[] header = new string[sqlDataReader.FieldCount];
                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    header[i] = sqlDataReader.GetName(i);
                }
                csvWriter.Write(header);
                csvWriter.WriteNewLine();

                //write csv content
                int count = 0;
                while (sqlDataReader.Read())
                {
                    count++;
                    object[] fields = new object[sqlDataReader.FieldCount];
                    sqlDataReader.GetValues(fields);
                    string[] strFields = new string[fields.Length];
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (fields[i] != null)
                        {
                            strFields[i] = fields[i].ToString();
                        }
                        else
                        {
                            strFields[i] = "";
                        }
                    }
                    csvWriter.Write(strFields);
                    csvWriter.WriteNewLine();

                    if (count % 10000 == 0)
                    {
                        //flush every 10000 records
                        //csvWriter.Flush();
                        txtWriter.Flush();
                        response.Flush();
                    }
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }

                if (cn != null)
                {
                    cn.Close();
                }

                //csvWriter.Flush();
                if (txtWriter != null)
                {
                    txtWriter.Flush();
                }

                if (response != null)
                {
                    response.Flush();
                    response.End();
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void DownloadQueryData(DWDataSource TheDWDataSource, string TheQueryDate, string condition, CSVWriter csvWriter)
        {
            DataSet ds = null;
            if (TheQueryDate.Trim().Length != 0)
            {
                ds = this.FindViewAllResult(TheDWDataSource, TheQueryDate);
            }
            else
            {
                ds = this.FindViewAllResult(TheDWDataSource);
            }
            DataTableReader dataReader = ds.CreateDataReader();
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = condition;

            //write csv header
            string[] header = new string[dataReader.FieldCount];
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                header[i] = dataReader.GetName(i);
            }
            csvWriter.Write(header);
            csvWriter.WriteNewLine();

            //write csv content
            foreach (DataRowView row in dv)
            {
                string[] strFields = new string[dataReader.FieldCount];
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (row[header[i]] != null)
                    {
                        strFields[i] = row[header[i]].ToString();
                    }
                    else
                    {
                        strFields[i] = "";
                    }
                }
               
                csvWriter.Write(strFields);
                csvWriter.WriteNewLine();
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void DownloadUpdateQueryData(DWDataSource ds, CSVWriter csvWriter)
        {
            string rule = ds.DeleteQuerySQL;

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();

            //write csv header
            string[] header = new string[dataReader.FieldCount];
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                header[i] = dataReader.GetName(i);
            }
            csvWriter.Write(header);
            csvWriter.WriteNewLine();

            //write csv content
            while (dataReader.Read())
            {
                object[] fields = new object[dataReader.FieldCount];
                dataReader.GetValues(fields);
                string[] strFields = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    if (fields[i] != null)
                    {
                        strFields[i] = fields[i].ToString();
                    }
                    else
                    {
                        strFields[i] = "";
                    }
                }
                csvWriter.Write(strFields);
                csvWriter.WriteNewLine();
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSet FindViewAllResult(DWDataSource ds)
        {
            string rule = ds.QuerySQL;

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            return dataSet;
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSet FindViewAllResult(DWDataSource ds, String QueryDate)
        {
            string rule = ds.QuerySQL.Replace("<$QueryDate$>", QueryDate);

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            return dataSet;
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSet FindViewUpdateResult(DWDataSource ds)
        {
            string rule = ds.DeleteQuerySQL.ToUpper();

            //if (strCondition.Trim().Length > 0)
            //{
            //    rule = "Select * From (" + rule + ") as Res where " + strCondition;
            //}
            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            return dataSet;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void DeleteDWDataSourceMergeRule(IList<int> DWDataSourceMergeRuleIdList)
        {
            this.DWDataSourceMergeRuleDao.DeleteDWDataSourceMergeRule(DWDataSourceMergeRuleIdList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DWDataSourceMergeRule LoadDWDataSourceMergeRule(int ruleId)
        {
            return this.DWDataSourceMergeRuleDao.LoadDWDataSourceMergeRule(ruleId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDWDataSourceMergeRuleByDWDataSourceId(int dwDataSourceId)
        {
            return this.DWDataSourceMergeRuleDao.FindAllByDWDataSourceId(dwDataSourceId);
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateDWDataSourceMergeRule(DWDataSourceMergeRule dwDataSourceMergeRule)
        {
            this.DWDataSourceMergeRuleDao.CreateDWDataSourceMergeRule(dwDataSourceMergeRule);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateDWDataSourceMergeRule(DWDataSourceMergeRule dwDataSourceMergeRule)
        {
            this.DWDataSourceMergeRuleDao.UpdateDWDataSourceMergeRule(dwDataSourceMergeRule);
        }

        //[Transaction(TransactionMode.Unspecified)]
        //public void DeleteSelectedResult(DWDataSource ds, int RowNo, string ActionSource, string ActionUser, string strCondition)
        //{
        //    string rule = ds.DeleteQuerySQL.ToUpper();

        //    if (strCondition.Trim().Length > 0)
        //    {
        //        rule = "Select * From (" + rule + ") as Res where " + strCondition;
        //    }

        //    DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
        //    // Use a DataTable object's DataColumnCollection.
        //    DataColumnCollection columns = dataSet.Tables[0].Columns;

        //    rule = ds.DeleteSQL.ToUpper();

        //    DataRow dataRow = dataSet.Tables[0].Rows[RowNo];
        //    foreach (DataColumn column in columns)
        //    {
        //        if (dataRow[column] == null)
        //        {
        //            rule = rule.Replace("<$" + column.ColumnName.ToUpper() + "$>", "");
        //        }
        //        else
        //        {
        //            rule = rule.Replace("<$" + column.ColumnName.ToUpper() + "$>", dataRow[column].ToString());
        //        }
        //    }
        //    sqlHelperDao.ExecuteNonQuery(rule);
        //    LogDBAction(rule, "DWUpdate", ActionSource, ActionUser);
        //}

        [Transaction(TransactionMode.Unspecified)]
        public void DeleteSelectedResult(DWDataSource ds, IList<KeyValuePair<string, string>> pkKeyValuePairList, User ActionUser)
        {
            string rule = ds.DeleteSQL.ToUpper();
            rule = rule.Replace("<$ACTIONUSER$>", ActionUser.Id.ToString());
            foreach (KeyValuePair<string, string> pkKeyValuePair in pkKeyValuePairList)
            {
                rule = rule.Replace("<$" + pkKeyValuePair.Key.ToUpper() + "$>", pkKeyValuePair.Value);
            }
            sqlHelperDao.ExecuteNonQuery(rule);
            LogDBAction(rule, "DWUpdate", ds.Name, ActionUser.UserName);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSet FindMergeRecords(int dwDataSourceId, string mergeFromRecordId, string mergeToRecordId)
        {
            DWDataSource ds = this.DWDataSourceDao.LoadDWDataSource(dwDataSourceId);
            string mergeFromQuerySql = ds.MergeQuerySQL.Replace("<$RecID$>", mergeFromRecordId);
            string mergeToQuerySql = ds.MergeQuerySQL.Replace("<$RecID$>", mergeToRecordId);

            DataSet mergedFromDS = sqlHelperDao.ExecuteDataset(mergeFromQuerySql);
            DataSet mergedToDS = sqlHelperDao.ExecuteDataset(mergeToQuerySql);
            mergedFromDS.Merge(mergedToDS);

            return mergedFromDS;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDWDataSourceMergeRuleByIds(string ruleIds)
        {
            return this.DWDataSourceMergeRuleDao.FindAllByDWDataSourceMergeRuleIds(ruleIds);
        }

        [Transaction(TransactionMode.Unspecified)]
        public string ValidateMergeRule(DWDataSourceMergeRule rule, string MergeFromId, string MergeToId, User actionUser)
        {
            string ruleContent = rule.RuleContent;            

            //Update Field Content in the SQL Rule
            ruleContent = UpdateValidationSQLContent(ruleContent, MergeFromId, MergeToId, null, actionUser);

            DataSet dataSet = sqlHelperDao.ExecuteDataset(ruleContent);
            DataTableReader dataReader = dataSet.CreateDataReader();
            int failRowCount = 0;
            while (dataReader.Read())
            {
                failRowCount++;
            }
            if (failRowCount > 0)
            {
                rule.Status = "Failed";
            }
            else
            {
                rule.Status = "Passed";
            }
            rule.FaildRowCount = failRowCount;
            rule.ValidationStatus = null;

            string result = rule.Id.ToString() + ":" + rule.Status;

            IList dependenceRuleList = this.DWDataSourceMergeRuleDao.FindAllByDependenceRuleId(rule.Id);

            if (dependenceRuleList != null)
            {
                foreach(DWDataSourceMergeRule dRule in dependenceRuleList) 
                {
                    result += ";" + dRule.Id.ToString() + ":" + rule.Status;
                }
            }
            return result;
        }

        public string MergeDWData(int DWDataSourceId, string MergeFromId, string MergeToId, string itemNewName, User actionUser)
        {
            DWDataSource ds = this.DWDataSourceDao.LoadDWDataSource(DWDataSourceId);
            string rule = ds.MergeSQL;

            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(rule, MergeFromId, MergeToId, itemNewName, actionUser);

            sqlHelperDao.ExecuteNonQueryWithNoTransaction(rule);
            
            rule = ds.MergeResultSQL;
            rule = UpdateValidationSQLContent(rule, MergeFromId, MergeToId, itemNewName, actionUser);
            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            if (dataSet.Tables != null && dataSet.Tables.Count > 0 
                && dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
            {
                object obj = dataSet.Tables[0].Rows[0][0];

                if (obj != null)
                {
                    return obj.ToString();
                }
            }

            return null;
        }

        public void DownloadMergeRuleErrorResult(DWDataSourceMergeRule mergeRule, string MergeFromId, string MergeToId, User actionUser, CSVWriter csvWriter)
        {
            string rule = mergeRule.ResultContent;
            rule = UpdateValidationSQLContent(rule, MergeFromId, MergeToId, null, actionUser);

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();

            //write csv header
            string[] header = new string[dataReader.FieldCount];
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                header[i] = dataReader.GetName(i);
            }
            csvWriter.Write(header);
            csvWriter.WriteNewLine();

            //write csv content
            while (dataReader.Read())
            {
                object[] fields = new object[dataReader.FieldCount];
                dataReader.GetValues(fields);
                string[] strFields = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    if (fields[i] != null)
                    {
                        strFields[i] = fields[i].ToString();
                    }
                    else
                    {
                        strFields[i] = "";
                    }
                }
                csvWriter.Write(strFields);
                csvWriter.WriteNewLine();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void CopyUserPermission(int fromUserId, IList<int> toUserId)
        {
            IList<DWDataSourceOperator> dsoList = this.DWDataSourceOperatorDao.FindAllByUserId(fromUserId);

            foreach (DWDataSourceOperator dso in dsoList)
            {
                IList<DWDataSourceOperator> list = this.DWDataSourceOperatorDao.FindAllByDWDataSourceIdAndAllowType(dso.TheDWDataSource.Id, dso.AllowType);

                foreach (int id in toUserId)
                {
                    if (fromUserId == id)
                    {
                        continue;
                    }

                    bool findMatch = false;
                    foreach (DWDataSourceOperator d in list)
                    {

                        if (d.TheUser.Id == id)
                        {
                            findMatch = true;
                            break;
                        }
                    }

                    if (!findMatch)
                    {
                        DWDataSourceOperator dwDataSourceOperator = new DWDataSourceOperator();
                        dwDataSourceOperator.AllowType = dso.AllowType;
                        dwDataSourceOperator.TheDWDataSource = dso.TheDWDataSource;
                        dwDataSourceOperator.TheUser = userDao.LoadUser(id);

                        this.DWDataSourceOperatorDao.CreateDWDataSourceOperator(dwDataSourceOperator);
                    }
                }
            }
        }

        private string UpdateValidationSQLContent(string rule, string MergeFromId, string MergeToId, string itemNewName, User actionUser)
        {
            rule = rule.Replace("<$MergeFromRecId$>", MergeFromId);
            rule = rule.Replace("<$MergeToRecId$>", MergeToId);
            if (itemNewName != null)
            {
                rule = rule.Replace("<$ItemNewName$>", itemNewName);
            }
            if (actionUser != null)
            {
                rule = rule.Replace("<$ActionUser$>", actionUser.Id.ToString());
            }
            return rule;
        }

        private void LogDBAction(string ActionSql, string ActionType, string ActionSource, string ActionUser)
        {
            string rule = "Insert into Sys_Action_Log (ActionCategory, ActionSource, ActionContent, Action_User_Name, ActionDate)";
            rule = rule + " Values('" + ActionType.Replace("'", "''") + "', ";
            rule = rule + " '" + ActionSource.Replace("'", "''") + "', ";
            rule = rule + " '" + ActionSql.Replace("'", "''") + "', ";
            rule = rule + " '" + ActionUser.Replace("'", "''") + "', GetDate())";
            sqlHelperDao.ExecuteNonQuery(rule);
        }
        #endregion Customized Methods
    }
}
