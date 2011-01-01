using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class AppPermissionMgr : AppPermissionBaseMgr, IAppPermissionMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.MasterData.Impl
{
    [Transactional]
    public partial class AppPermissionMgrE : com.LocalSystem.Service.MasterData.Impl.AppPermissionMgr, IAppPermissionMgrE
    {
    }
}

#endregion Extend Class