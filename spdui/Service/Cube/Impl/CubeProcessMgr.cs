using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Security;
using Dndp.Persistence.Dao;
using Dndp.Utility;
using System.Data;
using Dndp.Utility.CSV;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube.Impl
{
    [Transactional]
    public class CubeProcessMgr : SessionBase, ICubeProcessMgr
    {
        private ICubeProcessDao processDao;
        private ICubeProcessParameterDao parameterDao;
        private ICubeProcessValidationResultDao validationResultDao;
        private ICubeDefinedParameterDao cubeDefinedParameterDao;
        private ICubeParameterDao cubeParameterDao;
        private ICubeReleaseDao releaseDao;
        private SqlHelperDao sqlHelperDao;
        private string DEFAULT_DW_DBString = "SPDW.dbo.";

        public CubeProcessMgr(ICubeProcessDao processDao,
                              ICubeProcessParameterDao parameterDao,
                              ICubeProcessValidationResultDao validationResultDao,
                              ICubeDefinedParameterDao cubeDefinedParameterDao,
                              ICubeParameterDao cubeParameterDao,
                              ICubeReleaseDao releaseDao,
                              SqlHelperDao sqlHelperDao)
        {
            this.processDao = processDao;
            this.parameterDao = parameterDao;
            this.validationResultDao = validationResultDao;
            this.cubeDefinedParameterDao = cubeDefinedParameterDao;
            this.cubeParameterDao = cubeParameterDao;
            this.releaseDao = releaseDao;
            this.sqlHelperDao = sqlHelperDao;
        }

        private string dwDBString;
        public string DWDBString
        {
            get
            {
                if (this.dwDBString == null)
                {
                    return DEFAULT_DW_DBString;
                }
                else
                {
                    return this.dwDBString;
                }
            }
            set
            {
                this.dwDBString = value;
            }
        }

        #region CubeProcess Related
        [Transaction(TransactionMode.Requires)]
        public void CreateCubeProcess(CubeProcess entity)
        {
            //TODO: Add other code here.
			
            processDao.CreateCubeProcess(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeProcess LoadCubeProcess(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return processDao.LoadCubeProcess(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeProcess(CubeProcess entity)
        {
        	//TODO: Add other code here.
            //CubeProcess newEntity = processDao.LoadCubeProcess(entity.Id);

            //if (newEntity.Status == CubeProcess.PROCESS_STATUS_ProcessSuccess)
            //{
            //    throw new ArgumentException("The Cube Process is already run successed, can't save");
            //}
            //else if (newEntity.Status == CubeProcess.PROCESS_STATUS_ProcessStart)
            //{
            //    throw new ArgumentException("The Cube Process is already running, can't save");
            //}
            //else if (newEntity.Status == CubeProcess.PROCESS_STATUS_ProcessSubmit
            //    && entity.Status != CubeProcess.PROCESS_STATUS_ProcessCancelled)
            //{
            //    throw new ArgumentException("The Cube Process is already submitted, can't save");
            //}

            processDao.UpdateCubeProcess(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcess(int id)
        {

            //CubeProcess newEntity = processDao.LoadCubeProcess(id);

            //if (newEntity.Status == CubeProcess.PROCESS_STATUS_ProcessSuccess)
            //{
            //    throw new ArgumentException("The Cube Process is already run successed, can't delete");
            //}
            //else if (newEntity.Status == CubeProcess.PROCESS_STATUS_ProcessStart)
            //{
            //    throw new ArgumentException("The Cube Process is already running, can't delete");
            //}
            //else if (newEntity.Status == CubeProcess.PROCESS_STATUS_ProcessSubmit)
            //{
            //    throw new ArgumentException("The Cube Process is already submitted, can't delete");
            //}

            parameterDao.DeleteCubeProcessParameterByProcessId(id);
            validationResultDao.DeleteCubeProcessValidationResultByProcessId(id);
            processDao.DeleteCubeProcess(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcess(CubeProcess entity)
        {
            DeleteCubeProcess(entity.Id);        
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcess(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            foreach(int id in idList)
            {
                this.DeleteCubeProcess(id);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcess(IList<CubeProcess> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            foreach (CubeProcess entity in entityList)
            {
                this.DeleteCubeProcess(entity);
            }
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeProcess> FindAllLastestCubeProcess(int userId)
        {
            return processDao.FindAllLastestCubeProcess(userId);
        }

        [Transaction(TransactionMode.NotSupported)]
        public CubeProcess FindLastestCubeProcessByCubeId(int cubeId, int userId)
        {
            return processDao.FindLastestCubeProcessByCubeId(cubeId, userId);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeProcess> FindAllCubeProcessByCubeId(int cubeId, int userId)
        {
            return processDao.FindAllCubeProcessByCubeId(cubeId, userId);
        }

        [Transaction(TransactionMode.Requires)]
        public CubeProcess CreateNewCubeProcess(CubeDefinition cube, User user, Hashtable parameter, string cubeProcessDescription)
        {
            //create cube process
            CubeProcess process = new CubeProcess();
            process.Description = cubeProcessDescription;
            process.TheCube = cube;
            process.ProcessUser = user;            
            if (cube.ErrorRuleCount > 0)
            {
                process.Status = CubeProcess.PROCESS_STATUS_WaitingValidate;
            }
            else
            {
                process.Status = CubeProcess.PROCESS_STATUS_ValidatedPassed;
            }
            process.Errors = 0;
            process.Problems = 0;
            process.Warnings = 0;
            process.HasReleased = 0;
            process.RunPreProcessSQL = 1;
            process.StartTime = DateTime.Now;
            process.EndTime = DateTime.Now;
            process.CreateUser = user;
            process.CreateDate = DateTime.Now;

            processDao.CreateCubeProcess(process);

            //create Cube Process Parameter
            if (parameter != null && parameter.Count > 0)
            {
                 process.CubeProcessParameterList = new List<CubeProcessParameter>();

                IDictionaryEnumerator enumerator = parameter.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    CubeParameter cubeParameter = cubeParameterDao.LoadCubeParameter(int.Parse(enumerator.Key.ToString()));

                    CubeProcessParameter cubeProcessParameter = new CubeProcessParameter();
                    cubeProcessParameter.TheParameter = cubeParameter;
                    cubeProcessParameter.TheProcess = process;
                    cubeProcessParameter.Value = enumerator.Value.ToString();

                    parameterDao.CreateCubeProcessParameter(cubeProcessParameter);

                    process.CubeProcessParameterList.Add(cubeProcessParameter);
                }
            }

            //create Cube Process Validation Result 
            if (cube.CubeValidationRuleList != null && cube.CubeValidationRuleList.Count > 0)
            {
                process.CubeProcessValidationResultList = new List<CubeProcessValidationResult>();
                foreach (CubeValidationRule rule in cube.CubeValidationRuleList)
                {
                    if (rule.ValidationTarget.Equals("SPDW"))
                    {
                        CubeProcessValidationResult result = new CubeProcessValidationResult();
                        result.TheProcess = process;
                        result.TheRule = rule;
                        result.Status = CubeProcessValidationResult.CubeProcessValidationResult_Status_Pending;
                        result.FailedRowCount = 0;

                        validationResultDao.CreateCubeProcessValidationResult(result); 

                        process.CubeProcessValidationResultList.Add(result);
                    }
                }
            }

            return process;
        }

        [Transaction(TransactionMode.NotSupported)]
        public CubeProcess FindCubeProcessWithAllInfoById(int id)
        {
            CubeProcess process = processDao.LoadCubeProcess(id);

            process.CubeProcessParameterList = parameterDao.FindCubeProcessParameterByProcessId(id);
            process.CubeProcessValidationResultList = validationResultDao.FindCubeProcessValidationResultByProcessId(id, "SPDW");

            return process;
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeProcess> FindAllCubeForCubeReleaseByUserId(int userId)
        {
            IList<CubeProcess> processList = processDao.FindAllLastestSuccessCubeProcess(userId);
            IList<CubeRelease> releaseList = releaseDao.FindAllLastestCubeRelease(userId);
            IList<CubeRelease> successReleaseList = releaseDao.FindAllSuccessLastestCubeRelease(userId);

            if (processList != null && processList.Count > 0
                && releaseList != null && releaseList.Count > 0)
            {
                // Modified by vincent at 2007-11-09 begin
                /*
                foreach (CubeProcess process in processList)
                {
                    foreach (CubeRelease release in releaseList)
                    {
                        if (process.Id == release.TheProcess.Id)
                        {
                            process.TheLastestCubeRelease = release;
                        }
                    }

                    if (successReleaseList != null && successReleaseList.Count > 0)
                    {
                        foreach (CubeRelease release in successReleaseList)
                        {
                            if (process.Id == release.TheProcess.Id)
                            {
                                process.TheLastestSuccessCubeRelease = release;
                            }
                            
                        }
                    }
                }
                */

                foreach (CubeProcess process in processList)
                {
                    foreach (CubeRelease release in releaseList)
                    {
                        if (process.TheCube.Id == release.TheProcess.TheCube.Id)
                        {
                            process.TheLastestCubeRelease = release;
                        }
                    }
                }
                // Modified by vincent at 2007-11-09 end
            }

            return processList;
        }
        #endregion CubeProcess Related

        #region CubeProcessParameter related

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeProcessParameter(CubeProcessParameter entity)
        {
            //TODO: Add other code here.

            parameterDao.CreateCubeProcessParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeProcessParameter LoadCubeProcessParameter(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return parameterDao.LoadCubeProcessParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeProcessParameter(CubeProcessParameter entity)
        {
            //TODO: Add other code here.
            parameterDao.UpdateCubeProcessParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessParameter(int id)
        {
            parameterDao.DeleteCubeProcessParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessParameter(CubeProcessParameter entity)
        {
            parameterDao.DeleteCubeProcessParameter(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessParameter(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            parameterDao.DeleteCubeProcessParameter(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessParameter(IList<CubeProcessParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            parameterDao.DeleteCubeProcessParameter(entityList);
        }

        #endregion CubeProcessParameter related

        #region CubeProcessValidationResult Related

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeProcessValidationResult(CubeProcessValidationResult entity)
        {
            //TODO: Add other code here.

            validationResultDao.CreateCubeProcessValidationResult(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeProcessValidationResult LoadCubeProcessValidationResult(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return validationResultDao.LoadCubeProcessValidationResult(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeProcessValidationResult(CubeProcessValidationResult entity)
        {
            //TODO: Add other code here.
            validationResultDao.UpdateCubeProcessValidationResult(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessValidationResult(int id)
        {
            validationResultDao.DeleteCubeProcessValidationResult(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessValidationResult(CubeProcessValidationResult entity)
        {
            validationResultDao.DeleteCubeProcessValidationResult(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessValidationResult(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            validationResultDao.DeleteCubeProcessValidationResult(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeProcessValidationResult(IList<CubeProcessValidationResult> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            validationResultDao.DeleteCubeProcessValidationResult(entityList);
        }


        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeProcessValidationResult> FindCubeProcessValidationResultByIds(string validationIds)
        {
           return  validationResultDao.FindCubeProcessValidationResultByIds(validationIds);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeProcessValidationResult> FindCubeProcessValidationResultByProcessId(int processId, string validationTarget)
        {
            return validationResultDao.FindCubeProcessValidationResultByProcessId(processId, validationTarget);
        }

        [Transaction(TransactionMode.Requires)]
        public CubeProcessValidationResult ValidateCubeProcessRule(int validateResultId, IList<CubeProcessParameter> processParameterList)
        {
            CubeProcessValidationResult vr = validationResultDao.LoadCubeProcessValidationResult(validateResultId);
            string rule = vr.TheRule.Content;

            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(rule, processParameterList);

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();
            int failRowCount = 0;
            string orgStatus = vr.Status;
            while (dataReader.Read())
            {
                failRowCount++;
            }
            if (failRowCount > 0)
            {
                vr.Status = "Failed";
            }
            else
            {
                vr.Status = "Passed";
            }
            vr.FailedRowCount = failRowCount;
            validationResultDao.UpdateCubeProcessValidationResult(vr);

            RefreshValidateResultCount(vr.TheProcess);
            return vr;
        }

        [Transaction(TransactionMode.NotSupported)]
        public void DownloadCubeProcessValidateResult(string rule, IList<CubeProcessParameter> processParameterList, CSVWriter csvWriter)
        {
            
            //Update Field Content in the SQL Rule
            rule = UpdateValidationSQLContent(rule, processParameterList); ;

            DataSet dataSet = sqlHelperDao.ExecuteDataset(rule);
            DataTableReader dataReader = dataSet.CreateDataReader();

            //write csv header
            string[] header = new string[dataReader.FieldCount];
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                header[i] = dataReader.GetName(i);
            }
            csvWriter.Write(header);
            csvWriter.WriteNewLine();

            //write csv content
            while (dataReader.Read())
            {
                object[] fields = new object[dataReader.FieldCount];
                dataReader.GetValues(fields);
                string[] strFields = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    if (fields[i] != null)
                    {
                        strFields[i] = fields[i].ToString();
                    }
                    else
                    {
                        strFields[i] = "";
                    }
                }
                csvWriter.Write(strFields);
                csvWriter.WriteNewLine();
            }
        }
        #endregion CubeProcessValidationResult Related

        #region private Methods

        private string UpdateValidationSQLContent(string rule, IList<CubeProcessParameter> processParameterList)
        {           
            //rule = rule.Replace("<$Category$>", vr.TheDataSourceUpload.TheDataSourceCategory.Name.ToString());
            rule = rule.Replace("<$DWDBString$>", this.DWDBString);

            if (processParameterList != null && processParameterList.Count > 0)
            {
                foreach (CubeProcessParameter processParameter in processParameterList)
                {
                    string parameterName = DataParameterHelper.PARAMETER_PREFIX + processParameter.TheParameter.Name.Trim() + DataParameterHelper.PARAMETER_POSTFIX;
                    string parameterValue = processParameter.Value;

                    rule = rule.Replace(parameterName, parameterValue);
                }
            }
            return rule;
        }

        [Transaction(TransactionMode.Unspecified)]
        private void RefreshValidateResultCount(CubeProcess TheCubeProcess)
        {
            TheCubeProcess.Errors = 0;
            TheCubeProcess.Problems = 0;
            TheCubeProcess.Warnings = 0;
            TheCubeProcess.CubeProcessValidationResultList = validationResultDao.FindCubeProcessValidationResultByProcessId(TheCubeProcess.Id, "SPDW");
            if (TheCubeProcess.CubeProcessValidationResultList != null)
            {
                IList<CubeProcessValidationResult> list = new List<CubeProcessValidationResult>();
                int pendingErrorRule = 0;
                foreach (CubeProcessValidationResult vr in TheCubeProcess.CubeProcessValidationResultList)
                {
                    if (vr.TheRule.Type == "ERROR" && vr.Status == CubeProcessValidationResult.CubeProcessValidationResult_Status_Failed)
                    {
                        TheCubeProcess.Errors = TheCubeProcess.Errors + 1;
                    }
                    
                    if (vr.TheRule.Type == "ERROR" && vr.Status == CubeProcessValidationResult.CubeProcessValidationResult_Status_Pending)
                    {
                        pendingErrorRule++;
                    }

                    if (vr.TheRule.Type == "PROBLEM" && vr.Status == CubeProcessValidationResult.CubeProcessValidationResult_Status_Failed)
                    {
                        TheCubeProcess.Problems = TheCubeProcess.Problems + 1;
                    }
                    if (vr.TheRule.Type == "WARNING" && vr.Status == CubeProcessValidationResult.CubeProcessValidationResult_Status_Failed)
                    {
                        TheCubeProcess.Warnings = TheCubeProcess.Warnings + 1;
                    }
                }

                if (TheCubeProcess.Errors > 0)
                {
                    TheCubeProcess.Status = CubeProcess.PROCESS_STATUS_ValidatedFailed;
                }
                else if (pendingErrorRule > 0)
                {
                    TheCubeProcess.Status = CubeProcess.PROCESS_STATUS_WaitingValidate;
                }
                else
                {
                    TheCubeProcess.Status = CubeProcess.PROCESS_STATUS_ValidatedPassed;
                }
            }
        }

        #endregion private Methods
    }
}
