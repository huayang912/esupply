using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
using Dndp.Utility;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube.Impl
{
    [Transactional]
    public class CubeMgr : SessionBase, ICubeMgr
    {
        private ICubeDao entityDao;
        private ICubeValidationRuleDao ruleDao;
        private ICubeParameterDao paraDao;
        private ICubeDefinedParameterDao cubeParaDao;
        private ICubeOperatorDao operatorDao;
        private ICubeDimensionDao dimensionDao;
        private ICubeWarmMDXDao MDXDao;
        private ICubeProcessDao processDao;
        private ICubeDistributionJobDao distributionDao;
        private ICubeMeasureDao measureDao;
        
        public CubeMgr(ICubeDao entityDao, 
            ICubeValidationRuleDao ruleDao,
            ICubeParameterDao paraDao,
            ICubeDefinedParameterDao cubeParaDao,
            ICubeOperatorDao operatorDao,
            ICubeDimensionDao dimensionDao,
            ICubeWarmMDXDao MDXDao,
            ICubeProcessDao processDao,
            ICubeDistributionJobDao distributionDao,
            ICubeMeasureDao measureDao)
        {
            this.entityDao = entityDao;
            this.ruleDao = ruleDao;
            this.paraDao = paraDao;
            this.cubeParaDao = cubeParaDao;
            this.operatorDao = operatorDao;
            this.dimensionDao = dimensionDao;
            this.MDXDao = MDXDao;
            this.processDao = processDao;
            this.distributionDao = distributionDao;
            this.measureDao = measureDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCube(CubeDefinition entity)
        {           			
            entityDao.CreateCube(entity);

            //Create CubeDefinedParameter
            CreateCubeDefinedParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeDefinition LoadCube(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return entityDao.LoadCube(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCube(CubeDefinition entity)
        {
            entityDao.UpdateCube(entity);

            //Delete CubeDefinedParameter
            cubeParaDao.DeleteCubeDefinedParameterByCubeId(entity.Id);

            //Create CubeDefinedParameter
            CreateCubeDefinedParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCube(int id)
        {
            ////Delete CubeValidationRule
            //ruleDao.DeleteCubeValidationRuleByCubeId(id);

            ////Delete CubeDefinedParameter
            //cubeParaDao.DeleteCubeDefinedParameterByCubeId(id);

            ////Delete CubeOperator
            //operatorDao.DeleteCubeOperatorByCubeId(id);

            ////Delete CubeDimension
            //dimensionDao.DeleteCubeDimensionByCubeId(id);

            ////Delete CubeMDX
            //MDXDao.DeleteCubeWarmMDXByCubeId(id);

            //Delete Cube
            CubeDefinition cube  = entityDao.LoadCube(id);
            cube.ActiveFlag = 0;
            entityDao.UpdateCube(cube);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCube(CubeDefinition entity)
        {
            //Delete CubeValidationRule
            //ruleDao.DeleteCubeValidationRuleByCubeId(entity.Id);

            ////Delete CubeDefinedParameter
            //cubeParaDao.DeleteCubeDefinedParameterByCubeId(entity.Id);

            ////Delete CubeOperator
            //operatorDao.DeleteCubeOperatorByCubeId(entity.Id);

            ////Delete CubeDimension
            //dimensionDao.DeleteCubeDimensionByCubeId(entity.Id);

            ////Delete CubeMDX
            //MDXDao.DeleteCubeWarmMDXByCubeId(entity.Id);

            //Delete Cube
            entity.ActiveFlag = 0;
            entityDao.UpdateCube(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCube(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            foreach (int id in idList)
            {
                //Delete CubeValidationRule
                //ruleDao.DeleteCubeValidationRuleByCubeId(id);

                ////Delete CubeDefinedParameter
                //cubeParaDao.DeleteCubeDefinedParameterByCubeId(id);

                ////Delete CubeOperator
                //operatorDao.DeleteCubeOperatorByCubeId(id);

                ////Delete CubeDimension
                //dimensionDao.DeleteCubeDimensionByCubeId(id);

                ////Delete CubeMDX
                //MDXDao.DeleteCubeWarmMDXByCubeId(id);

                CubeDefinition cube = entityDao.LoadCube(id);
                cube.ActiveFlag = 0;
                entityDao.UpdateCube(cube);
            }

            //entityDao.DeleteCube(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCube(IList<CubeDefinition> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            foreach (CubeDefinition cube in entityList)
            {
                ////Delete CubeValidationRule
                //ruleDao.DeleteCubeValidationRuleByCubeId(cube.Id);

                ////Delete CubeDefinedParameter
                //cubeParaDao.DeleteCubeDefinedParameterByCubeId(cube.Id);

                ////Delete CubeOperator
                //operatorDao.DeleteCubeOperatorByCubeId(cube.Id);

                ////Delete CubeDimension
                //dimensionDao.DeleteCubeDimensionByCubeId(cube.Id);

                ////Delete CubeMDX
                //MDXDao.DeleteCubeWarmMDXByCubeId(cube.Id);
                cube.ActiveFlag = 0;
                entityDao.UpdateCube(cube);
            }

            //entityDao.DeleteCube(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeDefinition> LoadAllActiveCube()
        {
            return entityDao.LoadAllActiveCube();
        }

        [Transaction(TransactionMode.NotSupported)]
        public CubeDefinition FindCubeWtihFullInformationById(int id)
        {
            CubeDefinition cube = entityDao.LoadCube(id);
            cube.CubeValidationRuleList = ruleDao.FindCubeValidationRuleWithCubeId(id);
            cube.CubeOperatorList = operatorDao.FindOperatorByCubeId(id);
            cube.CubeDimensionList = dimensionDao.FindDimensionByCubeId(id);
            cube.CubeWarmMDXList = MDXDao.FindCubeWarmMDXByCubeId(id);
            cube.CubeDefinedParameterList = cubeParaDao.FindCubeDefinedParameterByCubeId(id);
            cube.CubeMeasureList = measureDao.FindMeasureByCubeId(id);

            return cube;
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeDefinition> FindAllCubeForCubeProcessByUserId(int userId)
        {
            IList<CubeDefinition> cubeList = entityDao.FindCubeByUserIdAndAllowType(userId, "Process");
            IList<CubeProcess> processList = processDao.FindAllLastestCubeProcess(userId);

            if (cubeList != null && cubeList.Count > 0
                && processList != null && processList.Count > 0)
            {
                foreach (CubeDefinition cube in cubeList)
                {
                    foreach (CubeProcess process in processList)
                    {
                        if (cube.Id == process.TheCube.Id)
                        {
                            cube.TheLastestCubeProcess = process;
                            break;
                        }
                    }
                }
            }

            return cubeList;
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeDefinition> FindAllCubeForCubeDistribution()
        {
            IList<CubeDefinition> cubeList = entityDao.LoadAllActiveCube();

            IList<CubeDistributionJob> jobList = distributionDao.FindAllLastestCubeDistributionJob();

            if (cubeList != null && cubeList.Count > 0
                && jobList != null && jobList.Count > 0)
            {
                foreach (CubeDefinition cube in cubeList)
                {
                    foreach (CubeDistributionJob job in jobList)
                    {
                        if (cube.Id == job.TheCube.Id)
                        {
                            cube.TheLastestCubeDistributionJob = job;
                            break;
                        }
                    }
                }
            }

            return cubeList;
        }

        private IList<CubeParameter>  GetCubeUsedParameter(string content)
        {
            IList<CubeParameter> paraList = paraDao.LoadAllActiveCubeParameter();
            IList<CubeParameter> resultList = new List<CubeParameter>();
            if (paraList != null)
            {
                foreach (CubeParameter para in paraList)
                {

                    if (content.Contains(DataParameterHelper.PARAMETER_PREFIX + para.Name + DataParameterHelper.PARAMETER_POSTFIX))
                    {
                        resultList.Add(para);
                    }
                }
            }
            return resultList;
        }

        private void CreateCubeDefinedParameter(CubeDefinition entity)
        {
            IList<CubeParameter> list = GetCubeUsedParameter(entity.PreProcessSQL);
            foreach (CubeParameter para in list)
            {
                CubeDefinedParameter cubeDefinedParameter = new CubeDefinedParameter();
                cubeDefinedParameter.TheCube = entity;
                cubeDefinedParameter.TheParameter = para;
                cubeParaDao.CreateCubeDefinedParameter(cubeDefinedParameter);
            }
        }

        #endregion Customized Methods
    }
}
