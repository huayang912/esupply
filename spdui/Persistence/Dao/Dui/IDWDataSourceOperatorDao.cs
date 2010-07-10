using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDWDataSourceOperatorDao
    {
        #region Method Created By CodeSmith

        void CreateDWDataSourceOperator(DWDataSourceOperator entity);

        DWDataSourceOperator LoadDWDataSourceOperator(int id);

        void UpdateDWDataSourceOperator(DWDataSourceOperator entity);
        
        void DeleteDWDataSourceOperator(int id);

        void DeleteDWDataSourceOperator(DWDataSourceOperator entity);

        void DeleteDWDataSourceOperator(IList<int> idList);

        void DeleteDWDataSourceOperator(IList<DWDataSourceOperator> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods
        
        IList FindAllByDWDataSourceId(int DWDataSourceId);

        IList<DWDataSourceOperator> FindAllByDWDataSourceIdAndAllowType(int dsId, string type);
        
        void DeleteDWDataSourceOperatorByDSId(int dsId);

        #endregion Customized Methods
    }
}
