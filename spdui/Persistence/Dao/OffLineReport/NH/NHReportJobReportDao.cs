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
    public class NHReportJobReportDao : NHDaoBase, IReportJobReportDao
    {
        public NHReportJobReportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportJobReport(ReportJobReport entity)
        {
            Create(entity);
        }

        public ReportJobReport LoadReportJobReport(int id)
        {
            return FindById(typeof(ReportJobReport), id) as ReportJobReport;
        }

        public void UpdateReportJobReport(ReportJobReport entity)
        {
            Update(entity);
        }

        public void DeleteReportJobReport(int id)
        {
            string hql = @"from ReportJobReport entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportJobReport(ReportJobReport entity)
        {
            Delete(entity);
        }

        public void DeleteReportJobReport(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportJobReport entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportJobReport(IList<ReportJobReport> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportJobReport entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportJobReport(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList FindAllByJobId(int Id)
        {
            return FindAllWithCustomQuery("from ReportJobReport entity where entity.TheJob.Id=? order by entity.TheReport.Name", Id);
        }

        public void DeleteAllByJobId(int Id)
        {
            string hql = @"from ReportJobReport entity where entity.TheJob.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
