using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeDistributionJobItem : EntityBase
    {
        public const string JOB_ITEM_STATUS_PENDING = "Submit";
        public const string JOB_ITEM_STATUS_SUCCEED = "Succeed";
        public const string JOB_ITEM_STATUS_FAILED = "Failed";
        public const string JOB_ITEM_STATUS_ON_GOING = "On Going";
        public const string JOB_ITEM_STATUS_CREATE_CUBE_FILE_SUCCEED = "Create Cube File Succeed";
        public const string JOB_ITEM_STATUS_CREATE_CUBE_FILE_FAILED = "Create Cube File Failed";
        public const string JOB_ITEM_STATUS_SEND_MAIL_SUCCEED = "Send Mail Succeed";
        public const string JOB_ITEM_STATUS_SEND_MAIL_FAILED = "Send Mail Failed";
        public const string JOB_ITEM_STATUS_PUBLISH_SUCCEED = "Publish Succeed";
        public const string JOB_ITEM_STATUS_PUBLISH_FAILED = "Publish Failed";

        #region O/R Mapping Properties

        private CubeDistributionJob _theJob;
        public CubeDistributionJob TheJob
        {
            get
            {
                return _theJob;
            }
            set
            {
                _theJob = value;
            }
        }

        private CubeUser _theCubeUser;
        public CubeUser TheCubeUser
        {
            get
            {
                return _theCubeUser;
            }
            set
            {
                _theCubeUser = value;
            }
        }

		private string _userName;
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
			}
		}
		
		private string _serverName;
		public string ServerName
		{
			get
			{
				return _serverName;
			}
			set
			{
				_serverName = value;
			}
		}
		
		private string _databaseName;
		public string DatabaseName
		{
			get
			{
				return _databaseName;
			}
			set
			{
				_databaseName = value;
			}
		}
		
		private string _cubeName;
		public string CubeName
		{
			get
			{
				return _cubeName;
			}
			set
			{
				_cubeName = value;
			}
		}
		
		private string _cubeUserName;
		public string CubeUserName
		{
			get
			{
				return _cubeUserName;
			}
			set
			{
				_cubeUserName = value;
			}
		}
		
		private string _email;
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
			}
		}
		
		private string _needSendMail;
		public string NeedSendMail
		{
			get
			{
				return _needSendMail;
			}
			set
			{
				_needSendMail = value;
			}
		}
		
		private string _portalUserName;
		public string PortalUserName
		{
			get
			{
				return _portalUserName;
			}
			set
			{
				_portalUserName = value;
			}
		}
		
		private string _userPortalSite;
		public string UserPortalSite
		{
			get
			{
				return _userPortalSite;
			}
			set
			{
				_userPortalSite = value;
			}
		}
		
		private string _userPortalFolder;
		public string UserPortalFolder
		{
			get
			{
				return _userPortalFolder;
			}
			set
			{
				_userPortalFolder = value;
			}
		}
		
		private string _needPublishToPortal;
		public string NeedPublishToPortal
		{
			get
			{
				return _needPublishToPortal;
			}
			set
			{
				_needPublishToPortal = value;
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
		
		private string _statusDescription;
		public string StatusDescription
		{
			get
			{
				return _statusDescription;
			}
			set
			{
				_statusDescription = value;
			}
		}

        private string _portalFolderReadUsers;
        public string PortalFolderReadUsers
        {
            get
            {
                return _portalFolderReadUsers;
            }
            set
            {
                _portalFolderReadUsers = value;
            }
        }

        private string _portalFolderFullControlUsers;
        public string PortalFolderFullControlUsers
        {
            get
            {
                return _portalFolderFullControlUsers;
            }
            set
            {
                _portalFolderFullControlUsers = value;
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
            CubeDistributionJobItem another = obj as CubeDistributionJobItem;
			
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
