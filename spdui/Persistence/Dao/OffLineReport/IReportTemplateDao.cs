using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportTemplateDao
    {
        #region Method Created By CodeSmith

        void CreateReportTemplate(ReportTemplate entity);

        ReportTemplate LoadReportTemplate(int id);

        void UpdateReportTemplate(ReportTemplate entity);
        
        void DeleteReportTemplate(int id);

        void DeleteReportTemplate(ReportTemplate entity);

        void DeleteReportTemplate(IList<int> idList);

        void DeleteReportTemplate(IList<ReportTemplate> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList LoadAllActiveReportTemplate();

        IList<ReportTemplate> FindReportForReportBatch(int Id);

        #endregion Customized Methods
    }
}
