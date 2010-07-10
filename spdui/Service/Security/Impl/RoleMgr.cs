using System;
using System.Collections;
using System.Text;

using NHibernate;

using Dndp.Persistence.Entity.Security;
using Dndp.Utility;
using Dndp.Persistence.Dao.Security;

namespace Dndp.Service.Security.Impl
{
    public class RoleMgr : SessionBase, IRoleMgr
    {
        private IRoleDao roleDao;

        public RoleMgr(IRoleDao roleDao)
        {
            this.roleDao = roleDao;
        }

        public IList LoadAllRoles()
        {
            return roleDao.LoadAllRoles();
        }

    }
}
