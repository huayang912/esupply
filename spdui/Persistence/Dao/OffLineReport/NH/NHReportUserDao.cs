using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.Facilities.NHibernateIntegration;
using System.Collections;
using NHibernate.Type;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Persistence.Dao.OffLineReport;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.OffLineReport.NH
{
    public class NHReportUserDao : NHDaoBase, IReportUserDao
    {
        public NHReportUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportUser(ReportUser entity)
        {
            Create(entity);
        }

        public ReportUser LoadReportUser(int id)
        {
            return FindById(typeof(ReportUser), id) as ReportUser;
        }

        public void UpdateReportUser(ReportUser entity)
        {
            Update(entity);
        }

        public void DeleteReportUser(int id)
        {
            string hql = @"from ReportUser entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportUser(ReportUser entity)
        {
            Delete(entity);
        }

        public void DeleteReportUser(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportUser entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportUser(IList<ReportUser> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportUser entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportUser(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList LoadAllActiveReportUser()
        {
            return FindAllWithCustomQuery("from ReportUser ds where ds.ActiveFlag=1 and ds.TheUser.ActiveFlag=1 and ds.TheUser.IsReportUser = 1 order by ds.Name");
        }

        public IList<ReportUser> FindUserForReportBatch(int Id)
        {
            string hql = "Select distinct entity.TheUser from ReportUserSheet as entity where entity.TheReport.Id in "
                       + " (select distinct rbr.TheReport.Id "
                       + " from ReportBatchReports as rbr"
                       + " where rbr.TheReportBatch.Id = ? "
                       + " ) and entity.TheUser.ActiveFlag=1and entity.TheUser.TheUser.ActiveFlag=1 and entity.TheUser.TheUser.IsReportUser = 1 order by entity.TheUser.Name";

            IList<ReportUser> list = FindAllWithCustomQuery(
                hql, new object[] { Id },
                new IType[] { NHibernateUtil.Int32 }) as IList<ReportUser>;

            return list;
        }

        public IList<ReportUser> FindReportUserByName(string userName)
        {
            string hql = @"from ReportUser entity where (entity.Name like ? or entity.Description like ?)
                        and entity.ActiveFlag=1 and entity.TheUser.ActiveFlag=1 and entity.TheUser.IsReportUser = 1 order by entity.Name ";

            IList<ReportUser> list = FindAllWithCustomQuery(
                hql, new object[] { "%" + userName + "%", "%" + userName + "%" },
                new IType[] { NHibernateUtil.String, NHibernateUtil.String }) as IList<ReportUser>;

            return list;
        }

        #endregion Customized Methods
    }
}
