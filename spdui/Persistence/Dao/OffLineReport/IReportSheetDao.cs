using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportSheetDao
    {
        #region Method Created By CodeSmith

        void CreateReportSheet(ReportSheet entity);

        ReportSheet LoadReportSheet(int id);

        void UpdateReportSheet(ReportSheet entity);
        
        void DeleteReportSheet(int id);

        void DeleteReportSheet(ReportSheet entity);

        void DeleteReportSheet(IList<int> idList);

        void DeleteReportSheet(IList<ReportSheet> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList FindAllByReportId(int reportId);

        void DeleteAllByReportId(int Id);

        #endregion Customized Methods
    }
}
