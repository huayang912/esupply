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
    public class NHReportTemplateDao : NHDaoBase, IReportTemplateDao
    {
        public NHReportTemplateDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportTemplate(ReportTemplate entity)
        {
            Create(entity);
        }

        public ReportTemplate LoadReportTemplate(int id)
        {
            return FindById(typeof(ReportTemplate), id) as ReportTemplate;
        }

        public void UpdateReportTemplate(ReportTemplate entity)
        {
            Update(entity);
        }

        public void DeleteReportTemplate(int id)
        {
            string hql = @"from ReportTemplate entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportTemplate(ReportTemplate entity)
        {
            Delete(entity);
        }

        public void DeleteReportTemplate(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportTemplate entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportTemplate(IList<ReportTemplate> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportTemplate entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportTemplate(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList LoadAllActiveReportTemplate()
        {
            return FindAllWithCustomQuery("from ReportTemplate ds where ds.ActiveFlag=1 order by ds.Description");
        }

        public IList<ReportTemplate> FindReportForReportBatch(int Id)
        {
            string hql = "Select distinct entity.TheReport from ReportBatchReports as entity"
                       + " where entity.TheReportBatch.Id = ? and entity.TheReport.ActiveFlag=1"
                       + " order by entity.TheReport.Name";

            IList<ReportTemplate> list = FindAllWithCustomQuery(
                hql, new object[] { Id },
                new IType[] { NHibernateUtil.Int32 }) as IList<ReportTemplate>;

            return list;
        }

        #endregion Customized Methods
    }
}
