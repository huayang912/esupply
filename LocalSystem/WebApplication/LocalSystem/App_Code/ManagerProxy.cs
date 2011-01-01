using System;
using System.Collections;
using System.Collections.Generic;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Utility;
using NHibernate.Expression;

/// <summary>
/// Summary description for ManagerProxy
/// </summary>
namespace com.LocalSystem.Web
{
    public class CriteriaMgrProxy
    {
        private ICriteriaMgrE CriteriaMgr
        {
            get
            {
                return ServiceLocator.GetService<ICriteriaMgrE>("CriteriaMgr.service");
            }
        }

        public CriteriaMgrProxy()
        {
        }

        public IList FindAll(DetachedCriteria selectCriteria, int firstRow, int maxRows)
        {
            return CriteriaMgr.FindAll(selectCriteria, firstRow, maxRows);
        }

        public int FindCount(DetachedCriteria selectCriteria)
        {
            IList list = CriteriaMgr.FindAll(selectCriteria);
            if (list != null && list.Count > 0)
            {
                if (list[0] is int)
                {
                    return int.Parse(list[0].ToString());
                }
                else if (list[0] is object[])
                {
                    return int.Parse(((object[])list[0])[0].ToString());
                }
                //由于性能问题,此后禁用该方法。
                //else if (list[0] is object)
                //{
                //    return list.Count;
                //}
                else
                {
                    throw new Exception("unknow result type");
                }
            }
            else
            {
                return 0;
            }
        }
    }
    public class EntityPreferenceMgrProxy
    {
        private IEntityPreferenceMgrE EntityPreferenceMgr
        {
            get
            {
                return ServiceLocator.GetService<IEntityPreferenceMgrE>("EntityPreferenceMgr.service");
            }
        }

        public EntityPreferenceMgrProxy()
        {
        }

        public void UpdateEntityPreference()
        { }

        public IList<EntityPreference> LoadEntityPreference()
        {
            return EntityPreferenceMgr.GetAllEntityPreferenceOrderBySeq();
        }

        public void UpdateEntityPreference(EntityPreference entityPreference)
        {
            EntityPreferenceMgr.UpdateEntityPreference(entityPreference);
        }

    }
}