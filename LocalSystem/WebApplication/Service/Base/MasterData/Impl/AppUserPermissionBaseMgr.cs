using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class AppUserPermissionBaseMgr : SessionBase, IAppUserPermissionBaseMgr
    {
        public IAppUserPermissionDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateAppUserPermission(AppUserPermission entity)
        {
            entityDao.CreateAppUserPermission(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual AppUserPermission LoadAppUserPermission(Int32 id)
        {
            return entityDao.LoadAppUserPermission(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<AppUserPermission> GetAllAppUserPermission()
        {
            return entityDao.GetAllAppUserPermission();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateAppUserPermission(AppUserPermission entity)
        {
            entityDao.UpdateAppUserPermission(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUserPermission(Int32 id)
        {
            entityDao.DeleteAppUserPermission(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUserPermission(AppUserPermission entity)
        {
            entityDao.DeleteAppUserPermission(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUserPermission(IList<Int32> pkList)
        {
            entityDao.DeleteAppUserPermission(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUserPermission(IList<AppUserPermission> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteAppUserPermission(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
