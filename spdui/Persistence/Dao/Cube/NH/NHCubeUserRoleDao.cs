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
    public class NHCubeUserRoleDao : NHDaoBase, ICubeUserRoleDao
    {
        public NHCubeUserRoleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeUserRole(CubeUserRole entity)
        {
            Create(entity);
        }

        public CubeUserRole LoadCubeUserRole(int id)
        {
            return FindById(typeof(CubeUserRole), id) as CubeUserRole;
        }

        public void UpdateCubeUserRole(CubeUserRole entity)
        {
            Update(entity);
        }

        public void DeleteCubeUserRole(int id)
        {
            string hql = @"from CubeUserRole entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeUserRole(CubeUserRole entity)
        {
            Delete(entity);
        }

        public void DeleteCubeUserRole(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeUserRole entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeUserRole(IList<CubeUserRole> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeUserRole entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeUserRole(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeUser> FindCubeUserByRoleId(int roleId)
        {
            string hql = "select entity.TheCubeUser from CubeUserRole entity where entity.TheCubeRole.Id = ? order by entity.TheCubeUser.Name ";

            return FindAllWithCustomQuery(hql,
                new object[] { roleId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeUser>;
        }

        public void DeleteCubeUserByRoleId(int roleId)
        {
            string hql = @"from CubeUserRole entity where entity.TheCubeRole.Id = ?";

            Delete(hql, roleId, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeUserRole(int cubeRoleId, IList<int> cubeUserIdList)
        {
            StringBuilder hql = new StringBuilder();

            hql.Append("from CubeUserRole entity where entity.TheCubeRole.Id = ? and entity.TheCubeUser.Id in (");
            hql.Append(cubeUserIdList[0]);
            for (int i = 1; i < cubeUserIdList.Count; i++)
            {
                hql.Append(",");
                hql.Append(cubeUserIdList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString(), cubeRoleId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
