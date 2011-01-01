using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.LocalSystem.Entity.Operation;
//TODO: Add other using statements here.

namespace com.LocalSystem.Persistence.Operation
{
    public interface IPoBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePo(Po entity);

        Po LoadPo(String code);
  
        IList<Po> GetAllPo();
  
        void UpdatePo(Po entity);
        
        void DeletePo(String code);
    
        void DeletePo(Po entity);
    
        void DeletePo(IList<String> pkList);
    
        void DeletePo(IList<Po> entityList);    
        #endregion Method Created By CodeSmith
    }
}
