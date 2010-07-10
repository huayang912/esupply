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
    public class CubeMeasureMgr : SessionBase, ICubeMeasureMgr
    {
        private ICubeMeasureDao entityDao;
        
        public CubeMeasureMgr(ICubeMeasureDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeMeasure(CubeMeasure entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateCubeMeasure(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeMeasure LoadCubeMeasure(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadCubeMeasure(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeMeasure(CubeMeasure entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateCubeMeasure(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeMeasure(int id)
        {
            entityDao.DeleteCubeMeasure(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeMeasure(CubeMeasure entity)
        {
            entityDao.DeleteCubeMeasure(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeMeasure(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeMeasure(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeMeasure(IList<CubeMeasure> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeMeasure(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeMeasure> FindMeasureByCubeId(int cubeId)
        {
            return entityDao.FindMeasureByCubeId(cubeId);
        }

        public void DeleteCubeMeasureByCubeId(int cubeId)
        {
            entityDao.DeleteCubeMeasureByCubeId(cubeId);
        }

        #endregion Customized Methods
    }
}
