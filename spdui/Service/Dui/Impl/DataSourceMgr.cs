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
using Dndp.Persistence.Dao;
using Dndp.Persistence.Dao.Security;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Service.Dui.Impl
{
    [Transactional]
    public class DataSourceMgr : SessionBase, IDataSourceMgr
    {
        private IUserDao userDao;
        private IDataSourceDao dataSourceDao;
        private IDataSourceCategoryDao dataSourceCategoryDao;
        private IDataSourceWithDrawTableDao dataSourceWithDrawTableDao;
        private IDataSourceFieldDao dataSourceFieldDao;
        private IDataSourceOperatorDao dataSourceOperatorDao;
        private IDataSourceRuleDao dataSourceRuleDao;
        private IDataSourceUploadDao dataSourceUploadDao;
        private IValidationResultDao validationResultDao;
        private SqlHelperDao sqlHelperDao;

        public DataSourceMgr(IDataSourceDao dataSourceDao,
            IDataSourceCategoryDao dataSourceCategoryDao,
            IDataSourceWithDrawTableDao dataSourceWithDrawTableDao,
            IDataSourceFieldDao dataSourceFieldDao,
            IDataSourceOperatorDao dataSourceOperatorDao,
            IDataSourceRuleDao dataSourceRuleDao,
            IDataSourceUploadDao dataSourceUploadDao,
            IValidationResultDao validationResultDao,
            SqlHelperDao sqlHelperDao,
            IUserDao userDao)
        {
            this.dataSourceDao = dataSourceDao;
            this.dataSourceCategoryDao = dataSourceCategoryDao;
            this.dataSourceWithDrawTableDao = dataSourceWithDrawTableDao;
            this.dataSourceFieldDao = dataSourceFieldDao;
            this.dataSourceOperatorDao = dataSourceOperatorDao;
            this.dataSourceRuleDao = dataSourceRuleDao;
            this.dataSourceUploadDao = dataSourceUploadDao;
            this.validationResultDao = validationResultDao;
            this.sqlHelperDao = sqlHelperDao;
            this.userDao = userDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateDataSource(DataSource entity)
        {
            //TODO: Add other code here.
			
            dataSourceDao.CreateDataSource(entity);   
 
            //create temp table and history table
            string sql = GernateCreateTableSql(entity.Name);
            
            sqlHelperDao.ExecuteNonQuery(sql);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSource LoadDataSource(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            DataSource ds = dataSourceDao.LoadDataSource(id);
            if (ds != null)
            {
                ds.DataSourceCategoryList = dataSourceCategoryDao.FindAllByDataSourceId(id);
                ds.DataSourceFieldList = dataSourceFieldDao.FindAllByDataSourceId(id);
                ds.DataSourceOperatorList = dataSourceOperatorDao.FindAllByDataSourceId(id);
                ds.DataSourceRuleList = dataSourceRuleDao.FindAllByDataSourceId(id);
                ds.DataSourceWithDrawTableList = dataSourceWithDrawTableDao.FindAllByDataSourceId(id);
            }
			
            return ds;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDataSource(DataSource entity)
        {
        	//TODO: Add other code here.
            dataSourceDao.UpdateDataSource(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSource(int id)
        {
            DataSource ds = dataSourceDao.LoadDataSource(id);
            string sql = GernateDropTableSql(ds.Name);
            
            validationResultDao.DeleteValidationResultByDSId(id);
            dataSourceWithDrawTableDao.DeleteDataSourceWithDrawTableByDSId(id);
            dataSourceUploadDao.DeleteDataSourceUploadByDSId(id);
            dataSourceRuleDao.DeleteDataSourceRuleByDSId(id);
            dataSourceOperatorDao.DeleteDataSourceOperatorByDSId(id);
            dataSourceFieldDao.DeleteDataSourceFieldByDSId(id);
            dataSourceCategoryDao.DeleteDataSourceCategoryByDSId(id);
            dataSourceDao.DeleteDataSource(id);

            sqlHelperDao.ExecuteNonQuery(sql); 
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSource(DataSource entity)
        {
            DeleteDataSource(entity.Id);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSource(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            //delete temp table and history table
            foreach (int id in idList)
            {
                DeleteDataSource(id);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSource(IList<DataSource> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            //delete temp table and history table
            foreach (DataSource entity in entityList)
            {
                DeleteDataSource(entity);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public bool IsDuplicateField(int dsId, string newFieldNm)
        {
           return dataSourceFieldDao.HasField(dsId, newFieldNm);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public IList LoadAllActiveDataSource()
        {
            return dataSourceDao.LoadAllActiveDataSource();
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceField(IList<int> fieldIdList)
        {
            if ((fieldIdList == null) || (fieldIdList.Count == 0))
            {
                return;
            }                        

            //delete field from temp table and history table
            foreach (int id in fieldIdList) {
                DataSourceField dsf = dataSourceFieldDao.LoadDataSourceField(id);
                string sql = GernateDropFieldSql(dsf);
                sqlHelperDao.ExecuteNonQuery(sql);
            }

            dataSourceFieldDao.DeleteDataSourceField(fieldIdList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceOperator(IList<int> dataSourceOperatorIdList)
        {
            if ((dataSourceOperatorIdList == null) || (dataSourceOperatorIdList.Count == 0))
            {
                return;
            }

            dataSourceOperatorDao.DeleteDataSourceOperator(dataSourceOperatorIdList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceRule(IList<int> ruleIdList)
        {
            if ((ruleIdList == null) || (ruleIdList.Count == 0))
            {
                return;
            }

            dataSourceRuleDao.DeleteDataSourceRule(ruleIdList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceCategory(IList<int> categoryIdList)
        {
            if ((categoryIdList == null) || (categoryIdList.Count == 0))
            {
                return;
            }

            dataSourceCategoryDao.DeleteDataSourceCategory(categoryIdList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDataSourceWithDrawTable(IList<int> withDrawTableIdList)
        {
            if ((withDrawTableIdList == null) || (withDrawTableIdList.Count == 0))
            {
                return;
            }
            DataSource TheDataSource = dataSourceWithDrawTableDao.LoadDataSourceWithDrawTable(withDrawTableIdList[0]).TheDataSource;
            dataSourceWithDrawTableDao.DeleteDataSourceWithDrawTable(withDrawTableIdList);

            RefreshWithDrawTableCount(TheDataSource);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void RefreshWithDrawTableCount(DataSource TheDataSource)
        {
            TheDataSource.DataSourceWithDrawTableList = FindDataSourceWithDrawTableByDataSourceId(TheDataSource.Id);
            if (TheDataSource.DataSourceWithDrawTableList == null)
            {
                TheDataSource.WithDrawTables = 0;
            }
            else
            {
                TheDataSource.WithDrawTables = TheDataSource.DataSourceWithDrawTableList.Count;
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void AddDataSourceField(int dataSourceId, DataSourceField dsf)
        {
            dsf.SequenceNo = dataSourceFieldDao.GetMaxSequenceNo(dataSourceId) + 1;
            dsf.TheDataSource = dataSourceDao.LoadDataSource(dataSourceId);
            dataSourceFieldDao.CreateDataSourceField(dsf);
           
            //alter temp table and history table
            string sql = GernateAlterTableSql(dsf);

            sqlHelperDao.ExecuteNonQuery(sql);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDataSourceFieldByDataSourceId(int dsId)
        {
            return dataSourceFieldDao.FindAllByDataSourceId(dsId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDataSourceOperatorByDataSourceId(int dsId)
        {
            return dataSourceOperatorDao.FindAllByDataSourceId(dsId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDataSourceRuleByDataSourceId(int dsId)
        {
            return dataSourceRuleDao.FindAllByDataSourceId(dsId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDataSourceCategoryByDataSourceId(int dsId)
        {
            return dataSourceCategoryDao.FindAllByDataSourceId(dsId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindDataSourceWithDrawTableByDataSourceId(int dsId)
        {
            return dataSourceWithDrawTableDao.FindAllByDataSourceId(dsId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<User> FindUserByRole(int roleId)
        {
            return userDao.FindUserByRole(roleId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSourceOperator> FindOperatorByDSIdAndAllowType(int dsId, string type)
        {
            return dataSourceOperatorDao.FindAllByDataSourceIdAndAllowType(dsId, type);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateDataSourceOperator(IList<int> userIdList, int dsId, string allowType)
        {
            //delete original operator
            IList<DataSourceOperator> dsOperatorList = FindOperatorByDSIdAndAllowType(dsId, allowType);

            if (dsOperatorList != null && dsOperatorList.Count > 0)
            {
                IList<int> dsOperatorIdList = new List<int>();
                foreach (DataSourceOperator dso in dsOperatorList)
                {
                    dsOperatorIdList.Add(dso.Id);
                }

                this.DeleteDataSourceOperator(dsOperatorIdList);
            }

            //update new operator
            DataSource ds = dataSourceDao.LoadDataSource(dsId);
            if (userIdList != null && userIdList.Count > 0) 
            {
                foreach(int userId in userIdList) {
                    User user = userDao.LoadUser(userId);
                    DataSourceOperator dso = new DataSourceOperator();
                    dso.AllowType = allowType;
                    dso.TheDataSource = ds;
                    dso.TheUser = user;

                    dataSourceOperatorDao.CreateDataSourceOperator(dso);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateDataSourceRule(DataSourceRule dsr)
        {
            dsr.SequenceNo = dataSourceRuleDao.GetMaxSequenceNo(dsr.TheDataSource.Id, dsr.RuleType);
            dataSourceRuleDao.CreateDataSourceRule(dsr);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateDateSourceRule(DataSourceRule dsr)
        {
            dataSourceRuleDao.UpdateDataSourceRule(dsr);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSourceRule LoadDataSourceRule(int dsRuleId)
        {
            return dataSourceRuleDao.LoadDataSourceRule(dsRuleId);
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateDataSourceCategory(DataSourceCategory dsc)
        {
            dataSourceCategoryDao.CreateDataSourceCategory(dsc);
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateDataSourceWithDrawTable(DataSourceWithDrawTable dsc)
        {
            dataSourceWithDrawTableDao.CreateDataSourceWithDrawTable(dsc);
            RefreshWithDrawTableCount(dsc.TheDataSource);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ValidationResult> FindValidationResultByIds(string validationResultIds)
        {
            return validationResultDao.FindAllByIds(validationResultIds);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataSourceUpload LoadDataSourceUpload(int dsUploadId)
        {
            return dataSourceUploadDao.LoadDataSourceUpload(dsUploadId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<DataSource> FindActiveDataSourceByTypeAndName(string type, string name)
        {
            return dataSourceDao.FindActiveDataSourceByTypeAndName(type, name);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<string> FindAllDataSourceType()
        {
            return dataSourceDao.FindAllDataSourceType();
        }

        private string GernateCreateTableSql(string dsName)
        {
            StringBuilder createTempTableSql = new StringBuilder();
            StringBuilder createHistoryTableSql = new StringBuilder();

            createTempTableSql.Append("create table ");
            createTempTableSql.Append(DataSourceHelper.GetTempTableName(dsName));
            createTempTableSql.Append(" ( Rec_Id numeric(18, 0) IDENTITY(1,1) NOT NULL, ");
            createTempTableSql.Append("BATCH_NO int not null, ");
            createTempTableSql.Append("ROW_NO int not null, ");
            createTempTableSql.Append("CATEGORY_id int not null, ");
            createTempTableSql.Append("CATEGORY nvarchar(50) not null, ");
            createTempTableSql.Append("CONSTRAINT PK_" + DataSourceHelper.GetTempTableName(dsName) + "_1 PRIMARY key CLUSTERED ( Rec_Id));");
            createTempTableSql.Append(" CREATE UNIQUE NONCLUSTERED INDEX IX_" + DataSourceHelper.GetTempTableName(dsName) + " on " + DataSourceHelper.GetTempTableName(dsName) + " (BATCH_NO, CATEGORY, ROW_NO);");

            createHistoryTableSql.Append(" create table ");
            createHistoryTableSql.Append(DataSourceHelper.GetHistoryTableName(dsName));
            createHistoryTableSql.Append(" ( Rec_Id numeric(18, 0) IDENTITY(1,1) NOT NULL, ");
            createHistoryTableSql.Append("BATCH_NO int not null, ");
            createHistoryTableSql.Append("ROW_NO int not null, ");
            createHistoryTableSql.Append("CATEGORY_id int not null, ");
            createHistoryTableSql.Append("CATEGORY nvarchar(50) not null, ");
            createHistoryTableSql.Append("CONSTRAINT PK_" + DataSourceHelper.GetHistoryTableName(dsName) + "_1 PRIMARY key CLUSTERED ( Rec_Id));");
            createHistoryTableSql.Append(" CREATE UNIQUE NONCLUSTERED INDEX IX_" + DataSourceHelper.GetHistoryTableName(dsName) + " on " + DataSourceHelper.GetHistoryTableName(dsName) + " (BATCH_NO, CATEGORY, ROW_NO);");

            return createTempTableSql.ToString() + createHistoryTableSql.ToString();
        }

        private string GernateDropTableSql(string dsName)
        {
            StringBuilder dropTempTableSql = new StringBuilder();
            StringBuilder dropHistoryTableSql = new StringBuilder();

            dropTempTableSql.Append("drop table ");
            dropTempTableSql.Append(DataSourceHelper.GetTempTableName(dsName) + ";");

            dropHistoryTableSql.Append("drop table ");
            dropHistoryTableSql.Append(DataSourceHelper.GetHistoryTableName(dsName));

            return dropTempTableSql.ToString() + dropHistoryTableSql.ToString();
        }

        private string GernateAlterTableSql(DataSourceField dsf)
        {
            StringBuilder alterTempTableSql = new StringBuilder();
            StringBuilder alterHistoryTableSql = new StringBuilder();

            alterTempTableSql.Append("alter table ");
            alterTempTableSql.Append(DataSourceHelper.GetTempTableName(dsf.TheDataSource.Name));
            alterTempTableSql.Append(" add ");
            alterTempTableSql.Append(GernateCreateFeildForSql(dsf.Name, dsf.FieldType, dsf.FieldLength,dsf.IsNullable, DataSourceHelper.GetTempTableName(dsf.TheDataSource.Name)) + ";");

            alterHistoryTableSql.Append("alter table ");
            alterHistoryTableSql.Append(DataSourceHelper.GetHistoryTableName(dsf.TheDataSource.Name));
            alterHistoryTableSql.Append(" add ");
            alterHistoryTableSql.Append(GernateCreateFeildForSql(dsf.Name, dsf.FieldType, dsf.FieldLength, dsf.IsNullable, DataSourceHelper.GetHistoryTableName(dsf.TheDataSource.Name)) + ";");

            return alterTempTableSql.ToString() + alterHistoryTableSql.ToString();
        }

        private string GernateDropFieldSql(DataSourceField dsf)
        {
            StringBuilder dropFieldFromTempTableSql = new StringBuilder();
            StringBuilder dropFieldFromHistoryTableSql = new StringBuilder();

            /*if (dsf.FieldType.Equals("Text"))
            {
                dropFieldFromTempTableSql.Append("alter table ");
                dropFieldFromTempTableSql.Append(DataSourceHelper.GetTempTableName(dsf.TheDataSource.Name));
                dropFieldFromTempTableSql.Append(" drop constraint ");
                dropFieldFromTempTableSql.Append("DF_" + DataSourceHelper.GetTempTableName(dsf.TheDataSource.Name) + "__" + dsf.Name + ";");

                dropFieldFromHistoryTableSql.Append("alter table ");
                dropFieldFromHistoryTableSql.Append(DataSourceHelper.GetHistoryTableName(dsf.TheDataSource.Name));
                dropFieldFromHistoryTableSql.Append(" drop constraint ");
                dropFieldFromHistoryTableSql.Append("DF_" + DataSourceHelper.GetHistoryTableName(dsf.TheDataSource.Name) + "__" + dsf.Name + ";");
            }
            */
            dropFieldFromTempTableSql.Append("alter table ");
            dropFieldFromTempTableSql.Append(DataSourceHelper.GetTempTableName(dsf.TheDataSource.Name));
            dropFieldFromTempTableSql.Append(" drop column ");
            dropFieldFromTempTableSql.Append(dsf.Name + ";");

            dropFieldFromHistoryTableSql.Append("alter table ");
            dropFieldFromHistoryTableSql.Append(DataSourceHelper.GetHistoryTableName(dsf.TheDataSource.Name));
            dropFieldFromHistoryTableSql.Append(" drop column ");
            dropFieldFromHistoryTableSql.Append(dsf.Name + ";");

            return dropFieldFromTempTableSql.ToString() + dropFieldFromHistoryTableSql.ToString();
        }

        private string GernateCreateFeildForSql(string fieldName, string fieldType, string fieldLength, bool isNullable, string tableNm)
        {
            if (fieldType.Equals("Text"))
            {
                return fieldName + " nvarchar(" + (fieldLength == null || fieldLength.Trim().Length == 0 ? "255" : fieldLength) + ") null";//" + (isNullable ? " null " : " not null "); //+ "CONSTRAINT DF_" + tableNm + "__" + fieldName + (isNullable ? "" : " Default ''");
            }
            else if (fieldType.Equals("Integer"))
            {
                return fieldName + " int null";// + (isNullable ? " null" : " not null default 0 ");
            }
            else if (fieldType.Equals("Numeric"))
            {
                return fieldName + " numeric(" + (fieldLength == null || fieldLength.Trim().Length == 0 ? "18,2" : fieldLength) + ") null";// + (isNullable ? " null" : " not null default 0 ");
            }
            else if (fieldType.Equals("DateTime"))
            {
                return fieldName + " datetime null";//" + (isNullable ? " null" : " not null default getdate() ");
            }

            return null;
        }

        private string GeneratCreateUniqueKey(string dsNm, string fieldNm, string tableNm)
        {
            return "alter table " + DataSourceHelper.GetTempTableName(dsNm) + " add unique (BATCH_NO, CATEGORY, " + fieldNm + ")";  
        }

        #endregion Customized Methods
    }
}
