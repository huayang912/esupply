using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeRoleDimensionMember : EntityBase
    {
        #region O/R Mapping Properties

        private CubeRole _theCubeRole;
        public CubeRole TheCubeRole
        {
            get
            {
                return _theCubeRole;
            }
            set
            {
                _theCubeRole = value;
            }
        }

        private CubeDimension _theDimension;
        public CubeDimension TheDimension
        {
            get
            {
                return _theDimension;
            }
            set
            {
                _theDimension = value;
            }
        }

        private string _memberId;
        public string MemberId
		{
			get
			{
				return _memberId;
			}
			set
			{
				_memberId = value;
			}
		}
		
		private string _memberName;
		public string MemberName
		{
			get
			{
				return _memberName;
			}
			set
			{
				_memberName = value;
			}
		}
		
		private string _memberValue;
		public string MemberValue
		{
			get
			{
				return _memberValue;
			}
			set
			{
				_memberValue = value;
			}
		}
		
        private int _isVisualtotal;
		public int IsVisualtotal
		{
			get
			{
				return _isVisualtotal;
			}
			set
			{
				_isVisualtotal = value;
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
            CubeRoleDimensionMember another = obj as CubeRoleDimensionMember;
			
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
