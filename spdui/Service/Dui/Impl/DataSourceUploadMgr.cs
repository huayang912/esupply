using System;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Dao.Dui;
using Castle.Services.Transaction;
using Dndp.Persistence.Entity.Dui;
using System.IO;
using Dndp.Utility.CSV;
using System.Collections;
using Dndp.Utility;
using Dndp.Persistence.Dao;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using Dndp.Persistence.Entity.Security;

namespace Dndp.Service.Dui.Impl
{
    [Transactional]
    public class DataSourceUploadMgr : SessionBase, IDataSourceUploadMgr
    {
        private IDataSourceCategoryDao dataSourceCategoryDao;
        private IDataSourceRuleDao dataSourceRuleDao;
        private IDataSourceUploadDao dataSourceUploadDao;
        private IDataSourceFieldDao dataSourceFieldDao;
        private IValidationResultDao validationResultDao;
        private IDataSourceWithDrawTableDao dataSourceWithDrawTableDao;
        private SqlHelperDao sqlHelperDao;
        private string DEFAULT_CSV_ENCODING = "GB2312";
        private string DEFAULT_DW_DBString = "SPDW.dbo.";
        private int DEFAULT_CSV_RECORD_PER_PARSE = 20;

        public DataSourceUploadMgr(IDataSourceCategoryDao dataSourceCategoryDao,
                                   IDataSourceRuleDao dataSourceRuleDao,
                                   IDataSourceUploadDao dataSourceUploadDao,
                                   IDataSourceFieldDao dataSourceFieldDao,
                                   IValidationResultDao validationResultDao,
                                   IDataSourceWithDrawTableDao dataSourceWithDrawTableDao,
                                   SqlHelperDao sqlHelperDao)
        {
            this.dataSourceCategoryDao = dataSourceCategoryDao;
            this.dataSourceRuleDao = dataSourceRuleDao;
            this.dataSourceUploadDao = dataSourceUploadDao;
            this.dataSourceFieldDao = dataSourceFieldDao;
            this.validationResultDao = validationResultDao;
            this.dataSourceWithDrawTableDao = dataSourceWithDrawTableDao;
            this.sqlHelperDao = sqlHelperDao;
        }

        private string csvEncoding;
        public string CSVEncoding
        {
            get
            {
                if (this.csvEncoding == null)
                {
                    return DEFAULT_CSV_ENCODING;
                }
                else
                {
                    return this.csvEncoding;
                }
            }
            set
            {
                this.csvEncoding = value;
            }
        }

        private string dwDBString;
        public string DWDBString
        {
            get
            {
                if (this.dwDBString == null)
                {
                    return DEFAULT_DW_DBString;
                }
                else
                {
                    return this.dwDBString;
                }
            }
            set
            {
                this.dwDBString = value;
            }
        }

        private int csvRecordPerParse;
        public int CSVRecordPerParse
        {
            get
            {
                if (this.csvRecordPerParse <= 0)
                {
                    return DEFAULT_CSV_RECORD_PER_PARSE;
                }
                else
                {
                    return this.csvRecordPerParse;
                }
            }
            set
            {
                this.csvRecordPerParse = value;
            }
        }
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateDataSourceUpload(DataSourceUpload entity)
        {
            //TODO: Add other code here.

            dataSourceUploadDao.CreateDataSourceUpload(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSourceUpload LoadDataSourceUpload(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            DataSourceUpload ds = dataSourceUploadDao.LoadDataSourceUpload(id);
            return ds;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDataSourceUpload(DataSourceUpload entity)
        {
            //TODO: Add other code here.
            dataSourceUploadDao.UpdateDataSourceUpload(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceUpload(int id)
        {
            dataSourceUploadDao.DeleteDataSourceUpload(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceUpload(DataSourceUpload entity)
        {
            dataSourceUploadDao.DeleteDataSourceUpload(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceUpload(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            dataSourceUploadDao.DeleteDataSourceUpload(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceUpload(IList<DataSourceUpload> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            dataSourceUploadDao.DeleteDataSourceUpload(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceCategory> FindDataSourceCategoryForOwner(int userId, string strCategory, string strType, string strStatus, string strDSName)
        {
            IList<DataSourceCategory> dataSourceCategoryList = dataSourceCategoryDao.FindDataSourceCategory(userId, "OWNER", strCategory, strType);
            IList<DataSourceUpload> dataSourceUploadList = dataSourceUploadDao.FindLastestDSUpload(userId, "OWNER");

            if (dataSourceUploadList != null && dataSourceUploadList.Count > 0
                && dataSourceCategoryList != null && dataSourceUploadList.Count > 0)
            {
                foreach (DataSourceUpload dataSourceUpload in dataSourceUploadList)
                {
                    if (!dataSourceUpload.ProcessStatus.Equals(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_SUCCESS))
                    {
                        int dataSourceCategoryId = dataSourceUpload.TheDataSourceCategory.Id;
                        foreach (DataSourceCategory dataSourceCategory in dataSourceCategoryList)
                        {
                            if (dataSourceCategory.Id == dataSourceCategoryId)
                            {
                                dataSourceCategory.LastestDataSourceUpload = dataSourceUpload;
                                break;
                            }
                        }
                    }
                }
            }

            List<DataSourceCategory> FoundResult = new List<DataSourceCategory>();
            if (dataSourceCategoryList != null && dataSourceCategoryList.Count > 0)
            {
                foreach (DataSourceCategory dataSourceCategory in dataSourceCategoryList)
                {
                    if (strStatus.Equals("ALL") || 
                        (dataSourceCategory.LastestDataSourceUpload != null && dataSourceCategory.LastestDataSourceUpload.ProcessStatus.Equals(strStatus)))
                    {
                        if (strDSName.Equals("") || dataSourceCategory.TheDataSource.Name.ToUpper().Contains(strDSName.ToUpper()) || dataSourceCategory.TheDataSource.Description.ToUpper().Contains(strDSName.ToUpper()))
                        {
                            FoundResult.Add(dataSourceCategory);
                        }
                    }
                }
            }
            return FoundResult;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceCategory> FindDataSourceCategoryForETLConfirmer(int userId, string strCategory, string strType, string strStatus)
        {
            IList<DataSourceCategory> dataSourceCategoryList = dataSourceCategoryDao.FindDataSourceCategory(userId, "ETL", strCategory, strType);
            IList<DataSourceUpload> dataSourceUploadList = dataSourceUploadDao.FindLastestDSUpload(userId, "ETL");

            if (dataSourceUploadList != null && dataSourceUploadList.Count > 0
                && dataSourceCategoryList != null && dataSourceCategoryList.Count > 0)
            {
                foreach (DataSourceUpload dataSourceUpload in dataSourceUploadList)
                {
                    if (dataSourceUpload.ProcessStatus.Equals(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_LOCKED) || dataSourceUpload.ProcessStatus.Equals(DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED)
                        || dataSourceUpload.ProcessStatus.Equals(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED) || dataSourceUpload.ProcessStatus.Equals(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED))
                    {
                        int dataSourceCategoryId = dataSourceUpload.TheDataSourceCategory.Id;
                        foreach (DataSourceCategory dataSourceCategory in dataSourceCategoryList)
                        {
                            if (dataSourceCategory.Id == dataSourceCategoryId)
                            {
                                dataSourceCategory.LastestDataSourceUpload = dataSourceUpload;
                                break;
                            }
                        }
                    }
                }
            }
            if (!strStatus.Equals("ALL"))
            {
                List<DataSourceCategory> FoundResult = new List<DataSourceCategory>();

                if (dataSourceCategoryList != null && dataSourceCategoryList.Count > 0)
                {
                    foreach (DataSourceCategory dataSourceCategory in dataSourceCategoryList)
                    {
                        if (dataSourceCategory.LastestDataSourceUpload != null)
                        {
                            if (dataSourceCategory.LastestDataSourceUpload.ProcessStatus.Equals(strStatus))
                            {
                                FoundResult.Add(dataSourceCategory);
                            }
                        }
                    }
                }
                return FoundResult;
            }
            else
            {
                return dataSourceCategoryList;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<string> FindDataSourceCategoryListForETLConfirmer(int userId, bool includeInactive)
        {
            IList<string> dataSourceCategoryList = dataSourceCategoryDao.FindDataSourceCategoryList(userId, "ETL", includeInactive);
            return dataSourceCategoryList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<string> FindDataSourceCategoryListForOwner(int userId, bool includeInactive)
        {
            IList<string> dataSourceCategoryList = dataSourceCategoryDao.FindDataSourceCategoryList(userId, "OWNER", includeInactive);
            return dataSourceCategoryList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<string> FindDataSourceTypeListForETLConfirmer(int userId)
        {
            IList<string> dataSourceTypeList = dataSourceCategoryDao.FindDataSourceTypeList(userId, "ETL");
            return dataSourceTypeList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<string> FindDataSourceTypeListForOwner(int userId)
        {
            IList<string> dataSourceTypeList = dataSourceCategoryDao.FindDataSourceTypeList(userId, "Owner");
            return dataSourceTypeList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public List<ListItem> FindDataSourceStatusListForOwner(int userId)
        {
            List<ListItem> dataSourceStatusList = new List<ListItem>();
            dataSourceStatusList.Add(new ListItem("(All)", "ALL"));
            dataSourceStatusList.Add(new ListItem("OWNER_UPLOADED", ""));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED, DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED, DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED, DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_LOCKED, DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_LOCKED));

            return dataSourceStatusList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public List<ListItem> FindDataSourceStatusListForETLConfirmer(int userId)
        {
            List<ListItem> dataSourceStatusList = new List<ListItem>();
            dataSourceStatusList.Add(new ListItem("(All)", "ALL"));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED, DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED, DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED, DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED));
            dataSourceStatusList.Add(new ListItem(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_LOCKED, DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_LOCKED));

            return dataSourceStatusList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceUpload> FindDataSourceUpload(int dataSourceId)
        {
            return dataSourceUploadDao.FindDataSourceUpload(dataSourceId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceCategory> FindDataSourceCategory(int datasourceId)
        {
            return dataSourceCategoryDao.FindAllByDataSourceId(datasourceId) as IList<DataSourceCategory>;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<string> UploadCSV(DataSourceUpload dsUpload, Stream s)
        {
            //get batch no for this upload
            //I do not handle when two upload a file with the same datasource at the same time
            //maybe in this situation they will get the same batch no
            DataSourceCategory dscCategory = dataSourceCategoryDao.LoadDataSourceCategory(dsUpload.TheDataSourceCategory.Id);
            int batchNo = 0;
            //find and delete if last upload record is confirmed
            DataSourceUpload lastDSUpload = dataSourceUploadDao.FindLastestDSUpload(dscCategory.Id);
            if (lastDSUpload != null && (lastDSUpload.ProcessStatus.Equals("") || lastDSUpload.ProcessStatus.Equals(DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED)))
            {
                this.DeleteDataSourceUploadAndUploadedData(lastDSUpload.Id);
                batchNo = lastDSUpload.BatchNo;
            }
            else
            {
                if (lastDSUpload == null || lastDSUpload.ProcessStatus.Equals(DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_SUCCESS))
                {
                    batchNo = dataSourceUploadDao.GenerateBatchNo(dscCategory.Id);
                }
                else
                {
                    List<string> errorArray = new List<string>();
                    errorArray.Add("The data file can not been uploaded because of there is a same category file in proceeding!");
                    return errorArray;
                }
            }

            IList dsFieldList = dataSourceFieldDao.FindAllByDataSourceId(dscCategory.TheDataSource.Id);
            //Transfer the data source field list to data source field hash table
            //because class DataSourceField is in persistence project, it can be used in util project
            //so I use CSVDataSourceField to transfer the data
            List<CSVDataSourceField> dsFieldTable = new List<CSVDataSourceField>();
            foreach (DataSourceField dsField in dsFieldList)
            {
                CSVDataSourceField csvDSField = new CSVDataSourceField();
                csvDSField.FieldLength = dsField.FieldLength;
                csvDSField.FieldType = dsField.FieldType;
                csvDSField.Name = dsField.Name.ToUpper();
                csvDSField.IsDataKey = dsField.IsDataKey;
                csvDSField.IsNullable = dsField.IsNullable;
                dsFieldTable.Add(csvDSField);
            }

            //new a CSVDataContainer to store the uploaded data
            CSVDataContainer dataContainer =
                new CSVDataContainer(batchNo, dscCategory.TheDataSource.Name, dscCategory.Id, dscCategory.Name, s, this.CSVEncoding, dsFieldTable);

            //Get errorMessage from dataContainer
            IList<string> errorList = dataContainer.GetErrorMessages();
            if (errorList != null && errorList.Count > 0)
            {
                return errorList;
            }

            //insert temp table and history table
            dataContainer.setRecordCountPerParse(CSVRecordPerParse);
            //sqlHelperDao.ExecuteNonQuery(dataContainer.GetClearTableSql());
            while (dataContainer.ParseNext())
            {
                try
                {
                    sqlHelperDao.ExecuteNonQuery(dataContainer.GetInsertTableSql());
                }
                catch (SqlException ex)
                {
                    //manually do rollback transaction
                    if (dataContainer.GetClearTableSql() != null && dataContainer.GetClearTableSql().Trim().Length != 0) 
                    {
                        sqlHelperDao.ExecuteNonQuery(dataContainer.GetClearTableSql());
                    }

                    //error message
                    errorList = new List<string>();
                    errorList.Add(ex.Message);
                    return errorList;
                }
            }

            //Get errorMessage from dataContainer
            errorList = dataContainer.GetErrorMessages();
            if (errorList != null && errorList.Count > 0)
            {
                //manually do rollback transaction
                if (dataContainer.GetClearTableSql() != null && dataContainer.GetClearTableSql().Trim().Length != 0)
                {
                    sqlHelperDao.ExecuteNonQuery(dataContainer.GetClearTableSql());
                }

                //error message
                return errorList;
            }

            //insert a new dsUpload record
            dsUpload.UploadFileLength = s.Length;
            dsUpload.BatchNo = batchNo;
            dsUpload.RecordRows = dataContainer.GetRecordRows();
            dsUpload.ProcessStatus = "";
            dsUpload.ProcessStatusDate = DateTime.Now;
            dataSourceUploadDao.CreateDataSourceUpload(dsUpload);

            //insert validate result table
            IList dsRuleList = dataSourceRuleDao.FindAllByDataSourceId(dscCategory.TheDataSource.Id);
            if (dsRuleList != null)
            {
                foreach (DataSourceRule dsRule in dsRuleList)
                {
                    ValidationResult vr = new ValidationResult();
                    vr.TheDataSourceRule = dsRule;
                    vr.TheDataSourceUpload = dsUpload;
                    vr.Status = "";
                    validationResultDao.CreateValidationResult(vr);
                }
                RefreshValidateResultCount(dsUpload);
            }

            // Modified by vincent at 2007-11-29 begin
            //string tempsql = string.Format("select * from (select count(*) as a  from inmarketSales) as a inner join (select count(*) as b  from inmarketSales_history where batch_No = {0}) as b on a.a = b.b"
            //    , batchNo);
            //DataSet tempds = sqlHelperDao.ExecuteDataset(tempsql);
            //if (tempds != null && tempds.Tables.Count > 0 && tempds.Tables[0].Rows.Count = 0)
            //{
            //    errorList.Add("Faild to Upload.");
            //}
            // Modified by vincent at 2007-11-29 end

            return null;
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceUploadAndUploadedData(int id)
        {
            DataSourceUpload dsUpload = LoadDataSourceUpload(id);
            string tempTableNm =
                DataSourceHelper.GetTempTableName(dsUpload.TheDataSourceCategory.TheDataSource.Name);
            string historyTableNm =
                DataSourceHelper.GetHistoryTableName(dsUpload.TheDataSourceCategory.TheDataSource.Name);

            //delete records from temp and history table
            string deleteTempTableSQL = "delete from " + tempTableNm + " where BATCH_NO = " + dsUpload.BatchNo + " and CATEGORY = '" + dsUpload.TheDataSourceCategory.Name + "';";
            string deleteHistoryTableSQL = "delete from " + historyTableNm + " where BATCH_NO = " + dsUpload.BatchNo + " and CATEGORY = '" + dsUpload.TheDataSourceCategory.Name + "';";
            sqlHelperDao.ExecuteNonQuery(deleteTempTableSQL + deleteHistoryTableSQL);

            //delete validation result
            validationResultDao.DeleteValidationResultByDSUploadId(dsUpload.Id);

            //delete the specified dsUpload record
            DeleteDataSourceUpload(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSourceCategory LoadDataSourceCategory(int id)
        {
            return dataSourceCategoryDao.LoadDataSourceCategory(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void ConfirmDataSourceUpload(int id, User user)
        {
            DataSourceUpload dsUpload = dataSourceUploadDao.LoadDataSourceUpload(id);
            if (dsUpload.ProcessStatus == "")
            {
                dsUpload.ProcessStatus = DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED;
                dsUpload.ProcessStatusDate = DateTime.Now;
                dsUpload.OwnerConfirmBy = user;
                dsUpload.OwnerConfirmDate = dsUpload.ProcessStatusDate;
                dataSourceUploadDao.UpdateDataSourceUpload(dsUpload);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void UnconfirmDataSourceUpload(int id)
        {
            DataSourceUpload dsUpload = dataSourceUploadDao.LoadDataSourceUpload(id);
            if (dsUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED)
            {
                dsUpload.ProcessStatus = "";
                dsUpload.ProcessStatusDate = DateTime.Now;
                dsUpload.OwnerConfirmBy = null;
                dsUpload.OwnerConfirmDate = null;
                dataSourceUploadDao.UpdateDataSourceUpload(dsUpload);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void ETLConfirmDataSourceUpload(int id, User user)
        {
            DataSourceUpload dsUpload = dataSourceUploadDao.LoadDataSourceUpload(id);
            if (dsUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED)
            {
                dsUpload.ProcessStatus = DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED;
                dsUpload.ProcessStatusDate = DateTime.Now;
                dsUpload.ETLConfirmBy = user;
                dsUpload.ETLConfirmDate = dsUpload.ProcessStatusDate;
                dataSourceUploadDao.UpdateDataSourceUpload(dsUpload);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void ETLUnconfirmDataSourceUpload(int id)
        {
            DataSourceUpload dsUpload = dataSourceUploadDao.LoadDataSourceUpload(id);
            if (dsUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED || dsUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED)
            {
                dsUpload.ProcessStatus = DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED;
                dsUpload.ProcessStatusDate = DateTime.Now;
                dsUpload.ETLConfirmBy = null;
                dsUpload.ETLConfirmDate = null;
                dataSourceUploadDao.UpdateDataSourceUpload(dsUpload);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ValidationResult> FindValidationResultByDataSourceUploadId(int datasourceId)
        {
            return validationResultDao.FindAllByDSUploadId(datasourceId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ValidationResult> FindValidationResultByDataSourceUploadIdAndRuleType(int datasourceId, string ruleType)
        {
            return validationResultDao.FindAllByDSUploadIdAndRuleType(datasourceId, ruleType);
        }

        [Transaction(TransactionMode.Requires)]
        public ValidationResult ValidateRule(int validateResultId)
        {
            ValidationResult vr = validationResultDao.LoadValidationResult(validateResultId);
            string rule = vr.TheDataSourceRule.RuleContent;
            
            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(vr, rule);

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();
            int failRowCount = 0;
            string orgStatus = vr.Status;
            while (dataReader.Read())
            {
                failRowCount++;
            }
            if (failRowCount > 0)
            {
                vr.Status = "Failed";
            }
            else
            {
                vr.Status = "Passed";
            }
            vr.FaildRowCount = failRowCount;
            validationResultDao.UpdateValidationResult(vr);

            RefreshValidateResultCount(vr.TheDataSourceUpload);
            return vr;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void RefreshValidateResultCount(DataSourceUpload TheDataSourceUpload)
        {
            TheDataSourceUpload.Errors = 0;
            TheDataSourceUpload.Problems = 0;
            TheDataSourceUpload.Warnings = 0;
            TheDataSourceUpload.ValidationResultList = FindValidationResultByDataSourceUploadId(TheDataSourceUpload.Id);
            if (TheDataSourceUpload.ValidationResultList != null)
            {
                IList<ValidationResult> list = new List<ValidationResult>();
                foreach (ValidationResult vr in TheDataSourceUpload.ValidationResultList)
                {
                    if (vr.TheDataSourceRule.RuleType == "ERROR" && vr.Status != "Passed")
                    {
                        TheDataSourceUpload.Errors = TheDataSourceUpload.Errors + 1;
                    }
                    if (vr.TheDataSourceRule.RuleType == "PROBLEM" && vr.Status != "Passed")
                    {
                        TheDataSourceUpload.Problems = TheDataSourceUpload.Problems + 1;
                    }
                    if (vr.TheDataSourceRule.RuleType == "WARNING" && vr.Status != "Passed")
                    {
                        TheDataSourceUpload.Warnings = TheDataSourceUpload.Warnings + 1;
                    }
                }
            } 
        }

        [Transaction(TransactionMode.Unspecified)]
        public void DownloadValidateResult(ValidationResult vr, CSVWriter csvWriter)
        {
            string rule = vr.TheDataSourceRule.ResultContent;

            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(vr, rule); ;

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
        public DataSet FindValidateUpdateResult(ValidationResult vr)
        {
            string rule = vr.TheDataSourceRule.UpdateContent;

            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(vr, rule);

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            return dataSet;
        }

        private string UpdateValidationSQLContent(ValidationResult vr, string rule)
        {
            rule = rule.Replace("<$Category$>", vr.TheDataSourceUpload.TheDataSourceCategory.Name.ToString());
            //rule = rule.Replace("<$BatchNo$>", vr.TheDataSourceUpload.BatchNo.ToString());
            rule = rule.Replace("<$DWDBString$>", this.DWDBString);
            return rule;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void DownloadUploadData(DataSourceUpload dsFile, CSVWriter csvWriter)
        {
            string rule = "Select ";
            IList iL = dataSourceFieldDao.FindAllByDataSourceId(dsFile.TheDataSourceCategory.TheDataSource.Id);
            foreach (Dndp.Persistence.Entity.Dui.DataSourceField dsFld in iL)
            {
                rule = rule + dsFld.Name.ToString() + ", ";
            }

            rule = rule + "1 from " + dsFile.TheDataSourceCategory.TheDataSource.Name + "_HISTORY where BATCH_NO = " + dsFile.BatchNo + " and CATEGORY = '" + dsFile.TheDataSourceCategory.Name + "' Order by ROW_NO";

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();

            //write csv header
            string[] header = new string[dataReader.FieldCount];
            for (int i = 0; i < dataReader.FieldCount - 1; i++)
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
                for (int i = 0; i < fields.Length - 1; i++)
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
        public void DownloadUploadTemplate(DataSource ds, CSVWriter csvWriter)
        {
            int i = 0;
            IList iL = dataSourceFieldDao.FindAllByDataSourceId(ds.Id);
            if (iL != null && iL.Count > 0)
            {
                string[] header = new string[iL.Count];
                foreach (Dndp.Persistence.Entity.Dui.DataSourceField dsFld in iL)
                {
                    header[i] = dsFld.Name.ToString();
                    i = i + 1;
                }
                csvWriter.Write(header);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void DownloadETLLog(DataSourceUpload dsFile, CSVWriter csvWriter)
        {
            string rule = "select Row_No, ErrorInformation, ErrorDate from sys_etl_log where LogbatchNo = " +
                "(Select isnull(Max(LogbatchNo), '$$$') from sys_etl_log where Data_Source_Upload_Id = " + dsFile.Id.ToString() + ")";

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
                for (int i = 0; i < fields.Length - 1; i++)
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
        public ValidationResult LoadValidationResult(int id)
        {
            return validationResultDao.LoadValidationResult(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void WithDrawLoadedRecord(int id, User ActionUser)
        {
            DataSourceUpload TheDataSourceUpload = dataSourceUploadDao.LoadDataSourceUpload(id);
            string SQLStatement = "";
            if (TheDataSourceUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_SUCCESS)
            {
                TheDataSourceUpload.IsWithdraw = 1;
                dataSourceUploadDao.UpdateDataSourceUpload(TheDataSourceUpload);

                IList DataSourceWithDrawTableList = dataSourceWithDrawTableDao.FindAllByDataSourceId(TheDataSourceUpload.TheDataSourceCategory.TheDataSource.Id);
                if (DataSourceWithDrawTableList != null)
                {
                    foreach (DataSourceWithDrawTable dsWDT in DataSourceWithDrawTableList)
                    {
                        SQLStatement = SQLStatement + " Delete From " + dsWDT.WithDrawTableName.ToString() + " Where Category = '" + TheDataSourceUpload.TheDataSourceCategory.Name + "' and Batch_No = " + TheDataSourceUpload.BatchNo.ToString();
                    }
                    sqlHelperDao.ExecuteNonQuery(SQLStatement);
                    LogDBAction(SQLStatement, "WithDrawData", TheDataSourceUpload.Id.ToString() + "-" + TheDataSourceUpload.Name, ActionUser.UserName);

                    TheDataSourceUpload.WithDrawBy = ActionUser;
                    TheDataSourceUpload.WithDrawDate = DateTime.Now;

                    this.dataSourceUploadDao.UpdateDataSourceUpload(TheDataSourceUpload);
                }
            }
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

        [Transaction(TransactionMode.Requires)]
        public void DeleteUpdateRecord(DataSourceUpload TheDataSourceUpload, String RecId)
        {
            string SQLStatement = "";
            if (TheDataSourceUpload.ProcessStatus == "" || TheDataSourceUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED)
            {
                DataSource ds = TheDataSourceUpload.TheDataSourceCategory.TheDataSource;
                SQLStatement = "Select BATCH_NO, ROW_NO, CATEGORY From " + ds.Name + " Where Rec_Id = " + RecId;
                DataSet dataSet = sqlHelperDao.ExecuteDataset(SQLStatement);
                DataTableReader dataReader = dataSet.CreateDataReader();
                if (dataReader.Read())
                {
                    object[] fields = new object[dataReader.FieldCount];
                    dataReader.GetValues(fields);
                    SQLStatement = "Delete From " + ds.Name + " Where Rec_Id = " + RecId;
                    SQLStatement = SQLStatement + " Delete From " + ds.Name + "_History Where BATCH_NO = " + fields[0].ToString() + " and ROW_NO = " + fields[1].ToString() + " and CATEGORY = '" + fields[2].ToString() + "'";
                    sqlHelperDao.ExecuteNonQuery(SQLStatement);

                    TheDataSourceUpload.RecordRows--;
                    if (TheDataSourceUpload.RecordRows == 0)
                    {
                        this.DeleteDataSourceUploadAndUploadedData(TheDataSourceUpload.Id);
                    }
                    else
                    {
                        this.dataSourceUploadDao.UpdateDataSourceUpload(TheDataSourceUpload);
                    }
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteUploadRecordHistory(int id, User ActionUser)
        {
            DataSourceUpload TheDataSourceUpload = dataSourceUploadDao.LoadDataSourceUpload(id);
            string SQLStatement = "";
            if (TheDataSourceUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_SUCCESS)
            {
                TheDataSourceUpload.IsHitoryDelete = 1;
                dataSourceUploadDao.UpdateDataSourceUpload(TheDataSourceUpload);

                SQLStatement = SQLStatement + " Delete From " + DataSourceHelper.GetHistoryTableName(TheDataSourceUpload.TheDataSourceCategory.TheDataSource.Name) + 
                        " Where Category = '" + TheDataSourceUpload.TheDataSourceCategory.Name + "' and Batch_No = " + TheDataSourceUpload.BatchNo.ToString();

                sqlHelperDao.ExecuteNonQuery(SQLStatement);

                LogDBAction(SQLStatement, "DeleteHistoryRecord", TheDataSourceUpload.Id.ToString() + "-" + TheDataSourceUpload.Name, ActionUser.UserName);

                TheDataSourceUpload.RowDeleteBy = ActionUser;
                TheDataSourceUpload.RowDeleteDate = DateTime.Now;

                this.dataSourceUploadDao.UpdateDataSourceUpload(TheDataSourceUpload);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void SaveUpdateRecord(DataSourceUpload TheDataSourceUpload, String RecId, Hashtable updFieldTable)
        {
            string SQLStatement = "";
            if (TheDataSourceUpload.ProcessStatus == "" || TheDataSourceUpload.ProcessStatus == DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED)
            {
                DataSource ds = TheDataSourceUpload.TheDataSourceCategory.TheDataSource;
                SQLStatement = "Select BATCH_NO, ROW_NO, CATEGORY From " + ds.Name + " Where Rec_Id = " + RecId;
                DataSet dataSet = sqlHelperDao.ExecuteDataset(SQLStatement);
                DataTableReader dataReader = dataSet.CreateDataReader();
                if (dataReader.Read())
                {
                    object[] fields = new object[dataReader.FieldCount];
                    dataReader.GetValues(fields);
                    IList dsFieldList = dataSourceFieldDao.FindAllByDataSourceId(ds.Id);
                    //Transfer the data source field list to data source field hash table
                    //because class DataSourceField is in persistence project, it can be used in util project
                    //so I use CSVDataSourceField to transfer the data
                    Hashtable dsFieldTable = new Hashtable();
                    string SQLTempTable = "Update " + ds.Name + " Set ";
                    string SQLHistoryTable = "; Update " + ds.Name + "_History Set ";
                    foreach (DataSourceField dsField in dsFieldList)
                    {
                        if (updFieldTable.Contains(dsField.Name.ToUpper())) {
                            if (dsField.FieldType.Equals("Integer") || dsField.FieldType.Equals("Numeric"))
                            {
                                if (updFieldTable[dsField.Name.ToUpper()].ToString().Trim().Length > 0)
                                {
                                    SQLTempTable = SQLTempTable + dsField.Name + " = " + updFieldTable[dsField.Name.ToUpper()].ToString() + ", ";
                                    SQLHistoryTable = SQLHistoryTable + dsField.Name + " = " + updFieldTable[dsField.Name.ToUpper()].ToString() + ", ";
                                }
                            }
                            else
                            {
                                SQLTempTable = SQLTempTable + dsField.Name + " = '" + updFieldTable[dsField.Name.ToUpper()].ToString() + "', ";
                                SQLHistoryTable = SQLHistoryTable + dsField.Name + " = '" + updFieldTable[dsField.Name.ToUpper()].ToString() + "', ";
                            }
                        }
                    }
                    SQLTempTable = SQLTempTable + "BATCH_NO = BATCH_NO Where Rec_Id = " + RecId;
                    SQLHistoryTable = SQLHistoryTable + "BATCH_NO = BATCH_NO Where BATCH_NO = " + fields[0].ToString() + " and ROW_NO = " + fields[1].ToString() + " and CATEGORY = '" + fields[2].ToString() + "'";
                    sqlHelperDao.ExecuteNonQuery(SQLTempTable + SQLHistoryTable);
                }
            }
        }
        
        [Transaction(TransactionMode.Requires)]
        public void RunETLPackage(string ETLJobName)
        {
            sqlHelperDao.ExecuteNonQuery("Use msdb; EXEC dbo.sp_start_job '" + ETLJobName + "'");
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceUpload> FindDataSourceUploadForETL()
        {
            return dataSourceUploadDao.FindDataSourceUploadForETL();
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceUpload> FindDataSourceUploadInETL()
        {
            return dataSourceUploadDao.FindDataSourceUploadInETL();
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSet FindETLAgentLog()
        {
            string SQLStatement = "Select top(100) * from sys_etlagent_log order by rec_id DESC";

            DataSet dataSet = sqlHelperDao.ExecuteDataset(SQLStatement);
            return dataSet;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceUpload> FindDataSourceUploadByLogBatchNo(string LogBatchNo)
        {
            string SQLStatement = "select data_source_upload_id from sys_etl_datasource_log where LogBatchNo = '" + LogBatchNo + "'";
            DataSet dataSet = sqlHelperDao.ExecuteDataset(SQLStatement);

            IList<int> idList = new List<int>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                idList.Add(Int32.Parse(row[0].ToString()));
            }
            if (idList.Count == 0)
            {
                return null;
            }
            else
            {
                return dataSourceUploadDao.FindDataSourceUpload(idList);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public Boolean FindETLRunStatus()
        {
            string SQLStatement = "Select rec_id from Sys_ETLAgent_log where Status = 'In Progress'";

            DataSet dataSet = sqlHelperDao.ExecuteDataset(SQLStatement);
            if (dataSet.Tables[0].Rows.Count.Equals(0)) {
                return true;
            } 
            else 
            {
                return false;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceUpload> FindDataSourceUpload(int datasourceId, string category, string subject, string fileName, string createBy, User user)
        {
            return dataSourceUploadDao.FindDataSourceUpload(datasourceId, category, subject, fileName, createBy, user);
        }
        #endregion Customized Methods
    }
}
