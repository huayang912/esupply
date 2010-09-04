using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportBatchUserDao
    {
        #region Method Created By CodeSmith

        void CreateReportBatchUser(ReportBatchUser entity);

        ReportBatchUser LoadReportBatchUser(int id);

        void UpdateReportBatchUser(ReportBatchUser entity);

        void DeleteReportBatchUser(int id);

        void DeleteReportBatchUser(ReportBatchUser entity);

        void DeleteReportBatchUser(IList<int> idList);

        void DeleteReportBatchUser(IList<ReportBatchUser> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList FindAllByBatchId(int batchId);

        #endregion Customized Methods
    }
}
