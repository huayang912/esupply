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
    public class NHCubeDao : NHDaoBase, ICubeDao
    {
        public NHCubeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCube(CubeDefinition entity)
        {
            Create(entity);
        }

        public CubeDefinition LoadCube(int id)
        {
            return FindById(typeof(CubeDefinition), id) as CubeDefinition;
        }

        public void UpdateCube(CubeDefinition entity)
        {
            Update(entity);
        }

        public void DeleteCube(int id)
        {
            string hql = @"from CubeDefinition entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCube(CubeDefinition entity)
        {
            Delete(entity);
        }

        public void DeleteCube(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeDefinition entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCube(IList<CubeDefinition> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeDefinition entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCube(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeDefinition> LoadAllActiveCube()
        {
            string hql = "from CubeDefinition entity where entity.ActiveFlag = 1";
            IList list = FindAllWithCustomQuery(hql);
            return list as IList<CubeDefinition>;
        }

        public IList<CubeDefinition> FindCubeByUserIdAndAllowType(int userId, string allowType)
        {
            string hql = @"from CubeDefinition entity where entity.ActiveFlag = 1
                            and entity.Id in(select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = ?) ";

            IList list = FindAllWithCustomQuery(
                hql, new Object[] {userId, allowType},
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String });

            return list as IList<CubeDefinition>;
        }

        #endregion Customized Methods
    }
}
