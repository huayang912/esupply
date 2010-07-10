using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportJobReportDao
    {
        #region Method Created By CodeSmith

        void CreateReportJobReport(ReportJobReport entity);

        ReportJobReport LoadReportJobReport(int id);

        void UpdateReportJobReport(ReportJobReport entity);
        
        void DeleteReportJobReport(int id);

        void DeleteReportJobReport(ReportJobReport entity);

        void DeleteReportJobReport(IList<int> idList);

        void DeleteReportJobReport(IList<ReportJobReport> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList FindAllByJobId(int Id);

        void DeleteAllByJobId(int Id);

        #endregion Customized Methods
    }
}
