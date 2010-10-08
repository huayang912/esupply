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
    public class NHDWDataSourceDao : NHDaoBase, IDWDataSourceDao
    {
        public NHDWDataSourceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDWDataSource(DWDataSource entity)
        {
            Create(entity);
        }

        public DWDataSource LoadDWDataSource(int id)
        {
            return FindById(typeof(DWDataSource), id) as DWDataSource;
        }

        public void UpdateDWDataSource(DWDataSource entity)
        {
            Update(entity);
        }

        public void DeleteDWDataSource(int id)
        {
            string hql = @"from DWDataSource entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDWDataSource(DWDataSource entity)
        {
            Delete(entity);
        }

        public void DeleteDWDataSource(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DWDataSource entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDWDataSource(IList<DWDataSource> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DWDataSource entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDWDataSource(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList LoadAllDWDataSource()
        {
            return FindAllWithCustomQuery("from DWDataSource ds order by ds.Name");
        }

        public IList<DWDataSource> FindDWDataSource(int userId)
        {
            string hql = " from DWDataSource as ds where ds.Id in "
                       + " (select dso.TheDWDataSource.Id "
                       + " from DWDataSourceOperator as dso "
                       + " where dso.TheUser.id = ?)"
                       + " order by ds.Name, ds.DSType";
            
            IList<DWDataSource> list = FindAllWithCustomQuery(
                hql, userId, NHibernateUtil.Int32) as IList<DWDataSource>;

            return list;
        }

        public IList<DWDataSource> FindDWDataSourceByAllowType(int userId, string AllowType)
        {
            string hql = " from DWDataSource as ds where ds.Id in "
                       + " (select dso.TheDWDataSource.Id "
                       + " from DWDataSourceOperator as dso "
                       + " where dso.TheUser.id = ?"
                       + " and dso.AllowType = ? )"
                       + " order by ds.Name, ds.DSType";

            IList<DWDataSource> list = FindAllWithCustomQuery(
                hql, new object[] { userId, AllowType },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String }) as IList<DWDataSource>;

            return list;
        }

        public IList<DWDataSource> FindDWDataSourceByTypeAndName(int userId, string type, string DWName)
        {
            int paraCount = 1;

            string hql = " from DWDataSource as ds where ds.Id in "
                       + " (select dso.TheDWDataSource.Id "
                       + " from DWDataSourceOperator as dso "
                       + " where dso.TheUser.id = ?)";

            if (type != null & type.Trim().Length >¡¡0)
            {
                hql += " and ds.DSType = ?";
                paraCount++;
            }

            if (DWName != null & DWName.Trim().Length > 0)
            {
                hql += " and (ds.Name like ? or ds.Description like ?)";
                paraCount++;
                paraCount++;
            }

            hql += " order by ds.Name, ds.DSType";

            object[] paraValues = new object[paraCount];
            IType[] paraTypes = new IType[paraCount];

            int i = 0;
            paraValues[i] = userId;
            paraTypes[i] = NHibernateUtil.Int32;

            if (type != null & type.Trim().Length > 0)
            {
                i++;
                paraValues[i] = type;
                paraTypes[i] = NHibernateUtil.String;
            }

            if (DWName != null & DWName.Trim().Length > 0)
            {
                i++;
                paraValues[i] = "%" + DWName + "%";
                paraTypes[i] = NHibernateUtil.String;
                i++;
                paraValues[i] = "%" + DWName + "%";
                paraTypes[i] = NHibernateUtil.String;
            }


            IList<DWDataSource> list = FindAllWithCustomQuery(hql, paraValues, paraTypes) as IList<DWDataSource>;

            return list;
        }

        public IList<DWDataSource> FindDWDataSourceByTypeAndName(string type, string DWName)
        {
            int paraCount = 0;
            int i = 0;
            string hql = " from DWDataSource as ds where 1=1";

            if (type != null & type.Trim().Length > 0)
            {
                hql += " and ds.DSType = ?";
                paraCount++;
            }

            if (DWName != null & DWName.Trim().Length > 0)
            {
                hql += " and (ds.Name like ? or ds.Description like ?)";
                paraCount++;
                paraCount++;
            }

            hql += " order by ds.Name, ds.DSType";

            object[] paraValues = new object[paraCount];
            IType[] paraTypes = new IType[paraCount];

            if (type != null & type.Trim().Length > 0)
            {                
                paraValues[i] = type;
                paraTypes[i] = NHibernateUtil.String;
                i++;
            }

            if (DWName != null & DWName.Trim().Length > 0)
            {                
                paraValues[i] = "%" + DWName + "%";
                paraTypes[i] = NHibernateUtil.String;
                i++;
                paraValues[i] = "%" + DWName + "%";
                paraTypes[i] = NHibernateUtil.String;
                i++;
            }


            IList<DWDataSource> list = FindAllWithCustomQuery(hql, paraValues, paraTypes) as IList<DWDataSource>;

            return list;
        }

        public IList<DWDataSource> FindDWDataSourceByTypeAndName(string type, string DWName, User user, string allowType)
        {
            int paraCount = 0;
            int i = 0;
            string hql = " select distinct ds from DWDataSourceOperator as dso inner join dso.TheDWDataSource as ds where 1=1" ;

            if (type != null & type.Trim().Length > 0)
            {
                hql += " and ds.DSType = ?";
                paraCount++;
            }

            if (DWName != null & DWName.Trim().Length > 0)
            {
                hql += " and (ds.Name like ? or ds.Description like ?)";
                paraCount++;
                paraCount++;
            }

            if (user != null)
            {
                hql += " and dso.TheUser.Id = ?";
                paraCount++;
            }

            if (allowType != null & allowType.Trim().Length > 0)
            {
                hql += " and dso.AllowType = ?";
                paraCount++;
            }

            hql += " order by ds.Name, ds.DSType";

            object[] paraValues = new object[paraCount];
            IType[] paraTypes = new IType[paraCount];

            if (type != null & type.Trim().Length > 0)
            {
                paraValues[i] = type;
                paraTypes[i] = NHibernateUtil.String;
                i++;
            }

            if (DWName != null & DWName.Trim().Length > 0)
            {
                paraValues[i] = "%" + DWName + "%";
                paraTypes[i] = NHibernateUtil.String;
                i++;
                paraValues[i] = "%" + DWName + "%";
                paraTypes[i] = NHibernateUtil.String;
                i++;
            }

            if (user != null)
            {
                paraValues[i] = user.Id;
                paraTypes[i] = NHibernateUtil.Int32;
                i++;
            }

            if (allowType != null & allowType.Trim().Length > 0)
            {
                paraValues[i] = allowType;
                paraTypes[i] = NHibernateUtil.String;
                i++;
            }


            IList<DWDataSource> list = FindAllWithCustomQuery(hql, paraValues, paraTypes) as IList<DWDataSource>;

            return list;
        }

        public IList<string> FindDWDataSourceTypeList(int userId, string AllowType)
        {
            string hql = "select distinct ds.DSType from DWDataSource as ds where ds.Id in "
                       + " (select dso.TheDWDataSource.Id "
                       + " from DWDataSourceOperator as dso "
                       + " where dso.TheUser.id = ?"
                       + " and dso.AllowType = ? )"
                       + " order by ds.DSType";

            IList<string> list = FindAllWithCustomQuery(
                hql, new object[] { userId, AllowType },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String }) as IList<string>;

            return list;
        }


        public IList<string> FindDWDataSourceTypeList(int userId)
        {
            string hql = "select distinct ds.DSType from DWDataSource as ds where ds.Id in "
                       + " (select dso.TheDWDataSource.Id "
                       + " from DWDataSourceOperator as dso "
                       + " where dso.TheUser.id = ?)"
                       //+ " and dso.AllowType = ? )"
                       + " order by ds.DSType";

            IList<string> list = FindAllWithCustomQuery(
                hql, new object[] { userId },
                new IType[] { NHibernateUtil.Int32 }) as IList<string>;

            return list;
        }

        public IList<string> FindAllDWDataSourceTypeList()
        {
            string hql = "select distinct ds.DSType from DWDataSource as ds order by ds.DSType";

            IList<string> list = FindAllWithCustomQuery(hql) as IList<string>;

            return list;
        }
        #endregion Customized Methods
    }
}
