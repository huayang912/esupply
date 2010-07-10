using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Service.OffLineReport
{
    public interface IReportTemplateMgr
    {
        #region Method Created By CodeSmith

        void CreateReportTemplate(ReportTemplate entity);

        ReportTemplate LoadReportTemplate(int id);

        void UpdateReportTemplate(ReportTemplate entity);

        void DeleteReportTemplate(int id);

        void DeleteReportTemplate(ReportTemplate entity);

        void DeleteReportTemplate(IList<int> idList);

        void DeleteReportTemplate(IList<ReportTemplate> entityList);

	void CreateReportSheet(ReportSheet entity);

        ReportSheet LoadReportSheet(int id);

        void UpdateReportSheet(ReportSheet entity);

        void DeleteReportSheet(int id);

        void DeleteReportSheet(ReportSheet entity);

        void DeleteReportSheet(IList<int> idList);

        void DeleteReportSheet(IList<ReportSheet> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        IList LoadAllActiveReportTemplate();

        IList FindReportSheetByReportId(int Id);

        #endregion Customized Methods

    }
}
