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
using Dndp.Persistence.Entity.Security;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.Dui.NH
{
    public class NHDataSourceOperatorDao : NHDaoBase, IDataSourceOperatorDao
    {
        public NHDataSourceOperatorDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDataSourceOperator(DataSourceOperator entity)
        {
            Create(entity);
        }

        public DataSourceOperator LoadDataSourceOperator(int id)
        {
            return FindById(typeof(DataSourceOperator), id) as DataSourceOperator;
        }

        public void UpdateDataSourceOperator(DataSourceOperator entity)
        {
            Update(entity);
        }

        public void DeleteDataSourceOperator(int id)
        {
            string hql = @"from DataSourceOperator entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDataSourceOperator(DataSourceOperator entity)
        {
            Delete(entity);
        }

        public void DeleteDataSourceOperator(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSourceOperator entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDataSourceOperator(IList<DataSourceOperator> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DataSourceOperator entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDataSourceOperator(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByDataSourceId(int dataSourceId)
        {
            return FindAllWithCustomQuery("from DataSourceOperator dso where dso.TheDataSource.Id=?", dataSourceId);
        }
       

        public IList<DataSourceOperator> FindAllByDataSourceIdAndAllowType(int dsId, string type)
        {
            string hql = "select dso from DataSourceOperator dso where dso.TheDataSource.Id = ? and dso.AllowType = ?";

            return FindAllWithCustomQuery(hql,
                new object[] { dsId, type },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String }) as IList<DataSourceOperator>;
        }

        public void DeleteDataSourceOperatorByDSId(int dsId)
        {
            string hql = @"from DataSourceOperator entity where entity.TheDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
