using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeOperatorMgr
    {
        #region Method Created By CodeSmith

        void CreateCubeOperator(CubeOperator entity);

        CubeOperator LoadCubeOperator(int id);

        void UpdateCubeOperator(CubeOperator entity);

        void DeleteCubeOperator(int id);

        void DeleteCubeOperator(CubeOperator entity);

        void DeleteCubeOperator(IList<int> idList);

        void DeleteCubeOperator(IList<CubeOperator> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeOperator> FindOperatorByCubeIdAndAllowType(int cubeId, string type);

        IList<CubeOperator> FindOperatorByCubeId(int cubeId);

        void UpdateCubeOperator(IList<int> userIdList, int cubeId, string allowType);

        #endregion Customized Methods

    }
}
