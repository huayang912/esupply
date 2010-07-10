using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.OffLineReport;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.OffLineReport
{
    public interface IReportParameterDao
    {
        #region Method Created By CodeSmith

        void CreateReportParameter(ReportParameter entity);

        ReportParameter LoadReportParameter(int id);

        void UpdateReportParameter(ReportParameter entity);
        
        void DeleteReportParameter(int id);

        void DeleteReportParameter(ReportParameter entity);

        void DeleteReportParameter(IList<int> idList);

        void DeleteReportParameter(IList<ReportParameter> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other customized methods.
        IList LoadAllActiveReportParameter();

        #endregion Customized Methods
    }
}
