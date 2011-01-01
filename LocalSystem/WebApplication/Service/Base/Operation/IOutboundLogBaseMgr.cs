using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.LocalSystem.Entity.Operation;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation
{
    public interface IOutboundLogBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateOutboundLog(OutboundLog entity);

        OutboundLog LoadOutboundLog(Int32 id);

        IList<OutboundLog> GetAllOutboundLog();
    
        void UpdateOutboundLog(OutboundLog entity);

        void DeleteOutboundLog(Int32 id);
    
        void DeleteOutboundLog(OutboundLog entity);
    
        void DeleteOutboundLog(IList<Int32> pkList);
    
        void DeleteOutboundLog(IList<OutboundLog> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
