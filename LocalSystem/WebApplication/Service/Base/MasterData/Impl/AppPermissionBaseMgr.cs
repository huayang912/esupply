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
    public class AppPermissionBaseMgr : SessionBase, IAppPermissionBaseMgr
    {
        public IAppPermissionDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateAppPermission(AppPermission entity)
        {
            entityDao.CreateAppPermission(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual AppPermission LoadAppPermission(String code)
        {
            return entityDao.LoadAppPermission(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<AppPermission> GetAllAppPermission()
        {
            return entityDao.GetAllAppPermission();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateAppPermission(AppPermission entity)
        {
            entityDao.UpdateAppPermission(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppPermission(String code)
        {
            entityDao.DeleteAppPermission(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppPermission(AppPermission entity)
        {
            entityDao.DeleteAppPermission(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppPermission(IList<String> pkList)
        {
            entityDao.DeleteAppPermission(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppPermission(IList<AppPermission> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteAppPermission(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
