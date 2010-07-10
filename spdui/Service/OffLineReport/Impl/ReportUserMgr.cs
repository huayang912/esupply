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
    public class ReportUserMgr : SessionBase, IReportUserMgr
    {
        private IReportUserDao reportUserDao;
	    private IReportUserSheetParameterDao reportUserSheetParameterDao;
	    private IReportUserSheetDao reportUserSheetDao;
        private IReportParameterDao reportParameterDao;
        private IReportTemplateDao reportTemplateDao;
        
        public ReportUserMgr(IReportUserDao reportUserDao,
		    IReportUserSheetDao reportUserSheetDao,
		    IReportUserSheetParameterDao reportUserSheetParameterDao,
            IReportParameterDao reportParameterDao,
            IReportTemplateDao reportTemplateDao)
        {
            this.reportUserDao = reportUserDao;
	        this.reportUserSheetDao = reportUserSheetDao;
	        this.reportUserSheetParameterDao = reportUserSheetParameterDao;
            this.reportParameterDao = reportParameterDao;
            this.reportTemplateDao = reportTemplateDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateReportUser(ReportUser entity)
        {
            //TODO: Add other code here.
			
            reportUserDao.CreateReportUser(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportUser LoadReportUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.
            ReportUser ru = reportUserDao.LoadReportUser(id);
            if (ru != null)
            {
                ru.ReportList = reportUserSheetDao.FindAllByReportUserId(id);
                ru.ReportParameterList = reportUserSheetParameterDao.FindAllByReportUserId(id);
            }

            return ru;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportUser(ReportUser entity)
        {
        	//TODO: Add other code here.
            reportUserDao.UpdateReportUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUser(int id)
        {
            //reportUserSheetParameterDao.DeleteAllByReportUserId(id);
            //reportUserSheetDao.DeleteAllByReportUserId(id);
            //reportUserDao.DeleteReportUser(id);

            //Only Disable the Report User, changed by Jeffrey 2006-11-25
            ReportUser entity = reportUserDao.LoadReportUser(id);
            entity.ActiveFlag = 0;
            UpdateReportUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUser(ReportUser entity)
        {
            //reportUserSheetParameterDao.DeleteAllByReportUserId(entity.Id);
            //reportUserSheetDao.DeleteAllByReportUserId(entity.Id);
            //reportUserDao.DeleteReportUser(entity);

            //Only Disable the Report User, changed by Jeffrey 2006-11-25
            DeleteReportUser(entity.Id);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUser(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            //Only Disable the Report User, changed by Jeffrey 2006-11-25
            foreach (int id in idList)
            {
                DeleteReportUser(id);
            }

            //reportUserDao.DeleteReportUser(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUser(IList<ReportUser> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            //Only Disable the Report User, changed by Jeffrey 2006-11-25
            foreach (ReportUser entity in entityList)
            {
                DeleteReportUser(entity);
            }

            //reportUserDao.DeleteReportUser(entityList);
        }

	    [Transaction(TransactionMode.Requires)]
        public void CreateReportUserSheet(ReportUserSheet entity)
        {
            //TODO: Add other code here.
			
            reportUserSheetDao.CreateReportUserSheet(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportUserSheet LoadReportUserSheet(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.
            ReportUserSheet rus = reportUserSheetDao.LoadReportUserSheet(id);
            
            return rus;
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateReportUserSheet(IList<int> idList, int userId)
        {
            //delete original operator
            IList<ReportUserSheet> reportUserSheetList = (this.FindReportByReportUserId(userId) as IList<ReportUserSheet>);

            if (reportUserSheetList != null && reportUserSheetList.Count > 0)
            {
                IList<int> reportUserSheetIdList = new List<int>();
                foreach (ReportUserSheet rus in reportUserSheetList)
                {
                    reportUserSheetIdList.Add(rus.Id);
                }

                this.DeleteReportUserSheet(reportUserSheetIdList);
            }

            //update new operator
            ReportUser ru = reportUserDao.LoadReportUser(userId);
            if (idList != null && idList.Count > 0)
            {
                foreach (int Id in idList)
                {
                    ReportTemplate rt = reportTemplateDao.LoadReportTemplate(Id);
                    ReportUserSheet rus = new ReportUserSheet();
                    rus.TheReport = rt;
                    rus.TheUser = ru;

                    reportUserSheetDao.CreateReportUserSheet(rus);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheet(int id)
        {
            reportUserSheetDao.DeleteReportUserSheet(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheet(ReportUserSheet entity)
        {
            reportUserSheetDao.DeleteReportUserSheet(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheet(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportUserSheetDao.DeleteReportUserSheet(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheet(IList<ReportUserSheet> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportUserSheetDao.DeleteReportUserSheet(entityList);
        }

	    [Transaction(TransactionMode.Requires)]
        public void CreateReportUserSheetParameter(ReportUserSheetParameter entity)
        {
            //TODO: Add other code here.
			
            reportUserSheetParameterDao.CreateReportUserSheetParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportUserSheetParameter LoadReportUserSheetParameter(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return reportUserSheetParameterDao.LoadReportUserSheetParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportUserSheetParameter(ReportUserSheetParameter entity)
        {
        	//TODO: Add other code here.
            reportUserSheetParameterDao.UpdateReportUserSheetParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheetParameter(int id)
        {
            reportUserSheetParameterDao.DeleteReportUserSheetParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheetParameter(ReportUserSheetParameter entity)
        {
            reportUserSheetParameterDao.DeleteReportUserSheetParameter(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheetParameter(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportUserSheetParameterDao.DeleteReportUserSheetParameter(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportUserSheetParameter(IList<ReportUserSheetParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportUserSheetParameterDao.DeleteReportUserSheetParameter(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        [Transaction(TransactionMode.Requires)]
        public IList LoadAllActiveReportUser()
        {
            return reportUserDao.LoadAllActiveReportUser();
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindReportByReportUserId(int Id)
        {
            return reportUserSheetDao.FindAllByReportUserId(Id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindParameterByReportUserId(int Id)
        {
            return reportUserSheetParameterDao.FindAllByReportUserId(Id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ReportParameter> FindParameterForReportUser(int Id)
        {
            IList<ReportParameter> allParameterList = reportParameterDao.LoadAllActiveReportParameter() as IList<ReportParameter>;
            IList<ReportUserSheetParameter> reportParameterList = this.FindParameterByReportUserId(Id) as IList<ReportUserSheetParameter>;

            List<ReportParameter> AvailableParameterList = new List<ReportParameter>();
            Boolean DataStatus;
            if (allParameterList == null)
            {
                return null;
            }
            else {
                if (reportParameterList == null)
                {
                    return allParameterList;
                }
                else
                {
                    foreach (ReportParameter rp in allParameterList)
                    {
                        DataStatus = true;
                        foreach (ReportUserSheetParameter rup in reportParameterList)
                        {
                            if (rup.TheParamter.Id.Equals(rp.Id))
                            {
                                DataStatus = false;
                                break;
                            }
                        }
                        if (DataStatus)
                        {
                            AvailableParameterList.Add(rp);
                        }
                    }
                }
            }
            return AvailableParameterList;
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<ReportUser> FindReportUserByName(string userName)
        {
            return reportUserDao.FindReportUserByName(userName);
        }

        #endregion Customized Methods
    }
}
