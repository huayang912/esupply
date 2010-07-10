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
    public class NHReportUserSheetParameterDao : NHDaoBase, IReportUserSheetParameterDao
    {
        public NHReportUserSheetParameterDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportUserSheetParameter(ReportUserSheetParameter entity)
        {
            Create(entity);
        }

        public ReportUserSheetParameter LoadReportUserSheetParameter(int id)
        {
            return FindById(typeof(ReportUserSheetParameter), id) as ReportUserSheetParameter;
        }

        public void UpdateReportUserSheetParameter(ReportUserSheetParameter entity)
        {
            Update(entity);
        }

        public void DeleteReportUserSheetParameter(int id)
        {
            string hql = @"from ReportUserSheetParameter entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportUserSheetParameter(ReportUserSheetParameter entity)
        {
            Delete(entity);
        }

        public void DeleteReportUserSheetParameter(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportUserSheetParameter entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportUserSheetParameter(IList<ReportUserSheetParameter> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportUserSheetParameter entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportUserSheetParameter(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList FindAllByReportUserId(int reportUserId)
        {
            return FindAllWithCustomQuery("from ReportUserSheetParameter rusp where rusp.TheUser.Id=? order by rusp.TheParamter.Name", reportUserId);
        }

        public void DeleteAllByReportParameterId(int Id)
        {
            string hql = @"from ReportUserSheetParameter entity where entity.TheParameter.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteAllByReportUserId(int Id)
        {
            string hql = @"from ReportUserSheetParameter entity where entity.TheUser.Id = ?";
            Delete(hql, Id, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
