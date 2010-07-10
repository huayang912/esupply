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
    public class NHDataSourceWithDrawTableDao : NHDaoBase, IDataSourceWithDrawTableDao
    {
        public NHDataSourceWithDrawTableDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDataSourceWithDrawTable(DataSourceWithDrawTable entity)
        {
            Create(entity);
        }

        public DataSourceWithDrawTable LoadDataSourceWithDrawTable(int id)
        {
            return FindById(typeof(DataSourceWithDrawTable), id) as DataSourceWithDrawTable;
        }

        public void UpdateDataSourceWithDrawTable(DataSourceWithDrawTable entity)
        {
            Update(entity);
        }

        public void DeleteDataSourceWithDrawTable(int id)
        {
            string hql = @"from DataSourceWithDrawTable entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDataSourceWithDrawTable(DataSourceWithDrawTable entity)
        {
            Delete(entity);
        }

        public void DeleteDataSourceWithDrawTable(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSourceWithDrawTable entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDataSourceWithDrawTable(IList<DataSourceWithDrawTable> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DataSourceWithDrawTable entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDataSourceWithDrawTable(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        //TODO: Add other methods here.
        public IList FindAllByDataSourceId(int dataSourceId)
        {
            return FindAllWithCustomQuery("from DataSourceWithDrawTable entity where entity.TheDataSource.Id=?", dataSourceId);
        }

        public void DeleteDataSourceWithDrawTableByDSId(int dsId)
        {
            string hql = @"from DataSourceWithDrawTable entity where entity.TheDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
