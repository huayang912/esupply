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
    public class NHCubeParameterDao : NHDaoBase, ICubeParameterDao
    {
        public NHCubeParameterDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeParameter(CubeParameter entity)
        {
            Create(entity);
        }

        public CubeParameter LoadCubeParameter(int id)
        {
            return FindById(typeof(CubeParameter), id) as CubeParameter;
        }

        public void UpdateCubeParameter(CubeParameter entity)
        {
            Update(entity);
        }

        public void DeleteCubeParameter(int id)
        {
            string hql = @"from CubeParameter entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeParameter(CubeParameter entity)
        {
            Delete(entity);
        }

        public void DeleteCubeParameter(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeParameter entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeParameter(IList<CubeParameter> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeParameter entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeParameter(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeParameter> LoadAllActiveCubeParameter()
        {
            string hql = "from CubeParameter ds order by ds.Name";
            return FindAllWithCustomQuery(hql) as IList<CubeParameter>;
        }

        #endregion Customized Methods
    }
}
