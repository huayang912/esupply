using System;
using System.Collections;
using Dndp.Persistence.Entity.Distribution;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportUser : EntityBase
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

        private string _portalSite;
        public string PortalSite
        {
            get
            {
                return _portalSite;
            }
            set
            {
                _portalSite = value;
            }
        }

        private string _portalDocumentLibrary;
        public string PortalDocumentLibrary
        {
            get
            {
                return _portalDocumentLibrary;
            }
            set
            {
                _portalDocumentLibrary = value;
            }
        }

        private string _portalReadUserList;
        public string PortalReadUserList
        {
            get
            {
                return _portalReadUserList;
            }
            set
            {
                _portalReadUserList = value;
            }
        }

        private string _portalFullControlUserList;
        public string PortalFullControlUserList
        {
            get
            {
                return _portalFullControlUserList;
            }
            set
            {
                _portalFullControlUserList = value;
            }
        }     
   
        private DistributionUser _theUser;
        public DistributionUser TheUser
        {
            get
            {
                return _theUser;
            }
            set
            {
                _theUser = value;
            }
        }
        #endregion

        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 
        private IList _reportList;
        public IList ReportList
        {
            get
            {
                return _reportList;
            }
            set
            {
                _reportList = value;
            }
        }

        private IList _reportParameterList;
        public IList ReportParameterList
        {
            get
            {
                return _reportParameterList;
            }
            set
            {
                _reportParameterList = value;
            }
        }

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
            ReportUser another = obj as ReportUser;
			
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