using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDataSourceUploadDao
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

        IList<DataSourceUpload> FindLastestDSUpload(int userId, string allowType);

        DataSourceUpload FindLastestDSUpload(int dsId);

        IList<DataSourceUpload> FindDataSourceUpload(int dsId);

        int GenerateBatchNo(int dsCategoryId);

        void DeleteDataSourceUploadByDSId(int dsId);

        IList<DataSourceUpload> FindDataSourceUploadForETL();

        IList<DataSourceUpload> FindDataSourceUploadInETL();

        IList<DataSourceUpload> FindDataSourceUpload(IList<int> idList);

        #endregion Customized Methods
    }
}
