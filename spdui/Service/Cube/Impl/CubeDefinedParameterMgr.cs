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
    public class CubeDefinedParameterMgr : SessionBase, ICubeDefinedParameterMgr
    {
        private ICubeDefinedParameterDao entityDao;
        
        public CubeDefinedParameterMgr(ICubeDefinedParameterDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeDefinedParameter(CubeDefinedParameter entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateCubeDefinedParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeDefinedParameter LoadCubeDefinedParameter(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadCubeDefinedParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeDefinedParameter(CubeDefinedParameter entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateCubeDefinedParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDefinedParameter(int id)
        {
            entityDao.DeleteCubeDefinedParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDefinedParameter(CubeDefinedParameter entity)
        {
            entityDao.DeleteCubeDefinedParameter(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDefinedParameter(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeDefinedParameter(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDefinedParameter(IList<CubeDefinedParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeDefinedParameter(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeDefinedParameter> FindCubeDefinedParameterByCubeId(int cubeId)
        {
            return entityDao.FindCubeDefinedParameterByCubeId(cubeId);
        }

        #endregion Customized Methods
    }
}
