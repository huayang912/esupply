using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDataSourceFieldDao
    {
        #region Method Created By CodeSmith

        void CreateDataSourceField(DataSourceField entity);

        DataSourceField LoadDataSourceField(int id);

        void UpdateDataSourceField(DataSourceField entity);
        
        void DeleteDataSourceField(int id);

        void DeleteDataSourceField(DataSourceField entity);

        void DeleteDataSourceField(IList<int> idList);

        void DeleteDataSourceField(IList<DataSourceField> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList FindAllByDataSourceId(int dataSourceId);

        int GetMaxSequenceNo(int dataSourceId);

        bool HasField(int dsId, string newFieldNm);

        void DeleteDataSourceFieldByDSId(int dsId);

        #endregion Customized Methods
    }
}
