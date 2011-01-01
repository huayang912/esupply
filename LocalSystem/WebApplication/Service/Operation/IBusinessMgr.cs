using System;
using com.LocalSystem.Entity.Operation;
using System.Collections.Generic;
using com.LocalSystem.Entity.MasterData;
using System.IO;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation
{
    public interface IBusinessMgr
    {
        #region Customized Methods

        List<Item> ReadItemFromXls(Stream inputStream, string userCode);

        List<Item> ReadItemFromCSV(Stream inputStream, string userCode);

        List<Supplier> ReadSupplierFromXls(Stream inputStream, string userCode);

        List<Supplier> ReadSupplierFromCSV(Stream inputStream, string userCode);
        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.Operation
{
    public partial interface IBusinessMgrE : com.LocalSystem.Service.Operation.IBusinessMgr
    {
    }
}

#endregion Extend Interface