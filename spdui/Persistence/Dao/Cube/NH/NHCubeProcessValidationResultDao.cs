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
    public class NHCubeProcessValidationResultDao : NHDaoBase, ICubeProcessValidationResultDao
    {
        public NHCubeProcessValidationResultDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeProcessValidationResult(CubeProcessValidationResult entity)
        {
            Create(entity);
        }

        public CubeProcessValidationResult LoadCubeProcessValidationResult(int id)
        {
            return FindById(typeof(CubeProcessValidationResult), id) as CubeProcessValidationResult;
        }

        public void UpdateCubeProcessValidationResult(CubeProcessValidationResult entity)
        {
            Update(entity);
        }

        public void DeleteCubeProcessValidationResult(int id)
        {
            string hql = @"from CubeProcessValidationResult entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeProcessValidationResult(CubeProcessValidationResult entity)
        {
            Delete(entity);
        }

        public void DeleteCubeProcessValidationResult(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeProcessValidationResult entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeProcessValidationResult(IList<CubeProcessValidationResult> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeProcessValidationResult entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeProcessValidationResult(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeProcessValidationResult> FindCubeProcessValidationResultByProcessId(int processId, string validationTarget)
        {
            string hql = "from CubeProcessValidationResult result where result.TheProcess.Id = ? and result.TheRule.ValidationTarget = ? order by result.TheRule.Name ";

            return FindAllWithCustomQuery(
                hql,
                new Object[] { processId, validationTarget },
                new IType[] {NHibernateUtil.Int32, NHibernateUtil.String}) as IList<CubeProcessValidationResult>;
        }

        public void DeleteCubeProcessValidationResultByProcessId(int processId)
        {
            string hql = "from CubeProcessValidationResult result where result.TheProcess.Id = ?";

            Delete(hql, processId, NHibernateUtil.Int32);
        }

        public IList<CubeProcessValidationResult> FindCubeProcessValidationResultByIds(string validationIds)
        {
            string hql = "from CubeProcessValidationResult result where result.Id in (" + validationIds + ") ";

            return FindAllWithCustomQuery(hql) as IList<CubeProcessValidationResult>;
        }

        #endregion Customized Methods
    }
}
