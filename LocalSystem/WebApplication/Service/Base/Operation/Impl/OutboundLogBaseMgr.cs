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
    public class OutboundLogBaseMgr : SessionBase, IOutboundLogBaseMgr
    {
        public IOutboundLogDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOutboundLog(OutboundLog entity)
        {
            entityDao.CreateOutboundLog(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OutboundLog LoadOutboundLog(Int32 id)
        {
            return entityDao.LoadOutboundLog(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OutboundLog> GetAllOutboundLog()
        {
            return entityDao.GetAllOutboundLog();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOutboundLog(OutboundLog entity)
        {
            entityDao.UpdateOutboundLog(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOutboundLog(Int32 id)
        {
            entityDao.DeleteOutboundLog(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOutboundLog(OutboundLog entity)
        {
            entityDao.DeleteOutboundLog(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOutboundLog(IList<Int32> pkList)
        {
            entityDao.DeleteOutboundLog(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOutboundLog(IList<OutboundLog> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOutboundLog(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
