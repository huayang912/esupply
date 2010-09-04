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
    public class NHReportValidationResultDao : NHDaoBase, IReportValidationResultDao
    {
        public NHReportValidationResultDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportValidationResult(ReportValidationResult entity)
        {
            Create(entity);
        }

        public ReportValidationResult LoadReportValidationResult(int id)
        {
            return FindById(typeof(ReportValidationResult), id) as ReportValidationResult;
        }

        public void UpdateReportValidationResult(ReportValidationResult entity)
        {
            Update(entity);
        }

        public void DeleteReportValidationResult(int id)
        {
            string hql = @"from ReportValidationResult entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportValidationResult(ReportValidationResult entity)
        {
            Delete(entity);
        }

        public void DeleteReportValidationResult(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportValidationResult entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportValidationResult(IList<ReportValidationResult> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportValidationResult entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportValidationResult(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods


        #endregion Customized Methods
    }
}
