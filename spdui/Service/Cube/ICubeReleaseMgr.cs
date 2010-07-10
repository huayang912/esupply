using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeReleaseMgr
    {
        #region Method Created By CodeSmith

        void CreateCubeRelease(CubeRelease entity);

        CubeRelease LoadCubeRelease(int id);

        void UpdateCubeRelease(CubeRelease entity);

        void DeleteCubeRelease(int id);

        void DeleteCubeRelease(CubeRelease entity);

        void DeleteCubeRelease(IList<int> idList);

        void DeleteCubeRelease(IList<CubeRelease> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods


        void ReleaseCube(CubeProcess process, User user);
        bool IsProcessCancelled(int cubeid);
        bool IsDistributing(int cubeId);
        bool IsReleasing(int cubeId);
        void RunPackage(string packageName);
        void WarmCache();
        //void RollbackCube(CubeRelease lastRelease, User user);

        void UploadRoleToCube(int cubeId);

        IList<CubeRelease> FindAllCubeReleaseByCubeId(int cubeId, User user);

        void UpdateProcessDescription(int cubeId, string description);

        #endregion Customized Methods

    }
}
