using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Cube;
using System.Data;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube
{
    public interface ICubeAuthorizationMgr
    {
        #region Method Related to CubeUser

        void CreateCubeUser(CubeUser entity);

        CubeUser LoadCubeUser(int id);

        void UpdateCubeUser(CubeUser entity);

        void DeleteCubeUser(int id);

        void DeleteCubeUser(CubeUser entity);

        void DeleteCubeUser(IList<int> idList);

        void DeleteCubeUser(IList<CubeUser> entityList);

        IList LoadAllActiveCubeUser();

        IList<CubeUser> FindUserForCubeDistribution(int Id);

        IList<CubeUser> FindCubeUserByName(string userName);

        IList<CubeUser> FindUserExcludeSpecifiedRoleByNameAndDescription(int roleId, string name, string description);

        #endregion Method Related to CubeUser

        #region Method Related to CubeRole
        // Modified by vincent at 2007-11-09 begin
        Dndp.Persistence.Dao.SqlHelperDao GetSqlDao();
        void DeleteSetDimensionVisualTotal(int roleid);
        void InsertSetDimensionVisualTotal(int roleid, string setDimensionName, string visualtotal);
        bool GetSetDimensionVisualTotal(int roleid, string setDimensionName);
        // Modified by vincent at 2007-11-09 end
        void CreateCubeRole(CubeRole entity);

        CubeRole LoadCubeRole(int id);

        CubeRole LoadCubeRoleWithAllInfo(int id);

        void UpdateCubeRole(CubeRole entity);

        void DeleteCubeRole(int id);

        void DeleteCubeRole(CubeRole entity);

        void DeleteCubeRole(IList<int> idList);

        void DeleteCubeRole(IList<CubeRole> entityList);

        IList<CubeRole> FindCubeRoleByName(string roleName);

        IList<CubeRole> FindCubeRoleByNameAndDescription(string roleName, string description);

        void UploadRoleToCube(int roleId, string serverAddr, string databaseName, string cubeName);

        void UploadRoleToCube(CubeRole role, string serverAddr, string databaseName, string cubeName);

        #endregion Method Related to CubeRole

        #region Method Related to CubeUserRole

        void CreateCubeUserRole(CubeUserRole entity);

        CubeUserRole LoadCubeUserRole(int id);

        void UpdateCubeUserRole(CubeUserRole entity);

        void DeleteCubeUserRole(int id);

        void DeleteCubeUserRole(CubeUserRole entity);

        void DeleteCubeUserRole(IList<int> idList);

        void DeleteCubeUserRole(int CubeRoleId, IList<int> cubeUserIdList);

        void DeleteCubeUserRole(IList<CubeUserRole> entityList);

        IList<CubeUser> FindCubeUserByRoleId(int roleId);

        void DeleteCubeUserByRoleId(int roleId);

        void UpdateCubeUserRole(CubeRole cubeRole, IList<int> cubeUserIdList); 

        #endregion Method Related to CubeUserRole

        #region Method Related to CubeRoleDimensionMember

        void CreateCubeRoleDimensionMember(CubeRoleDimensionMember entity);

        CubeRoleDimensionMember LoadCubeRoleDimensionMember(int id);

        void UpdateCubeRoleDimensionMember(CubeRoleDimensionMember entity);

        void DeleteCubeRoleDimensionMember(int id);

        void DeleteCubeRoleDimensionMember(CubeRoleDimensionMember entity);

        void DeleteCubeRoleDimensionMember(IList<int> idList);

        void DeleteCubeRoleDimensionMember(IList<CubeRoleDimensionMember> entityList);

        IList<CubeRoleDimensionMember> FindCubeRoleDimensionMemberByRoleId(int roleId);

        void DeleteCubeRoleDimensionMemberByRoleId(int roleId);        

        IList<CubeDimensionMember> GetDimensionMembers(CubeDefinition cube, string dimensionName, string attributeName);

        void UpdateCubeRoleDimensionMember(CubeRole cubeRole, IList<CubeRoleDimensionMember> memberList, string dimensionName, string attributeName);

        void SynchronizeVisualtotal(int cubeId);

        #endregion Method Related to CubeRoleDimensionMember
    }
}
