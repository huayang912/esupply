using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.MasterData
{
    [Serializable]
    public abstract class SupplierBase : EntityBase
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
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		private string _address;
		public string Address
		{
			get
			{
				return _address;
			}
			set
			{
				_address = value;
			}
		}
		private string _contact;
		public string Contact
		{
			get
			{
				return _contact;
			}
			set
			{
				_contact = value;
			}
		}
		private string _fax;
		public string Fax
		{
			get
			{
				return _fax;
			}
			set
			{
				_fax = value;
			}
		}
		private string _phone;
		public string Phone
		{
			get
			{
				return _phone;
			}
			set
			{
				_phone = value;
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
		private string _lastmodifyUser;
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
        private string _carrier;
        public string Carrier
        {
            get
            {
                return _carrier;
            }
            set
            {
                _carrier = value;
            }
        }
		private Boolean _isActive;
		public Boolean IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}

        [XmlIgnore]
        public Decimal? LeadTime { get; set; }
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
            SupplierBase another = obj as SupplierBase;

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
