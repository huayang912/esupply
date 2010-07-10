using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDWDataSourceParameterDao
    {
        #region Method Created By CodeSmith

        void CreateDWDataSourceParameter(DWDataSourceParameter entity);

        DWDataSourceParameter LoadDWDataSourceParameter(int id);

        void UpdateDWDataSourceParameter(DWDataSourceParameter entity);
        
        void DeleteDWDataSourceParameter(int id);

        void DeleteDWDataSourceParameter(DWDataSourceParameter entity);

        void DeleteDWDataSourceParameter(IList<int> idList);

        void DeleteDWDataSourceParameter(IList<DWDataSourceParameter> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<DWDataSourceParameter> LoadAllActiveDWDataSourceParameter();

        #endregion Customized Methods
    }
}
