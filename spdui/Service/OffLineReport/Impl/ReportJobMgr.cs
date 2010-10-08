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
using Dndp.Utility.CSV;
using System.Data;
using Dndp.Persistence.Dao;
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
        private IReportJobValidationResultDao reportJobValidationResultDao;
        private IReportValidationRuleDao reportValidationRuleDao;
        private SqlHelperDao sqlHelperDao;

        public ReportJobMgr(IReportJobDao reportJobDao,
            IReportJobReportDao reportJobReportDao,
            IReportJobUserDao reportJobUserDao,
            IReportBatchDao reportBatchDao,
            IReportUserDao reportUserDao,
            IReportTemplateDao reportTemplateDao,
            IReportUserSheetDao reportUserSheetDao,
            IReportJobValidationResultDao reportJobValidationResultDao,
            IReportValidationRuleDao reportValidationRuleDao,
            SqlHelperDao sqlHelperDao)
        {
            this.reportJobDao = reportJobDao;
            this.reportJobReportDao = reportJobReportDao;
            this.reportJobUserDao = reportJobUserDao;
            this.reportBatchDao = reportBatchDao;
            this.reportUserDao = reportUserDao;
            this.reportTemplateDao = reportTemplateDao;
            this.reportUserSheetDao = reportUserSheetDao;
            this.reportJobValidationResultDao = reportJobValidationResultDao;
            this.reportValidationRuleDao = reportValidationRuleDao;
            this.sqlHelperDao = sqlHelperDao;
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
                rj.RuleList = this.FindRuleByJobId(id);
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
            reportJobValidationResultDao.DeleteAllByJobId(id);
            reportJobReportDao.DeleteAllByJobId(id);            
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteReportJob(ReportJob entity)
        {
            reportJobDao.DeleteReportJob(entity);
            reportJobUserDao.DeleteAllByJobId(entity.Id);
            reportJobValidationResultDao.DeleteAllByJobId(entity.Id);
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

        [Transaction(TransactionMode.Unspecified)]
        public IList FindRuleByJobId(int Id)
        {
            return (reportJobValidationResultDao.FindAllByJobId(Id) as IList);
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
                newReportJob.ValidateStatus = ReportJob.REPORT_JOB_VALIDATE_STATUS_WaitingValidate;
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

                IList rvList = this.reportValidationRuleDao.GetReportValidationRuleByBatchId(id);
                if (rvList != null)
                {
                    foreach (ReportValidationRule rv in rvList)
                    {
                        ReportJobValidationResult rjvr = new ReportJobValidationResult();
                        rjvr.TheJob = newReportJob;
                        rjvr.TheRule = rv;
                        this.reportJobValidationResultDao.CreateReportJobValidationResult(rjvr);
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

        public ReportJobValidationResult LoadReportJobValidationResult(int id)
        {
            return this.reportJobValidationResultDao.LoadReportJobValidationResult(id);
        }

        [Transaction(TransactionMode.NotSupported)]
        public void DownloadValidateResult(string rule, CSVWriter csvWriter, int jobId)
        {
    
            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(rule, this.reportJobDao.LoadReportJob(jobId), null); 

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();

            //write csv header
            string[] header = new string[dataReader.FieldCount];
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                header[i] = dataReader.GetName(i);
            }
            csvWriter.Write(header);
            csvWriter.WriteNewLine();

            //write csv content
            while (dataReader.Read())
            {
                object[] fields = new object[dataReader.FieldCount];
                dataReader.GetValues(fields);
                string[] strFields = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    if (fields[i] != null)
                    {
                        strFields[i] = fields[i].ToString();
                    }
                    else
                    {
                        strFields[i] = "";
                    }
                }
                csvWriter.Write(strFields);
                csvWriter.WriteNewLine();
            }
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList FindValidationResultByIds(string validationIds)
        {
            return this.reportJobValidationResultDao.FindValidationResultByIds(validationIds);
        }

        [Transaction(TransactionMode.Requires)]
        public ReportJobValidationResult ValidateRule(int id, User actionUser)
        {
            ReportJobValidationResult vr = this.reportJobValidationResultDao.LoadReportJobValidationResult(id);
            string rule = vr.TheRule.Content;

            IList dependenceVRList = this.reportJobValidationResultDao.FindValidationResultByDependenceRuleId(vr.TheRule.Id);

            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(rule, vr.TheJob, actionUser);

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();
            int failRowCount = 0;
            string orgStatus = vr.Status;
            while (dataReader.Read())
            {
                failRowCount++;
            }
            if (failRowCount > 0)
            {
                vr.Status = "Failed";
            }
            else
            {
                vr.Status = "Passed";
            }
            vr.FailedRowCount = failRowCount;
            this.reportJobValidationResultDao.UpdateReportJobValidationResult(vr);

            if (dependenceVRList != null && dependenceVRList.Count > 0)
            {
                foreach (ReportJobValidationResult dependenceVR in dependenceVRList)
                {
                    dependenceVR.Status = vr.Status;
                    dependenceVR.FailedRowCount = vr.FailedRowCount;
                    this.reportJobValidationResultDao.UpdateReportJobValidationResult(dependenceVR);
                }
            }

            RefreshValidateResultCount(vr.TheJob.Id);
            return vr;
        }
        #endregion Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        private void RefreshValidateResultCount(int reportJobId)
        {
            ReportJob reportJob = this.reportJobDao.LoadReportJob(reportJobId);
            reportJob.Errors = 0;
            reportJob.Problems = 0;
            reportJob.Warnings = 0;
            reportJob.RuleList = this.reportJobValidationResultDao.FindAllByJobId(reportJobId);
            if (reportJob.RuleList != null)
            {
                IList<ReportJobValidationResult> list = new List<ReportJobValidationResult>();
                int pendingErrorRule = 0;
                foreach (ReportJobValidationResult vr in reportJob.RuleList)
                {
                    if (vr.TheRule.Type == "ERROR" && vr.Status == ReportJobValidationResult.ReportJobValidationResult_Status_Failed)
                    {
                        reportJob.Errors = reportJob.Errors + 1;
                    }

                    if (vr.TheRule.Type == "ERROR" && vr.Status == ReportJobValidationResult.ReportJobValidationResult_Status_Pending)
                    {
                        pendingErrorRule++;
                    }

                    if (vr.TheRule.Type == "PROBLEM" && vr.Status == ReportJobValidationResult.ReportJobValidationResult_Status_Failed)
                    {
                        reportJob.Problems = reportJob.Problems + 1;
                    }
                    if (vr.TheRule.Type == "WARNING" && vr.Status == ReportJobValidationResult.ReportJobValidationResult_Status_Failed)
                    {
                        reportJob.Warnings = reportJob.Warnings + 1;
                    }
                }

                if (reportJob.Errors > 0)
                {
                    reportJob.ValidateStatus = ReportJob.REPORT_JOB_VALIDATE_STATUS_ValidatedFailed;
                }
                else if (pendingErrorRule > 0)
                {
                    reportJob.ValidateStatus = ReportJob.REPORT_JOB_VALIDATE_STATUS_WaitingValidate;
                }
                else
                {
                    reportJob.ValidateStatus = ReportJob.REPORT_JOB_VALIDATE_STATUS_ValidatedPassed;
                }
            }
        }

        private string UpdateValidationSQLContent(string rule, ReportJob reportJob, User actionUser)
        {
            //rule = rule.Replace("<$Category$>", vr.TheDataSourceUpload.TheDataSourceCategory.Name.ToString());
            ////rule = rule.Replace("<$BatchNo$>", vr.TheDataSourceUpload.BatchNo.ToString());
            rule = rule.Replace("<$ReportDate$>", reportJob.ReportDate.ToShortDateString());
            if (actionUser != null)
            {
                rule = rule.Replace("<$ActionUser$>", actionUser.Id.ToString());
            }
            return rule;
        }

    }
}
