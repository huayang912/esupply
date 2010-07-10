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
    public class NHReportBatchReportsDao : NHDaoBase, IReportBatchReportsDao
    {
        public NHReportBatchReportsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportBatchReports(ReportBatchReports entity)
        {
            Create(entity);
        }

        public ReportBatchReports LoadReportBatchReports(int id)
        {
            return FindById(typeof(ReportBatchReports), id) as ReportBatchReports;
        }

        public void UpdateReportBatchReports(ReportBatchReports entity)
        {
            Update(entity);
        }

        public void DeleteReportBatchReports(int id)
        {
            string hql = @"from ReportBatchReports entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportBatchReports(ReportBatchReports entity)
        {
            Delete(entity);
        }

        public void DeleteReportBatchReports(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportBatchReports entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportBatchReports(IList<ReportBatchReports> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportBatchReports entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportBatchReports(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList FindAllByBatchId(int batchId)
        {
            return FindAllWithCustomQuery("from ReportBatchReports rbr where rbr.TheReportBatch.Id=? order by rbr.TheReportBatch.Name, rbr.TheReport.Name", batchId);
        }

        public void DeleteAllByBatchId(int Id)
        {
            string hql = @"from ReportBatchReports entity where entity.TheReportBatch.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
