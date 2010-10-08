using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Dui;
using Dndp.Persistence.Entity.Security;
using Dndp.Utility;
using Dndp.Utility.CSV;
using System.Data.SqlClient;
using System.Data;
using System.Web;
//TODO: Add other using statements here.

namespace Dndp.Service.Dui
{
    public interface IDWDataSourceMgr
    {
        #region Method Created By CodeSmith

        void CreateDWDataSource(DWDataSource entity);

        DWDataSource LoadDWDataSource(int id);

        void UpdateDWDataSource(DWDataSource entity);

        void DeleteDWDataSource(int id);

        void DeleteDWDataSource(DWDataSource entity);

        void DeleteDWDataSource(IList<int> idList);

        void DeleteDWDataSource(IList<DWDataSource> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList LoadAllDWDataSource();

        IList<DWDataSource> FindDWDataSourceByAllowType(int userId, string AllowType, string strType, string strDSName);

        IList<DWDataSource> FindDWDataSourceByTypeAndName(int userId, string strType, string strDSName);

        IList<DWDataSource> FindDWDataSourceByTypeAndName(string strType, string strDSName);

        IList<DWDataSource> FindDWDataSourceByTypeAndName(string strType, string strDSName, User user, string allowType);

        IList<string> FindDWDataSourceTypeList(int userId, string AllowType);

        IList<string> FindDWDataSourceTypeList(int userId);

        IList<string> FindDWDataSourceTypeList();

        void DeleteDWDataSourceOperator(IList<int> DWDataSourceOperatorIdList);

        IList FindDWDataSourceOperatorByDWDataSourceId(int dsId);

        IList<DWDataSourceOperator> FindDWOperatorByDSIdAndAllowType(int dsId, string type);

        void UpdateDWDataSourceOperator(IList<int> userIdList, int dsId, string allowType);

        IList<User> FindUserByRole(int roleId);

        IList<DWDataSource> FindDWDataSource(int userId);

        void DownloadQueryData(DWDataSource ds, HttpResponse response, string QueryDate);

        void DownloadQueryData(DWDataSource TheDWDataSource, string TheQueryDate, string condition, CSVWriter csvWriter);

        void DownloadUpdateQueryData(DWDataSource ds, CSVWriter csvWriter);

        DataSet FindViewAllResult(DWDataSource ds);

        DataSet FindViewAllResult(DWDataSource ds, String QueryDate);

        DataSet FindViewUpdateResult(DWDataSource ds);

        //void DeleteSelectedResult(DWDataSource ds, int RowNo, string ActionSource, string ActionUser, string strCondition);

        void DeleteSelectedResult(DWDataSource ds, IList<KeyValuePair<string, string>> pkKeyValuePairList, User ActionUser);

        #endregion Customized Methods

    }
}
