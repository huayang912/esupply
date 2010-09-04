using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Service.OffLineReport.Impl
{
    [Transactional]
    public class ReportValidationRuleMgr : SessionBase, IReportValidationRuleMgr
    {
        private IReportValidationRuleDao entityDao;
        
        public ReportValidationRuleMgr(IReportValidationRuleDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateReportValidationRule(ReportValidationRule entity)
        {
            //TODO: Add other code here.
			
            entityDao.CreateReportValidationRule(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportValidationRule LoadReportValidationRule(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadReportValidationRule(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportValidationRule(ReportValidationRule entity)
        {
        	//TODO: Add other code here.
            entityDao.UpdateReportValidationRule(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportValidationRule(int id)
        {
            entityDao.DeleteReportValidationRule(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportValidationRule(ReportValidationRule entity)
        {
            entityDao.DeleteReportValidationRule(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportValidationRule(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            entityDao.DeleteReportValidationRule(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportValidationRule(IList<ReportValidationRule> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteReportValidationRule(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        #endregion Customized Methods
    }
}
