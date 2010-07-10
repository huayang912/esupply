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
    public class NHDWDataSourceOperatorDao : NHDaoBase, IDWDataSourceOperatorDao
    {
        public NHDWDataSourceOperatorDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDWDataSourceOperator(DWDataSourceOperator entity)
        {
            Create(entity);
        }

        public DWDataSourceOperator LoadDWDataSourceOperator(int id)
        {
            return FindById(typeof(DWDataSourceOperator), id) as DWDataSourceOperator;
        }

        public void UpdateDWDataSourceOperator(DWDataSourceOperator entity)
        {
            Update(entity);
        }

        public void DeleteDWDataSourceOperator(int id)
        {
            string hql = @"from DWDataSourceOperator entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDWDataSourceOperator(DWDataSourceOperator entity)
        {
            Delete(entity);
        }

        public void DeleteDWDataSourceOperator(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DWDataSourceOperator entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDWDataSourceOperator(IList<DWDataSourceOperator> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DWDataSourceOperator entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDWDataSourceOperator(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList FindAllByDWDataSourceId(int DWDataSourceId)
        {
            return FindAllWithCustomQuery("from DWDataSourceOperator dso where dso.TheDWDataSource.Id=?", DWDataSourceId);
        }

        public IList<DWDataSourceOperator> FindAllByDWDataSourceIdAndAllowType(int dsId, string type)
        {
            string hql = "select dso from DWDataSourceOperator dso where dso.TheDWDataSource.Id = ? and dso.AllowType = ?";

            return FindAllWithCustomQuery(hql,
                new object[] { dsId, type },
                new IType[] { NHibernateUtil.Int32, NHibernateUtil.String }) as IList<DWDataSourceOperator>;
        }

        public void DeleteDWDataSourceOperatorByDSId(int dsId)
        {
            string hql = @"from DWDataSourceOperator entity where entity.TheDWDataSource.Id = ?";
            Delete(hql, dsId, NHibernate.NHibernateUtil.Int32);
        }

        #endregion Customized Methods
    }
}
