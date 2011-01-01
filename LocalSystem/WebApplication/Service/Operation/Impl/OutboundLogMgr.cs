using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.Operation;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation.Impl
{
    [Transactional]
    public class OutboundLogMgr : OutboundLogBaseMgr, IOutboundLogMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.Operation.Impl
{
    [Transactional]
    public partial class OutboundLogMgrE : com.LocalSystem.Service.Operation.Impl.OutboundLogMgr, IOutboundLogMgrE
    {
    }
}

#endregion Extend Class