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
    public class PoDetailBaseMgr : SessionBase, IPoDetailBaseMgr
    {
        public IPoDetailDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePoDetail(PoDetail entity)
        {
            entityDao.CreatePoDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PoDetail LoadPoDetail(Int32 id)
        {
            return entityDao.LoadPoDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PoDetail> GetAllPoDetail()
        {
            return entityDao.GetAllPoDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePoDetail(PoDetail entity)
        {
            entityDao.UpdatePoDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePoDetail(Int32 id)
        {
            entityDao.DeletePoDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePoDetail(PoDetail entity)
        {
            entityDao.DeletePoDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePoDetail(IList<Int32> pkList)
        {
            entityDao.DeletePoDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePoDetail(IList<PoDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePoDetail(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual PoDetail LoadPoDetail(String seq, String poCode)
        {
            return entityDao.LoadPoDetail(seq, poCode);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePoDetail(String seq, String poCode)
        {
            entityDao.DeletePoDetail(seq, poCode);
        }   
        #endregion Method Created By CodeSmith
    }
}
