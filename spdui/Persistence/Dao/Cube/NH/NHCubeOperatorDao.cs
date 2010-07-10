using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.Facilities.NHibernateIntegration;
using System.Collections;
using NHibernate.Type;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.Cube.NH
{
    public class NHCubeOperatorDao : NHDaoBase, ICubeOperatorDao
    {
        public NHCubeOperatorDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeOperator(CubeOperator entity)
        {
            Create(entity);
        }

        public CubeOperator LoadCubeOperator(int id)
        {
            return FindById(typeof(CubeOperator), id) as CubeOperator;
        }

        public void UpdateCubeOperator(CubeOperator entity)
        {
            Update(entity);
        }

        public void DeleteCubeOperator(int id)
        {
            string hql = @"from CubeOperator entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeOperator(CubeOperator entity)
        {
            Delete(entity);
        }

        public void DeleteCubeOperator(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeOperator entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeOperator(IList<CubeOperator> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeOperator entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeOperator(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeOperator> FindAllByCubeIdAndAllowType(int cubeId, string type)
        {
            string hql = "select co from CubeOperator co where co.TheCube.Id = ? and co.AllowType = ? ";

            return FindAllWithCustomQuery(hql,
                new object[] { cubeId, type },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String }) as IList<CubeOperator>;
        }

        public IList<CubeOperator> FindOperatorByCubeId(int cubeId)
        {
            string hql = "select co from CubeOperator co where co.TheCube.Id = ? order by co.AllowType ";

            return FindAllWithCustomQuery(hql,
                new object[] { cubeId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeOperator>;
        }


        public void DeleteCubeOperatorByCubeId(int cubeId)
        {
            string hql = @"from CubeOperator entity where entity.TheCube.Id = ?";

            Delete(hql, cubeId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
