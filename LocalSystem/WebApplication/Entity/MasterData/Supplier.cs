using System;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.MasterData
{
    [Serializable]
    public class Supplier : SupplierBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 

        public string CodeName
        {
            get
            {
                return this.Name + "[" + this.Code + "]";
            }
        }

        #endregion
    }
}