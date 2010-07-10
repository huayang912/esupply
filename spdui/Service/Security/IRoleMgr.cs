using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Service.Security
{
    public interface IRoleMgr
    {
        IList LoadAllRoles();
    }
}
