using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.MasterData;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Entity.Exception;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class AppUserMgr : AppUserBaseMgr, IAppUserMgr
    {
        public IAppUserPermissionMgrE appUserPermissionMgrE { get; set; }
        #region Customized Methods

        //TODO: Add other methods here.
        [Transaction(TransactionMode.Requires)]
        public bool HasPermission(string userCode, string permissionCode)
        {
            IList<AppUserPermission> appUserPermissions = appUserPermissionMgrE.GetAppUserPermission(userCode);

            foreach (AppUserPermission p in appUserPermissions)
            {
                if (p.AppPermission == permissionCode)
                {
                    return true;
                }
            }
            return false;
        }

        [Transaction(TransactionMode.Unspecified)]
        public AppUser CheckAndLoadAppUser(string userCode)
        {
            AppUser user = this.LoadAppUser(userCode, true);
            if (user == null)
            {
                throw new BusinessErrorException("Security.Error.UserCodeNotExist", userCode);
            }

            return user;
        }

        [Transaction(TransactionMode.Unspecified)]
        public AppUser LoadAppUser(string userCode, bool isLoadPermission)
        {
            AppUser user = entityDao.LoadAppUser(userCode);
            if (user != null && isLoadPermission)
            {
                user.AppUserPermissions = appUserPermissionMgrE.GetAppUserPermission(userCode).ToList();
            }
            return user;
        }
        
        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.MasterData.Impl
{
    [Transactional]
    public partial class AppUserMgrE : com.LocalSystem.Service.MasterData.Impl.AppUserMgr, IAppUserMgrE
    {
    }
}

#endregion Extend Class