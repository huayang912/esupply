using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportJobUserDao
    {
        #region Method Created By CodeSmith

        void CreateReportJobUser(ReportJobUser entity);

        ReportJobUser LoadReportJobUser(int id);

        void UpdateReportJobUser(ReportJobUser entity);
        
        void DeleteReportJobUser(int id);

        void DeleteReportJobUser(ReportJobUser entity);

        void DeleteReportJobUser(IList<int> idList);

        void DeleteReportJobUser(IList<ReportJobUser> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList FindAllByJobId(int Id);

        void DeleteAllByJobId(int Id);

        IList<ReportUser> FindReportJobUserByName(int Id, string userName);

        #endregion Customized Methods
    }
}
