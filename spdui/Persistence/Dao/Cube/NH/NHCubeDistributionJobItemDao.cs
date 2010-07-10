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
    public class NHCubeDistributionJobItemDao : NHDaoBase, ICubeDistributionJobItemDao
    {
        public NHCubeDistributionJobItemDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeDistributionJobItem(CubeDistributionJobItem entity)
        {
            Create(entity);
        }

        public CubeDistributionJobItem LoadCubeDistributionJobItem(int id)
        {
            return FindById(typeof(CubeDistributionJobItem), id) as CubeDistributionJobItem;
        }

        public void UpdateCubeDistributionJobItem(CubeDistributionJobItem entity)
        {
            Update(entity);
        }

        public void DeleteCubeDistributionJobItem(int id)
        {
            string hql = @"from CubeDistributionJobItem entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeDistributionJobItem(CubeDistributionJobItem entity)
        {
            Delete(entity);
        }

        public void DeleteCubeDistributionJobItem(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeDistributionJobItem entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeDistributionJobItem(IList<CubeDistributionJobItem> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeDistributionJobItem entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeDistributionJobItem(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeUser> FindCubeUserByCubeDistributionJobId(int jobId)
        {
            string hql = "select entity.TheCubeUser from CubeDistributionJobItem entity where entity.TheJob.Id = ? order by entity.TheCubeUser.Name ";

            return FindAllWithCustomQuery(hql,
                new object[] { jobId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeUser>;
        }

        public IList<CubeDistributionJobItem> FindCubeDistributionJobItemByCubeDistributionJobId(int jobId)
        {
            string hql = "select entity from CubeDistributionJobItem entity where entity.TheJob.Id = ? order by entity.TheCubeUser.Name ";

            return FindAllWithCustomQuery(hql,
                new object[] { jobId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeDistributionJobItem>;
        }

        public void DeleteCubeDistributionJobItemByJobId(int jobId)
        {
            string hql = "from CubeDistributionJobItem entity where entity.TheJob.Id = ? ";

            Delete(hql,
                new object[] { jobId },
                new IType[] { NHibernateUtil.Int32 });
        }

        #endregion Customized Methods
    }
}
