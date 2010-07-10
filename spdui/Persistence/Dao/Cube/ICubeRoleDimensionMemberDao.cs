using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
//TODO: Add other using statements here.

namespace Dndp.Persistence.Dao.Cube
{
    public interface ICubeRoleDimensionMemberDao
    {
        #region Method Created By CodeSmith

        void CreateCubeRoleDimensionMember(CubeRoleDimensionMember entity);

        CubeRoleDimensionMember LoadCubeRoleDimensionMember(int id);

        void UpdateCubeRoleDimensionMember(CubeRoleDimensionMember entity);
        
        void DeleteCubeRoleDimensionMember(int id);

        void DeleteCubeRoleDimensionMember(CubeRoleDimensionMember entity);

        void DeleteCubeRoleDimensionMember(IList<int> idList);

        void DeleteCubeRoleDimensionMember(IList<CubeRoleDimensionMember> entityList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList<CubeRoleDimensionMember> FindCubeRoleDimensionMemberByRoleId(int roleId);

        void DeleteCubeRoleDimensionMemberByRoleId(int roleId);

        void DeleteCubeRoleDimensionMemberByDimId(int dimId);

        void DeleteCubeRoleDimensionMemberByRoleIdAndDimAndAtt(int roleId, string dimensionName, string attributeName);

        #endregion Customized Methods
    }
}
