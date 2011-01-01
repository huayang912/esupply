using com.LocalSystem.Service.Ext.MasterData;


using System.Collections.Generic;
using Castle.Services.Transaction;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Persistence.MasterData;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Service.Ext.MasterData;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class EntityPreferenceMgr : EntityPreferenceBaseMgr, IEntityPreferenceMgr
    {
        public ICriteriaMgrE criteriaMgrE { get; set; }
        

        #region Customized Methods

        public IList<EntityPreference> GetAllEntityPreferenceOrderBySeq()
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(EntityPreference)).AddOrder(Order.Asc("Seq"));
            return criteriaMgrE.FindAll<EntityPreference>(criteria);
        }

        #endregion Customized Methods
    }
}


#region Extend Class
namespace com.LocalSystem.Service.Ext.MasterData.Impl
{
    [Transactional]
    public partial class EntityPreferenceMgrE : com.LocalSystem.Service.MasterData.Impl.EntityPreferenceMgr, IEntityPreferenceMgrE
    {
        
    }
}
#endregion
