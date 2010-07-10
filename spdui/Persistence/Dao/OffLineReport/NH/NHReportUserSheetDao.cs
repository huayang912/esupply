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
    public class NHReportUserSheetDao : NHDaoBase, IReportUserSheetDao
    {
        public NHReportUserSheetDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportUserSheet(ReportUserSheet entity)
        {
            Create(entity);
        }

        public ReportUserSheet LoadReportUserSheet(int id)
        {
            return FindById(typeof(ReportUserSheet), id) as ReportUserSheet;
        }

        public void UpdateReportUserSheet(ReportUserSheet entity)
        {
            Update(entity);
        }

        public void DeleteReportUserSheet(int id)
        {
            string hql = @"from ReportUserSheet entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportUserSheet(ReportUserSheet entity)
        {
            Delete(entity);
        }

        public void DeleteReportUserSheet(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportUserSheet entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportUserSheet(IList<ReportUserSheet> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportUserSheet entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportUserSheet(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList FindAllByReportUserId(int reportUserId)
        {
            return FindAllWithCustomQuery("from ReportUserSheet rus where rus.TheUser.Id=? order by rus.TheReport.Name", reportUserId);
        }

        public void DeleteAllByReportUserId(int Id)
        {
            string hql = @"from ReportUserSheet entity where entity.TheUser.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        public IList<ReportUser> FindReportUserByReportBatchIdAndUserNameAndUserDescription(int batchId, string userName, string userDescription)
        {
            string hql = "Select distinct entity.TheUser from ReportUserSheet as entity where entity.TheReport.Id in "
                       + " (select distinct rbr.TheReport.Id "
                       + " from ReportBatchReports as rbr"
                       + " where rbr.TheReportBatch.Id = ? "
                       + " ) and entity.TheUser.Name like ? and entity.TheUser.Description like ? and entity.TheUser.ActiveFlag=1 and entity.TheUser.TheUser.ActiveFlag=1 and entity.TheUser.TheUser.IsReportUser = 1 order by entity.TheUser.Name";

            IList<ReportUser> list = FindAllWithCustomQuery(
                hql, new object[] { batchId, "%" + userName + "%", "%" + userDescription + "%" },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String, NHibernateUtil.String }) as IList<ReportUser>;

            return list;
        }

        #endregion Customized Methods
    }
}
