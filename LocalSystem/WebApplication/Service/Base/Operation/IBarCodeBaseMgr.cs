using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.LocalSystem.Entity.Operation;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation
{
    public interface IBarCodeBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateBarCode(BarCode entity);

        BarCode LoadBarCode(Int32 id);

        IList<BarCode> GetAllBarCode();
    
        void UpdateBarCode(BarCode entity);

        void DeleteBarCode(Int32 id);
    
        void DeleteBarCode(BarCode entity);
    
        void DeleteBarCode(IList<Int32> pkList);
    
        void DeleteBarCode(IList<BarCode> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
