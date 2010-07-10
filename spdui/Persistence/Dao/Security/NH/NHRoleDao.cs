using System;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Entity.Security;
using Castle.Facilities.NHibernateIntegration;
using System.Collections;
using NHibernate;
using Dndp.Persistence.Dao;

namespace Dndp.Persistence.Dao.Security.NH
{
    public class NHRoleDao : NHDaoBase, IRoleDao
    {
        public NHRoleDao(ISessionManager sessionManager)
            : base(sessionManager)
		{
		}

        public Role SearchRoleByPK(int roleId)
        {
            return (Role)FindById(typeof(Role), roleId);   
        }

        public IList LoadAllRoles()
        {
            string hql = @"from Role r order by r.Name";
            return FindAllWithCustomQuery(hql);
        }
    }
}
