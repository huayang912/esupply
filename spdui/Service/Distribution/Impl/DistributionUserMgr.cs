using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Distribution;
using Dndp.Persistence.Dao.Distribution;
//TODO: Add other using statements here.

namespace Dndp.Service.Distribution.Impl
{
    [Transactional]
    public class DistributionUserMgr : SessionBase, IDistributionUserMgr
    {
        private IDistributionUserDao entityDao;
        
        public DistributionUserMgr(IDistributionUserDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateDistributionUser(DistributionUser entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateDistributionUser(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DistributionUser LoadDistributionUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadDistributionUser(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDistributionUser(DistributionUser entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateDistributionUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDistributionUser(int id)
        {
            DistributionUser entity = entityDao.LoadDistributionUser(id);
            entity.ActiveFlag = 0;
            entityDao.UpdateDistributionUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDistributionUser(DistributionUser entity)
        {
            entity.ActiveFlag = 0;
            entityDao.UpdateDistributionUser(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteDistributionUser(IList<int> idList)
        {
            foreach (int id in idList)
            {
                DistributionUser entity = entityDao.LoadDistributionUser(id);
                entity.ActiveFlag = 0;
                entityDao.UpdateDistributionUser(entity);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDistributionUser(IList<DistributionUser> entityList)
        {
            foreach (DistributionUser entity in entityList)
            {
                entity.ActiveFlag = 0;
                entityDao.UpdateDistributionUser(entity);
            }
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<DistributionUser> FindDistributionUserByName(string name)
        {
            return entityDao.FindDistributionUserByName(name);
        }

        public IList<DistributionUser> FindDistributionUserByName(string name, bool isOfflineReportUser, bool isOfflineCubeUser, bool isOnlineCubeUser)
        {
            return entityDao.FindDistributionUserByName(name, isOfflineReportUser, isOfflineCubeUser, isOnlineCubeUser);
        }

        public IList<DistributionUser> LoadAllActiveDistributionUser()
        {
            return entityDao.LoadAllActiveDistributionUser();
        }

        public IList<DistributionUser> LoadAllActiveDistributionUser(bool isOfflineReportUser, bool isOfflineCubeUser, bool isOnlineCubeUser)
        {
            return entityDao.LoadAllActiveDistributionUser(isOfflineReportUser, isOfflineCubeUser, isOnlineCubeUser);
        }

        #endregion Customized Methods
    }
}
