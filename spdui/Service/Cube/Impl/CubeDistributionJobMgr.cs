using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Security;
using Dndp.Persistence.Dao;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube.Impl
{
    [Transactional]
    public class CubeDistributionJobMgr : SessionBase, ICubeDistributionJobMgr
    {
        private ICubeDistributionJobDao jobDao;
        private ICubeDistributionJobItemDao jobItemDao;
        private ICubeDao cubeDao;
        private ICubeUserDao cubeUserDao;
        private SqlHelperDao sqlHelperDao;
        private ICubeMeasureDao measureDao;

        public CubeDistributionJobMgr(ICubeDistributionJobDao jobDao, 
                                      ICubeDistributionJobItemDao jobItemDao,
                                      ICubeDao cubeDao,
                                      ICubeUserDao cubeUserDao,
                                      SqlHelperDao sqlHelperDao,
                                      ICubeMeasureDao measureDao)
        {
            this.jobDao = jobDao;
            this.jobItemDao = jobItemDao;
            this.cubeDao = cubeDao;
            this.cubeUserDao = cubeUserDao;
            this.sqlHelperDao = sqlHelperDao;
            this.measureDao = measureDao;
        }

        #region Method CubeDistributionJob

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeDistributionJob(CubeDistributionJob entity)
        {
            //TODO: Add other code here.
			
            jobDao.CreateCubeDistributionJob(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeDistributionJob LoadCubeDistributionJob(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return jobDao.LoadCubeDistributionJob(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeDistributionJob(CubeDistributionJob entity)
        {
            //CubeDistributionJob newEntity = jobDao.LoadCubeDistributionJob(entity.Id);
            //if (newEntity.Status == CubeDistributionJob.DISTRIBUTION_STATUS_Success)
            //{                
            //    throw new ArgumentException("The Offline Cube Distrubution Job is already run successed, can't saved");
            //}
            //else if (newEntity.Status == CubeDistributionJob.DISTRIBUTION_STATUS_Submit
            //    && entity.Status != CubeDistributionJob.DISTRIBUTION_STATUS_Cancelled)
            //{
            //    throw new ArgumentException("The Offline Cube Distrubution Job is already submitted, can't saved");
            //}
            
            jobDao.UpdateCubeDistributionJob(entity);
            
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJob(int id)
        {
            //CubeDistributionJob newEntity = jobDao.LoadCubeDistributionJob(id);
            //if (newEntity.Status == CubeDistributionJob.DISTRIBUTION_STATUS_Success)
            //{
            //    throw new ArgumentException("The Offline Cube Distrubution Job is already run successed, can't be deleted");
            //}
            //else if (newEntity.Status == CubeDistributionJob.DISTRIBUTION_STATUS_Submit)
            //{
            //    throw new ArgumentException("The Offline Cube Distrubution Job is already submitted, can't be deleted");
            //}

            jobItemDao.DeleteCubeDistributionJobItemByJobId(id);
            jobDao.DeleteCubeDistributionJob(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJob(CubeDistributionJob entity)
        {
            DeleteCubeDistributionJob(entity.Id);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJob(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            foreach (int id in idList)
            {
                DeleteCubeDistributionJob(id);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJob(IList<CubeDistributionJob> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            foreach (CubeDistributionJob entity in entityList)
            {
                DeleteCubeDistributionJob(entity.Id);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public CubeDistributionJob CreateNewCubeDistributionJobByCubeId(int cubeId, User user)
        {
            CubeDefinition cube = cubeDao.LoadCube(cubeId);
            CubeDistributionJob newJob = new CubeDistributionJob();
            newJob.TheCube = cube;
            newJob.Status = CubeDistributionJob.DISTRIBUTION_STATUS_Pending;
            newJob.NeedSendMail = "NO";
            newJob.AppendDateToFileName = "YES";
            newJob.NeedPublishToPortal = "NO";
            newJob.NeedCreateSubFolder = "YES";
            DateTime NowTime = DateTime.Now;
            if (NowTime.Hour >= 21)
            {
                newJob.JobStartDate = NowTime.AddMinutes(30);
            }
            else
            {
                newJob.JobStartDate = NowTime.AddHours(21 - NowTime.Hour).AddMinutes(-1 * NowTime.Minute).AddSeconds(-1 * NowTime.Second);
            }
            newJob.JobEndDate = DateTime.MaxValue;
            DateTime beginDate = NowTime.AddYears(-2).AddMonths(-1 * (NowTime.Month - 1)).AddDays(-1 * (NowTime.Day - 1));
            newJob.BeginDate = beginDate;
            newJob.EndDate = NowTime;
            newJob.CreateDate = NowTime;
            newJob.Creator = user.UserName;
            newJob.UpdateDate = NowTime;
            // Modified by vincent at 2007-11-13 begin
            newJob.PublishFolder = DateTime.Now.ToString("yyyyMM");
            newJob.EMailSubject = "Offline Cube Description";
            newJob.EMailBody = "Offline Cube Description";
            // Modified by vincent at 2007-11-13 end
            IList<CubeMeasure> measureList = measureDao.FindMeasureByCubeId(cubeId);
            if (measureList != null && measureList.Count > 0)
            {
                string strMeasureList = string.Empty;
                foreach (CubeMeasure measure in measureList)
                {
                    strMeasureList += measure.Name + ",";
                }

                strMeasureList.Trim(',');

                newJob.MeasureList = strMeasureList;
            }
            else
            {
                newJob.MeasureList = string.Empty;
            }
            newJob.DimensionList = string.Empty;
            newJob.CubeRoleList = string.Empty;

            jobDao.CreateCubeDistributionJob(newJob);

            return newJob;
        }

        [Transaction(TransactionMode.NotSupported)]
        public CubeDistributionJob FindCubeDistributionJobWithAllInfo(int jobId)
        {
            CubeDistributionJob job = jobDao.LoadCubeDistributionJob(jobId);
            job.CubeDistributionJobItemList = 
                jobItemDao.FindCubeDistributionJobItemByCubeDistributionJobId(jobId);

            return job;
        }

        #endregion CubeDistributionJob

        #region Methods CubeDistributionJobItem

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeDistributionJobItem(CubeDistributionJobItem entity)
        {
            //TODO: Add other code here.

            jobItemDao.CreateCubeDistributionJobItem(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeDistributionJobItem LoadCubeDistributionJobItem(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return jobItemDao.LoadCubeDistributionJobItem(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeDistributionJobItem(CubeDistributionJobItem entity)
        {
           
            jobItemDao.UpdateCubeDistributionJobItem(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJobItem(int id)
        {
            jobItemDao.DeleteCubeDistributionJobItem(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJobItem(CubeDistributionJobItem entity)
        {
            jobItemDao.DeleteCubeDistributionJobItem(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJobItem(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            jobItemDao.DeleteCubeDistributionJobItem(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeDistributionJobItem(IList<CubeDistributionJobItem> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            jobItemDao.DeleteCubeDistributionJobItem(entityList);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeUser> FindCubeUserByCubeIdAndUserNameAndUserDescription(int cubeId, string userName, string userDescription)
        {
            return cubeUserDao.FindCubeUserByCubeIdAndUserNameAndUserDescription(cubeId, userName, userDescription);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeDistributionJobItem> FindJobItemByJobId(int jobId)
        {
            return jobItemDao.FindCubeDistributionJobItemByCubeDistributionJobId(jobId);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<CubeDistributionJobItem> AddCubeDistributionJobItems(CubeDistributionJob job, IList<int> cubeUserIds)
        { 
            if (cubeUserIds != null)
            {
                IList<CubeDistributionJobItem> list = new List<CubeDistributionJobItem>();
                foreach (int id in cubeUserIds)
                {
                    CubeDistributionJobItem jobItem = new CubeDistributionJobItem();
                    jobItem.TheJob = job;
                    jobItem.TheCubeUser = cubeUserDao.LoadCubeUser(id);
                    jobItem.UserName = jobItem.TheCubeUser.Name;
                    jobItem.ServerName = job.TheCube.ReleaseServerAddr;
                    jobItem.DatabaseName = job.TheCube.ReleaseDatabaseName;
                    jobItem.CubeName = job.TheCube.ReleaseCubeName;
                    jobItem.CubeUserName = jobItem.TheCubeUser.TheDistributionUser.DomainAccount;
                    jobItem.Email = jobItem.TheCubeUser.TheDistributionUser.Email;
                    jobItem.NeedSendMail = job.NeedSendMail;
                    jobItem.PortalUserName = jobItem.TheCubeUser.TheDistributionUser.DomainAccount;
                    jobItem.UserPortalSite = jobItem.TheCubeUser.CubeSite;
                    jobItem.UserPortalFolder = jobItem.TheCubeUser.CubeDocumentLibrary.Replace('\\', '/').Trim('/')
                        + (job.PublishFolder != null && job.PublishFolder.Trim().Length > 0 ? "/" + job.PublishFolder.Replace('\\', '/').Trim('/') : "")
                        + (job.NeedCreateSubFolder == "YES" ? "/" + jobItem.TheCubeUser.Name : "");
                    jobItem.NeedPublishToPortal = job.NeedPublishToPortal;
                    jobItem.PortalFolderFullControlUsers = jobItem.TheCubeUser.CubeFullControlUserList;
                    jobItem.PortalFolderReadUsers = jobItem.TheCubeUser.CubeReadUserList;
                    jobItem.CreateDate = DateTime.Now;
                    jobItem.UpdateDate = DateTime.Now;
                    jobItem.Status = CubeDistributionJobItem.JOB_ITEM_STATUS_PENDING;

                    jobItemDao.CreateCubeDistributionJobItem(jobItem);

                    list.Add(jobItem);
                }

                return list;
            }

            return null;
        }

        public void SynchronizeCubeDistributionJobItem(CubeDistributionJob job)
        {
            string sql = @"update CubeDistributionJobItem set NeedSendMail = job.NeedSendMail, NeedPublishToPortal = job.NeedPublishToPortal
                            from CubeDistributionJobItem as jobItem inner join CubeDistributionJob as job on job.jobId = jobItem.jobId 
                            where job.jobId = " + job.Id.ToString();
            sqlHelperDao.ExecuteNonQuery(sql);
        }
        #endregion Methods CubeDistributionJobItem
    }
}
