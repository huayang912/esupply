using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube.Impl
{
    [Transactional]
    public class CubeValidationRuleMgr : SessionBase, ICubeValidationRuleMgr
    {
        private ICubeValidationRuleDao entityDao;
        
        public CubeValidationRuleMgr(ICubeValidationRuleDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeValidationRule(CubeValidationRule entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateCubeValidationRule(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeValidationRule LoadCubeValidationRule(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadCubeValidationRule(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeValidationRule(CubeValidationRule entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateCubeValidationRule(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeValidationRule(int id)
        {
            entityDao.DeleteCubeValidationRule(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeValidationRule(CubeValidationRule entity)
        {
            entityDao.DeleteCubeValidationRule(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeValidationRule(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeValidationRule(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeValidationRule(IList<CubeValidationRule> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeValidationRule(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeValidationRule> FindCubeValidationRuleWithCubeId(int id)
        {
            return entityDao.FindCubeValidationRuleWithCubeId(id);
        }

        #endregion Customized Methods
    }
}
