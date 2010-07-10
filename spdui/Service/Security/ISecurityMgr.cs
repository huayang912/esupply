using System;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Entity.Security;

namespace Dndp.Service.Security
{
    public interface ISecurityMgr
    {
        User Login(string userName, string password);

        User DomainLogin(string windowsDomain, string windowsUserName);
    }
}
