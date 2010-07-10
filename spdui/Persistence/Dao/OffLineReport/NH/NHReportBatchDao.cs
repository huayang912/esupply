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
    public class NHReportBatchDao : NHDaoBase, IReportBatchDao
    {
        public NHReportBatchDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateReportBatch(ReportBatch entity)
        {
            Create(entity);
        }

        public ReportBatch LoadReportBatch(int id)
        {
            return FindById(typeof(ReportBatch), id) as ReportBatch;
        }

        public void UpdateReportBatch(ReportBatch entity)
        {
            Update(entity);
        }

        public void DeleteReportBatch(int id)
        {
            string hql = @"from ReportBatch entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportBatch(ReportBatch entity)
        {
            Delete(entity);
        }

        public void DeleteReportBatch(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportBatch entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportBatch(IList<ReportBatch> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportBatch entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportBatch(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList LoadAllActiveReportBatch()
        {
            return FindAllWithCustomQuery("from ReportBatch ds where ds.ActiveFlag=1 order by ds.Description");
        }

        #endregion Customized Methods
    }
}
