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
    public class NHCubeReleaseDao : NHDaoBase, ICubeReleaseDao
    {
        public NHCubeReleaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeRelease(CubeRelease entity)
        {
            Create(entity);
        }

        public CubeRelease LoadCubeRelease(int id)
        {
            return FindById(typeof(CubeRelease), id) as CubeRelease;
        }

        public void UpdateCubeRelease(CubeRelease entity)
        {
            Update(entity);
        }

        public void DeleteCubeRelease(int id)
        {
            string hql = @"from CubeRelease entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeRelease(CubeRelease entity)
        {
            Delete(entity);
        }

        public void DeleteCubeRelease(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeRelease entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeRelease(IList<CubeRelease> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeRelease entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeRelease(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeRelease> FindAllLastestCubeRelease(int userId)
        {
            // Modified by vincent at 2007-11-09 begin
//            string hql = @" from CubeRelease as entity 
//            where entity.Id in (select max(cr.Id) from CubeRelease cr where cr.TheProcess.TheCube.ActiveFlag = 1 group by cr.TheProcess.TheCube) 
//                and entity.TheProcess.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Release')
//                and entity.TheProcess.TheCube.ActiveFlag = 1";
            string hql = @" from CubeRelease as entity 
            where entity.Id in (select max(cr.Id) from CubeRelease cr where cr.TheProcess.TheCube.ActiveFlag = 1 group by cr.TheProcess.TheCube)
                and entity.TheProcess.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Release')
                and entity.TheProcess.TheCube.ActiveFlag = 1";
            CubeRelease cr = new CubeRelease();
            
            // Modified by vincent at 2007-11-09 end
            IList<CubeRelease> list = FindAllWithCustomQuery(
                hql,
                new object[] { userId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeRelease>;

            return list;
        }

        public IList<CubeRelease> FindAllSuccessLastestCubeRelease(int userId)
        {
            string hql = @" from CubeRelease as entity 
            where entity.Id in (select max(cr.Id) from CubeRelease cr where cr.TheProcess.TheCube.ActiveFlag = 1 group by cr.TheProcess.TheCube) 
                and entity.TheProcess.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Release')
                and entity.TheProcess.TheCube.ActiveFlag = 1
                and entity.Status = ? ";

            IList<CubeRelease> list = FindAllWithCustomQuery(
                hql,
                new object[] { userId, CubeRelease.RELEASE_STATUS_Success },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String }) as IList<CubeRelease>;

            return list;
        }

        public CubeRelease FindLastestCubeReleaseByCubeId(int cubeId, int userId)
        {
            string hql = @" from CubeRelease as entity 
                   where entity.Id in (select max(cr.Id) from CubeRelease cr where cr.TheProcess.TheCube.ActiveFlag = 1 and cr.TheProcess.TheCube.Id = ? group by cr.TheProcess.TheCube) 
                    and entity.TheProcess.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Release')
                    and entity.TheProcess.TheCube.ActiveFlag = 1 ";

            IList<CubeRelease> list = FindAllWithCustomQuery(
                hql,
                new object[] { cubeId, userId },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 }) as IList<CubeRelease>;

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public CubeRelease FindLastestSuccessCubeReleaseByCubeId(int cubeId, int userId)
        {
            string hql = @" from CubeRelease as entity 
                   where entity.Id in (select max(cr.Id) from CubeRelease cr where cr.TheProcess.TheCube.ActiveFlag = 1 and cr.TheProcess.TheCube.Id = ? group by cr.TheProcess.TheCube) 
                    and entity.TheProcess.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Release')
                    and entity.TheProcess.TheCube.ActiveFlag = 1
                    and entity.Status = ? ";

            IList<CubeRelease> list = FindAllWithCustomQuery(
                hql,
                new object[] { cubeId, userId, CubeRelease.RELEASE_STATUS_Success },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32, NHibernateUtil.String }) as IList<CubeRelease>;

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public IList<CubeRelease> FindAllCubeReleaseByCubeId(int cubeId, int userId)
        {
            return FindAllWithCustomQuery(
                @"from CubeRelease as entity 
                where entity.TheProcess.TheCube.Id = ? 
                and entity.TheProcess.TheCube.ActiveFlag = 1 
                and entity.TheProcess.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Release')  
                 order by entity.ReleaseDate Desc",
                new object[] { cubeId, userId },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 }) as IList<CubeRelease>;
        }

        #endregion Customized Methods
    }
}
