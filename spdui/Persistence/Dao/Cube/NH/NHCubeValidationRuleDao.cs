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
    public class NHCubeValidationRuleDao : NHDaoBase, ICubeValidationRuleDao
    {
        public NHCubeValidationRuleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateCubeValidationRule(CubeValidationRule entity)
        {
            Create(entity);
        }

        public CubeValidationRule LoadCubeValidationRule(int id)
        {
            return FindById(typeof(CubeValidationRule), id) as CubeValidationRule;
        }

        public void UpdateCubeValidationRule(CubeValidationRule entity)
        {
            Update(entity);
        }

        public void DeleteCubeValidationRule(int id)
        {
            string hql = @"from CubeValidationRule entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteCubeValidationRule(CubeValidationRule entity)
        {
            Delete(entity);
        }

        public void DeleteCubeValidationRule(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from CubeValidationRule entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteCubeValidationRule(IList<CubeValidationRule> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (CubeValidationRule entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteCubeValidationRule(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<CubeValidationRule> FindCubeValidationRuleWithCubeId(int id)
        {
            string hql = "from CubeValidationRule as entity where entity.TheCube.Id = ? and entity.ActiveFlag = 1 order by entity.ValidationTarget desc, entity.Type, entity.Name";
            IList result = FindAllWithCustomQuery(hql, id, NHibernateUtil.Int32);
            return result as IList<CubeValidationRule>;
        }

        public void DeleteCubeValidationRuleByCubeId(int cubeId)
        {
            string hql = @"from CubeValidationRule entity where entity.TheCube.Id = ?";

            Delete(hql, cubeId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
