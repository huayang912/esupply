using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDataSourceCategoryDao
    {
        #region Method Created By CodeSmith

        void CreateDataSourceCategory(DataSourceCategory entity);

        DataSourceCategory LoadDataSourceCategory(int id);

        void UpdateDataSourceCategory(DataSourceCategory entity);
        
        void DeleteDataSourceCategory(int id);

        void DeleteDataSourceCategory(DataSourceCategory entity);

        void DeleteDataSourceCategory(IList<int> idList);

        void DeleteDataSourceCategory(IList<DataSourceCategory> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList FindAllByDataSourceId(int dataSourceId);

        IList<DataSourceCategory> FindDataSourceCategory(int userId, string allowType, string strCategory, string strType);

        void DeleteDataSourceCategoryByDSId(int dsId);

        IList<string> FindDataSourceCategoryList(int userId, string allowType, bool includeInactive);

        IList<string> FindDataSourceTypeList(int userId, string allowType);

        #endregion Customized Methods
    }
}
