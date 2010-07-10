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
    public class CubeDimensionMgr : SessionBase, ICubeDimensionMgr
    {
        private ICubeDimensionDao dimDao;
        private ICubeRoleDimensionMemberDao dimMemberDao;

        public CubeDimensionMgr(ICubeDimensionDao dimDao, 
                ICubeRoleDimensionMemberDao dimMemberDao)
        {
            this.dimDao = dimDao;
            this.dimMemberDao = dimMemberDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeDimension(CubeDimension entity)
        {
            //TODO: Add other code here.
			
            dimDao.CreateCubeDimension(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeDimension LoadCubeDimension(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return dimDao.LoadCubeDimension(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeDimension(CubeDimension entity)
        {
        	//TODO: Add other code here.
            dimDao.UpdateCubeDimension(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDimension(int id)
        {            
            dimMemberDao.DeleteCubeRoleDimensionMemberByDimId(id);
            dimDao.DeleteCubeDimension(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDimension(CubeDimension entity)
        {
            dimMemberDao.DeleteCubeRoleDimensionMemberByDimId(entity.Id);
            dimDao.DeleteCubeDimension(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDimension(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            foreach(int id in idList)
            {
                DeleteCubeDimension(id);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDimension(IList<CubeDimension> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            foreach (CubeDimension entity in entityList)
            {
                DeleteCubeDimension(entity);
            }
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeDimension> FindDimensionByCubeId(int cubeId)
        {
            return dimDao.FindDimensionByCubeId(cubeId);
        }

        public void DeleteCubeDimensionByCubeId(int cubeId)
        {
            dimDao.DeleteCubeDimensionByCubeId(cubeId);
        }

        #endregion Customized Methods
    }
}
