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
    public class NHReportJobUserDao : NHDaoBase, IReportJobUserDao
    {
        public NHReportJobUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportJobUser(ReportJobUser entity)
        {
            Create(entity);
        }

        public ReportJobUser LoadReportJobUser(int id)
        {
            return FindById(typeof(ReportJobUser), id) as ReportJobUser;
        }

        public void UpdateReportJobUser(ReportJobUser entity)
        {
            Update(entity);
        }

        public void DeleteReportJobUser(int id)
        {
            string hql = @"from ReportJobUser entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportJobUser(ReportJobUser entity)
        {
            Delete(entity);
        }

        public void DeleteReportJobUser(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportJobUser entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportJobUser(IList<ReportJobUser> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportJobUser entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportJobUser(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList FindAllByJobId(int Id)
        {
            return FindAllWithCustomQuery("from ReportJobUser as entity where entity.TheJob.Id = ? order by entity.TheUser.Name", Id);
        }

        public void DeleteAllByJobId(int Id)
        {
            string hql = @"from ReportJobUser as entity where entity.TheJob.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        public IList<ReportUser> FindReportJobUserByName(int Id, string userName)
        {
            string hql = @"select entity.TheUser from ReportJobUser as entity where entity.TheJob.Id = ? and entity.TheUser.Name like ? ";

            IList<ReportUser> list = FindAllWithCustomQuery(
                hql, new object[] { Id, "%" + userName + "%" },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String }) as IList<ReportUser>;

            return list;
        }

        #endregion Customized Methods
    }
}
