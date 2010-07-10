using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.Facilities.NHibernateIntegration;
using System.Collections;
using NHibernate.Type;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.Cube.NH
{
    public class NHCubeRoleDimensionMemberDao : NHDaoBase, ICubeRoleDimensionMemberDao
    {
        public NHCubeRoleDimensionMemberDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeRoleDimensionMember(CubeRoleDimensionMember entity)
        {
            Create(entity);
        }

        public CubeRoleDimensionMember LoadCubeRoleDimensionMember(int id)
        {
            return FindById(typeof(CubeRoleDimensionMember), id) as CubeRoleDimensionMember;
        }

        public void UpdateCubeRoleDimensionMember(CubeRoleDimensionMember entity)
        {
            Update(entity);
        }

        public void DeleteCubeRoleDimensionMember(int id)
        {
            string hql = @"from CubeRoleDimensionMember entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeRoleDimensionMember(CubeRoleDimensionMember entity)
        {
            Delete(entity);
        }

        public void DeleteCubeRoleDimensionMember(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeRoleDimensionMember entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeRoleDimensionMember(IList<CubeRoleDimensionMember> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeRoleDimensionMember entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeRoleDimensionMember(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeRoleDimensionMember> FindCubeRoleDimensionMemberByRoleId(int roleId)
        {
            string hql = "from CubeRoleDimensionMember entity where entity.TheCubeRole.Id = ? order by entity.TheDimension.DimensionName ";

            return FindAllWithCustomQuery(hql,
                new object[] { roleId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeRoleDimensionMember>;
        }

        public void DeleteCubeRoleDimensionMemberByRoleId(int roleId)
        {
            string hql = "from CubeRoleDimensionMember entity where entity.TheCubeRole.Id = ?";

            Delete(hql, roleId, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeRoleDimensionMemberByDimId(int dimId)
        {
            string hql = "from CubeRoleDimensionMember entity where entity.TheDimension.Id = ?";

            Delete(hql, dimId, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeRoleDimensionMemberByRoleIdAndDimAndAtt(int roleId, string dimensionName, string attributeName)
        {
            string hql = "from CubeRoleDimensionMember entity where entity.TheCubeRole.Id = ? and entity.TheDimension.DimensionName = ? and entity.TheDimension.AttributeName = ? ";

            Delete(hql,
                new object[] { roleId, dimensionName, attributeName },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String, NHibernateUtil.String });
        }

        #endregion Customized Methods
    }
}
