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
    public class NHCubeProcessParameterDao : NHDaoBase, ICubeProcessParameterDao
    {
        public NHCubeProcessParameterDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeProcessParameter(CubeProcessParameter entity)
        {
            Create(entity);
        }

        public CubeProcessParameter LoadCubeProcessParameter(int id)
        {
            return FindById(typeof(CubeProcessParameter), id) as CubeProcessParameter;
        }

        public void UpdateCubeProcessParameter(CubeProcessParameter entity)
        {
            Update(entity);
        }

        public void DeleteCubeProcessParameter(int id)
        {
            string hql = @"from CubeProcessParameter entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeProcessParameter(CubeProcessParameter entity)
        {
            Delete(entity);
        }

        public void DeleteCubeProcessParameter(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeProcessParameter entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeProcessParameter(IList<CubeProcessParameter> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeProcessParameter entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeProcessParameter(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeProcessParameter> FindCubeProcessParameterByProcessId(int processId)
        {
            string hql = "from CubeProcessParameter para where para.TheProcess.Id = ? order by para.Id ";

            return FindAllWithCustomQuery(hql, processId, NHibernateUtil.Int32) as IList<CubeProcessParameter>;
        }

        public void DeleteCubeProcessParameterByProcessId(int processId)
        {
            string hql = "from CubeProcessParameter para where para.TheProcess.Id = ? ";

            Delete(hql, processId, NHibernateUtil.Int32);
        }

        public CubeProcessParameter FindLastestCubeProcessParameter(int cubeId, int parameterId)
        {
            string hql = @"select cpp from CubeProcessParameter cpp 
                           inner join cpp.TheParameter as para 
                            inner join cpp.TheProcess as proc 
                            inner join proc.TheCube as cube
                            where cube.Id = ? and para.Id = ?
                          order by proc.Id desc";

            IList<CubeProcessParameter> list = FindAllWithCustomQuery(hql, new object[] { cubeId, parameterId }, 0, 1) as IList<CubeProcessParameter>;
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        #endregion Customized Methods
    }
}
