using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.MasterData;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class AppUserPermissionMgr : AppUserPermissionBaseMgr, IAppUserPermissionMgr
    {
        public ICriteriaMgrE criteriaMgrE { get; set; }

        #region Customized Methods

        //TODO: Add other methods here.
        public IList<AppUserPermission> GetAppUserPermission(string userCode)
        {

            DetachedCriteria criteria = DetachedCriteria.For(typeof(AppUserPermission))
                .Add(Expression.Eq("AppUser", userCode));

            return criteriaMgrE.FindAll<AppUserPermission>(criteria);
        }
        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.MasterData.Impl
{
    [Transactional]
    public partial class AppUserPermissionMgrE : com.LocalSystem.Service.MasterData.Impl.AppUserPermissionMgr, IAppUserPermissionMgrE
    {
    }
}

#endregion Extend Class