using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportJobValidationResultDao
    {
        #region Method Created By CodeSmith

        void CreateReportJobValidationResult(ReportJobValidationResult entity);

        ReportJobValidationResult LoadReportJobValidationResult(int id);

        void UpdateReportJobValidationResult(ReportJobValidationResult entity);
        
        void DeleteReportJobValidationResult(int id);

        void DeleteReportJobValidationResult(ReportJobValidationResult entity);

        void DeleteReportJobValidationResult(IList<int> idList);

        void DeleteReportJobValidationResult(IList<ReportJobValidationResult> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList FindAllByJobId(int Id);

        void DeleteAllByJobId(int Id);

        IList FindValidationResultByIds(string validationIds);

        IList FindValidationResultByDependenceRuleId(int ruleId);

        #endregion Customized Methods
    }
}
