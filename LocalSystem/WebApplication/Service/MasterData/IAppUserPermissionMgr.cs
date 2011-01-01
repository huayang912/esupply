using System;
using com.LocalSystem.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface IAppUserPermissionMgr : IAppUserPermissionBaseMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.
        IList<AppUserPermission> GetAppUserPermission(string userCode);

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface IAppUserPermissionMgrE : com.LocalSystem.Service.MasterData.IAppUserPermissionMgr
    {
    }
}

#endregion Extend Interface