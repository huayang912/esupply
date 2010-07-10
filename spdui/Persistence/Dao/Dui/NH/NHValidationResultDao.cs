using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.Facilities.NHibernateIntegration;
using System.Collections;
using NHibernate.Type;
using Dndp.Persistence.Dao;
using Dndp.Persistence.Entity.Dui;
using Dndp.Persistence.Dao.Dui;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.Dui.NH
{
    public class NHValidationResultDao : NHDaoBase, IValidationResultDao
    {
        public NHValidationResultDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateValidationResult(ValidationResult entity)
        {
            Create(entity);
        }

        public ValidationResult LoadValidationResult(int id)
        {
            return FindById(typeof(ValidationResult), id) as ValidationResult;
        }

        public void UpdateValidationResult(ValidationResult entity)
        {
            Update(entity);
        }

        public void DeleteValidationResult(int id)
        {
            string hql = @"from ValidationResult entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteValidationResult(ValidationResult entity)
        {
            Delete(entity);
        }

        public void DeleteValidationResult(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from ValidationResult entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteValidationResult(IList<ValidationResult> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (ValidationResult entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteValidationResult(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public void DeleteValidationResultByDSUploadId(int dsUploadId)
        {
            string hql = @"from ValidationResult entity where entity.TheDataSourceUpload.Id = ?";
            Delete(hql, dsUploadId, NHibernate.NHibernateUtil.Int32);
        }

        public IList<ValidationResult> FindAllByDSUploadId(int dsUploadId)
        {
            string hql = @"from ValidationResult entity where entity.TheDataSourceUpload.Id = ? order by entity.TheDataSourceRule.RuleType, entity.TheDataSourceRule.Name";
            return FindAllWithCustomQuery(hql, new object[] { dsUploadId }, new IType[] { NHibernateUtil.Int32 }) as IList<ValidationResult>;
        }

        public IList<ValidationResult> FindAllByDSUploadIdAndRuleType(int dsUploadId, string ruleType)
        {
            string hql = @"from ValidationResult entity where entity.TheDataSourceUpload.Id = ? and entity.TheDataSourceRule.RuleType = ? order by entity.TheDataSourceRule.Name";
            return FindAllWithCustomQuery(
                hql, 
                new object[] { dsUploadId, ruleType } , 
                new IType[] { NHibernateUtil.Int32, NHibernate.NHibernateUtil.String }) 
                as IList<ValidationResult>;
        }

        public IList<ValidationResult> FindAllByIds(string validationResultIds)
        {
            string hql = @"from ValidationResult entity where entity.Id in (" + validationResultIds + ") order by entity.TheDataSourceRule.RuleType, entity.TheDataSourceRule.Name";
            return FindAllWithCustomQuery(hql) as IList<ValidationResult>;
        }

        public void DeleteValidationResultByDSId(int dsId)
        {
            string hql = @"from ValidationResult entity where entity.TheDataSourceRule.TheDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
