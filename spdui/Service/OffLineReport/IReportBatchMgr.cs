using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Service.OffLineReport
{
    public interface IReportBatchMgr
    {
        #region Method Created By CodeSmith

        void CreateReportBatch(ReportBatch entity);

        ReportBatch LoadReportBatch(int id);

        void UpdateReportBatch(ReportBatch entity);

        void DeleteReportBatch(int id);

        void DeleteReportBatch(ReportBatch entity);

        void DeleteReportBatch(IList<int> idList);

        void DeleteReportBatch(IList<ReportBatch> entityList);

        void CreateReportBatchReports(ReportBatchReports entity);

        ReportBatchReports LoadReportBatchReports(int id);

        void UpdateReportBatchReports(IList<int> idList, int batchId);

        void DeleteReportBatchReports(int id);

        void DeleteReportBatchReports(ReportBatchReports entity);

        void DeleteReportBatchReports(IList<int> idList);

        void DeleteReportBatchReports(IList<ReportBatchReports> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        IList LoadAllActiveReportBatch();

        IList FindReportByBatchId(int Id);

        void AddReportBatchUser(int batchId, IList<int> userIdList);

        IList FindUserByBatchId(int batchId);

        void DeleteReportBatchUser(IList<int> idList);

        void DeleteReportRule(IList<int> idList);

        #endregion Customized Methods

    }
}
