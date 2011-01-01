using System;
using com.LocalSystem.Entity.Operation;
using System.Collections.Generic;
using com.LocalSystem.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.Operation
{
    public interface IPoMgr : IPoBaseMgr
    {
        #region Customized Methods
        //void CreatePo(Po po);

        void CreatePo(List<PoDetail> poDetails, Supplier supplier, string createUser);

        Po LoadPo(string poCode, bool includeDetail);

        //void DeletePo(Po po);

        void CancelPo(Po po);

        void CreatePo(List<BarCode> barCodes, string createUser);

        //void CreatePo(List<PoDetail> poDetails, string createUser);

        void ReleasePo(Po po, string user);

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.Operation
{
    public partial interface IPoMgrE : com.LocalSystem.Service.Operation.IPoMgr
    {
    }
}

#endregion Extend Interface