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
    public class AppUserBaseMgr : SessionBase, IAppUserBaseMgr
    {
        public IAppUserDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateAppUser(AppUser entity)
        {
            entityDao.CreateAppUser(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual AppUser LoadAppUser(String code)
        {
            return entityDao.LoadAppUser(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<AppUser> GetAllAppUser()
        {
            return entityDao.GetAllAppUser(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<AppUser> GetAllAppUser(bool includeInactive)
        {
            return entityDao.GetAllAppUser(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateAppUser(AppUser entity)
        {
            entityDao.UpdateAppUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUser(String code)
        {
            entityDao.DeleteAppUser(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUser(AppUser entity)
        {
            entityDao.DeleteAppUser(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUser(IList<String> pkList)
        {
            entityDao.DeleteAppUser(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAppUser(IList<AppUser> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteAppUser(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
