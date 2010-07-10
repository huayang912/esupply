using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeValidationRuleMgr
    {
        #region Method Created By CodeSmith

        void CreateCubeValidationRule(CubeValidationRule entity);

        CubeValidationRule LoadCubeValidationRule(int id);

        void UpdateCubeValidationRule(CubeValidationRule entity);

        void DeleteCubeValidationRule(int id);

        void DeleteCubeValidationRule(CubeValidationRule entity);

        void DeleteCubeValidationRule(IList<int> idList);

        void DeleteCubeValidationRule(IList<CubeValidationRule> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeValidationRule> FindCubeValidationRuleWithCubeId(int id);

        #endregion Customized Methods

    }
}
