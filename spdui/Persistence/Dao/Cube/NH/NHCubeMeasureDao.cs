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
    public class NHCubeMeasureDao : NHDaoBase, ICubeMeasureDao
    {
        public NHCubeMeasureDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeMeasure(CubeMeasure entity)
        {
            Create(entity);
        }

        public CubeMeasure LoadCubeMeasure(int id)
        {
            return FindById(typeof(CubeMeasure), id) as CubeMeasure;
        }

        public void UpdateCubeMeasure(CubeMeasure entity)
        {
            Update(entity);
        }

        public void DeleteCubeMeasure(int id)
        {
            string hql = @"from CubeMeasure entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeMeasure(CubeMeasure entity)
        {
            Delete(entity);
        }

        public void DeleteCubeMeasure(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeMeasure entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeMeasure(IList<CubeMeasure> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeMeasure entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeMeasure(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeMeasure> FindMeasureByCubeId(int cubeId)
        {
            string hql = "from CubeMeasure entity where entity.TheCube.Id = ? order by entity.DisplayName ";

            return FindAllWithCustomQuery(hql,
                new object[] { cubeId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeMeasure>;
        }

        public void DeleteCubeMeasureByCubeId(int cubeId)
        {
            string hql = @"from CubeMeasure entity where entity.TheCube.Id = ?";

            Delete(hql, cubeId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
