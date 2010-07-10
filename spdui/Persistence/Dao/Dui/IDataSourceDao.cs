using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDataSourceDao
    {
        #region Method Created By CodeSmith

        void CreateDataSource(DataSource entity);

        DataSource LoadDataSource(int id);

        void UpdateDataSource(DataSource entity);
        
        void DeleteDataSource(int id);

        void DeleteDataSource(DataSource entity);

        void DeleteDataSource(IList<int> idList);

        void DeleteDataSource(IList<DataSource> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList LoadAllActiveDataSource();

        void InactivateDataSource(IList<int> idList);

        IList<DataSource> FindActiveDataSourceByTypeAndName(string type, string name);

        IList<string> FindAllDataSourceType();

        #endregion Customized Methods
    }
}
