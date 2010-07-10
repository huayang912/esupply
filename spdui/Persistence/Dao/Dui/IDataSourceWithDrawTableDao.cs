using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDataSourceWithDrawTableDao
    {
        #region Method Created By CodeSmith

        void CreateDataSourceWithDrawTable(DataSourceWithDrawTable entity);

        DataSourceWithDrawTable LoadDataSourceWithDrawTable(int id);

        void UpdateDataSourceWithDrawTable(DataSourceWithDrawTable entity);
        
        void DeleteDataSourceWithDrawTable(int id);

        void DeleteDataSourceWithDrawTable(DataSourceWithDrawTable entity);

        void DeleteDataSourceWithDrawTable(IList<int> idList);

        void DeleteDataSourceWithDrawTable(IList<DataSourceWithDrawTable> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList FindAllByDataSourceId(int dataSourceId);

        void DeleteDataSourceWithDrawTableByDSId(int dsId);

        #endregion Customized Methods
    }
}
