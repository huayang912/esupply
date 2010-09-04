using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Service.OffLineReport.Impl
{
    [Transactional]
    public class ReportJobMgr : SessionBase, IReportJobMgr
    {
        private IReportJobDao reportJobDao;
        private IReportJobReportDao reportJobReportDao;
        private IReportJobUserDao reportJobUserDao;
        private IReportBatchDao reportBatchDao;
        private IReportUserDao reportUserDao;
        private IReportTemplateDao reportTemplateDao;
        private IReportUserSheetDao reportUserSheetDao;

        public ReportJobMgr(IReportJobDao reportJobDao,
            IReportJobReportDao reportJobReportDao,
            IReportJobUserDao reportJobUserDao,
            IReportBatchDao reportBatchDao,
            IReportUserDao reportUserDao,
            IReportTemplateDao reportTemplateDao,
            IReportUserSheetDao reportUserSheetDao)
        {
            this.reportJobDao = reportJobDao;
            this.reportJobReportDao = reportJobReportDao;
            this.reportJobUserDao = reportJobUserDao;
            this.reportBatchDao = reportBatchDao;
            this.reportUserDao = reportUserDao;
            this.reportTemplateDao = reportTemplateDao;
            this.reportUserSheetDao = reportUserSheetDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateReportJob(ReportJob entity)
        {
            //TODO: Add other code here.
			
            reportJobDao.CreateReportJob(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportJob LoadReportJob(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.
            ReportJob rj = reportJobDao.LoadReportJob(id);
            if (rj != null)
            {
                rj.ReportList = this.FindReportByJobId(id);
                rj.UserList = this.FindUserByJobId(id);
            }

            return rj;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReportJob(ReportJob entity)
        {
        	//TODO: Add other code here.
            reportJobDao.UpdateReportJob(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJob(int id)
        {
            reportJobDao.DeleteReportJob(id);
            reportJobUserDao.DeleteAllByJobId(id);
            reportJobReportDao.DeleteAllByJobId(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJob(ReportJob entity)
        {
            reportJobDao.DeleteReportJob(entity);
            reportJobUserDao.DeleteAllByJobId(entity.Id);
            reportJobReportDao.DeleteAllByJobId(entity.Id);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJob(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportJobDao.DeleteReportJob(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJob(IList<ReportJob> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportJobDao.DeleteReportJob(entityList);
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateReportJobReport(ReportJobReport entity)
        {
            //TODO: Add other code here.

            reportJobReportDao.CreateReportJobReport(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportJobReport LoadReportJobReport(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return reportJobReportDao.LoadReportJobReport(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateReportJobReport(IList<int> idList, int jobId)
        {
            //delete original operator
            IList<ReportJobReport> reportJobReportList = (this.FindReportByJobId(jobId) as IList<ReportJobReport>);

            if (reportJobReportList != null && reportJobReportList.Count > 0)
            {
                IList<int> reportJobReportIdList = new List<int>();
                foreach (ReportJobReport rjr in reportJobReportList)
                {
                    reportJobReportIdList.Add(rjr.Id);
                }

                this.DeleteReportJobReport(reportJobReportIdList);
            }

            //update new operator
            ReportJob rj = reportJobDao.LoadReportJob(jobId);
            if (idList != null && idList.Count > 0)
            {
                foreach (int Id in idList)
                {
                    ReportTemplate rt = reportTemplateDao.LoadReportTemplate(Id);
                    ReportJobReport rjr = new ReportJobReport();
                    rjr.TheReport = rt;
                    rjr.TheJob = rj;

                    reportJobReportDao.CreateReportJobReport(rjr);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobReport(int id)
        {
            reportJobReportDao.DeleteReportJobReport(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobReport(ReportJobReport entity)
        {
            reportJobReportDao.DeleteReportJobReport(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobReport(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportJobReportDao.DeleteReportJobReport(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobReport(IList<ReportJobReport> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportJobReportDao.DeleteReportJobReport(entityList);
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateReportJobUser(ReportJobUser entity)
        {
            //TODO: Add other code here.

            reportJobUserDao.CreateReportJobUser(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public ReportJobUser LoadReportJobUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return reportJobUserDao.LoadReportJobUser(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateReportJobUser(IList<int> idList, int jobId)
        {
            //delete original operator
            IList<ReportJobUser> reportJobUserList = (this.FindUserByJobId(jobId) as IList<ReportJobUser>);

            if (reportJobUserList != null && reportJobUserList.Count > 0)
            {
                IList<int> reportJobUserIdList = new List<int>();
                foreach (ReportJobUser rjr in reportJobUserList)
                {
                    reportJobUserIdList.Add(rjr.Id);
                }

                this.DeleteReportJobUser(reportJobUserIdList);
            }

            //update new operator
            ReportJob rj = reportJobDao.LoadReportJob(jobId);
            if (idList != null && idList.Count > 0)
            {
                foreach (int Id in idList)
                {
                    ReportUser rt = reportUserDao.LoadReportUser(Id);
                    ReportJobUser rjr = new ReportJobUser();
                    rjr.TheUser = rt;
                    rjr.TheJob = rj;

                    reportJobUserDao.CreateReportJobUser(rjr);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobUser(int id)
        {
            reportJobUserDao.DeleteReportJobUser(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobUser(ReportJobUser entity)
        {
            reportJobUserDao.DeleteReportJobUser(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobUser(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            reportJobUserDao.DeleteReportJobUser(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJobUser(IList<ReportJobUser> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            reportJobUserDao.DeleteReportJobUser(entityList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ReportBatch> FindReportBatchWithJob(User user)
        {
            IList<ReportBatch> reportBatchList = reportBatchDao.LoadlActiveReportBatchByUser(user) as IList<ReportBatch>;

            if (reportBatchList != null && reportBatchList.Count > 0)
            {
                foreach(ReportBatch reportBatch in reportBatchList) 
                {
                    ReportJob reportJob = reportJobDao.FindLastestRunningReportJobByBatchId(reportBatch.Id);
                    if (reportJob != null)
                    {
                        reportBatch.LastestReportJob = reportJob;
                    }
                    else
                    {
                        reportBatch.LastestReportJob = reportJobDao.FindLastestRunnedReportJobByBatchId(reportBatch.Id);
                    }
                }
            }

            return reportBatchList;

            // Modified by vincent at 2007-12-04 begin
            //IList<ReportJob> reportJobList = null;
            //IList reportJobList = new ArrayList();
            //IList<ReportJob> latestJob = reportJobDao.FindAllLastestReportJob();
            //IList<ReportJob> runningJob = reportJobDao.FindAllLastestRunningReportJob();
            //IList<ReportJob> runningJob = reportJobDao.FindAllReportJobByStatus(new string[] { ReportJob.REPORT_JOB_STATUS_RUNNING, 
            //    ReportJob.REPORT_JOB_STATUS_SUBMIT, ReportJob.REPORT_JOB_STATUS_PENDING });

            //IList<ReportJob> resultReportJob = new List<ReportJob>();
            //foreach(ReportBatch reportBatch in reportBatchList) 
            //{
            //    ReportJob newReportJob = new ReportJob();
            //    newReportJob.TheBatch = reportBatch;
            //    resultReportJob.Add(newReportJob);
            //    if (runningJob != null && runningJob.Count > 0)
            //    {
            //        foreach (ReportJob reportJob in runningJob)
            //        {
            //            if (reportJob.TheBatch.Id == reportBatch.Id)
            //            {
            //                resultReportJob.Add(reportJob);
            //            }
            //        }
            //    }
            //}

            //return resultReportJob;

            //if (runningJob == null)
            //{
            //    //reportJobList = latestJob;
            //    foreach(
               
            //}
            //else
            //{
                //reportJobList = runningJob;

            //if (latestJob != null)
            //{
            //    foreach (ReportJob latestrj in latestJob)
            //    {
            //        bool inRunningJob = false;
            //        if (runningJob != null)
            //        {
            //            foreach (ReportJob runningrj in runningJob)
            //            {
            //                if (latestrj.TheBatch.Id == runningrj.TheBatch.Id)
            //                {
            //                    inRunningJob = true;
            //                    reportJobList.Add(runningrj);
            //                    break;
            //                }
            //            }
            //        }
            //        if (!inRunningJob)
            //        {
            //            reportJobList.Add(latestrj);
            //        }
            //    }
            //}

            ////}
            //// Modified by vincent at 2007-12-04 end

            //if (reportJobList != null && reportBatchList != null)
            //{
            //    foreach (ReportJob rj in reportJobList)
            //    {
            //        if (!rj.Status.Equals("Sucess"))
            //        {
            //            int reportBatchId = rj.TheBatch.Id;
            //            foreach (ReportBatch rb in reportBatchList)
            //            {
            //                if (rb.Id == reportBatchId)
            //                {
            //                    rb.LastestReportJob = rj;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            //return reportBatchList;
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList FindReportJobByBatchId(int Id)
        {
            return reportJobDao.FindAllReportJobByBatchId(Id) as IList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindReportByJobId(int Id)
        {
            return (reportJobReportDao.FindAllByJobId(Id) as IList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList FindUserByJobId(int Id)
        {
            return (reportJobUserDao.FindAllByJobId(Id) as IList);
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelReportJob(ReportJob rj, User user)
        {
            //ReportJob rj = reportJobDao.LoadReportJob(id);
            //Modified Vincent at 2007-10-30 Begin
            if (rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_PENDING)
                || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_RUNNING, StringComparison.OrdinalIgnoreCase)
                || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_SUBMIT))
            {
                rj.Status = ReportJob.REPORT_JOB_STATUS_CANCEL;
                rj.EndTime = DateTime.Now;
                rj.UpdateDate = DateTime.Now;
                rj.UpdateUser = user;
                reportJobDao.UpdateReportJob(rj);
            }
            //Modified Vincent 2007-10-30 End
        }

        [Transaction(TransactionMode.Requires)]
        public void RestartReportJob(ReportJob rj, User user)
        {
            //ReportJob rj = reportJobDao.LoadReportJob(id);
            //if (rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_CANCEL) || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_FAILED))
            if (!rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_SUBMIT))
            {
                rj.Status = ReportJob.REPORT_JOB_STATUS_SUBMIT;
                rj.EndTime = DateTime.Now;
                rj.UpdateDate = DateTime.Now;
                rj.UpdateUser = user;
                reportJobDao.UpdateReportJob(rj);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void CancelReportJob(int id, User user)
        {
            ReportJob rj = reportJobDao.LoadReportJob(id);
            // Modified By Vincent At 2007-10-30 Begin
            if (rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_PENDING)
                || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_RUNNING, StringComparison.OrdinalIgnoreCase)
                || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_SUBMIT))
            {
                rj.Status = ReportJob.REPORT_JOB_STATUS_CANCEL;
                rj.EndTime = DateTime.Now;
                rj.UpdateDate = DateTime.Now;
                rj.UpdateUser = user;
                reportJobDao.UpdateReportJob(rj);
            }
            // Modified By Vincent At 2007-10-30 End
        }

        [Transaction(TransactionMode.Requires)]
        public void RestartReportJob(int id, User user)
        {
            ReportJob rj = reportJobDao.LoadReportJob(id);
            //1. restart the report job no matter success, cancel or fail
            //if (rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_SUBMIT) || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_FAILED))
            if (!rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_SUBMIT))
            {
                rj.Status = ReportJob.REPORT_JOB_STATUS_SUBMIT;
                rj.EndTime = DateTime.Now;
                rj.UpdateDate = DateTime.Now;
                rj.UpdateUser = user;
                reportJobDao.UpdateReportJob(rj);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void SubmitReportJob(int id, User user)
        {
            ReportJob rj = reportJobDao.LoadReportJob(id);
            if (rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_PENDING))
            {
                rj.Status = ReportJob.REPORT_JOB_STATUS_SUBMIT;
                rj.EndTime = DateTime.Now;
                rj.UpdateDate = DateTime.Now;
                rj.UpdateUser = user;
                reportJobDao.UpdateReportJob(rj);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public ReportJob CreateNewReportJobByBatchId(int id, User user)
        {
            //ReportJob rj = reportJobDao.FindLastestReportJobByBatchId(id);
            //if (rj == null || (rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_CANCEL)
            //    || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_FAILED) || rj.Status.Equals(ReportJob.REPORT_JOB_STATUS_SUCCESS)))
            //{
                ReportBatch rb = reportBatchDao.LoadReportBatch(id);
                ReportJob newReportJob = new ReportJob();
                newReportJob.TheBatch = rb;
                newReportJob.Status = ReportJob.REPORT_JOB_STATUS_PENDING;
                newReportJob.EmailBody = rb.EmailBody;
                newReportJob.EMailSubject = rb.EMailSubject;

                newReportJob.NeedSendMail = "NO";
                newReportJob.AppendDateToFileName = "YES";
                newReportJob.RunPreSQL = "YES";

                newReportJob.AppendUserNameToFileName = "NO";
                newReportJob.NeedCreateSubFolder = "NO";
                newReportJob.NeedUploadToPortal = "NO";                

                DateTime NowTime = DateTime.Now;
                if (NowTime.Hour >= 21)
                {
                    newReportJob.StartTime = NowTime.AddMinutes(30);
                }
                else
                {
                    newReportJob.StartTime = NowTime.AddHours(21 - NowTime.Hour).AddMinutes(-1*NowTime.Minute).AddSeconds(-1*NowTime.Second);
                }
                newReportJob.EndTime = DateTime.MaxValue;
                newReportJob.ReportDate = NowTime;

                newReportJob.CreateDate = NowTime;
                newReportJob.CreateUser = user;

                newReportJob.UpdateDate = NowTime;
                newReportJob.UpdateUser = user;

                this.CreateReportJob(newReportJob);

                IList<ReportUser> ruList = reportUserDao.FindUserForReportBatch(id);
                if (ruList != null)
                {
                    foreach (ReportUser ru in ruList)
                    {
                        ReportJobUser rju = new ReportJobUser();
                        rju.TheJob = newReportJob;
                        rju.TheUser = ru;
                        this.CreateReportJobUser(rju);
                    }
                }

                IList<ReportTemplate> rtList = reportTemplateDao.FindReportForReportBatch(id);
                if (rtList != null)
                {
                    foreach (ReportTemplate rt in rtList)
                    {
                        ReportJobReport rjr = new ReportJobReport();
                        rjr.TheJob = newReportJob;
                        rjr.TheReport = rt;
                        this.CreateReportJobReport(rjr);
                    }
                }

                return this.LoadReportJob(newReportJob.Id);
            //}
            //return this.LoadReportJob(rj.Id);
        }

        public IList<ReportUser> FindReportUserByReportBatchIdAndUserNameAndUserDescription(int batchId, string userName, string userDescription)
        {
            return reportUserSheetDao.FindReportUserByReportBatchIdAndUserNameAndUserDescription(batchId, userName, userDescription);
        }
        #endregion Customized Methods
    }
}
