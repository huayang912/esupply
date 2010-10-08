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
    public interface IDWDataSourceDao
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

        IList<DWDataSource> FindDWDataSource(int userId);

        IList<DWDataSource> FindDWDataSourceByAllowType(int userId, string AllowType);

        IList<DWDataSource> FindDWDataSourceByTypeAndName(int userId, string type, string DWName);

        IList<string> FindDWDataSourceTypeList(int userId, string AllowType);

        IList<string> FindDWDataSourceTypeList(int userId);

        IList<string> FindAllDWDataSourceTypeList();

        IList<DWDataSource> FindDWDataSourceByTypeAndName(string type, string DWName);

        IList<DWDataSource> FindDWDataSourceByTypeAndName(string type, string DWName, User user, string allowType);

        #endregion Customized Methods
    }
}
