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
    public class NHReportSheetDao : NHDaoBase, IReportSheetDao
    {
        public NHReportSheetDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportSheet(ReportSheet entity)
        {
            Create(entity);
        }

        public ReportSheet LoadReportSheet(int id)
        {
            return FindById(typeof(ReportSheet), id) as ReportSheet;
        }

        public void UpdateReportSheet(ReportSheet entity)
        {
            Update(entity);
        }

        public void DeleteReportSheet(int id)
        {
            string hql = @"from ReportSheet entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportSheet(ReportSheet entity)
        {
            Delete(entity);
        }

        public void DeleteReportSheet(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportSheet entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportSheet(IList<ReportSheet> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportSheet entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportSheet(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList FindAllByReportId(int reportId)
        {
            return FindAllWithCustomQuery("from ReportSheet rs where rs.TheReport.Id=? order by rs.Name, rs.SequenceNo", reportId);
        }

        public void DeleteAllByReportId(int Id)
        {
            string hql = @"from ReportSheet entity where entity.TheReport.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
