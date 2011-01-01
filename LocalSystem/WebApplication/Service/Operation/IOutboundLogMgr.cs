using System;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation
{
    public interface IOutboundLogMgr : IOutboundLogBaseMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.Operation
{
    public partial interface IOutboundLogMgrE : com.LocalSystem.Service.Operation.IOutboundLogMgr
    {
    }
}

#endregion Extend Interface