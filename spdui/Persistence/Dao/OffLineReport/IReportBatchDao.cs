using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportBatchDao
    {
        #region Method Created By CodeSmith

        void CreateReportBatch(ReportBatch entity);

        ReportBatch LoadReportBatch(int id);

        void UpdateReportBatch(ReportBatch entity);
        
        void DeleteReportBatch(int id);

        void DeleteReportBatch(ReportBatch entity);

        void DeleteReportBatch(IList<int> idList);

        void DeleteReportBatch(IList<ReportBatch> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.

        IList LoadAllActiveReportBatch();

        IList LoadlActiveReportBatchByUser(User user);

        #endregion Customized Methods
    }
}
