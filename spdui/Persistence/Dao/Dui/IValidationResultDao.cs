using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Dui
{
    public interface IValidationResultDao
    {
        #region Method Created By CodeSmith

        void CreateValidationResult(ValidationResult entity);

        ValidationResult LoadValidationResult(int id);

        void UpdateValidationResult(ValidationResult entity);
        
        void DeleteValidationResult(int id);

        void DeleteValidationResult(ValidationResult entity);

        void DeleteValidationResult(IList<int> idList);

        void DeleteValidationResult(IList<ValidationResult> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        void DeleteValidationResultByDSUploadId(int dsUploadId);

        IList<ValidationResult> FindAllByDSUploadId(int dsUploadId);

        IList<ValidationResult> FindAllByDSUploadIdAndRuleType(int dsUploadId, string ruleType);

        IList<ValidationResult> FindAllByIds(string validationResultIds);

        void DeleteValidationResultByDSId(int dsId);

        IList<ValidationResult> FindAllByDependenceRuleId(int ruleId);

        #endregion Customized Methods
    }
}
