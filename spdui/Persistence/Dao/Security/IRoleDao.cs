using System;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Entity.Security;
using System.Collections;

namespace Dndp.Persistence.Dao.Security
{
    public interface IRoleDao
    {
        Role SearchRoleByPK(int roleId);

        IList LoadAllRoles();
    }
}
