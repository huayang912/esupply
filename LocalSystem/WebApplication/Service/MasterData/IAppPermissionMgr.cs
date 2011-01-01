using System;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface IAppPermissionMgr : IAppPermissionBaseMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface IAppPermissionMgrE : com.LocalSystem.Service.MasterData.IAppPermissionMgr
    {
    }
}

#endregion Extend Interface