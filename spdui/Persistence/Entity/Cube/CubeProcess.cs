using System;
using System.Collections;
using Dndp.Persistence.Entity.Security;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeProcess : EntityBase
    {
        public const string PROCESS_STATUS_WaitingValidate = "Waiting Validate";
        public const string PROCESS_STATUS_ValidatedFailed = "Validate Failed";
        public const string PROCESS_STATUS_ValidatedPassed = "Validate Passed";
        public const string PROCESS_STATUS_ProcessSubmit = "Process Submit";
        public const string PROCESS_STATUS_ProcessStart = "Process Start";        
        public const string PROCESS_STATUS_ProcessCancelled = "Process Cancelled";
        public const string PROCESS_STATUS_ProcessSuccess = "Process Success";
        public const string PROCESS_STATUS_ProcessFailed = "Process Failed";
        public const string PROCESS_STATUS_UpdateRole = "Updating Role";
        public const string PROCESS_STATUS_UpdateRoleCompleted = "Update Role Completed";
        public const string PROCESS_STATUS_UpdateRoleCancelled = "Update Role Cancelled";

        #region O/R Mapping Properties

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
		
		private DateTime _startTime;
		public DateTime StartTime
		{
			get
			{
				return _startTime;
			}
			set
			{
				_startTime = value;
			}
		}
		
		private DateTime _endTime;
		public DateTime EndTime
		{
			get
			{
				return _endTime;
			}
			set
			{
				_endTime = value;
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
		
		private int _errors;
		public int Errors
		{
			get
			{
				return _errors;
			}
			set
			{
				_errors = value;
			}
		}
		
		private int _problems;
		public int Problems
		{
			get
			{
				return _problems;
			}
			set
			{
				_problems = value;
			}
		}
		
		private int _warnings;
		public int Warnings
		{
			get
			{
				return _warnings;
			}
			set
			{
				_warnings = value;
			}
		}
		
		private int _hasReleased;
		public int HasReleased
		{
			get
			{
				return _hasReleased;
			}
			set
			{
				_hasReleased = value;
			}
		}
		
		private int _runPreProcessSQL;
		public int RunPreProcessSQL
		{
			get
			{
				return _runPreProcessSQL;
			}
			set
			{
				_runPreProcessSQL = value;
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

        private User _processUser;
        public User ProcessUser
        {
            get
            {
                return _processUser;
            }
            set
            {
                _processUser = value;
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
        
        #endregion

        #region Non O/R Mapping Properties

        private IList<CubeProcessParameter> _cubeProcessParameterList;
        public IList<CubeProcessParameter> CubeProcessParameterList
        {
            get
            {
                return _cubeProcessParameterList;
            }
            set
            {
                _cubeProcessParameterList = value;
            }
        }

        private IList<CubeProcessValidationResult> _cubeProcessValidationResultList;
        public IList<CubeProcessValidationResult> CubeProcessValidationResultList
        {
            get
            {
                return _cubeProcessValidationResultList;
            }
            set
            {
                _cubeProcessValidationResultList = value;
            }
        }

        public IList<CubeProcessValidationResult> ErrorCubeProcessValidationResultList
        {
            get
            {
                if (CubeProcessValidationResultList != null)
                {
                    IList<CubeProcessValidationResult> list = new List<CubeProcessValidationResult>();
                    foreach (CubeProcessValidationResult result in CubeProcessValidationResultList)
                    {
                        if (result.TheRule.Type.ToUpper().Equals("ERROR"))
                        {
                            list.Add(result);
                        }
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<CubeProcessValidationResult> ProblemCubeProcessValidationResultList
        {
            get
            {
                if (CubeProcessValidationResultList != null)
                {
                    IList<CubeProcessValidationResult> list = new List<CubeProcessValidationResult>();
                    foreach (CubeProcessValidationResult result in CubeProcessValidationResultList)
                    {
                        if (result.TheRule.Type.ToUpper().Equals("PROBLEM"))
                        {
                            list.Add(result);
                        }
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<CubeProcessValidationResult> WarningCubeProcessValidationResultList
        {
            get
            {
                if (CubeProcessValidationResultList != null)
                {
                    IList<CubeProcessValidationResult> list = new List<CubeProcessValidationResult>();
                    foreach (CubeProcessValidationResult result in CubeProcessValidationResultList)
                    {
                        if (result.TheRule.Type.ToUpper().Equals("WARNING"))
                        {
                            list.Add(result);
                        }
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public string CubeProcessValidationResultIds
        {
            get
            {
                if (CubeProcessValidationResultList != null)
                {
                    string ids = "";
                    foreach (CubeProcessValidationResult result in CubeProcessValidationResultList)
                    {
                        ids += "," + result.Id.ToString();
                    }

                    return ids.Trim(',');
                }
                else
                {
                    return string.Empty;
                }
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

        private CubeRelease _theLastestSuccessCubeRelease;
        public CubeRelease TheLastestSuccessCubeRelease
        {
            get
            {
                return _theLastestSuccessCubeRelease;
            }
            set
            {
                _theLastestSuccessCubeRelease = value;
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
            CubeProcess another = obj as CubeProcess;
			
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
