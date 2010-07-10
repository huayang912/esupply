using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportJobDao
    {
        #region Method Created By CodeSmith

        void CreateReportJob(ReportJob entity);

        ReportJob LoadReportJob(int id);

        void UpdateReportJob(ReportJob entity);
        
        void DeleteReportJob(int id);

        void DeleteReportJob(ReportJob entity);

        void DeleteReportJob(IList<int> idList);

        void DeleteReportJob(IList<ReportJob> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList<ReportJob> FindAllLastestReportJob();

        // Modified by vincent at 2007-12-04 Begin
        IList<ReportJob> FindAllLastestRunningReportJob();
        // Modified by vincent at 2007-12-04 End

        ReportJob FindLastestReportJobByBatchId(int batchId);

        IList<ReportJob> FindAllReportJobByBatchId(int batchId);

        void DeleteAllReportJobByBatchId(int batchId);

        #endregion Customized Methods
    }
}
