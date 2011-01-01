using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.LocalSystem.Entity.Operation;
//TODO: Add other using statements here.

namespace com.LocalSystem.Persistence.Operation
{
    public interface IPoDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePoDetail(PoDetail entity);

        PoDetail LoadPoDetail(Int32 id);
  
        IList<PoDetail> GetAllPoDetail();
  
        void UpdatePoDetail(PoDetail entity);
        
        void DeletePoDetail(Int32 id);
    
        void DeletePoDetail(PoDetail entity);
    
        void DeletePoDetail(IList<Int32> pkList);
    
        void DeletePoDetail(IList<PoDetail> entityList);    
        
        PoDetail LoadPoDetail(String seq, String poCode);
    
        void DeletePoDetail(String seq, String poCode);
        #endregion Method Created By CodeSmith
    }
}
