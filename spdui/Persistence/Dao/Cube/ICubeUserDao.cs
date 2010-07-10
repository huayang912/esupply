using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeUserDao
    {
        #region Method Created By CodeSmith

        void CreateCubeUser(CubeUser entity);

        CubeUser LoadCubeUser(int id);

        void UpdateCubeUser(CubeUser entity);
        
        void DeleteCubeUser(int id);

        void DeleteCubeUser(CubeUser entity);

        void DeleteCubeUser(IList<int> idList);

        void DeleteCubeUser(IList<CubeUser> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList LoadAllActiveCubeUser();

        IList<CubeUser> FindUserForCubeDistribution(int Id);

        IList<CubeUser> FindCubeUserByName(string userName);

        IList<CubeUser> FindUserExcludeSpecifiedRoleByNameAndDescription(int roleId, string name, string description);

        IList<CubeUser> FindCubeUserByRoleId(int roleId);

        IList<CubeUser> FindCubeUserByCubeIdAndUserNameAndUserDescription(int cubeId, string userName, string userDescription);

        #endregion Customized Methods
    }
}
