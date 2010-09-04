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
    public class NHReportBatchUserDao : NHDaoBase, IReportBatchUserDao
    {
        public NHReportBatchUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportBatchUser(ReportBatchUser entity)
        {
            Create(entity);
        }

        public ReportBatchUser LoadReportBatchUser(int id)
        {
            return FindById(typeof(ReportBatchUser), id) as ReportBatchUser;
        }

        public void UpdateReportBatchUser(ReportBatchUser entity)
        {
            Update(entity);
        }

        public void DeleteReportBatchUser(int id)
        {
            string hql = @"from ReportBatchUser entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportBatchUser(ReportBatchUser entity)
        {
            Delete(entity);
        }

        public void DeleteReportBatchUser(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportBatchUser entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportBatchUser(IList<ReportBatchUser> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportBatchUser entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportBatchUser(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByBatchId(int batchId)
        {
            return FindAllWithCustomQuery("from ReportBatchUser rbu where rbu.TheReportBatch.Id=? order by rbu.TheReportBatch.Name, rbu.TheUser.UserName", batchId);
        }

        #endregion Customized Methods
    }
}
