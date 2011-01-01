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
    public class NHAppPermissionBaseDao : NHDaoBase, IAppPermissionBaseDao
    {
        public NHAppPermissionBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateAppPermission(AppPermission entity)
        {
            Create(entity);
        }

        public virtual IList<AppPermission> GetAllAppPermission()
        {
            return FindAll<AppPermission>();
        }

        public virtual AppPermission LoadAppPermission(String code)
        {
            return FindById<AppPermission>(code);
        }

        public virtual void UpdateAppPermission(AppPermission entity)
        {
            Update(entity);
        }

        public virtual void DeleteAppPermission(String code)
        {
            string hql = @"from AppPermission entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteAppPermission(AppPermission entity)
        {
            Delete(entity);
        }

        public virtual void DeleteAppPermission(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from AppPermission entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteAppPermission(IList<AppPermission> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (AppPermission entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteAppPermission(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
