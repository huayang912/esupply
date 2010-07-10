using System;
using System.Collections;
using Dndp.Persistence.Entity.Security;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{   
    [Serializable]
    public class CubeRelease : EntityBase
    {
        public const string RELEASE_STATUS_Success = "Success";
        public const string RELEASE_STATUS_Failed = "Failed";
        public const string RELEASE_STATUS_Rollback = "Rollback";        
        public const string RELEASE_STATUS_Running = "Running";

        #region O/R Mapping Properties
		
		private DateTime _releaseDate;
		public DateTime ReleaseDate
		{
			get
			{
				return _releaseDate;
			}
			set
			{
				_releaseDate = value;
			}
		}
		
		private int _needWarmCache;
		public int NeedWarmCache
		{
			get
			{
				return _needWarmCache;
			}
			set
			{
				_needWarmCache = value;
			}
		}

        private CubeProcess _theProcess;
        public CubeProcess TheProcess
        {
            get
            {
                return _theProcess;
            }
            set
            {
                _theProcess = value;
            }
        }
		
		private User _releaseUser;
        public User ReleaseUser
		{
			get
			{
				return _releaseUser;
			}
			set
			{
				_releaseUser = value;
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

        private string _backupFile;
        public string BackupFile
        {
            get
            {
                return _backupFile;
            }
            set
            {
                _backupFile = value;
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
            CubeRelease another = obj as CubeRelease;
			
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
