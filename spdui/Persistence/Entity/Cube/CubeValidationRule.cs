using System;
using System.Collections;
using Dndp.Persistence.Entity.Security;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeValidationRule : EntityBase
    {
        #region O/R Mapping Properties

        private CubeDefinition _theCube;
        public CubeDefinition TheCube
        {
            get
            {
                return _theCube;
            }
            set
            {
                _theCube = value;
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
		
		private int _seqNo;
		public int SeqNo
		{
			get
			{
				return _seqNo;
			}
			set
			{
				_seqNo = value;
			}
		}
		
		private string _type;
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		
		private string _content;
		public string Content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}

        private string _resultContent;
        public string ResultContent
        {
            get
            {
                return _resultContent;
            }
            set
            {
                _resultContent = value;
            }
        }
		
		private string _updateContent;
		public string UpdateContent
		{
			get
			{
				return _updateContent;
			}
			set
			{
				_updateContent = value;
			}
		}

        private User _createUser;
        public User CreateUser
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

        private User _updateUser;
        public User UpdateUser
        {
            get
            {
                return _updateUser;
            }
            set
            {
                _updateUser = value;
            }
        }
		
		private DateTime _updateDate;
		public DateTime UpdateDate
		{
			get
			{
				return _updateDate;
			}
			set
			{
				_updateDate = value;
			}
		}

        private int _activeFlag;
        public int ActiveFlag
        {
            get
            {
                return _activeFlag;
            }
            set
            {
                _activeFlag = value;
            }
        }

        private string _validationTarget;
        public string ValidationTarget
        {
            get
            {
                return _validationTarget;
            }
            set
            {
                _validationTarget = value;
            }
        }
        #endregion

        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 

        #endregion

        public override int GetHashCode()
        {
            if (Id > 0)
            {
                return Id;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            CubeValidationRule another = obj as CubeValidationRule;
			
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
