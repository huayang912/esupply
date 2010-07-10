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
    public class CubeParameterMgr : SessionBase, ICubeParameterMgr
    {
        private ICubeParameterDao entityDao;
        
        public CubeParameterMgr(ICubeParameterDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeParameter(CubeParameter entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateCubeParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeParameter LoadCubeParameter(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadCubeParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeParameter(CubeParameter entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateCubeParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeParameter(int id)
        {
            entityDao.DeleteCubeParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeParameter(CubeParameter entity)
        {
            entityDao.DeleteCubeParameter(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeParameter(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeParameter(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeParameter(IList<CubeParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeParameter(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public IList<CubeParameter> LoadAllActiveCubeParameter()
        {
            return entityDao.LoadAllActiveCubeParameter();
        }


        #endregion Customized Methods
    }
}
