using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeParameterMgr
    {
        #region Method Created By CodeSmith

        void CreateCubeParameter(CubeParameter entity);

        CubeParameter LoadCubeParameter(int id);

        void UpdateCubeParameter(CubeParameter entity);

        void DeleteCubeParameter(int id);

        void DeleteCubeParameter(CubeParameter entity);

        void DeleteCubeParameter(IList<int> idList);

        void DeleteCubeParameter(IList<CubeParameter> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeParameter> LoadAllActiveCubeParameter();

        #endregion Customized Methods

    }
}
