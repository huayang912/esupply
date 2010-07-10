using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeUserRoleDao
    {
        #region Method Created By CodeSmith

        void CreateCubeUserRole(CubeUserRole entity);

        CubeUserRole LoadCubeUserRole(int id);

        void UpdateCubeUserRole(CubeUserRole entity);
        
        void DeleteCubeUserRole(int id);

        void DeleteCubeUserRole(CubeUserRole entity);

        void DeleteCubeUserRole(IList<int> idList);

        void DeleteCubeUserRole(IList<CubeUserRole> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeUser> FindCubeUserByRoleId(int roleId);

        void DeleteCubeUserByRoleId(int roleId);

        void DeleteCubeUserRole(int CubeRoleId, IList<int> cubeUserIdList);

        #endregion Customized Methods
    }
}
