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
    public class NHAppUserBaseDao : NHDaoBase, IAppUserBaseDao
    {
        public NHAppUserBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateAppUser(AppUser entity)
        {
            Create(entity);
        }

        public virtual IList<AppUser> GetAllAppUser()
        {
            return GetAllAppUser(false);
        }

        public virtual IList<AppUser> GetAllAppUser(bool includeInactive)
        {
            string hql = @"from AppUser entity";
            if (!includeInactive)
            {
                hql += " where entity.IsActive = 1";
            }
            IList<AppUser> result = FindAllWithCustomQuery<AppUser>(hql);
            return result;
        }

        public virtual AppUser LoadAppUser(String code)
        {
            return FindById<AppUser>(code);
        }

        public virtual void UpdateAppUser(AppUser entity)
        {
            Update(entity);
        }

        public virtual void DeleteAppUser(String code)
        {
            string hql = @"from AppUser entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeleteAppUser(AppUser entity)
        {
            Delete(entity);
        }

        public virtual void DeleteAppUser(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from AppUser entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteAppUser(IList<AppUser> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (AppUser entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeleteAppUser(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
