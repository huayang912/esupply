using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.Operation
{
    [Serializable]
    public abstract class OutboundLogBase : EntityBase
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
		private string _poCode;
		public string PoCode
		{
			get
			{
				return _poCode;
			}
			set
			{
				_poCode = value;
			}
		}
		private string _fileName;
		public string FileName
		{
			get
			{
				return _fileName;
			}
			set
			{
				_fileName = value;
			}
		}
		private DateTime _createDate;
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
		private DateTime _lastmodifyDate;
		public DateTime LastmodifyDate
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
		private string _memo;
		public string Memo
		{
			get
			{
				return _memo;
			}
			set
			{
				_memo = value;
			}
		}
		private string _outboundResult;
		public string OutboundResult
		{
			get
			{
				return _outboundResult;
			}
			set
			{
				_outboundResult = value;
			}
		}
        
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
            OutboundLogBase another = obj as OutboundLogBase;

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
