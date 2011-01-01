using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.LocalSystem.Entity.Operation;
using com.LocalSystem.Persistence.Operation;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation.Impl
{
    [Transactional]
    public class BarCodeBaseMgr : SessionBase, IBarCodeBaseMgr
    {
        public IBarCodeDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBarCode(BarCode entity)
        {
            entityDao.CreateBarCode(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BarCode LoadBarCode(Int32 id)
        {
            return entityDao.LoadBarCode(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BarCode> GetAllBarCode()
        {
            return entityDao.GetAllBarCode();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBarCode(BarCode entity)
        {
            entityDao.UpdateBarCode(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBarCode(Int32 id)
        {
            entityDao.DeleteBarCode(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBarCode(BarCode entity)
        {
            entityDao.DeleteBarCode(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBarCode(IList<Int32> pkList)
        {
            entityDao.DeleteBarCode(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBarCode(IList<BarCode> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBarCode(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
