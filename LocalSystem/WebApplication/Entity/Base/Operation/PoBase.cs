using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.Operation
{
    [Serializable]
    public abstract class PoBase : EntityBase
    {
        #region O/R Mapping Properties

        private string _code;
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }
        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        private string _supplier;
        public string Supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                _supplier = value;
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
        private DateTime _createDate;
        [XmlIgnore]
        public DateTime CreateDate
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
        private string _lastmodifyUser;
        [XmlIgnore]
        public string LastmodifyUser
        {
            get
            {
                return _lastmodifyUser;
            }
            set
            {
                _lastmodifyUser = value;
            }
        }
        private DateTime? _lastmodifyDate;
        [XmlIgnore]
        public DateTime? LastmodifyDate
        {
            get
            {
                return _lastmodifyDate;
            }
            set
            {
                _lastmodifyDate = value;
            }
        }
        private DateTime? _outboundDate;
        [XmlIgnore]
        public DateTime? OutboundDate
        {
            get
            {
                return _outboundDate;
            }
            set
            {
                _outboundDate = value;
            }
        }
        private Boolean _isOutbound;
        [XmlIgnore]
        public Boolean IsOutbound
        {
            get
            {
                return _isOutbound;
            }
            set
            {
                _isOutbound = value;
            }
        }
        [XmlIgnore]
        public string RefPo { get; set; }
        [XmlIgnore]
        public string PlantCode { get; set; }
        [XmlIgnore]
        public string PlantName { get; set; }
        [XmlIgnore]
        public string PlantContact { get; set; }
        [XmlIgnore]
        public string PlantAddress { get; set; }
        [XmlIgnore]
        public string PlantFax { get; set; }
        [XmlIgnore]
        public string PlantPhone { get; set; }
        [XmlIgnore]
        public string PlantDock { get; set; }
        [XmlIgnore]
        public string SupplierName { get; set; }
        [XmlIgnore]
        public string SupplierContact { get; set; }
        [XmlIgnore]
        public string SupplierFax { get; set; }
        [XmlIgnore]
        public string SupplierPhone { get; set; }
        [XmlIgnore]
        public string SupplierAddress { get; set; }
        [XmlIgnore]
        public string LotNo { get; set; }
        [XmlIgnore]
        public DateTime? WinTime { get; set; }
        [XmlIgnore]
        public Boolean IsExport { get; set; }
        [XmlIgnore]
        public string murn { get; set; }
        [XmlIgnore]
        public string orderg { get; set; }
        [XmlIgnore]
        public string delordgr { get; set; }
        [XmlIgnore]
        public string fdcode { get; set; }
        [XmlIgnore]
        public string route { get; set; }
        [XmlIgnore]
        public string mroute { get; set; }
        [XmlIgnore]
        public string mantitle { get; set; }
        [XmlIgnore]
        public IList<PoDetail> PoDetails { get; set; }

        #endregion

        public override int GetHashCode()
        {
            if (Code != null)
            {
                return Code.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            PoBase another = obj as PoBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Code == another.Code);
            }
        }
    }

}
