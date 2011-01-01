using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.LocalSystem.Entity.MasterData;

//TODO: Add other using statmens here.

namespace com.LocalSystem.Persistence.MasterData.NH
{
    public class NHAppUserPermissionBaseDao : NHDaoBase, IAppUserPermissionBaseDao
    {
        public NHAppUserPermissionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateAppUserPermission(AppUserPermission entity)
        {
            Create(entity);
        }

        public virtual IList<AppUserPermission> GetAllAppUserPermission()
        {
            return FindAll<AppUserPermission>();
        }

        public virtual AppUserPermission LoadAppUserPermission(Int32 id)
        {
            return FindById<AppUserPermission>(id);
        }

        public virtual void UpdateAppUserPermission(AppUserPermission entity)
        {
            Update(entity);
        }

        public virtual void DeleteAppUserPermission(Int32 id)
        {
            string hql = @"from AppUserPermission entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteAppUserPermission(AppUserPermission entity)
        {
            Delete(entity);
        }

        public virtual void DeleteAppUserPermission(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from AppUserPermission entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteAppUserPermission(IList<AppUserPermission> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (AppUserPermission entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteAppUserPermission(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
