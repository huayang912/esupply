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
    public class CubeWarmMDXMgr : SessionBase, ICubeWarmMDXMgr
    {
        private ICubeWarmMDXDao entityDao;
        
        public CubeWarmMDXMgr(ICubeWarmMDXDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeWarmMDX(CubeWarmMDX entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateCubeWarmMDX(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeWarmMDX LoadCubeWarmMDX(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadCubeWarmMDX(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeWarmMDX(CubeWarmMDX entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateCubeWarmMDX(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeWarmMDX(int id)
        {
            entityDao.DeleteCubeWarmMDX(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeWarmMDX(CubeWarmMDX entity)
        {
            entityDao.DeleteCubeWarmMDX(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeWarmMDX(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeWarmMDX(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeWarmMDX(IList<CubeWarmMDX> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeWarmMDX(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeWarmMDX> FindCubeWarmMDXByCubeId(int cubeId)
        {
           return entityDao.FindCubeWarmMDXByCubeId(cubeId);
        }

        public void DeleteCubeWarmMDXByCubeId(int cubeId)
        {
            entityDao.DeleteCubeWarmMDX(cubeId);
        }

        #endregion Customized Methods
    }
}
