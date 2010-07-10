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
    public class NHDWDataSourceParameterDao : NHDaoBase, IDWDataSourceParameterDao
    {
        public NHDWDataSourceParameterDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateDWDataSourceParameter(DWDataSourceParameter entity)
        {
            Create(entity);
        }

        public DWDataSourceParameter LoadDWDataSourceParameter(int id)
        {
            return FindById(typeof(DWDataSourceParameter), id) as DWDataSourceParameter;
        }

        public void UpdateDWDataSourceParameter(DWDataSourceParameter entity)
        {
            Update(entity);
        }

        public void DeleteDWDataSourceParameter(int id)
        {
            string hql = @"from DWDataSourceParameter entity where entity.Id = ?";

            Delete(hql, id, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteDWDataSourceParameter(DWDataSourceParameter entity)
        {
            Delete(entity);
        }

        public void DeleteDWDataSourceParameter(IList<int> idList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from DWDataSourceParameter entity where entity.Id in (");
            hql.Append(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(",");
                hql.Append(idList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteDWDataSourceParameter(IList<DWDataSourceParameter> entityList)
        {
            IList<int> idList = new List<int>();
            foreach (DWDataSourceParameter entity in entityList)
            {
                idList.Add(entity.Id);
            }

            DeleteDWDataSourceParameter(idList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList<DWDataSourceParameter> LoadAllActiveDWDataSourceParameter()
        {
            return FindAllWithCustomQuery("from DWDataSourceParameter ds order by ds.Name") as IList<DWDataSourceParameter>;
        }
        
        #endregion Customized Methods
    }
}
