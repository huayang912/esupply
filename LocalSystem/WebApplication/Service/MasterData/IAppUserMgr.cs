using System;
using com.LocalSystem.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface IAppUserMgr : IAppUserBaseMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.
        bool HasPermission(string userCode, string permissionCode);
        AppUser CheckAndLoadAppUser(string userCode);
        AppUser LoadAppUser(string userCode, bool isLoadPermission);

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface IAppUserMgrE : com.LocalSystem.Service.MasterData.IAppUserMgr
    {
    }
}

#endregion Extend Interface