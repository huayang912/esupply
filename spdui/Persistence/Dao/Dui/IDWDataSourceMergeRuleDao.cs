using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IDWDataSourceMergeRuleDao
    {
        #region Method Created By CodeSmith

        void CreateDWDataSourceMergeRule(DWDataSourceMergeRule entity);

        DWDataSourceMergeRule LoadDWDataSourceMergeRule(int id);

        void UpdateDWDataSourceMergeRule(DWDataSourceMergeRule entity);
        
        void DeleteDWDataSourceMergeRule(int id);

        void DeleteDWDataSourceMergeRule(DWDataSourceMergeRule entity);

        void DeleteDWDataSourceMergeRule(IList<int> idList);

        void DeleteDWDataSourceMergeRule(IList<DWDataSourceMergeRule> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList FindAllByDWDataSourceId(int dwDataSourceId);

        IList FindAllByDWDataSourceMergeRuleIds(string ruleIds);

        int GetMaxSequenceNo(int dataSourceId, string RuleType);

        void DeleteDWDataSourceMergeRuleByDSId(int dsId);

        IList FindAllByDependenceRuleId(int ruleId);

        #endregion Customized Methods
    }
}
