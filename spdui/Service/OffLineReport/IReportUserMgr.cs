using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Service.OffLineReport
{
    public interface IReportUserMgr
    {
        #region Method Created By CodeSmith

        void CreateReportUser(ReportUser entity);

        ReportUser LoadReportUser(int id);

        void UpdateReportUser(ReportUser entity);

        void DeleteReportUser(int id);

        void DeleteReportUser(ReportUser entity);

        void DeleteReportUser(IList<int> idList);

        void DeleteReportUser(IList<ReportUser> entityList);

	    void CreateReportUserSheet(ReportUserSheet entity);

        ReportUserSheet LoadReportUserSheet(int id);

        void UpdateReportUserSheet(IList<int> idList, int userId);

        void DeleteReportUserSheet(int id);

        void DeleteReportUserSheet(ReportUserSheet entity);

        void DeleteReportUserSheet(IList<int> idList);

        void DeleteReportUserSheet(IList<ReportUserSheet> entityList);

	    void CreateReportUserSheetParameter(ReportUserSheetParameter entity);

        ReportUserSheetParameter LoadReportUserSheetParameter(int id);

        void UpdateReportUserSheetParameter(ReportUserSheetParameter entity);

        void DeleteReportUserSheetParameter(int id);

        void DeleteReportUserSheetParameter(ReportUserSheetParameter entity);

        void DeleteReportUserSheetParameter(IList<int> idList);

        void DeleteReportUserSheetParameter(IList<ReportUserSheetParameter> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        IList LoadAllActiveReportUser();

        IList FindReportByReportUserId(int Id);

        IList FindParameterByReportUserId(int Id);

        IList<ReportParameter> FindParameterForReportUser(int Id);

        IList<ReportUser> FindReportUserByName(string userName);

        #endregion Customized Methods

    }
}
