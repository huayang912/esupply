using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;

//TODO: Add other using statmens here.

namespace com.LocalSystem.Persistence.MasterData.NH
{
    public class NHAppUserPermissionDao : NHAppUserPermissionBaseDao, IAppUserPermissionDao
    {
        public NHAppUserPermissionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}
