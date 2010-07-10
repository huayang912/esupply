using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.Facilities.NHibernateIntegration;
using System.Collections;
using NHibernate.Type;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Distribution;
using Dndp.Persistence.Dao.Distribution;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.Distribution.NH
{
    public class NHDistributionUserDao : NHDaoBase, IDistributionUserDao
    {
        public NHDistributionUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDistributionUser(DistributionUser entity)
        {
            Create(entity);
        }

        public DistributionUser LoadDistributionUser(int id)
        {
            return FindById(typeof(DistributionUser), id) as DistributionUser;
        }

        public void UpdateDistributionUser(DistributionUser entity)
        {
            Update(entity);
        }

        public void DeleteDistributionUser(int id)
        {
            string hql = @"from DistributionUser entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDistributionUser(DistributionUser entity)
        {
            Delete(entity);
        }

        public void DeleteDistributionUser(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DistributionUser entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDistributionUser(IList<DistributionUser> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DistributionUser entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDistributionUser(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<DistributionUser> FindDistributionUserByName(string name)
        {
            string hql = "from DistributionUser entity where entity.Name like ? and entity.ActiveFlag = 1";

            IList<DistributionUser> list = FindAllWithCustomQuery(
                hql, new object[] { "%" + name + "%" },
                new IType[] { NHibernateUtil.String }) as IList<DistributionUser>;

            return list;
        }

        public IList<DistributionUser> FindDistributionUserByName(string name, bool isOfflineReportUser, bool isOfflineCubeUser, bool isOnlineCubeUser)
        {
            string hql = "from DistributionUser entity where entity.Name like ? and (entity.IsReportUser = ? or entity.IsOfflineCubeUser = ? or entity.IsOnlineCubeUser = ?) and entity.ActiveFlag = 1";

            IList<DistributionUser> list = FindAllWithCustomQuery(
                hql, new object[] { "%" + name + "%", isOfflineReportUser ? 1 : 0, isOfflineCubeUser ? 1 : 0, isOnlineCubeUser ? 1 : 0 },
                new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.Int32, NHibernateUtil.Int32 }) as IList<DistributionUser>;

            return list;
        }

        public IList<DistributionUser> LoadAllActiveDistributionUser()
        {
            string hql = "from DistributionUser entity where entity.ActiveFlag = 1";

            IList<DistributionUser> list = FindAllWithCustomQuery(hql) as IList<DistributionUser>;

            return list;
        }

        public IList<DistributionUser> LoadAllActiveDistributionUser(bool isOfflineReportUser, bool isOfflineCubeUser, bool isOnlineCubeUser)
        {
            string hql = "from DistributionUser entity where (entity.IsReportUser = ? or entity.IsOfflineCubeUser = ? or entity.IsOnlineCubeUser = ?) and entity.ActiveFlag = 1";

            IList<DistributionUser> list = FindAllWithCustomQuery(
                hql, new object[] { isOfflineReportUser ? 1 : 0, isOfflineCubeUser ? 1 : 0, isOnlineCubeUser ? 1 : 0 },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32, NHibernateUtil.Int32 }) as IList<DistributionUser>;

            return list;
        }

        #endregion Customized Methods
    }
}
