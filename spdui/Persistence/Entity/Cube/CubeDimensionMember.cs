using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeDimensionMember
    {
        private CubeDimension _theCubeDimension;
        public CubeDimension TheCubeDimension
        {
            get
            {
                return _theCubeDimension;
            }
            set
            {
                _theCubeDimension = value;
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
    }
	
}
