using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Distribution;
//TODO: Add other using statements here.

namespace Dndp.Service.Distribution
{
    public interface IDistributionUserMgr
    {
        #region Method Created By CodeSmith

        void CreateDistributionUser(DistributionUser entity);

        DistributionUser LoadDistributionUser(int id);

        void UpdateDistributionUser(DistributionUser entity);

        void DeleteDistributionUser(int id);

        void DeleteDistributionUser(DistributionUser entity);

        void DeleteDistributionUser(IList<int> idList);

        void DeleteDistributionUser(IList<DistributionUser> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<DistributionUser> FindDistributionUserByName(string name);

        IList<DistributionUser> FindDistributionUserByName(string name, bool isOfflineReportUser, bool isOfflineCubeUser, bool isOnlineCubeUser);

        IList<DistributionUser> LoadAllActiveDistributionUser();

        IList<DistributionUser> LoadAllActiveDistributionUser(bool isOfflineReportUser, bool isOfflineCubeUser, bool isOnlineCubeUser);

        #endregion Customized Methods

    }
}
