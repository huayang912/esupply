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
    public class NHDataSourceRuleDao : NHDaoBase, IDataSourceRuleDao
    {
        public NHDataSourceRuleDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDataSourceRule(DataSourceRule entity)
        {
            Create(entity);
        }

        public DataSourceRule LoadDataSourceRule(int id)
        {
            return FindById(typeof(DataSourceRule), id) as DataSourceRule;
        }

        public void UpdateDataSourceRule(DataSourceRule entity)
        {
            Update(entity);
        }

        public void DeleteDataSourceRule(int id)
        {
            string hql = @"from DataSourceRule entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDataSourceRule(DataSourceRule entity)
        {
            Delete(entity);
        }

        public void DeleteDataSourceRule(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSourceRule entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDataSourceRule(IList<DataSourceRule> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DataSourceRule entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDataSourceRule(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByDataSourceId(int dataSourceId)
        {
            return FindAllWithCustomQuery("from DataSourceRule dsr where dsr.TheDataSource.Id=? order by dsr.RuleType, dsr.Name", dataSourceId);
        }

        public int GetMaxSequenceNo(int dataSourceId, string RuleType)
        {
            try
            {
                IList result =
                    FindAllWithCustomQuery(
                    "select max(dsr.SequenceNo) from DataSourceRule dsr where dsr.TheDataSource.Id=? and dsr.RuleType=? ",
                    new object[] { dataSourceId, RuleType });
                return (int)result[0];
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void DeleteDataSourceRuleByDSId(int dsId)
        {
            string hql = @"from DataSourceRule entity where entity.TheDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
