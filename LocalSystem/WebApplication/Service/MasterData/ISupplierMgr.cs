using System;
using com.LocalSystem.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface ISupplierMgr : ISupplierBaseMgr
    {
        #region Customized Methods

        Supplier CheckAndLoadSupplier(string code);

        void UpdateOrCreateSupplier(List<Supplier> suppliers, string userCode);

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface ISupplierMgrE : com.LocalSystem.Service.MasterData.ISupplierMgr
    {
    }
}

#endregion Extend Interface