using System;
using com.LocalSystem.Entity.Operation;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation
{
    public interface IPoDetailMgr : IPoDetailBaseMgr
    {
        #region Customized Methods

        void CreatePoDetail(PoDetail poDetail);

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.Operation
{
    public partial interface IPoDetailMgrE : com.LocalSystem.Service.Operation.IPoDetailMgr
    {
    }
}

#endregion Extend Interface