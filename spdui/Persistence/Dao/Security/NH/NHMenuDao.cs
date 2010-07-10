using System;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Entity.Security;
using System.Collections;
using Castle.Facilities.NHibernateIntegration;
using Dndp.Persistence.Dao;

namespace Dndp.Persistence.Dao.Security.NH
{
    public class NHMenuDao : NHDaoBase, IMenuDao
    {
        public NHMenuDao(ISessionManager sessionManager)
            : base(sessionManager)
		{
		}

        public IList LoadAllMenus()
        {
            return FindAll(typeof(Menu));
        }
    }
}
