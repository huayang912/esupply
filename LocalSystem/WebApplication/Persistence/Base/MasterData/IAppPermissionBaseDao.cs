using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.LocalSystem.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.LocalSystem.Persistence.MasterData
{
    public interface IAppPermissionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateAppPermission(AppPermission entity);

        AppPermission LoadAppPermission(String code);
  
        IList<AppPermission> GetAllAppPermission();
  
        void UpdateAppPermission(AppPermission entity);
        
        void DeleteAppPermission(String code);
    
        void DeleteAppPermission(AppPermission entity);
    
        void DeleteAppPermission(IList<String> pkList);
    
        void DeleteAppPermission(IList<AppPermission> entityList);    
        #endregion Method Created By CodeSmith
    }
}
