using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.Operation;
using com.LocalSystem.Entity.Operation;
using NHibernate.Expression;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Entity;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation.Impl
{
    [Transactional]
    public class BarCodeMgr : BarCodeBaseMgr, IBarCodeMgr
    {
        public ICriteriaMgrE criteriaMgrE { get; set; }
        public ISupplierMgrE supplierMgrE { get; set; }
        public IItemMgrE itemMgrE { get; set; }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<BarCode> GetBarCode(string LotNo, string ItemCode, string SupplierCode, string CreateUser, DateTime? StartDate, DateTime? EndDate, List<string> Status)
        {

            DetachedCriteria criteria = DetachedCriteria.For(typeof(BarCode));

            if (LotNo != null && LotNo.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("LotNo", LotNo));
            }
            if (ItemCode != null && ItemCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("ItemCode", ItemCode));
            }
            if (SupplierCode != null && SupplierCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("SupplierCode", SupplierCode));
            }
            if (CreateUser != null && CreateUser.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("CreateUser", CreateUser));
            }

            if (StartDate != null)
            {
                criteria.Add(Expression.Gt("CreateDate", StartDate));
            }
            if (EndDate != null)
            {
                criteria.Add(Expression.Lt("CreateDate", EndDate));
            }
            if (Status != null && Status.Count > 0)
            {
                criteria.Add(Expression.In("Status", Status));
            }

            criteria.AddOrder(Order.Desc("CreateDate"));
            //criteria.AddOrder(Order.Desc("LotNo"));
            //criteria.AddOrder(Order.Desc("Seq"));
            IList<BarCode> BarCodes = criteriaMgrE.FindAll<BarCode>(criteria, 0, 501);
            return BarCodes;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BarCode> GetBarCode(List<int> poDetailsId)
        {
            if (poDetailsId != null && poDetailsId.Count > 0)
            {
                DetachedCriteria criteria = DetachedCriteria.For(typeof(BarCode));
                criteria.Add(Expression.In("PoDetailId", poDetailsId));
                return criteriaMgrE.FindAll<BarCode>(criteria);
            }
            else
            {
                return null;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void UnCloseBarCode(List<int> poDetailsId)
        {
            IList<BarCode> barCodes = this.GetBarCode(poDetailsId);
            foreach (BarCode barCode in barCodes)
            {
                if (barCode.Memo == null || barCode.Memo.Trim() == string.Empty)
                {
                    barCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_CREATE;
                }
                else
                {
                    barCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_WARNING;
                }
                barCode.PoDetailId = null;
                this.UpdateBarCode(barCode);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void CreateBarCode(List<BarCode> barCodes)
        {
            int i = 1;
            foreach (var barCode in barCodes)
            {
                barCode.Seq = i;
                this.CreateBarCode(barCode);
                i++;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public BarCode CheckAndLoadBarCode(string barCode, string userCode)
        {
            BarCode newBarCode = new BarCode();
            newBarCode.BarCode = barCode;
            newBarCode.CreateDate = DateTime.Now;
            newBarCode.CreateUser = userCode;
            newBarCode.LotNo = DateTime.Now.ToString("yyMMddHHmm");
            if (barCode.Length > 11 && barCode.Substring(8).Contains("+"))
            {
                newBarCode.SupplierCode = barCode.Substring(2, 6);
                newBarCode.ItemCode = barCode.Substring(8).Split('+')[0];
                newBarCode.Qty = 0;
                Item item = itemMgrE.LoadItem(newBarCode.ItemCode);
                if (item == null)
                {
                    newBarCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_WARNING;
                    newBarCode.Memo = "物料不存在";
                }
                else
                {
                    newBarCode.ItemDescription = item.Description;
                    newBarCode.Uom = item.Uom;
                    newBarCode.UC = item.UC;
                    newBarCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_CREATE;
                }
                Supplier supplier = supplierMgrE.LoadSupplier(newBarCode.SupplierCode);
                if (supplier == null)
                {
                    newBarCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_ERROR;
                    newBarCode.Memo = "供应商不存在";
                }
                else
                {
                    newBarCode.SupplierCode = supplier.Code;
                    //newBarCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_CREATE;
                }
                try
                {
                    newBarCode.Qty = decimal.Parse(barCode.Substring(8).Split('+')[1]);
                }
                catch (Exception)
                {
                    newBarCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_ERROR;
                    newBarCode.Memo = "数量不合法";
                }
            }
            else
            {
                newBarCode.Status = BusinessConstants.BARCODE_STATUS_VALUE_ERROR;
                newBarCode.Memo = "条码不合法";
            }
            return newBarCode;
        }
        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.Operation.Impl
{
    [Transactional]
    public partial class BarCodeMgrE : com.LocalSystem.Service.Operation.Impl.BarCodeMgr, IBarCodeMgrE
    {
    }
}

#endregion Extend Class