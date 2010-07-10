using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Distribution
{
    [Serializable]
    public class DistributionUser : EntityBase
    {
        #region O/R Mapping Properties
		
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
		
		private string _domainAccount;
		public string DomainAccount
		{
			get
			{
				return _domainAccount;
			}
			set
			{
				_domainAccount = value;
			}
		}
		
		private int _isReportUser;
		public int IsReportUser
		{
			get
			{
                return _isReportUser;
			}
			set
			{
                _isReportUser = value;
			}
		}
		
		private int _isOnlineCubeUser;
		public int IsOnlineCubeUser
		{
			get
			{
                return _isOnlineCubeUser;
			}
			set
			{
                _isOnlineCubeUser = value;
			}
		}
		
		private int _isOfflineCubeUser;
		public int IsOfflineCubeUser
		{
			get
			{
                return _isOfflineCubeUser;
			}
			set
			{
                _isOfflineCubeUser = value;
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
            DistributionUser another = obj as DistributionUser;
			
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
