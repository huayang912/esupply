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
    public class NHCubeDistributionJobDao : NHDaoBase, ICubeDistributionJobDao
    {
        public NHCubeDistributionJobDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeDistributionJob(CubeDistributionJob entity)
        {
            Create(entity);
        }

        public CubeDistributionJob LoadCubeDistributionJob(int id)
        {
            return FindById(typeof(CubeDistributionJob), id) as CubeDistributionJob;
        }

        public void UpdateCubeDistributionJob(CubeDistributionJob entity)
        {
            Update(entity);
        }

        public void DeleteCubeDistributionJob(int id)
        {
            string hql = @"from CubeDistributionJob entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeDistributionJob(CubeDistributionJob entity)
        {
            Delete(entity);
        }

        public void DeleteCubeDistributionJob(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeDistributionJob entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeDistributionJob(IList<CubeDistributionJob> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeDistributionJob entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeDistributionJob(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeDistributionJob> FindAllLastestCubeDistributionJob()
        {
            string hql = @" from CubeDistributionJob as entity
            where entity.Id in (select max(job.Id) from CubeDistributionJob job where job.TheCube.ActiveFlag = 1 group by job.TheCube)                 
                and entity.TheCube.ActiveFlag = 1";

            IList<CubeDistributionJob> list = FindAllWithCustomQuery(hql) as IList<CubeDistributionJob>;

            return list;
        }

        public CubeDistributionJob FindLastestCubeDistributionJobByCubeId(int cubeId)
        {
            string hql = @" from CubeDistributionJob as entity 
                   where entity.Id in (select max(job.Id) from CubeDistributionJob job where job.TheCube.ActiveFlag = 1 and job.TheCube.Id = ? group by job.TheCube)                    
                    and entity.TheCube.ActiveFlag = 1 ";

            IList<CubeDistributionJob> list = FindAllWithCustomQuery(
                hql,
                new object[] { cubeId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeDistributionJob>;

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public IList<CubeDistributionJob> FindAllCubeDistributionJobByCubeId(int cubeId)
        {
            return FindAllWithCustomQuery(
               @"from CubeDistributionJob as entity 
                where entity.TheCube.Id = ? 
                and entity.TheCube.ActiveFlag = 1                 
                 order by entity.CreateDate Desc",
               new object[] { cubeId },
               new IType[] { NHibernateUtil.Int32 }) as IList<CubeDistributionJob>;
        }

        #endregion Customized Methods
    }
}
