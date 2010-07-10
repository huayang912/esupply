using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeRoleDao
    {
        #region Method Created By CodeSmith

        void CreateCubeRole(CubeRole entity);

        CubeRole LoadCubeRole(int id);

        void UpdateCubeRole(CubeRole entity);
        
        void DeleteCubeRole(int id);

        void DeleteCubeRole(CubeRole entity);

        void DeleteCubeRole(IList<int> idList);

        void DeleteCubeRole(IList<CubeRole> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeRole> FindCubeRoleByName(string roleName);

        IList<CubeRole> FindCubeRoleByCubeId(int cubeId);  

        #endregion Customized Methods
    }
}
