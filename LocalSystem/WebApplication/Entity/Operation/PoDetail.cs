using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.Operation
{
    [Serializable]
    public class PoDetail : PoDetailBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 
        //public string Supplier { get; set; }

        public List<int> BarCodeIds { get; set; }

        #endregion
    }
}