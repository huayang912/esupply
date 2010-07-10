using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeDistributionJobItemDao
    {
        #region Method Created By CodeSmith

        void CreateCubeDistributionJobItem(CubeDistributionJobItem entity);

        CubeDistributionJobItem LoadCubeDistributionJobItem(int id);

        void UpdateCubeDistributionJobItem(CubeDistributionJobItem entity);
        
        void DeleteCubeDistributionJobItem(int id);

        void DeleteCubeDistributionJobItem(CubeDistributionJobItem entity);

        void DeleteCubeDistributionJobItem(IList<int> idList);

        void DeleteCubeDistributionJobItem(IList<CubeDistributionJobItem> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeUser> FindCubeUserByCubeDistributionJobId(int jobId);

        IList<CubeDistributionJobItem> FindCubeDistributionJobItemByCubeDistributionJobId(int jobId);

        void DeleteCubeDistributionJobItemByJobId(int jobId);

        #endregion Customized Methods
    }
}
