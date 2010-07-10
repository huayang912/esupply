using System;
using System.Collections;
using Dndp.Persistence.Entity.Security;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeDefinition : EntityBase
    {
        #region O/R Mapping Properties

        private string _processCubeName;
		public string ProcessCubeName
		{
			get
			{
                return _processCubeName;
			}
			set
			{
                _processCubeName = value;
			}
		}

        private string _releaseCubeName;
        public string ReleaseCubeName
        {
            get
            {
                return _releaseCubeName;
            }
            set
            {
                _releaseCubeName = value;
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
		
		private string _processServerAddr;
		public string ProcessServerAddr
		{
			get
			{
				return _processServerAddr;
			}
			set
			{
				_processServerAddr = value;
			}
		}
		
		private string _processDatabaseName;
		public string ProcessDatabaseName
		{
			get
			{
				return _processDatabaseName;
			}
			set
			{
				_processDatabaseName = value;
			}
		}

        private string _processCubeBackupFolder;
        public string ProcessCubeBackupFolder
        {
            get
            {
                return _processCubeBackupFolder;
            }
            set
            {
                _processCubeBackupFolder = value;
            }
        }
		
		private string _releaseServerAddr;
		public string ReleaseServerAddr
		{
			get
			{
				return _releaseServerAddr;
			}
			set
			{
				_releaseServerAddr = value;
			}
		}
		
		private string _releaseDatabaseName;
		public string ReleaseDatabaseName
		{
			get
			{
				return _releaseDatabaseName;
			}
			set
			{
				_releaseDatabaseName = value;
			}
		}
		
		private string _preProcessSQL;
		public string PreProcessSQL
		{
			get
			{
				return _preProcessSQL;
			}
			set
			{
				_preProcessSQL = value;
			}
		}
		
		private string _postProcessSQL;
		public string PostProcessSQL
		{
			get
			{
				return _postProcessSQL;
			}
			set
			{
				_postProcessSQL = value;
			}
		}

        private string _preReleaseSQL;
        public string PreReleaseSQL
        {
            get
            {
                return _preReleaseSQL;
            }
            set
            {
                _preReleaseSQL = value;
            }
        }

        private string _postReleaseSQL;
        public string PostReleaseSQL
        {
            get
            {
                return _postReleaseSQL;
            }
            set
            {
                _postReleaseSQL = value;
            }
        }

        //Modified by vincent at 2007-11-13 begin
        private string _preUpdateRoleSQL;
        public string PreUpdateRoleSQL
        {
            get
            {
                return _preUpdateRoleSQL;
            }
            set
            {
                _preUpdateRoleSQL = value;
            }
        }

        private string _postUpdateRoleSQL;
        public string PostUpdateRoleSQL
        {
            get
            {
                return _postUpdateRoleSQL;
            }
            set
            {
                _postUpdateRoleSQL = value;
            }
        }

        private string _preUpdateDescriptionSQL;
        public string PreUpdateDescriptionSQL
        {
            get
            {
                return _preUpdateDescriptionSQL;
            }
            set
            {
                _preUpdateDescriptionSQL = value;
            }
        }

        private string _postUpdateDescriptionSQL;
        public string PostUpdateDescriptionSQL
        {
            get
            {
                return _postUpdateDescriptionSQL;
            }
            set
            {
                _postUpdateDescriptionSQL = value;
            }
        }
        //Modified by vincent at 2007-11-13 end

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
		
        
        #endregion

        #region Non O/R Mapping Properties

        private IList<CubeValidationRule> _cubeValidationRuleList;
        public IList<CubeValidationRule> CubeValidationRuleList
        {
            get
            {
                return _cubeValidationRuleList;
            }
            set
            {
                _cubeValidationRuleList = value;
            }
        }

        private IList<CubeDefinedParameter> _cubeDefinedParameterList;
        public IList<CubeDefinedParameter> CubeDefinedParameterList
        {
            get
            {
                return _cubeDefinedParameterList;
            }
            set
            {
                _cubeDefinedParameterList = value;
            }
        }

        private IList<CubeOperator> _cubeOperatorList;
        public IList<CubeOperator> CubeOperatorList
        {
            get
            {
                return _cubeOperatorList;
            }
            set
            {
                _cubeOperatorList = value;
            }
        }

        private IList<CubeMeasure> _cubeMeasureList;
        public IList<CubeMeasure> CubeMeasureList
        {
            get
            {
                return _cubeMeasureList;
            }
            set
            {
                _cubeMeasureList = value;
            }
        }

        private IList<CubeDimension> _cubeDimensionList;
        public IList<CubeDimension> CubeDimensionList
        {
            get
            {
                return _cubeDimensionList;
            }
            set
            {
                _cubeDimensionList = value;
            }
        }

        private IList<CubeWarmMDX> _cubeWarmMDXList;
        public IList<CubeWarmMDX> CubeWarmMDXList
        {
            get
            {
                return _cubeWarmMDXList;
            }
            set
            {
                _cubeWarmMDXList = value;
            }
        }

        private CubeProcess _theLastestCubeProcess;
        public CubeProcess TheLastestCubeProcess
        {
            get
            {
                return _theLastestCubeProcess;
            }
            set
            {
                _theLastestCubeProcess = value;
            }
        }

        private CubeRelease _theLastestCubeRelease;
        public CubeRelease TheLastestCubeRelease
        {
            get
            {
                return _theLastestCubeRelease;
            }
            set
            {
                _theLastestCubeRelease = value;
            }
        }

        private CubeDistributionJob _theLastestCubeDistributionJob;
        public CubeDistributionJob TheLastestCubeDistributionJob
        {
            get
            {
                return _theLastestCubeDistributionJob;
            }
            set
            {
                _theLastestCubeDistributionJob = value;
            }
        }

        public int _errorRuleCount;
        public int ErrorRuleCount
        {
            get
            {
                return GetRuleCountByType("Error");
            }
        }

        public int _problemRuleCount;
        public int ProblemRuleCount
        {
            get
            {
                return GetRuleCountByType("Problem");
            }
        }

        public int _warningRuleCount;
        public int WarningRuleCount
        {
            get
            {
                return GetRuleCountByType("Warning");
            }
        }

        private int GetRuleCountByType(string type)
        {
            if (CubeValidationRuleList != null && CubeValidationRuleList.Count > 0)
            {
                int count = 0;
                foreach (CubeValidationRule rule in CubeValidationRuleList)
                {
                    if (rule.Type.Trim().ToUpper().Equals(type.Trim().ToUpper()))
                    {
                        count++;
                    }
                }
                return count;
            }
            else
            {
                return 0;
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
            CubeDefinition another = obj as CubeDefinition;
			
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
