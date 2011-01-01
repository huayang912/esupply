using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.LocalSystem.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface IAppUserPermissionBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateAppUserPermission(AppUserPermission entity);

        AppUserPermission LoadAppUserPermission(Int32 id);

        IList<AppUserPermission> GetAllAppUserPermission();
    
        void UpdateAppUserPermission(AppUserPermission entity);

        void DeleteAppUserPermission(Int32 id);
    
        void DeleteAppUserPermission(AppUserPermission entity);
    
        void DeleteAppUserPermission(IList<Int32> pkList);
    
        void DeleteAppUserPermission(IList<AppUserPermission> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
