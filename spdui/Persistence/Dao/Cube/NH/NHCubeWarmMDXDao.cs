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
    public class NHCubeWarmMDXDao : NHDaoBase, ICubeWarmMDXDao
    {
        public NHCubeWarmMDXDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeWarmMDX(CubeWarmMDX entity)
        {
            Create(entity);
        }

        public CubeWarmMDX LoadCubeWarmMDX(int id)
        {
            return FindById(typeof(CubeWarmMDX), id) as CubeWarmMDX;
        }

        public void UpdateCubeWarmMDX(CubeWarmMDX entity)
        {
            Update(entity);
        }

        public void DeleteCubeWarmMDX(int id)
        {
            string hql = @"from CubeWarmMDX entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeWarmMDX(CubeWarmMDX entity)
        {
            Delete(entity);
        }

        public void DeleteCubeWarmMDX(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeWarmMDX entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeWarmMDX(IList<CubeWarmMDX> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeWarmMDX entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeWarmMDX(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeWarmMDX> FindCubeWarmMDXByCubeId(int id)
        {
            string hql = "from CubeWarmMDX as entity where entity.TheCube.Id = ? and entity.ActiveFlag = 1 order by entity.SequenceNo ";
            IList result = FindAllWithCustomQuery(hql, id, NHibernateUtil.Int32);
            return result as IList<CubeWarmMDX>;
        }

        public void DeleteCubeWarmMDXByCubeId(int cubeId)
        {
            string hql = "from CubeWarmMDX entity where entity.TheCube.Id = ?";

            Delete(hql, cubeId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
