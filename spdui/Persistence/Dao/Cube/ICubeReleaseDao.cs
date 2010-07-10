using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeReleaseDao
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

        IList<CubeRelease> FindAllLastestCubeRelease(int userId);

        IList<CubeRelease> FindAllSuccessLastestCubeRelease(int userId);

        CubeRelease FindLastestCubeReleaseByCubeId(int cubeId, int userId);

        CubeRelease FindLastestSuccessCubeReleaseByCubeId(int cubeId, int userId);

        IList<CubeRelease> FindAllCubeReleaseByCubeId(int cubeId, int userId);

        #endregion Customized Methods
    }
}
