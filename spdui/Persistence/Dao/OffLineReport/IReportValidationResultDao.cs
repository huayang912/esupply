using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportValidationResultDao
    {
        #region Method Created By CodeSmith

        void CreateReportValidationResult(ReportValidationResult entity);

        ReportValidationResult LoadReportValidationResult(int id);

        void UpdateReportValidationResult(ReportValidationResult entity);
        
        void DeleteReportValidationResult(int id);

        void DeleteReportValidationResult(ReportValidationResult entity);

        void DeleteReportValidationResult(IList<int> idList);

        void DeleteReportValidationResult(IList<ReportValidationResult> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        #endregion Customized Methods
    }
}
