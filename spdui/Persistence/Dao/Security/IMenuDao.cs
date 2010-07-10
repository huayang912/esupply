using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Persistence.Dao.Security
{
    public interface IMenuDao
    {
        IList LoadAllMenus();
    }
}
