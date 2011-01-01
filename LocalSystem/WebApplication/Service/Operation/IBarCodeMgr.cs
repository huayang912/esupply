using System;
using com.LocalSystem.Entity.Operation;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation
{
    public interface IBarCodeMgr : IBarCodeBaseMgr
    {
        #region Customized Methods

        IList<BarCode> GetBarCode(string LotNo, string ItemCode, string SupplierCode, string CreateUser, DateTime? StartDate, DateTime? EndDate, List<string> Status);

        IList<BarCode> GetBarCode(List<int> poDetailsId);

        void CreateBarCode(List<BarCode> barCodes);

        BarCode CheckAndLoadBarCode(string barCode, string userCode);

        void UnCloseBarCode(List<int> poDetailsId);

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.Operation
{
    public partial interface IBarCodeMgrE : com.LocalSystem.Service.Operation.IBarCodeMgr
    {
    }
}

#endregion Extend Interface