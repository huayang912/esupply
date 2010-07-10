using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportUserSheetParameterDao
    {
        #region Method Created By CodeSmith

        void CreateReportUserSheetParameter(ReportUserSheetParameter entity);

        ReportUserSheetParameter LoadReportUserSheetParameter(int id);

        void UpdateReportUserSheetParameter(ReportUserSheetParameter entity);
        
        void DeleteReportUserSheetParameter(int id);

        void DeleteReportUserSheetParameter(ReportUserSheetParameter entity);

        void DeleteReportUserSheetParameter(IList<int> idList);

        void DeleteReportUserSheetParameter(IList<ReportUserSheetParameter> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList FindAllByReportUserId(int reportUserId);

        void DeleteAllByReportParameterId(int Id);

        void DeleteAllByReportUserId(int Id);

        #endregion Customized Methods
    }
}
