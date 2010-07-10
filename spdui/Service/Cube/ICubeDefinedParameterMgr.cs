using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeDefinedParameterMgr
    {
        #region Method Created By CodeSmith

        void CreateCubeDefinedParameter(CubeDefinedParameter entity);

        CubeDefinedParameter LoadCubeDefinedParameter(int id);

        void UpdateCubeDefinedParameter(CubeDefinedParameter entity);

        void DeleteCubeDefinedParameter(int id);

        void DeleteCubeDefinedParameter(CubeDefinedParameter entity);

        void DeleteCubeDefinedParameter(IList<int> idList);

        void DeleteCubeDefinedParameter(IList<CubeDefinedParameter> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeDefinedParameter> FindCubeDefinedParameterByCubeId(int cubeId);

        #endregion Customized Methods

    }
}
