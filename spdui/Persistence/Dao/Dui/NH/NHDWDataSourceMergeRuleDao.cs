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
    public class NHDWDataSourceMergeRuleDao : NHDaoBase, IDWDataSourceMergeRuleDao
    {
        public NHDWDataSourceMergeRuleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDWDataSourceMergeRule(DWDataSourceMergeRule entity)
        {
            Create(entity);
        }

        public DWDataSourceMergeRule LoadDWDataSourceMergeRule(int id)
        {
            return FindById(typeof(DWDataSourceMergeRule), id) as DWDataSourceMergeRule;
        }

        public void UpdateDWDataSourceMergeRule(DWDataSourceMergeRule entity)
        {
            Update(entity);
        }

        public void DeleteDWDataSourceMergeRule(int id)
        {
            string hql = @"from DWDataSourceMergeRule entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDWDataSourceMergeRule(DWDataSourceMergeRule entity)
        {
            Delete(entity);
        }

        public void DeleteDWDataSourceMergeRule(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DWDataSourceMergeRule entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDWDataSourceMergeRule(IList<DWDataSourceMergeRule> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DWDataSourceMergeRule entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDWDataSourceMergeRule(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByDWDataSourceId(int dwDataSourceId)
        {
            return FindAllWithCustomQuery("from DWDataSourceMergeRule dsr where dsr.TheDWDataSource.Id=? order by dsr.RuleType, dsr.Name", dwDataSourceId);
        }

        public int GetMaxSequenceNo(int dataSourceId, string RuleType)
        {
            try
            {
                IList result =
                    FindAllWithCustomQuery(
                    "select max(dsr.SequenceNo) from DWDataSourceMergeRule dsr where dsr.TheDWDataSource.Id=? and dsr.RuleType=? ",
                    new object[] { dataSourceId, RuleType });
                return (int)result[0];
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void DeleteDWDataSourceMergeRuleByDSId(int dsId)
        {
            string hql = @"from DWDataSourceMergeRule entity where entity.TheDWDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
