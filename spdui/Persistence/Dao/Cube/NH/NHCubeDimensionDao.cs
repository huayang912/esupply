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
    public class NHCubeDimensionDao : NHDaoBase, ICubeDimensionDao
    {
        public NHCubeDimensionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeDimension(CubeDimension entity)
        {
            Create(entity);
        }

        public CubeDimension LoadCubeDimension(int id)
        {
            return FindById(typeof(CubeDimension), id) as CubeDimension;
        }

        public void UpdateCubeDimension(CubeDimension entity)
        {
            Update(entity);
        }

        public void DeleteCubeDimension(int id)
        {
            string hql = @"from CubeDimension entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeDimension(CubeDimension entity)
        {
            Delete(entity);
        }

        public void DeleteCubeDimension(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeDimension entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeDimension(IList<CubeDimension> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeDimension entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeDimension(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeDimension> FindDimensionByCubeId(int cubeId)
        {
            string hql = "select cd from CubeDimension cd where cd.TheCube.Id = ? order by cd.DimensionName, cd.AttributeName ";

            return FindAllWithCustomQuery(hql,
                new object[] { cubeId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeDimension>;
        }

        public IList<CubeDimension> FindDimensionByDimensionNameAndAttributeName(string dimensionName, string attributeName)
        {
            string hql = "select cd from CubeDimension cd where cd.DimensionName = ? and cd.AttributeName = ? order by cd.DimensionName, cd.AttributeName ";

            return FindAllWithCustomQuery(hql,
                new object[] { dimensionName, attributeName },
                new IType[] { NHibernateUtil.String, NHibernateUtil.String }) as IList<CubeDimension>;
        }

        public void DeleteCubeDimensionByCubeId(int cubeId)
        {
            string hql = @"from CubeDimension entity where entity.TheCube.Id = ?";

            Delete(hql, cubeId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
