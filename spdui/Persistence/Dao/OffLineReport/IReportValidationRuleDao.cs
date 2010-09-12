using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportValidationRuleDao
    {
        #region Method Created By CodeSmith

        void CreateReportValidationRule(ReportValidationRule entity);

        ReportValidationRule LoadReportValidationRule(int id);

        void UpdateReportValidationRule(ReportValidationRule entity);
        
        void DeleteReportValidationRule(int id);

        void DeleteReportValidationRule(ReportValidationRule entity);

        void DeleteReportValidationRule(IList<int> idList);

        void DeleteReportValidationRule(IList<ReportValidationRule> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList GetReportValidationRuleByBatchId(int id);

        #endregion Customized Methods
    }
}
