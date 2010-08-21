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
    public class NHDataSourceUploadDao : NHDaoBase, IDataSourceUploadDao
    {
        public NHDataSourceUploadDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDataSourceUpload(DataSourceUpload entity)
        {
            Create(entity);
        }

        public DataSourceUpload LoadDataSourceUpload(int id)
        {
            return FindById(typeof(DataSourceUpload), id) as DataSourceUpload;
        }

        public void UpdateDataSourceUpload(DataSourceUpload entity)
        {
            Update(entity);
        }

        public void DeleteDataSourceUpload(int id)
        {
            string hql = @"from DataSourceUpload entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDataSourceUpload(DataSourceUpload entity)
        {
            Delete(entity);
        }

        public void DeleteDataSourceUpload(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSourceUpload entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDataSourceUpload(IList<DataSourceUpload> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DataSourceUpload entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDataSourceUpload(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<DataSourceUpload> FindLastestDSUpload(int userId, string allowType)
        {
            string hql = " from DataSourceUpload as dataSourceUpload where dataSourceUpload.Id in "
                       + " (select max(dsu.Id) "
                       + " from DataSourceUpload as dsu, DataSourceOperator as dso "
                       + " where dso.TheUser.id = ? "
                       + " and dso.AllowType = ? "
                       + " and dsu.TheDataSourceCategory.TheDataSource.id = dso.TheDataSource.id "
                       + " and dso.TheDataSource.ActiveFlag = ? "
                       + " group by dsu.TheDataSourceCategory) order by dataSourceUpload.TheDataSourceCategory.TheDataSource.Description, dataSourceUpload.TheDataSourceCategory.Name";

            IList<DataSourceUpload> list = FindAllWithCustomQuery(
                hql, new object[] { userId, allowType, 1 },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String, NHibernateUtil.Int32 }) as IList<DataSourceUpload>;

            return list;
        }

        public DataSourceUpload FindLastestDSUpload(int dscId)
        {
            string hql = " from DataSourceUpload as dataSourceUpload where dataSourceUpload.Id = "
                       + " (select max(dsu.Id) "
                       + " from DataSourceUpload as dsu "
                       + " where dsu.TheDataSourceCategory.Id = ?) ";

            IList list = FindAllWithCustomQuery(
                hql, new object[] { dscId },
                new IType[] { NHibernateUtil.Int32 });

            if (list != null && list.Count > 0)
            {
                return list[0] as DataSourceUpload;
            }

            return null;
        }

        public IList<DataSourceUpload> FindDataSourceUpload(int dsId)
        {
            return FindAllWithCustomQuery(
                "from DataSourceUpload as dsu where dsu.TheDataSourceCategory.TheDataSource.Id = ? order by dsu.UploadDate Desc",
                new object[]{ dsId },
                new IType[] { NHibernateUtil.Int32 }) as IList<DataSourceUpload>;
        }

        public int GenerateBatchNo(int dsCategoryId)
        {
            IList list = FindAllWithCustomQuery(
                "select count(*) from DataSourceUpload as dsu where dsu.TheDataSourceCategory.Id = ?",
                new object[] { dsCategoryId },
                new IType[] { NHibernateUtil.Int32 });

            return (int.Parse(list[0].ToString()) + 1);
        }

        public void DeleteDataSourceUploadByDSId(int dsId)
        {
            string hql = @"from DataSourceUpload entity where entity.TheDataSourceCategory.TheDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        public IList<DataSourceUpload> FindDataSourceUploadForETL()
        {
            return FindAllWithCustomQuery(
                "from DataSourceUpload as dsu where dsu.ProcessStatus in ('ETL_FAILED','ETL_CONFIRMED')") as IList<DataSourceUpload>;
        }

        public IList<DataSourceUpload> FindDataSourceUploadInETL()
        {
            return FindAllWithCustomQuery(
                "from DataSourceUpload as dsu where dsu.ProcessStatus = 'ETL_LOCKED'") as IList<DataSourceUpload>;
        }

        public IList<DataSourceUpload> FindDataSourceUpload(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DataSourceUpload entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            return FindAllWithCustomQuery(hql.ToString()) as IList<DataSourceUpload>;
        }

        public IList<DataSourceUpload> FindDataSourceUpload(int datasourceId, string category, string subject, string fileName, string createBy, User user)
        {
            string hql = @"from DataSourceUpload as dsu 
                where dsu.TheDataSourceCategory.TheDataSource.Id = ?
                      and ? in elements(dsu.TheDataSourceCategory.Users) "
                + ((category != null && category.Trim() != string.Empty) ? (" and dsu.TheDataSourceCategory.Name =  '"+category.Trim()+"'") : "")
                + ((subject != null && subject.Trim() != string.Empty) ? (" and dsu.Name like  '%" + subject.Trim() + "%'") : "")
                + ((fileName != null && fileName.Trim() != string.Empty) ? (" and dsu.UploadFileOriginName like  '%" + fileName.Trim() + "%'") : "")
                + ((createBy != null && createBy.Trim() != string.Empty) ? (" and dsu.UploadBy.UserName like '%" + createBy.Trim() + "%'") : "")
                + @" order by dsu.UploadDate Desc";

            return FindAllWithCustomQuery(hql, new object[] { datasourceId, user.Id },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.Int32 }) as IList<DataSourceUpload>;
        }

        #endregion Customized Methods
    }
}
