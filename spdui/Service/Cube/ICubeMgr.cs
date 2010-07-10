using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeMgr
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

        CubeDefinition FindCubeWtihFullInformationById(int id);

        IList<CubeDefinition> FindAllCubeForCubeProcessByUserId(int userId);

        IList<CubeDefinition> FindAllCubeForCubeDistribution();       

        #endregion Customized Methods

    }
}
