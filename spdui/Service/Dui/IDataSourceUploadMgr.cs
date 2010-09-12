using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Entity.Dui;
using System.IO;
using System.Data;
using Dndp.Utility.CSV;
using System.Web.UI.WebControls;
using Dndp.Persistence.Entity.Security;

namespace Dndp.Service.Dui
{
    public interface IDataSourceUploadMgr 
    {
        #region Method Created By CodeSmith

        void CreateDataSourceUpload(DataSourceUpload entity);

        DataSourceUpload LoadDataSourceUpload(int id);

        void UpdateDataSourceUpload(DataSourceUpload entity);

        void DeleteDataSourceUpload(int id);

        void DeleteDataSourceUpload(DataSourceUpload entity);

        void DeleteDataSourceUpload(IList<int> idList);

        void DeleteDataSourceUpload(IList<DataSourceUpload> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods
        DataSourceCategory LoadDataSourceCategory(int id);

        ValidationResult LoadValidationResult(int id);

        IList<DataSourceCategory> FindDataSourceCategoryForOwner(int userId, string strCategory, string strType, string strStatus, string strDSName);

        IList<DataSourceCategory> FindDataSourceCategoryForETLConfirmer(int userId, string strCategory, string strType, string strStatus);

        IList<string> FindDataSourceCategoryListForETLConfirmer(int userId, bool includeInactive);

        IList<string> FindDataSourceCategoryListForOwner(int userId, bool includeInactive);

        IList<string> FindDataSourceTypeListForETLConfirmer(int userId);

        IList<string> FindDataSourceTypeListForOwner(int userId);

        List<ListItem> FindDataSourceStatusListForOwner(int userId);

        List<ListItem> FindDataSourceStatusListForETLConfirmer(int userId);

        IList<DataSourceUpload> FindDataSourceUpload(int datasourceId);

        IList<DataSourceCategory> FindDataSourceCategory(int datasourceId);

        IList<string> UploadCSV(DataSourceUpload dsUpload, Stream s);

        void DeleteDataSourceUploadAndUploadedData(int id);

        void ConfirmDataSourceUpload(int id, User user);

        void UnconfirmDataSourceUpload(int id);

        void ETLConfirmDataSourceUpload(int id, User user);

        void ETLUnconfirmDataSourceUpload(int id);

        IList<ValidationResult> FindValidationResultByDataSourceUploadId(int datasourceId);

        IList<ValidationResult> FindValidationResultByDataSourceUploadIdAndRuleType(int datasourceId, string ruleType);

        ValidationResult ValidateRule(int validateResultId);

        void DownloadValidateResult(ValidationResult vr, CSVWriter csvWriter);

        DataSet FindValidateUpdateResult(ValidationResult vr);
        
        void DownloadUploadData(DataSourceUpload dsFile, CSVWriter csvWriter);

        void DownloadUploadTemplate(DataSource ds, CSVWriter csvWriter);

        void DownloadETLLog(DataSourceUpload dsFile, CSVWriter csvWriter);

        void WithDrawLoadedRecord(int id, User ActionUser);

        void DeleteUploadRecordHistory(int id, User ActionUser);

        void DeleteUpdateRecord(DataSourceUpload TheDataSourceUpload, String RecId);

        void SaveUpdateRecord(DataSourceUpload TheDataSourceUpload, String RecId, Hashtable updFieldTable);

        void RunETLPackage(string ETLJobName);

        IList<DataSourceUpload> FindDataSourceUploadForETL();

        IList<DataSourceUpload> FindDataSourceUploadInETL();

        DataSet FindETLAgentLog();

        IList<DataSourceUpload> FindDataSourceUploadByLogBatchNo(string LogBatchNo);

        Boolean FindETLRunStatus();

        IList<DataSourceUpload> FindDataSourceUpload(int datasourceId, string category, string subject, string fileName, string createBy, User user);

        #endregion Customized Methods
    }
}
