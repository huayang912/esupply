using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeWarmMDX : EntityBase
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

		private int _sequenceNo;
		public int SequenceNo
		{
			get
			{
				return _sequenceNo;
			}
			set
			{
				_sequenceNo = value;
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

		private string _mDXStatement;
		public string MDXStatement
		{
			get
			{
				return _mDXStatement;
			}
			set
			{
				_mDXStatement = value;
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
            CubeWarmMDX another = obj as CubeWarmMDX;
			
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
