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
    public class NHReportValidationRuleDao : NHDaoBase, IReportValidationRuleDao
    {
        public NHReportValidationRuleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportValidationRule(ReportValidationRule entity)
        {
            Create(entity);
        }

        public ReportValidationRule LoadReportValidationRule(int id)
        {
            return FindById(typeof(ReportValidationRule), id) as ReportValidationRule;
        }

        public void UpdateReportValidationRule(ReportValidationRule entity)
        {
            Update(entity);
        }

        public void DeleteReportValidationRule(int id)
        {
            string hql = @"from ReportValidationRule entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportValidationRule(ReportValidationRule entity)
        {
            Delete(entity);
        }

        public void DeleteReportValidationRule(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportValidationRule entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportValidationRule(IList<ReportValidationRule> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportValidationRule entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportValidationRule(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<ReportValidationRule> GetReportValidationRuleByBatchId(int id)
        {
            string hql = "from ReportValidationRule entity where entity.TheReportBatch.Id = ?";

            return FindAllWithCustomQuery(hql, id) as IList<ReportValidationRule>;
        }

        #endregion Customized Methods
    }
}
