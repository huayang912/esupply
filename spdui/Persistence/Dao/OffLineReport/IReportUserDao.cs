using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportUserDao
    {
        #region Method Created By CodeSmith

        void CreateReportUser(ReportUser entity);

        ReportUser LoadReportUser(int id);

        void UpdateReportUser(ReportUser entity);
        
        void DeleteReportUser(int id);

        void DeleteReportUser(ReportUser entity);

        void DeleteReportUser(IList<int> idList);

        void DeleteReportUser(IList<ReportUser> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList LoadAllActiveReportUser();

        IList<ReportUser> FindUserForReportBatch(int Id);

        IList<ReportUser> FindReportUserByName(string userName);

        #endregion Customized Methods
    }
}
