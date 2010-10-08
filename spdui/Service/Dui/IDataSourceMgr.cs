using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Dui;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Service.Dui
{
    public interface IDataSourceMgr
    {
        #region Method Created By CodeSmith

        void CreateDataSource(DataSource entity);

        DataSource LoadDataSource(int id);

        void UpdateDataSource(DataSource entity);

        void DeleteDataSource(int id);

        void DeleteDataSource(DataSource entity);

        void DeleteDataSource(IList<int> idList);

        void DeleteDataSource(IList<DataSource> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        DataSourceRule LoadDataSourceRule(int dsRuleId);

        DataSourceUpload LoadDataSourceUpload(int dsUploadId);

        DataSourceCategory LoadDataSourceCategory(int dsCategoryId);

        DataSourceCategory LoadDataSourceCategory(int dsCategoryId, bool includeUser);

        IList LoadAllActiveDataSource();

        IList<ValidationResult> FindValidationResultByIds(string validationResultIds);

        void DeleteDataSourceField(IList<int> fieldIdList);

        void DeleteDataSourceOperator(IList<int> dataSourceOperatorIdList);

        void DeleteDataSourceRule(IList<int> ruleIdList);

        void DeleteDataSourceCategory(IList<int> categoryIdList);

        void DeleteDataSourceWithDrawTable(IList<int> withDrawTableIdList);

        void RefreshWithDrawTableCount(DataSource TheDataSource);

        void AddDataSourceField(int dataSourceId, DataSourceField dsf);

        bool IsDuplicateField(int dsId, string newFieldNm);

        IList FindDataSourceFieldByDataSourceId(int dsId);

        IList FindDataSourceOperatorByDataSourceId(int dsId);        

        IList FindDataSourceRuleByDataSourceId(int dsId);

        IList FindDataSourceCategoryByDataSourceId(int dsId);

        IList FindDataSourceCategoryByDataSourceId(int dsId, bool includeInactive);

        IList FindDataSourceCategoryByDataSourceId(int dsId, bool includeInactive, User user);

        IList FindDataSourceWithDrawTableByDataSourceId(int dsId);

        IList<User> FindUserByRole(int roleId);

        IList<User> GetAllUser();

        IList<DataSourceOperator> FindOperatorByDSIdAndAllowType(int dsId, string type);

        void CreateDataSourceRule(DataSourceRule dsu);

        void UpdateDateSourceRule(DataSourceRule dsu);

        void UpdateDataSourceOperator(IList<int> userIdList, int dsId, string allowType);

        void CreateDataSourceCategory(DataSourceCategory dsc);

        void UpdateDataSourceCategory(DataSourceCategory dsc, IList<int> userIdList);

        void CreateDataSourceWithDrawTable(DataSourceWithDrawTable dsc);

        IList<DataSource> FindActiveDataSourceByTypeAndName(string type, string name);

        IList<DataSource> FindActiveDataSourceByTypeAndName(string type, string name, User user);

        IList<string> FindAllDataSourceType();

        IList<string> FindAllDataSourceType(User user);

        #endregion Customized Methods

    }
}
