using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Entity.Security;
using Dndp.Utility.CSV;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeProcessMgr
    {
        #region Method Created By CodeSmith

        #region CubeProcess Related
        void CreateCubeProcess(CubeProcess entity);

        CubeProcess LoadCubeProcess(int id);

        void UpdateCubeProcess(CubeProcess entity);

        void DeleteCubeProcess(int id);

        void DeleteCubeProcess(CubeProcess entity);

        void DeleteCubeProcess(IList<int> idList);

        void DeleteCubeProcess(IList<CubeProcess> entityList);

        IList<CubeProcess> FindAllLastestCubeProcess(int userId);

        CubeProcess FindLastestCubeProcessByCubeId(int cubeId, int userId);

        IList<CubeProcess> FindAllCubeProcessByCubeId(int cubeId, int userId);

        CubeProcess CreateNewCubeProcess(CubeDefinition cube, User user, Hashtable parameter, string cubeProcessDescription);

        CubeProcess FindCubeProcessWithAllInfoById(int id);

        IList<CubeProcess> FindAllCubeForCubeReleaseByUserId(int userId);

        #endregion CubeProcess Related

        #region CubeProcessParameter Related
        void CreateCubeProcessParameter(CubeProcessParameter entity);

        CubeProcessParameter LoadCubeProcessParameter(int id);

        void UpdateCubeProcessParameter(CubeProcessParameter entity);

        void DeleteCubeProcessParameter(int id);

        void DeleteCubeProcessParameter(CubeProcessParameter entity);

        void DeleteCubeProcessParameter(IList<int> idList);

        void DeleteCubeProcessParameter(IList<CubeProcessParameter> entityList);
        #endregion CubeProcessParameter Related

        #region CubeProcessValidationResult Related
        void CreateCubeProcessValidationResult(CubeProcessValidationResult entity);

        CubeProcessValidationResult LoadCubeProcessValidationResult(int id);

        void UpdateCubeProcessValidationResult(CubeProcessValidationResult entity);

        void DeleteCubeProcessValidationResult(int id);

        void DeleteCubeProcessValidationResult(CubeProcessValidationResult entity);

        void DeleteCubeProcessValidationResult(IList<int> idList);

        void DeleteCubeProcessValidationResult(IList<CubeProcessValidationResult> entityList);

        IList<CubeProcessValidationResult> FindCubeProcessValidationResultByIds(string validationIds);

        IList<CubeProcessValidationResult> FindCubeProcessValidationResultByProcessId(int processId, string validationTarget);

        CubeProcessValidationResult ValidateCubeProcessRule(int validateResultId, IList<CubeProcessParameter> processParameterList, User actionUser);

        void DownloadCubeProcessValidateResult(string rule, IList<CubeProcessParameter> processParameterList, CSVWriter csvWriter);
        #endregion CubeProcessValidationResult Related

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods

    }
}
