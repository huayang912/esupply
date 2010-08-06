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
using Dndp.Persistence.Dao.Criteria;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.OffLineReport.NH
{
    public class NHReportJobDao : NHDaoBase, IReportJobDao
    {
        private ICriteriaDao criteriaDao;
        public NHReportJobDao(ISessionManager sessionManager,
            ICriteriaDao criteriaDao)
            : base(sessionManager)
        {
            this.criteriaDao = criteriaDao;
        }

        #region Method Created By CodeSmith

        public void CreateReportJob(ReportJob entity)
        {
            Create(entity);
        }

        public ReportJob LoadReportJob(int id)
        {
            return FindById(typeof(ReportJob), id) as ReportJob;
        }

        public void UpdateReportJob(ReportJob entity)
        {
            Update(entity);
        }

        public void DeleteReportJob(int id)
        {
            string hql = @"from ReportJob entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteReportJob(ReportJob entity)
        {
            Delete(entity);
        }

        public void DeleteReportJob(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ReportJob entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteReportJob(IList<ReportJob> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ReportJob entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteReportJob(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList<ReportJob> FindAllLastestReportJob()
        {
            string hql = " from ReportJob as entity where entity.Id in "
                       + " (select max(rj.Id) "
                       + " from ReportJob rj"
                       + " where rj.TheBatch.ActiveFlag = ? "
                       + " group by rj.TheBatch) order by entity.TheBatch.Name";

            IList<ReportJob> list = FindAllWithCustomQuery(
                hql, new object[] { 1 },
                new IType[] { NHibernateUtil.Int32 }) as IList<ReportJob>;
            
            return list;
        }

        // Modified by vincent at 2007-12-04 Begin
        public IList<ReportJob> FindAllLastestRunningReportJob()
        {
            string hql = " from ReportJob as entity where entity.Id in "
                       + " (select max(rj.Id) "
                       + " from ReportJob rj"
                       + " where rj.TheBatch.ActiveFlag = ? AND rj.Status in ('Running')"
                       + " group by rj.TheBatch) order by entity.TheBatch.Name";

            IList<ReportJob> list = FindAllWithCustomQuery(
                hql, new object[] { 1 },
                new IType[] { NHibernateUtil.Int32 }) as IList<ReportJob>;
            
            return list;
        }
        // Modified by vincent at 2007-12-04 End

        public ReportJob FindLastestReportJobByBatchId(int batchId)
        {
            string hql = " from ReportJob as entity where entity.Id = "
                       + " (select max(rj.Id) "
                       + " from ReportJob as rj "
                       + " where rj.TheBatch.Id = ?) ";

            IList list = FindAllWithCustomQuery(
                hql, new object[] { batchId },
                new IType[] { NHibernateUtil.Int32 });

            if (list != null && list.Count > 0)
            {
                return list[0] as ReportJob;
            }

            return null;
        }

        public IList<ReportJob> FindAllReportJobByBatchId(int batchId)
        {
            return FindAllWithCustomQuery(
                "from ReportJob as rj where rj.TheBatch.Id = ? order by rj.EndTime Desc",
                new object[] { batchId },
                new IType[] { NHibernateUtil.Int32 }) as IList<ReportJob>;
        }

        public void DeleteAllReportJobByBatchId(int batchId)
        {
            string hql = @"from ReportJob as rj where rj.TheBatch.Id = ?";
            Delete(hql, batchId, NHibernate.NHibernateUtil.Int32);
        }

        public IList<ReportJob> FindAllReportJobByStatus(string[] status)
        {
            if (status == null || status.Length == 0)
            {
                return null;
            }

            string statusCriteria = string.Empty;
            foreach(string s in status) 
            {
                statusCriteria += "'" + s + "',";
            }
            statusCriteria = statusCriteria.TrimEnd(',');


            return FindAllWithCustomQuery(
                "from ReportJob as rj where rj.Status in(" + statusCriteria + ") order by rj.Id") as IList<ReportJob>;
        }

        public ReportJob FindLastestRunningReportJobByBatchId(int batchId)
        {
            string hql = @" from ReportJob rj"
                       + " where rj.TheBatch.Id = ? AND rj.Status in ('Submit','Running')"
                       + " order by rj.Status, rj.StartTime";

            IList<ReportJob> reportJobList = FindAllWithCustomQuery(hql,
                new object[] { batchId },
                new IType[] { NHibernateUtil.Int32 }, 0, 1) as IList<ReportJob>;

            if (reportJobList != null && reportJobList.Count > 0)
            {
                return reportJobList[0];
            }
            return null;
        }

        public ReportJob FindLastestRunnedReportJobByBatchId(int batchId)
        {
            string hql = @" from ReportJob rj"
                       + " where rj.TheBatch.Id = ? AND rj.Status not in ('Submit','Running')"
                       + " order by rj.EndTime desc";

            IList<ReportJob> reportJobList = FindAllWithCustomQuery(hql,
                new object[] { batchId },
                new IType[] { NHibernateUtil.Int32 }, 0, 1) as IList<ReportJob>;

            if (reportJobList != null && reportJobList.Count > 0)
            {
                return reportJobList[0];
            }
            return null;
        }

        #endregion Customized Methods
    }
}
