using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeOperatorDao
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

        IList<CubeOperator> FindAllByCubeIdAndAllowType(int cubeId, string type);

        IList<CubeOperator> FindOperatorByCubeId(int cubeId);

        void DeleteCubeOperatorByCubeId(int cubeId);

        #endregion Customized Methods
    }
}
