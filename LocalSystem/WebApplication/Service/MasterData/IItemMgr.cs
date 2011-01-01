using System;
using com.LocalSystem.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface IItemMgr : IItemBaseMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.
        bool CheckItemExist(string code);

        Item CheckAndLoadItem(string itemCode);

        void UpdateOrCreateItem(List<Item> items, string userCode);

        #endregion Customized Methods
    }
}


#region Extend Interface

namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface IItemMgrE : com.LocalSystem.Service.MasterData.IItemMgr
    {
    }
}

#endregion Extend Interface