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
    public class NHDataSourceCategoryDao : NHDaoBase, IDataSourceCategoryDao
    {
        public NHDataSourceCategoryDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDataSourceCategory(DataSourceCategory entity)
        {
            Create(entity);
        }

        public DataSourceCategory LoadDataSourceCategory(int id)
        {
            return FindById(typeof(DataSourceCategory), id) as DataSourceCategory;
        }

        public void UpdateDataSourceCategory(DataSourceCategory entity)
        {
            Update(entity);
        }

        public void DeleteDataSourceCategory(int id)
        {
            string hql = @"from DataSourceCategory entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDataSourceCategory(DataSourceCategory entity)
        {
            Delete(entity);
        }

        public void DeleteDataSourceCategory(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSourceCategory entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDataSourceCategory(IList<DataSourceCategory> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DataSourceCategory entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDataSourceCategory(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByDataSourceId(int dataSourceId)
        {
            return FindAllWithCustomQuery("from DataSourceCategory dsc where dsc.TheDataSource.Id=?", dataSourceId);
        }

        public IList<DataSourceCategory> FindDataSourceCategory(int userId, string allowType, string strCategory, string strType)
        {
            string QueryStr = @"select dsc from DataSourceCategory dsc, DataSourceOperator dso 
                                where dsc.TheDataSource.id = dso.TheDataSource.id                                 
                                and dso.AllowType = ? 
                                and dso.TheDataSource.ActiveFlag = ?
                                and dsc.ActiveFlag = ?
                                and dso.TheUser.Id = ?
                                and dso.TheUser in elements(dsc.Users)";

            if (!strCategory.Equals(""))
            {
                QueryStr = QueryStr + " and dsc.Name = '" + strCategory + "'";
            }
            if (!strType.Equals(""))
            {
                QueryStr = QueryStr + " and dsc.TheDataSource.DSType = '" + strType + "'";
            }
            QueryStr = QueryStr + " order by dsc.Name, dsc.TheDataSource.DSType, dsc.TheDataSource.Description";
            return FindAllWithCustomQuery(
                QueryStr,
                new object[] { allowType, 1, 1, userId},
                new IType[] { NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.Int32, NHibernateUtil.Int32}
                ) as IList<DataSourceCategory>;
        }

        public IList<string> FindDataSourceCategoryList(int userId, string allowType, bool includeInactive)
        {
            string QueryStr = @"select distinct dsc.Name 
                                from DataSourceCategory dsc, 
                                DataSourceOperator dso 
                                where dsc.TheDataSource.id = dso.TheDataSource.id 
                                and dso.TheUser.Id = ? 
                                and dso.AllowType = ?    
                                and dso.TheDataSource.ActiveFlag = ?                             
                                and dso.TheUser in elements(dsc.Users)";

            if (!includeInactive) {
                QueryStr += " and dsc.ActiveFlag = 1 ";
                
            }
            return FindAllWithCustomQuery(
                QueryStr,
                new object[] { userId, allowType, 1 },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String, NHibernateUtil.Int32 }
                ) as IList<string>;          
        }

        public IList<string> FindDataSourceTypeList(int userId, string allowType)
        {
            return FindAllWithCustomQuery(
                @"select distinct dsc.TheDataSource.DSType 
                            from DataSourceCategory dsc, 
                            DataSourceOperator dso 
                            where dsc.TheDataSource.id = dso.TheDataSource.id 
                            and dso.TheUser.Id = ? 
                            and dso.AllowType = ? 
                            and dso.TheDataSource.ActiveFlag = ?
                            and dsc.ActiveFlag = ?
                            and dso.TheUser in elements(dsc.Users)",
                new object[] { userId, allowType, 1, true },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String, NHibernateUtil.Int32, NHibernateUtil.Boolean }
                ) as IList<string>;
        }

        public void DeleteDataSourceCategoryByDSId(int dsId)
        {
            string hql = @"from DataSourceCategory entity where entity.TheDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
