using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDataSourceRuleDao
    {
        #region Method Created By CodeSmith

        void CreateDataSourceRule(DataSourceRule entity);

        DataSourceRule LoadDataSourceRule(int id);

        void UpdateDataSourceRule(DataSourceRule entity);
        
        void DeleteDataSourceRule(int id);

        void DeleteDataSourceRule(DataSourceRule entity);

        void DeleteDataSourceRule(IList<int> idList);

        void DeleteDataSourceRule(IList<DataSourceRule> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList FindAllByDataSourceId(int dataSourceId);

        int GetMaxSequenceNo(int dataSourceId, string RuleType);

        void DeleteDataSourceRuleByDSId(int dsId);

        #endregion Customized Methods
    }
}
