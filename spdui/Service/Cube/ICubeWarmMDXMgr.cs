using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeWarmMDXMgr
    {
        #region Method Created By CodeSmith

        void CreateCubeWarmMDX(CubeWarmMDX entity);

        CubeWarmMDX LoadCubeWarmMDX(int id);

        void UpdateCubeWarmMDX(CubeWarmMDX entity);

        void DeleteCubeWarmMDX(int id);

        void DeleteCubeWarmMDX(CubeWarmMDX entity);

        void DeleteCubeWarmMDX(IList<int> idList);

        void DeleteCubeWarmMDX(IList<CubeWarmMDX> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeWarmMDX> FindCubeWarmMDXByCubeId(int cubeId);

        void DeleteCubeWarmMDXByCubeId(int cubeId);

        #endregion Customized Methods

    }
}
