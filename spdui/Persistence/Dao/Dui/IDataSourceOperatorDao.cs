using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDataSourceOperatorDao
    {
        #region Method Created By CodeSmith

        void CreateDataSourceOperator(DataSourceOperator entity);

        DataSourceOperator LoadDataSourceOperator(int id);

        void UpdateDataSourceOperator(DataSourceOperator entity);
        
        void DeleteDataSourceOperator(int id);

        void DeleteDataSourceOperator(DataSourceOperator entity);

        void DeleteDataSourceOperator(IList<int> idList);

        void DeleteDataSourceOperator(IList<DataSourceOperator> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        
        IList FindAllByDataSourceId(int dataSourceId);

        IList<DataSourceOperator> FindAllByDataSourceIdAndAllowType(int dsId, string type);
        
        void DeleteDataSourceOperatorByDSId(int dsId);

        #endregion Customized Methods
    }
}
