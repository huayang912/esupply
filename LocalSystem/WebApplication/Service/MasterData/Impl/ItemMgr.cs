using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.LocalSystem.Persistence.MasterData;
using NHibernate.Expression;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Entity.Exception;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class ItemMgr : ItemBaseMgr, IItemMgr
    {
        #region Customized Methods

        //TODO: Add other methods here.
        public ICriteriaMgrE criteriaMgrE { get; set; }

        [Transaction(TransactionMode.Unspecified)]
        public bool CheckItemExist(string code)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Item));
            criteria.Add(Expression.Eq("IsActive", true));
            criteria.Add(Expression.Eq("Code", code));
            criteria.SetProjection(Projections.ProjectionList().Add(Projections.Count("Code")));
            IList<int> count = criteriaMgrE.FindAll<int>(criteria);

            if (count[0] > 0)
            {
                return true;
            }
            return false;
        }


        [Transaction(TransactionMode.Unspecified)]
        public Item CheckAndLoadItem(string itemCode)
        {
            Item item = this.LoadItem(itemCode);
            if (item == null)
            {
                throw new BusinessErrorException("Item.Error.ItemCodeNotExist", itemCode);
            }

            return item;
        }

        [Transaction(TransactionMode.Unspecified)]
        public void UpdateOrCreateItem(List<Item> items, string userCode)
        {
            foreach (Item item in items)
            {
                item.IsActive = true;
                item.LastmodifyDate = DateTime.Now;
                item.LastmodifyUser = userCode;
                Item newItem = this.LoadItem(item.Code);
                if (newItem == null)
                {
                    item.CreateDate = DateTime.Now;
                    item.CreateUser = userCode;
                    this.CreateItem(item);
                }
                else
                {
                    newItem.Description = item.Description;
                    newItem.UC = item.UC;
                    newItem.Uom = item.Uom;
                    this.UpdateItem(newItem);
                }
            }
        }
        #endregion Customized Methods
    }
}


#region Extend Class

namespace com.LocalSystem.Service.Ext.MasterData.Impl
{
    [Transactional]
    public partial class ItemMgrE : com.LocalSystem.Service.MasterData.Impl.ItemMgr, IItemMgrE
    {
    }
}

#endregion Extend Class