using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.MasterData
{
    [Serializable]
    public abstract class ItemBase : EntityBase
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
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
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
            ItemBase another = obj as ItemBase;

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
