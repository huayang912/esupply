using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.MasterData;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Entity.Exception;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class SupplierMgr : SupplierBaseMgr, ISupplierMgr
    {
        #region Customized Methods

        public Supplier CheckAndLoadSupplier(string code)
        {
            Supplier supplier = this.LoadSupplier(code);
            if (supplier == null)
            {
                throw new BusinessErrorException("MasterData.Supplier.Error.SupplierCodeNotExist", code);
            }
            else
            {
                return supplier;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public void UpdateOrCreateSupplier(List<Supplier> suppliers, string userCode)
        {
            foreach (Supplier supplier in suppliers)
            {
                supplier.IsActive = true;
                supplier.LastmodifyDate = DateTime.Now;
                supplier.LastmodifyUser = userCode;
                Supplier newSupplier = this.LoadSupplier(supplier.Code);
                if (newSupplier == null)
                {
                    supplier.CreateDate = DateTime.Now;
                    supplier.CreateUser = userCode;
                    this.CreateSupplier(supplier);
                }
                else
                {
                    newSupplier.Name = supplier.Name;
                    newSupplier.Address = supplier.Address;
                    newSupplier.Contact = supplier.Contact;
                    newSupplier.Phone = supplier.Phone;
                    newSupplier.Fax = supplier.Fax;
                    this.UpdateSupplier(newSupplier);
                }
            }
        }

        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.MasterData.Impl
{
    [Transactional]
    public partial class SupplierMgrE : com.LocalSystem.Service.MasterData.Impl.SupplierMgr, ISupplierMgrE
    {
    }
}

#endregion Extend Class