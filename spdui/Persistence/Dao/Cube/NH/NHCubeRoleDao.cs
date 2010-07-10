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
    public class NHCubeRoleDao : NHDaoBase, ICubeRoleDao
    {
        public NHCubeRoleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeRole(CubeRole entity)
        {
            Create(entity);
        }

        public CubeRole LoadCubeRole(int id)
        {
            return FindById(typeof(CubeRole), id) as CubeRole;
        }

        public void UpdateCubeRole(CubeRole entity)
        {
            Update(entity);
        }

        public void DeleteCubeRole(int id)
        {
            string hql = @"from CubeRole entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeRole(CubeRole entity)
        {
            Delete(entity);
        }

        public void DeleteCubeRole(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeRole entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeRole(IList<CubeRole> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeRole entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeRole(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeRole> FindCubeRoleByName(string roleName)
        {
            string hql = "from CubeRole entity where entity.Name like ? and entity.TheCube.ActiveFlag = 1 order by entity.Name ";

            IList<CubeRole> list = FindAllWithCustomQuery(
                hql, new object[] { "%" + roleName + "%" },
                new IType[] { NHibernateUtil.String }) as IList<CubeRole>;

            return list;
        }

        public IList<CubeRole> FindCubeRoleByCubeId(int cubeId)
        {
            string hql = "from CubeRole entity where entity.TheCube.Id = ? and entity.TheCube.ActiveFlag = 1 order by entity.Name ";

            IList<CubeRole> list = FindAllWithCustomQuery(
                hql, new object[] { cubeId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeRole>;

            return list;
        }

        #endregion Customized Methods
    }
}
