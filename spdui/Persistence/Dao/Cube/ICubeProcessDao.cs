using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeProcessDao
    {
        #region Method Created By CodeSmith

        void CreateCubeProcess(CubeProcess entity);

        CubeProcess LoadCubeProcess(int id);

        void UpdateCubeProcess(CubeProcess entity);
        
        void DeleteCubeProcess(int id);

        void DeleteCubeProcess(CubeProcess entity);

        void DeleteCubeProcess(IList<int> idList);

        void DeleteCubeProcess(IList<CubeProcess> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeProcess> FindAllLastestCubeProcess(int userId);

        IList<CubeProcess> FindAllLastestSuccessCubeProcess(int userId);

        CubeProcess FindLastestCubeProcessByCubeId(int cubeId, int userId);

        IList<CubeProcess> FindAllCubeProcessByCubeId(int cubeId, int userId);

        #endregion Customized Methods
    }
}
