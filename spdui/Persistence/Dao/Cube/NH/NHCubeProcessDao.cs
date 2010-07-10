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
    public class NHCubeProcessDao : NHDaoBase, ICubeProcessDao
    {
        public NHCubeProcessDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeProcess(CubeProcess entity)
        {
            Create(entity);
        }

        public CubeProcess LoadCubeProcess(int id)
        {
            return FindById(typeof(CubeProcess), id) as CubeProcess;
        }

        public void UpdateCubeProcess(CubeProcess entity)
        {
            Update(entity);
        }

        public void DeleteCubeProcess(int id)
        {
            string hql = @"from CubeProcess entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeProcess(CubeProcess entity)
        {
            Delete(entity);
        }

        public void DeleteCubeProcess(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeProcess entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeProcess(IList<CubeProcess> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeProcess entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeProcess(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeProcess> FindAllLastestCubeProcess(int userId)
        {
            string hql = @" from CubeProcess as entity 
            where entity.Id in (select max(cp.Id) from CubeProcess cp  where cp.TheCube.ActiveFlag = 1 group by cp.TheCube) 
                and entity.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Process')
                and entity.TheCube.ActiveFlag = 1";

            IList<CubeProcess> list = FindAllWithCustomQuery(
                hql, 
                new object[] { userId },
                new IType[] { NHibernateUtil.Int32 }) as IList<CubeProcess>;

            return list;
        }

        public IList<CubeProcess> FindAllLastestSuccessCubeProcess(int userId)
        {
            // Modified by vincent at 2007-11-09 begin
//            string hql = @" from CubeProcess as entity 
//            where entity.Id in (select max(cp.Id) from CubeProcess cp  where cp.TheCube.ActiveFlag = 1 and cp.Status = ? group by cp.TheCube) 
//                and entity.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Process')
//                and entity.TheCube.ActiveFlag = 1";

//            IList<CubeProcess> list = FindAllWithCustomQuery(
//                hql,
//                new object[] { CubeProcess.PROCESS_STATUS_ProcessSuccess, userId },
//                new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 }) as IList<CubeProcess>;

            string hql = @" from CubeProcess as entity 
            where entity.Id in (select max(cp.Id) from CubeProcess cp  where cp.TheCube.ActiveFlag = 1 and ( 1 = 1 or cp.Status = ?) group by cp.TheCube) 
                and entity.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Process')
                and entity.TheCube.ActiveFlag = 1";

            IList<CubeProcess> list = FindAllWithCustomQuery(
                hql,
                new object[] { CubeProcess.PROCESS_STATUS_ProcessSuccess, userId },
                new IType[] { NHibernateUtil.String, NHibernateUtil.Int32 }) as IList<CubeProcess>;

            
            // Modified by vincent at 2007-11-09 end
            return list;
        }

        public CubeProcess FindLastestCubeProcessByCubeId(int cubeId, int userId)
        {
            string hql = @" from CubeProcess as entity 
                   where entity.Id in (select max(cp.Id) from CubeProcess cp where cp.TheCube.ActiveFlag = 1 and cp.TheCube.Id = ? group by cp.TheCube)
                    and entity.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Process') 
                    and entity.TheCube.ActiveFlag = 1 ";

            IList<CubeProcess> list = FindAllWithCustomQuery(
                hql,
                new object[] { cubeId, userId },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 }) as IList<CubeProcess>;

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public IList<CubeProcess> FindAllCubeProcessByCubeId(int cubeId, int userId)
        {
            return FindAllWithCustomQuery(
                @"from CubeProcess as entity 
                where entity.TheCube.Id = ? 
                and entity.TheCube.ActiveFlag = 1 
                and entity.TheCube.Id in (select co.TheCube.Id from CubeOperator as co where co.TheUser.Id = ? and co.AllowType = 'Process')  
                 order by entity.CreateDate Desc",
                new object[] { cubeId, userId },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 }) as IList<CubeProcess>;
        }

        #endregion Customized Methods
    }
}
