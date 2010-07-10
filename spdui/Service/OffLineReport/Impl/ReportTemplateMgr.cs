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
    public class ReportTemplateMgr : SessionBase, IReportTemplateMgr
    {
        private IReportTemplateDao reportTemplateDao;
	    private IReportSheetDao reportSheetDao;
        
        public ReportTemplateMgr(IReportTemplateDao reportTemplateDao,
		    IReportSheetDao reportSheetDao)
        {
            this.reportTemplateDao = reportTemplateDao;
	        this.reportSheetDao = reportSheetDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateReportTemplate(ReportTemplate entity)
        {
            //TODO: Add other code here.
			
            reportTemplateDao.CreateReportTemplate(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportTemplate LoadReportTemplate(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
            ReportTemplate rt = reportTemplateDao.LoadReportTemplate(id);
            if (rt != null)
            {
                rt.ReportSheetList = reportSheetDao.FindAllByReportId(id);
            }

            return rt;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportTemplate(ReportTemplate entity)
        {
        	//TODO: Add other code here.
            reportTemplateDao.UpdateReportTemplate(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportTemplate(int id)
        {
            //reportSheetDao.DeleteAllByReportId(id);
            //reportTemplateDao.DeleteReportTemplate(id);

            //Only Disable the Report Template, changed by Jeffrey 2006-11-25
            ReportTemplate entity = reportTemplateDao.LoadReportTemplate(id);
            entity.ActiveFlag = 0;
            UpdateReportTemplate(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportTemplate(ReportTemplate entity)
        {
            //reportSheetDao.DeleteAllByReportId(entity.Id);
            //reportTemplateDao.DeleteReportTemplate(entity);

            //Only Disable the Report Template, changed by Jeffrey 2006-11-25
            DeleteReportTemplate(entity.Id);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportTemplate(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            //Only Disable the Report Template, changed by Jeffrey 2006-11-25
            foreach (int id in idList)
            {
                DeleteReportTemplate(id);
            }

            //reportTemplateDao.DeleteReportTemplate(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportTemplate(IList<ReportTemplate> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            //Only Disable the Report Template, changed by Jeffrey 2006-11-25
            foreach (ReportTemplate entity in entityList)
            {
                DeleteReportTemplate(entity);
            }

            //reportTemplateDao.DeleteReportTemplate(entityList);
        }

	    [Transaction(TransactionMode.Requires)]
        public void CreateReportSheet(ReportSheet entity)
        {
            //TODO: Add other code here.
			
            reportSheetDao.CreateReportSheet(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportSheet LoadReportSheet(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return reportSheetDao.LoadReportSheet(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportSheet(ReportSheet entity)
        {
        	//TODO: Add other code here.
            reportSheetDao.UpdateReportSheet(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportSheet(int id)
        {
            reportSheetDao.DeleteReportSheet(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportSheet(ReportSheet entity)
        {
            reportSheetDao.DeleteReportSheet(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportSheet(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportSheetDao.DeleteReportSheet(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportSheet(IList<ReportSheet> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportSheetDao.DeleteReportSheet(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        [Transaction(TransactionMode.Requires)]
        public IList LoadAllActiveReportTemplate()
        {
            return reportTemplateDao.LoadAllActiveReportTemplate();
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindReportSheetByReportId(int Id)
        {
            return reportSheetDao.FindAllByReportId(Id);
        }

        #endregion Customized Methods
    }
}
