using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeDefinedParameterDao
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

        void CreateCubeDefinedParameter(IList<CubeDefinedParameter> list);

        void DeleteCubeDefinedParameterByCubeId(int cubeId);

        IList<CubeDefinedParameter> FindCubeDefinedParameterByCubeId(int cubeId);

        #endregion Customized Methods
    }
}
