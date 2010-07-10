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
    public class ReportParameterMgr : SessionBase, IReportParameterMgr
    {
        private IReportParameterDao reportParameterDao;
        
        public ReportParameterMgr(IReportParameterDao reportParameterDao)
        {
            this.reportParameterDao = reportParameterDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateReportParameter(ReportParameter entity)
        {
            //TODO: Add other code here.
			
            reportParameterDao.CreateReportParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportParameter LoadReportParameter(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return reportParameterDao.LoadReportParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportParameter(ReportParameter entity)
        {
        	//TODO: Add other code here.
            reportParameterDao.UpdateReportParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportParameter(int id)
        {
            reportParameterDao.DeleteReportParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportParameter(ReportParameter entity)
        {
            reportParameterDao.DeleteReportParameter(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportParameter(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportParameterDao.DeleteReportParameter(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportParameter(IList<ReportParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportParameterDao.DeleteReportParameter(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        [Transaction(TransactionMode.Requires)]
        public IList LoadAllActiveReportParameter()
        {
            return reportParameterDao.LoadAllActiveReportParameter();
        }

        #endregion Customized Methods
    }
}
