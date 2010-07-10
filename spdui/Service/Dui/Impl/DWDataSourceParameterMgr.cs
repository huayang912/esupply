using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Dui;
using Dndp.Persistence.Dao.Dui;
//TODO: Add other using statements here.

namespace Dndp.Service.Dui.Impl
{
    [Transactional]
    public class DWDataSourceParameterMgr : SessionBase, IDWDataSourceParameterMgr
    {
        private IDWDataSourceParameterDao entityDao;
        
        public DWDataSourceParameterMgr(IDWDataSourceParameterDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateDWDataSourceParameter(DWDataSourceParameter entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateDWDataSourceParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DWDataSourceParameter LoadDWDataSourceParameter(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadDWDataSourceParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDWDataSourceParameter(DWDataSourceParameter entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateDWDataSourceParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSourceParameter(int id)
        {
            entityDao.DeleteDWDataSourceParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSourceParameter(DWDataSourceParameter entity)
        {
            entityDao.DeleteDWDataSourceParameter(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSourceParameter(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteDWDataSourceParameter(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteDWDataSourceParameter(IList<DWDataSourceParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteDWDataSourceParameter(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<DWDataSourceParameter> LoadAllActiveDWDataSourceParameter()
        {
            return entityDao.LoadAllActiveDWDataSourceParameter();
        }

        #endregion Customized Methods
    }
}
