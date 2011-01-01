using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class CodeMasterBaseMgr : SessionBase, ICodeMasterBaseMgr
    {
        public ICodeMasterDao entityDao { get; set; }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCodeMaster(CodeMaster entity)
        {
            entityDao.CreateCodeMaster(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CodeMaster LoadCodeMaster(String code, String value)
        {
            return entityDao.LoadCodeMaster(code, value);
        }
    

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CodeMaster> GetAllCodeMaster()
        {
            return entityDao.GetAllCodeMaster();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCodeMaster(CodeMaster entity)
        {
            entityDao.UpdateCodeMaster(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCodeMaster(String code, String value)
        {
            entityDao.DeleteCodeMaster(code, value);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCodeMaster(CodeMaster entity)
        {
            entityDao.DeleteCodeMaster(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCodeMaster(IList<CodeMaster> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCodeMaster(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
