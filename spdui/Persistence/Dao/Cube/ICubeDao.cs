using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeDao
    {
        #region Method Created By CodeSmith

        void CreateCube(CubeDefinition entity);

        CubeDefinition LoadCube(int id);

        void UpdateCube(CubeDefinition entity);
        
        void DeleteCube(int id);

        void DeleteCube(CubeDefinition entity);

        void DeleteCube(IList<int> idList);

        void DeleteCube(IList<CubeDefinition> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeDefinition> LoadAllActiveCube();

        IList<CubeDefinition> FindCubeByUserIdAndAllowType(int userId, string allowType);

        #endregion Customized Methods
    }
}
