using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.Operation
{
    [Serializable]
    public abstract class BarCodeBase : EntityBase
    {
        #region O/R Mapping Properties

        private Int32 _id;
        public Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private Int32? _seq;
        public Int32? Seq
        {
            get
            {
                return _seq;
            }
            set
            {
                _seq = value;
            }
        }
        private string _barCode;
        public string BarCode
        {
            get
            {
                return _barCode;
            }
            set
            {
                _barCode = value;
            }
        }
        private string _lotNo;
        public string LotNo
        {
            get
            {
                return _lotNo;
            }
            set
            {
                _lotNo = value;
            }
        }
        private string _itemCode;
        public string ItemCode
        {
            get
            {
                return _itemCode;
            }
            set
            {
                _itemCode = value;
            }
        }
        private string _itemDescription;
        public string ItemDescription
        {
            get
            {
                return _itemDescription;
            }
            set
            {
                _itemDescription = value;
            }
        }
        private string _uom;
        public string Uom
        {
            get
            {
                return _uom;
            }
            set
            {
                _uom = value;
            }
        }
        private Decimal? _uC;
        public Decimal? UC
        {
            get
            {
                return _uC;
            }
            set
            {
                _uC = value;
            }
        }
        public Decimal Qty { get; set; }
        private string _supplierCode;
        public string SupplierCode
        {
            get
            {
                return _supplierCode;
            }
            set
            {
                _supplierCode = value;
            }
        }

        private string _createUser;
        public string CreateUser
        {
            get
            {
                return _createUser;
            }
            set
            {
                _createUser = value;
            }
        }
        private DateTime? _createDate;
        public DateTime? CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }
        public string Status { get; set; }
        public string Memo { get; set; }
        public Int32? PoDetailId { get; set; }
        #endregion

        public override int GetHashCode()
        {
            if (Id != null)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            BarCodeBase another = obj as BarCodeBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Id == another.Id);
            }
        }
    }

}
