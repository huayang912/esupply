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
    public class NHCubeUserDao : NHDaoBase, ICubeUserDao
    {
        public NHCubeUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeUser(CubeUser entity)
        {
            Create(entity);
        }

        public CubeUser LoadCubeUser(int id)
        {
            return FindById(typeof(CubeUser), id) as CubeUser;
        }

        public void UpdateCubeUser(CubeUser entity)
        {
            Update(entity);
        }

        public void DeleteCubeUser(int id)
        {
            string hql = @"from CubeUser entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeUser(CubeUser entity)
        {
            Delete(entity);
        }

        public void DeleteCubeUser(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeUser entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeUser(IList<CubeUser> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeUser entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeUser(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList LoadAllActiveCubeUser()
        {
            return FindAllWithCustomQuery("from CubeUser as ds where ds.ActiveFlag=1 and ds.TheDistributionUser.ActiveFlag=1 and (ds.TheDistributionUser.IsOnlineCubeUser = 1 or ds.TheDistributionUser.IsOfflineCubeUser = 1) order by ds.Name");
        }

        public IList<CubeUser> FindUserForCubeDistribution(int Id)
        {
            //string hql = "Select distinct entity.TheDistributionUser from ReportUserSheet as entity where entity.TheReport.Id in "
            //           + " (select distinct rbr.TheReport.Id "
            //           + " from ReportBatchReports as rbr"
            //           + " where rbr.TheReportBatch.Id = ? "
            //           + " ) and entity.TheDistributionUser.ActiveFlag=1and entity.TheDistributionUser.TheDistributionUser.ActiveFlag=1 and entity.TheDistributionUser.TheDistributionUser.IsReportUser = 1 order by entity.TheDistributionUser.Name";

            //IList<ReportUser> list = FindAllWithCustomQuery(
            //    hql, new object[] { Id },
            //    new IType[] { NHibernateUtil.Int32 }) as IList<ReportUser>;

            //return list;
            return null;
        }

        public IList<CubeUser> FindCubeUserByName(string userName)
        {
            string hql = "from CubeUser entity where (entity.Name like ? or entity.Description like ?) and entity.ActiveFlag=1 and entity.TheDistributionUser.ActiveFlag=1 and (entity.TheDistributionUser.IsOnlineCubeUser = 1 or entity.TheDistributionUser.IsOfflineCubeUser = 1) order by entity.Name ";

            IList<CubeUser> list = FindAllWithCustomQuery(
                hql, new object[] { "%" + userName + "%", "%" + userName + "%" },
                new IType[] { NHibernateUtil.String, NHibernateUtil.String }) as IList<CubeUser>;

            return list;
        }

        public IList<CubeUser> FindUserExcludeSpecifiedRoleByNameAndDescription(int roleId, string name, string description)
        {
            string hql = @"from CubeUser entity where entity.Name like ? and entity.Description like ?
                and entity.Id not in (select cur.TheCubeUser.Id from CubeUserRole as cur where cur.TheCubeRole.Id = ?)
                and entity.ActiveFlag=1 and entity.TheDistributionUser.ActiveFlag=1 
                and (entity.TheDistributionUser.IsOnlineCubeUser = 1 or entity.TheDistributionUser.IsOfflineCubeUser = 1) 
                order by entity.Name ";

            IList<CubeUser> list = FindAllWithCustomQuery(
                hql, new object[] { "%" + name + "%", "%" + description + "%", roleId },
                new IType[] { NHibernateUtil.String, NHibernateUtil.String, NHibernateUtil.Int32 }) as IList<CubeUser>;

            return list;
        }

        public IList<CubeUser> FindCubeUserByRoleId(int roleId)
        {
            string hql = @"select entity.TheCubeUser from CubeUserRole entity 
                                               where entity.TheCubeRole.Id = ? 
                                                 and entity.TheCubeUser.ActiveFlag=1 
                                                 and entity.TheCubeUser.TheDistributionUser.ActiveFlag=1 
                                                 and (entity.TheCubeUser.TheDistributionUser.IsOnlineCubeUser = 1 
                                                or entity.TheCubeUser.TheDistributionUser.IsOfflineCubeUser = 1) order by entity.TheCubeUser.Name ";

            IList<CubeUser> list = FindAllWithCustomQuery(
                hql, new object[] { roleId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeUser>;

            return list;
        }

        public IList<CubeUser> FindCubeUserByCubeIdAndUserNameAndUserDescription(int cubeId, string userName, string userDescription)
        {
            string hql = @"select distinct entity.TheCubeUser from CubeUserRole as entity 
                            where entity.TheCubeRole.TheCube.Id = ?
                              and entity.TheCubeUser.Name like ?
                              and entity.TheCubeUser.Description like ?
                              and entity.TheCubeUser.ActiveFlag = 1
                              and entity.TheCubeUser.TheDistributionUser.ActiveFlag = 1
                              and entity.TheCubeUser.TheDistributionUser.IsOfflineCubeUser = 1
                              order by entity.TheCubeUser.Name ";

            IList<CubeUser> list = FindAllWithCustomQuery(
                hql, new object[] { cubeId, "%" + userName + "%", "%" + userDescription + "%" },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String, NHibernateUtil.String }) as IList<CubeUser>;

            return list;
        }

        #endregion Customized Methods
    }
}
