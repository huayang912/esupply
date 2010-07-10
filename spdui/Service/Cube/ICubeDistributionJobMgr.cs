using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeDistributionJobMgr
    {
        #region Method CubeDistributionJob

        void CreateCubeDistributionJob(CubeDistributionJob entity);

        CubeDistributionJob LoadCubeDistributionJob(int id);

        void UpdateCubeDistributionJob(CubeDistributionJob entity);

        void DeleteCubeDistributionJob(int id);

        void DeleteCubeDistributionJob(CubeDistributionJob entity);

        void DeleteCubeDistributionJob(IList<int> idList);

        void DeleteCubeDistributionJob(IList<CubeDistributionJob> entityList);

        CubeDistributionJob CreateNewCubeDistributionJobByCubeId(int cubeId, User user);

        CubeDistributionJob FindCubeDistributionJobWithAllInfo(int jobId);

        #endregion Method CubeDistributionJob

        #region Methods CubeDistributionJobItem

        void CreateCubeDistributionJobItem(CubeDistributionJobItem entity);

        CubeDistributionJobItem LoadCubeDistributionJobItem(int id);

        void UpdateCubeDistributionJobItem(CubeDistributionJobItem entity);

        void DeleteCubeDistributionJobItem(int id);

        void DeleteCubeDistributionJobItem(CubeDistributionJobItem entity);

        void DeleteCubeDistributionJobItem(IList<int> idList);

        void DeleteCubeDistributionJobItem(IList<CubeDistributionJobItem> entityList);

        IList<CubeUser> FindCubeUserByCubeIdAndUserNameAndUserDescription(int cubeId, string userName, string userDescription);

        IList<CubeDistributionJobItem> FindJobItemByJobId(int jobId);

        IList<CubeDistributionJobItem> AddCubeDistributionJobItems(CubeDistributionJob job, IList<int> cubeUserIds);

        void SynchronizeCubeDistributionJobItem(CubeDistributionJob job);

        #endregion Methods CubeDistributionJobItem

    }
}
