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
    public class PoBaseMgr : SessionBase, IPoBaseMgr
    {
        public IPoDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePo(Po entity)
        {
            entityDao.CreatePo(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Po LoadPo(String code)
        {
            return entityDao.LoadPo(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Po> GetAllPo()
        {
            return entityDao.GetAllPo();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePo(Po entity)
        {
            entityDao.UpdatePo(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePo(String code)
        {
            entityDao.DeletePo(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePo(Po entity)
        {
            entityDao.DeletePo(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePo(IList<String> pkList)
        {
            entityDao.DeletePo(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePo(IList<Po> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePo(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
