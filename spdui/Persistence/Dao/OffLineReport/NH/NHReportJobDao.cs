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
    public class NHReportJobDao : NHDaoBase, IReportJobDao
    {
        public NHReportJobDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
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
                       + " where rj.TheBatch.ActiveFlag = ? AND rj.Status = 'Running'"
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

        #endregion Customized Methods
    }
}
