using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportUserSheetDao
    {
        #region Method Created By CodeSmith

        void CreateReportUserSheet(ReportUserSheet entity);

        ReportUserSheet LoadReportUserSheet(int id);

        void UpdateReportUserSheet(ReportUserSheet entity);
        
        void DeleteReportUserSheet(int id);

        void DeleteReportUserSheet(ReportUserSheet entity);

        void DeleteReportUserSheet(IList<int> idList);

        void DeleteReportUserSheet(IList<ReportUserSheet> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList FindAllByReportUserId(int reportUserId);

        void DeleteAllByReportUserId(int Id);

        IList<ReportUser> FindReportUserByReportBatchIdAndUserNameAndUserDescription(int batchId, string userName, string userDescription);

        #endregion Customized Methods
    }
}
