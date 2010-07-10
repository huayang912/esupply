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
using System.Data.SqlClient;
//TODO: Add other using statmens here.

namespace Dndp.Persistence.Dao.Dui.NH
{
    public class NHDataSourceFieldDao : NHDaoBase, IDataSourceFieldDao
    {
        public NHDataSourceFieldDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDataSourceField(DataSourceField entity)
        {
            Create(entity);
        }

        public DataSourceField LoadDataSourceField(int id)
        {
            return FindById(typeof(DataSourceField), id) as DataSourceField;
        }

        public void UpdateDataSourceField(DataSourceField entity)
        {
            Update(entity);
        }

        public void DeleteDataSourceField(int id)
        {
            string hql = @"from DataSourceField entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDataSourceField(DataSourceField entity)
        {
            Delete(entity);
        }

        public void DeleteDataSourceField(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSourceField entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDataSourceField(IList<DataSourceField> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DataSourceField entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDataSourceField(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByDataSourceId(int dataSourceId)
        {
            return FindAllWithCustomQuery("from DataSourceField dsf where dsf.TheDataSource.Id=? order by dsf.SequenceNo", dataSourceId);
        }

        public int GetMaxSequenceNo(int dataSourceId)
        {
            try
            {
                IList result = FindAllWithCustomQuery("select max(dsf.SequenceNo) from DataSourceField dsf where dsf.TheDataSource.Id=?", dataSourceId);
                return (int)result[0];
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool HasField(int dsId, string newFieldNm)
        {
            string hql = "from DataSourceField dsf where dsf.TheDataSource.Id=? and dsf.Name = ?";
            IList result = FindAllWithCustomQuery(hql, 
                new object[]{dsId, newFieldNm}, 
                new IType[]{NHibernateUtil.Int32, NHibernateUtil.String});
            if (result != null && result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteDataSourceFieldByDSId(int dsId)
        {
            string hql = @"from DataSourceField entity where entity.TheDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
