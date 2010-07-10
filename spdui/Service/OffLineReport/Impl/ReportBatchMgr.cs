using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Persistence.Dao.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Service.OffLineReport.Impl
{
    [Transactional]
    public class ReportBatchMgr : SessionBase, IReportBatchMgr
    {
        private IReportBatchDao reportBatchDao;
        private IReportBatchReportsDao reportBatchReportsDao;
        private IReportTemplateDao reportTemplateDao;

        public ReportBatchMgr(IReportBatchDao reportBatchDao,
		    IReportBatchReportsDao reportBatchReportsDao,
            IReportTemplateDao reportTemplateDao)
        {
            this.reportBatchDao = reportBatchDao;
	        this.reportBatchReportsDao = reportBatchReportsDao;
            this.reportTemplateDao = reportTemplateDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateReportBatch(ReportBatch entity)
        {
            //TODO: Add other code here.
			
            reportBatchDao.CreateReportBatch(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportBatch LoadReportBatch(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.
            ReportBatch rb = reportBatchDao.LoadReportBatch(id);
            if (rb != null)
            {
                rb.ReportList = reportBatchReportsDao.FindAllByBatchId(id);
            }

            return rb;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportBatch(ReportBatch entity)
        {
        	//TODO: Add other code here.
            reportBatchDao.UpdateReportBatch(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatch(int id)
        {
            reportBatchReportsDao.DeleteAllByBatchId(id);
            reportBatchDao.DeleteReportBatch(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatch(ReportBatch entity)
        {
            reportBatchReportsDao.DeleteAllByBatchId(entity.Id); 
            reportBatchDao.DeleteReportBatch(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatch(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportBatchDao.DeleteReportBatch(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatch(IList<ReportBatch> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportBatchDao.DeleteReportBatch(entityList);
        }

	    [Transaction(TransactionMode.Requires)]
        public void CreateReportBatchReports(ReportBatchReports entity)
        {
            //TODO: Add other code here.
			
            reportBatchReportsDao.CreateReportBatchReports(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportBatchReports LoadReportBatchReports(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return reportBatchReportsDao.LoadReportBatchReports(id);
        }
       
        [Transaction(TransactionMode.Requires)]
        public void UpdateReportBatchReports(IList<int> idList, int batchId)
        {
            //delete original operator
            IList<ReportBatchReports> reportBatchReportsList = (this.FindReportByBatchId(batchId) as IList<ReportBatchReports>) ;

            if (reportBatchReportsList != null && reportBatchReportsList.Count > 0)
            {
                IList<int> reportBatchReportsIdList = new List<int>();
                foreach (ReportBatchReports rbr in reportBatchReportsList)
                {
                    reportBatchReportsIdList.Add(rbr.Id);
                }

                this.DeleteReportBatchReports(reportBatchReportsIdList);
            }

            //update new operator
            ReportBatch rb = reportBatchDao.LoadReportBatch(batchId);
            if (idList != null && idList.Count > 0)
            {
                foreach (int Id in idList)
                {
                    ReportTemplate rt = reportTemplateDao.LoadReportTemplate(Id);
                    ReportBatchReports rbr = new ReportBatchReports();
                    rbr.TheReport = rt;
                    rbr.TheReportBatch = rb;

                    reportBatchReportsDao.CreateReportBatchReports(rbr);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatchReports(int id)
        {
            reportBatchReportsDao.DeleteReportBatchReports(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatchReports(ReportBatchReports entity)
        {
            reportBatchReportsDao.DeleteReportBatchReports(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatchReports(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportBatchReportsDao.DeleteReportBatchReports(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportBatchReports(IList<ReportBatchReports> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportBatchReportsDao.DeleteReportBatchReports(entityList);
        }
        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        [Transaction(TransactionMode.Requires)]
        public IList LoadAllActiveReportBatch()
        {
            return reportBatchDao.LoadAllActiveReportBatch();
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindReportByBatchId(int Id)
        {
            return (reportBatchReportsDao.FindAllByBatchId(Id) as IList);
        }

        #endregion Customized Methods
    }
}
