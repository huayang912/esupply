using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeProcessValidationResultDao
    {
        #region Method Created By CodeSmith

        void CreateCubeProcessValidationResult(CubeProcessValidationResult entity);

        CubeProcessValidationResult LoadCubeProcessValidationResult(int id);

        void UpdateCubeProcessValidationResult(CubeProcessValidationResult entity);
        
        void DeleteCubeProcessValidationResult(int id);

        void DeleteCubeProcessValidationResult(CubeProcessValidationResult entity);

        void DeleteCubeProcessValidationResult(IList<int> idList);

        void DeleteCubeProcessValidationResult(IList<CubeProcessValidationResult> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeProcessValidationResult> FindCubeProcessValidationResultByProcessId(int processId, string validationTarget);

        void DeleteCubeProcessValidationResultByProcessId(int processId);

        IList<CubeProcessValidationResult> FindCubeProcessValidationResultByIds(string validationIds);

        IList<CubeProcessValidationResult> FindCubeProcessValidationResultByDependenceRuleId(int ruleId);

        #endregion Customized Methods
    }
}
