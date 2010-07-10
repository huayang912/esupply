using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Security;
using Dndp.Persistence.Dao.Security;

namespace Dndp.Service.Cube.Impl
{
    [Transactional]
    public class CubeOperatorMgr : SessionBase, ICubeOperatorMgr
    {
        private ICubeOperatorDao entityDao;
        private ICubeDao cubeDao;
        private IUserDao userDao;
        
        public CubeOperatorMgr(ICubeOperatorDao entityDao, 
                                ICubeDao cubeDao,
                                IUserDao userDao)
        {
            this.entityDao = entityDao;
            this.cubeDao = cubeDao;
            this.userDao = userDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeOperator(CubeOperator entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateCubeOperator(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeOperator LoadCubeOperator(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadCubeOperator(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeOperator(CubeOperator entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateCubeOperator(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeOperator(int id)
        {
            entityDao.DeleteCubeOperator(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeOperator(CubeOperator entity)
        {
            entityDao.DeleteCubeOperator(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeOperator(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeOperator(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeOperator(IList<CubeOperator> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteCubeOperator(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<CubeOperator> FindOperatorByCubeIdAndAllowType(int cubeId, string type)
        {
            return entityDao.FindAllByCubeIdAndAllowType(cubeId, type);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<CubeOperator> FindOperatorByCubeId(int cubeId)
        {
            return entityDao.FindOperatorByCubeId(cubeId);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateCubeOperator(IList<int> userIdList, int cubeId, string allowType)
        {
            //delete original operator
            IList<CubeOperator> cubeOperatorList = FindOperatorByCubeIdAndAllowType(cubeId, allowType);

            if (cubeOperatorList != null && cubeOperatorList.Count > 0)
            {
                IList<int> cubeOperatorIdList = new List<int>();
                foreach (CubeOperator co in cubeOperatorList)
                {
                    cubeOperatorIdList.Add(co.Id);
                }

                this.DeleteCubeOperator(cubeOperatorIdList);
            }

            //update new operator
            CubeDefinition cube = cubeDao.LoadCube(cubeId);
            if (userIdList != null && userIdList.Count > 0)
            {
                foreach (int userId in userIdList)
                {
                    User user = userDao.LoadUser(userId);
                    CubeOperator co = new CubeOperator();
                    co.AllowType = allowType;
                    co.TheCube = cube;
                    co.TheUser = user;

                    entityDao.CreateCubeOperator(co);
                }
            }
        }

        #endregion Customized Methods
    }
}
