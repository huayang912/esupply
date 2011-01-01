using com.LocalSystem.Service.Ext.MasterData;
using System.Collections.Generic;
using com.LocalSystem.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface IEntityPreferenceMgr : IEntityPreferenceBaseMgr
    {
        #region Customized Methods

        IList<EntityPreference> GetAllEntityPreferenceOrderBySeq();

        #endregion Customized Methods
    }
}





#region Extend Interface





namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface IEntityPreferenceMgrE : com.LocalSystem.Service.MasterData.IEntityPreferenceMgr
    {
        
    }
}

#endregion
