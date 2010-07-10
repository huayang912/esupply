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
    public class NHCubeDefinedParameterDao : NHDaoBase, ICubeDefinedParameterDao
    {
        public NHCubeDefinedParameterDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeDefinedParameter(CubeDefinedParameter entity)
        {
            Create(entity);
        }

        public CubeDefinedParameter LoadCubeDefinedParameter(int id)
        {
            return FindById(typeof(CubeDefinedParameter), id) as CubeDefinedParameter;
        }

        public void UpdateCubeDefinedParameter(CubeDefinedParameter entity)
        {
            Update(entity);
        }

        public void DeleteCubeDefinedParameter(int id)
        {
            string hql = @"from CubeDefinedParameter entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeDefinedParameter(CubeDefinedParameter entity)
        {
            Delete(entity);
        }

        public void DeleteCubeDefinedParameter(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeDefinedParameter entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeDefinedParameter(IList<CubeDefinedParameter> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeDefinedParameter entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeDefinedParameter(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public void CreateCubeDefinedParameter(IList<CubeDefinedParameter> list)
        {
            foreach(CubeDefinedParameter entity in list)
            {
                Create(entity);
            }
        }

        public void DeleteCubeDefinedParameterByCubeId(int cubeId)
        {
            string hql = @"from CubeDefinedParameter entity where entity.TheCube.Id = ?";

            Delete(hql, cubeId, NHibernate.NHibernateUtil.Int32);
        }

        public IList<CubeDefinedParameter> FindCubeDefinedParameterByCubeId(int cubeId)
        {
            string hql = @"from CubeDefinedParameter entity where entity.TheCube.Id = ?";

            IList<CubeDefinedParameter> list = FindAllWithCustomQuery(hql, cubeId, NHibernate.NHibernateUtil.Int32) as IList<CubeDefinedParameter>;

            return list;
        }

        #endregion Customized Methods
    }
}
