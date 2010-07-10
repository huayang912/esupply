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
    public class NHReportParameterDao : NHDaoBase, IReportParameterDao
    {
        public NHReportParameterDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportParameter(ReportParameter entity)
        {
            Create(entity);
        }

        public ReportParameter LoadReportParameter(int id)
        {
            return FindById(typeof(ReportParameter), id) as ReportParameter;
        }

        public void UpdateReportParameter(ReportParameter entity)
        {
            Update(entity);
        }

        public void DeleteReportParameter(int id)
        {
            string hql = @"from ReportParameter entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportParameter(ReportParameter entity)
        {
            Delete(entity);
        }

        public void DeleteReportParameter(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportParameter entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportParameter(IList<ReportParameter> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportParameter entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportParameter(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList LoadAllActiveReportParameter()
        {
            return FindAllWithCustomQuery("from ReportParameter ds order by ds.Name");
        }

        #endregion Customized Methods
    }
}
