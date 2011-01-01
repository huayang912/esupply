using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.Operation;
using com.LocalSystem.Entity.Operation;
using com.LocalSystem.Service.Ext.Operation;
using com.LocalSystem.Entity;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation.Impl
{
    [Transactional]
    public class PoDetailMgr : PoDetailBaseMgr, IPoDetailMgr
    {
        #region Customized Methods
        public IBarCodeMgrE barCodeMgrE { get; set; }

        [Transaction(TransactionMode.Unspecified)]
        public override void CreatePoDetail(PoDetail poDetail)
        {
            base.CreatePoDetail(poDetail);
            if (poDetail.BarCodeIds != null && poDetail.BarCodeIds.Count > 0)
            {
                foreach (var id in poDetail.BarCodeIds)
                {
                    BarCode barCode = barCodeMgrE.LoadBarCode(id);
                    barCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_CLOSE;
                    barCode.PoDetailId = poDetail.Id;
                    barCodeMgrE.UpdateBarCode(barCode);
                }
            }
        }

        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.Operation.Impl
{
    [Transactional]
    public partial class PoDetailMgrE : com.LocalSystem.Service.Operation.Impl.PoDetailMgr, IPoDetailMgrE
    {
    }
}

#endregion Extend Class