using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeDistributionJobDao
    {
        #region Method Created By CodeSmith

        void CreateCubeDistributionJob(CubeDistributionJob entity);

        CubeDistributionJob LoadCubeDistributionJob(int id);

        void UpdateCubeDistributionJob(CubeDistributionJob entity);
        
        void DeleteCubeDistributionJob(int id);

        void DeleteCubeDistributionJob(CubeDistributionJob entity);

        void DeleteCubeDistributionJob(IList<int> idList);

        void DeleteCubeDistributionJob(IList<CubeDistributionJob> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeDistributionJob> FindAllLastestCubeDistributionJob();

        CubeDistributionJob FindLastestCubeDistributionJobByCubeId(int cubeId);

        IList<CubeDistributionJob> FindAllCubeDistributionJobByCubeId(int cubeId);

        #endregion Customized Methods
    }
}
