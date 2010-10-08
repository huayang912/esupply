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
    public class NHDataSourceDao : NHDaoBase, IDataSourceDao
    {
        public NHDataSourceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDataSource(DataSource entity)
        {
            Create(entity);
        }

        public DataSource LoadDataSource(int id)
        {
            return FindById(typeof(DataSource), id) as DataSource;
        }

        public void UpdateDataSource(DataSource entity)
        {
            Update(entity);
        }

        public void DeleteDataSource(int id)
        {
            string hql = @"from DataSource entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDataSource(DataSource entity)
        {
            Delete(entity);
        }

        public void DeleteDataSource(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSource entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDataSource(IList<DataSource> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DataSource entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDataSource(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList LoadAllActiveDataSource()
        {
            return FindAllWithCustomQuery("from DataSource ds where ds.ActiveFlag=1 order by ds.DSType, ds.Description");
        }

        public void InactivateDataSource(IList<int> idList)
        {
            foreach (int id in idList)
            {
                DataSource ds = (DataSource)FindById(typeof(DataSource), id);
                if (ds != null)
                {
                    ds.ActiveFlag = 0;
                    Update(ds);
                }
            }
        }

        public IList<string> FindAllDataSourceType()
        {
            IList<string> list = FindAllWithCustomQuery("select distinct ds.DSType from DataSource ds where ds.ActiveFlag=1 order by ds.DSType") as IList<string>;
            return list;
        }

        public IList<string> FindAllDataSourceType(User user)
        {
            IList<string> list = FindAllWithCustomQuery(@"select distinct ds.DSType 
                from DataSourceCategory as dsc inner join dsc.TheDataSource ds 
                where ds.ActiveFlag=1 and dsc.ActiveFlag=1 and " + user.Id + " in elements(dsc.Users) order by ds.DSType") as IList<string>;
            return list;
        }

        public IList<DataSource> FindActiveDataSourceByTypeAndName(string type, string name)
        {
            string hql = "from DataSource ds where ds.ActiveFlag=1 ";

            int paraCount = 0;

            if (type != null & type.Trim().Length > 0)
            {
                hql += " and ds.DSType = ?";
                paraCount++;
            }

            if (name != null & name.Trim().Length > 0)
            {
                hql += " and (ds.Name like ? or ds.Description like ?)";
                paraCount++;
                paraCount++;
            }

            hql += " order by ds.DSType, ds.Description";

            if (paraCount > 0)
            {
                object[] paraValues = new object[paraCount];
                IType[] paraTypes = new IType[paraCount];

                int i = 0;
                if (type != null & type.Trim().Length > 0)
                {                    
                    paraValues[i] = type;
                    paraTypes[i] = NHibernateUtil.String;
                    i++;
                }

                if (name != null & name.Trim().Length > 0)
                {                    
                    paraValues[i] = "%" + name + "%";
                    paraTypes[i] = NHibernateUtil.String;
                    i++;

                    paraValues[i] = "%" + name + "%";
                    paraTypes[i] = NHibernateUtil.String;
                    i++;
                }

                IList<DataSource> list = FindAllWithCustomQuery(hql, paraValues, paraTypes) as IList<DataSource>;
                return list;
            }
            else
            {
                IList<DataSource> list = FindAllWithCustomQuery(hql) as IList<DataSource>;
                return list;
            }
        }

        public IList<DataSource> FindActiveDataSourceByTypeAndName(string type, string name, User user)
        {
            string hql = "select distinct ds from DataSourceCategory as dsc inner join dsc.TheDataSource as ds where ds.ActiveFlag=1 and dsc.ActiveFlag=1 and " + user.Id + " in elements(dsc.Users) ";

            int paraCount = 0;

            if (type != null & type.Trim().Length > 0)
            {
                hql += " and ds.DSType = ?";
                paraCount++;
            }

            if (name != null & name.Trim().Length > 0)
            {
                hql += " and (ds.Name like ? or ds.Description like ?)";
                paraCount++;
                paraCount++;
            }

            hql += " order by ds.DSType, ds.Description";

            if (paraCount > 0)
            {
                object[] paraValues = new object[paraCount];
                IType[] paraTypes = new IType[paraCount];

                int i = 0;
                if (type != null & type.Trim().Length > 0)
                {
                    paraValues[i] = type;
                    paraTypes[i] = NHibernateUtil.String;
                    i++;
                }

                if (name != null & name.Trim().Length > 0)
                {
                    paraValues[i] = "%" + name + "%";
                    paraTypes[i] = NHibernateUtil.String;
                    i++;

                    paraValues[i] = "%" + name + "%";
                    paraTypes[i] = NHibernateUtil.String;
                    i++;
                }

                IList<DataSource> list = FindAllWithCustomQuery(hql, paraValues, paraTypes) as IList<DataSource>;
                return list;
            }
            else
            {
                IList<DataSource> list = FindAllWithCustomQuery(hql) as IList<DataSource>;
                return list;
            }
        }

        #endregion Customized Methods
    }
}
