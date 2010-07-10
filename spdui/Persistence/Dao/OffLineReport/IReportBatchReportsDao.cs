using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportBatchReportsDao
    {
        #region Method Created By CodeSmith

        void CreateReportBatchReports(ReportBatchReports entity);

        ReportBatchReports LoadReportBatchReports(int id);

        void UpdateReportBatchReports(ReportBatchReports entity);
        
        void DeleteReportBatchReports(int id);

        void DeleteReportBatchReports(ReportBatchReports entity);

        void DeleteReportBatchReports(IList<int> idList);

        void DeleteReportBatchReports(IList<ReportBatchReports> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList FindAllByBatchId(int batchId);

        void DeleteAllByBatchId(int Id);

        #endregion Customized Methods
    }
}
