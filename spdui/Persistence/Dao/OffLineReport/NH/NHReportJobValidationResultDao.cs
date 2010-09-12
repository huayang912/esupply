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
    public class NHReportJobValidationResultDao : NHDaoBase, IReportJobValidationResultDao
    {
        public NHReportJobValidationResultDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportJobValidationResult(ReportJobValidationResult entity)
        {
            Create(entity);
        }

        public ReportJobValidationResult LoadReportJobValidationResult(int id)
        {
            return FindById(typeof(ReportJobValidationResult), id) as ReportJobValidationResult;
        }

        public void UpdateReportJobValidationResult(ReportJobValidationResult entity)
        {
            Update(entity);
        }

        public void DeleteReportJobValidationResult(int id)
        {
            string hql = @"from ReportJobValidationResult entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportJobValidationResult(ReportJobValidationResult entity)
        {
            Delete(entity);
        }

        public void DeleteReportJobValidationResult(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportJobValidationResult entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportJobValidationResult(IList<ReportJobValidationResult> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportJobValidationResult entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportJobValidationResult(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByJobId(int Id)
        {
            return FindAllWithCustomQuery("from ReportJobValidationResult as entity where entity.TheJob.Id = ? order by entity.TheRule.Name", Id);
        }

        public void DeleteAllByJobId(int Id)
        {
            string hql = @"from ReportJobValidationResult as entity where entity.TheJob.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        public IList FindValidationResultByIds(string validationIds)
        {
            string hql = "from ReportJobValidationResult result where result.Id in (" + validationIds + ") ";

            return FindAllWithCustomQuery(hql);
        }
        #endregion Customized Methods
    }
}
