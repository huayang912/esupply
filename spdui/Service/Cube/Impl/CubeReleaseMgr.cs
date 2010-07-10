using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
using SPCubeUtility;
using Dndp.Utility;
using Dndp.Service.Property;
using Dndp.Persistence.Entity.Security;
using Dndp.Persistence.Dao;
using System.Data;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube.Impl
{
    [Transactional]
    public class CubeReleaseMgr : SessionBase, ICubeReleaseMgr
    {
        private ICubeReleaseDao releaseDao;
        private SqlHelperDao sqlHelpDao;
        private ICubeRoleDao roleDao;
        private ICubeAuthorizationMgr authMgr;
        private ICubeDao cubeDao;
        private IPropertyMgr propertyMgr;

        public CubeReleaseMgr(ICubeReleaseDao releaseDao, SqlHelperDao sqlHelpDao, ICubeRoleDao roleDao, ICubeAuthorizationMgr authMgr, ICubeDao cubeDao, IPropertyMgr propertyMgr)
        {
            this.releaseDao = releaseDao;
            this.sqlHelpDao = sqlHelpDao;
            this.roleDao = roleDao;
            this.authMgr = authMgr;
            this.cubeDao = cubeDao;
            this.propertyMgr = propertyMgr;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeRelease(CubeRelease entity)
        {
            //TODO: Add other code here.
			
            releaseDao.CreateCubeRelease(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeRelease LoadCubeRelease(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return releaseDao.LoadCubeRelease(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeRelease(CubeRelease entity)
        {
        	//TODO: Add other code here.
            releaseDao.UpdateCubeRelease(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRelease(int id)
        {
            releaseDao.DeleteCubeRelease(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRelease(CubeRelease entity)
        {
            releaseDao.DeleteCubeRelease(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRelease(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            releaseDao.DeleteCubeRelease(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRelease(IList<CubeRelease> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            releaseDao.DeleteCubeRelease(entityList);
        }

        #endregion Method Created By CodeSmith

        #region Modified by vincent at 2007-11-13 begin

        public void RunPackage(string packageName)
        {
            sqlHelpDao.ExecuteNonQuery("Use msdb; EXEC dbo.sp_start_job '" + packageName + "'");
        }
        
        public void WarmCache()
        {
            string packageName = "Run SPCubeCacheWarmer";
            sqlHelpDao.ExecuteNonQuery("Use msdb; EXEC dbo.sp_start_job '" + packageName + "'");
        }
        
        public bool IsDistributing(int cubeid)
        {
            bool result = false;
            string sql = string.Format("select jobid from cubeDistributionjob where Jobid in (select max(jobid) from cubeDistributionjob where cube_id = {0}) and status = '{1}'",
                cubeid, CubeDistributionJob.DISTRIBUTION_STATUS_Running);
            DataTable dt = GetData(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = true;
            }
            return result;
        }

        public bool IsReleasing(int cubeid)
        {
            bool result = false;
            string sql = string.Format("select release_id from cube_release where release_id in (select max(r.release_id) from cube_release as r inner join cube_process as p on p.process_id = r.process_id where p.cube_id = {0}) and release_status='{1}'",
                cubeid, CubeRelease.RELEASE_STATUS_Running);
            DataTable dt = GetData(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = true;
            }
            return result;
        }

        public bool IsProcessCancelled(int cubeid)
        {
            bool result = false;
            string sql = string.Format("select process_id from cube_process where process_id in (select max(p.process_id) from cube_process as p where p.cube_id = {0}) and Process_status = '{1}'",
                cubeid, CubeProcess.PROCESS_STATUS_ProcessCancelled);
            DataTable dt = GetData(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = true;
            }
            return result;
        }

        private DataTable GetData(string sql)
        {
            DataTable result = new DataTable();
            if (sql.Trim() != "")
            {
                DataSet ds = sqlHelpDao.ExecuteDataset(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    result = ds.Tables[0];
                }
            }
            return result;
        }

        #endregion

        #region Customized Methods

        public void ReleaseCube(CubeProcess process, User user)
        {
            //Test run MDX
            //CubeUtility processCubeUtility = new CubeUtility(process.TheCube.ProcessServerAddr, process.TheCube.ProcessDatabaseName, process.TheCube.ProcessCubeName, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);
            //string MDX = "SELECT NON EMPTY Hierarchize({DrilldownLevel({[Fact Sales Org].[Fact SV].[All]})}) DIMENSION PROPERTIES PARENT_UNIQUE_NAME,[Fact Sales Org].[Fact SV].[Fact SV].[Fact SV Id] ON COLUMNS  FROM [SP_CUBE] WHERE ([Measures].[Fact PSR Count]) CELL PROPERTIES VALUE, FORMAT_STRING, LANGUAGE, BACK_COLOR, FORE_COLOR, FONT_FLAGS";
            //processCubeUtility.ExecuteMDX("", MDX);

            CubeRelease release = new CubeRelease();
            release.ReleaseUser = user;
            release.ReleaseDate = DateTime.Now;
            release.NeedWarmCache = 1;
            release.TheProcess = process;
            // Modified by vincent at 2007-11-13 begin
            release.Status = CubeRelease.RELEASE_STATUS_Running;            
            releaseDao.CreateCubeRelease(release);

            release.Status = CubeRelease.RELEASE_STATUS_Success;
            // Modified by vincent at 2007-11-13 end
            try
            {
                //upload role setting to process cube
                //UploadRoleToCube(process.TheCube.Id);

                //run preReleaseSQL
                string preReleaseSQL = process.TheCube.PreReleaseSQL;
                if (preReleaseSQL != null && preReleaseSQL.Trim().Length > 0)
                {
                    sqlHelpDao.ExecuteNonQuery(preReleaseSQL);
                }

                string backupFileSuffix = "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                //backup process cube
                CubeUtility processCubeUtility = new CubeUtility(process.TheCube.ProcessServerAddr, process.TheCube.ProcessDatabaseName, process.TheCube.ProcessCubeName, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);

                string processCubeBackupFolder = process.TheCube.ProcessCubeBackupFolder != null
                            && process.TheCube.ProcessCubeBackupFolder.Trim().Length > 0 ?
                            process.TheCube.ProcessCubeBackupFolder : propertyMgr.ProcessCubeBackupFolder;

                string cubeBackupFileName = process.TheCube.ProcessCubeName + backupFileSuffix + ".ABF";

                string processCubeBackupFilePath = processCubeBackupFolder.Replace("/", "\\").TrimEnd('\\') + "\\" + cubeBackupFileName;

                processCubeUtility.BackupDatabase(processCubeBackupFilePath);

                //restore cube to a Temp name
                string cubeReleaseTempDatabaseName = process.TheCube.ReleaseDatabaseName + "_TEMP" + backupFileSuffix;

                string releaseCubeReleaseFilePath = process.TheCube.ProcessCubeBackupFolder.Replace("/", "\\").TrimEnd('\\') + "\\" + cubeBackupFileName;

                CubeUtility.RestoreDatabase(process.TheCube.ReleaseServerAddr, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword, cubeReleaseTempDatabaseName, releaseCubeReleaseFilePath);

                //backup last release cube
                try
                {
                    CubeUtility lastReleaseCubeUtility = new CubeUtility(process.TheCube.ReleaseServerAddr, process.TheCube.ReleaseDatabaseName, process.TheCube.ReleaseCubeName, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);
                    // Modified by vincent at 2007-11-13 begin
                    string lastReleaseCubeBackupName = "";
                    lastReleaseCubeBackupName = process.TheCube.ReleaseDatabaseName + "_BAK" + backupFileSuffix;
                    
                    lastReleaseCubeUtility.RenameDatabase(lastReleaseCubeBackupName);
                    // Modified by vincent at 2007-11-13 end
                    release.BackupFile = lastReleaseCubeBackupName;
                }
                catch (Exception e)
                {
                    //do not have any database named "ReleaseDatabaseName"
                    release.Description = e.Message;
                }

                //rename restored cube from Temp name to formal name
                CubeUtility releaseCubeUtility = new CubeUtility(process.TheCube.ReleaseServerAddr, cubeReleaseTempDatabaseName, process.TheCube.ReleaseCubeName, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);
                
                releaseCubeUtility.RenameDatabase(process.TheCube.ReleaseDatabaseName);                
                
                
            }
            catch (Exception ex)
            {
                release.Status = CubeRelease.RELEASE_STATUS_Failed;
                release.Description = ex.Message;
            }
            finally
            {
                releaseDao.UpdateCubeRelease(release);

                //run postReleaseSQL
                string postReleaseSQL = process.TheCube.PostReleaseSQL;
                if (postReleaseSQL != null && postReleaseSQL.Trim().Length > 0)
                {
                    sqlHelpDao.ExecuteNonQuery(postReleaseSQL);
                }
            }
            
            // Modified by vincent at 2007-11-14 begin
            WarmCache();
            // Modified by vincent at 2007-11-14 end
        }

        public void UploadRoleToCube(int cubeId)
        {
            //delete all cube roles
            CubeDefinition cube = cubeDao.LoadCube(cubeId);
            // Modified by vincent at 2007-11-13 begin
            string preUpdateRoleSQL = cube.PreUpdateRoleSQL;
            if (preUpdateRoleSQL != null && preUpdateRoleSQL.Trim().Length > 0)
            {
                sqlHelpDao.ExecuteNonQuery(preUpdateRoleSQL);
            }

            // Modified by vincent at 2007-11-13 end
            
            CubeUtility cubeUtility = new CubeUtility(
                cube.ProcessServerAddr, cube.ProcessDatabaseName, cube.ProcessCubeName, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);
            //cubeUtility.DeleteAllRoles();
            // Modified by vincent at 2007-11-20 begin

            string[] tempRoles = cubeUtility.GetRoles();

            foreach (string temprole in tempRoles)
            {
                if (IsProcessCancelled(cubeId))
                {
                    break;
                }
                cubeUtility.DeleteRole(temprole);
            }

            // Modified by vincent at 2007-11-20 begin
            

            IList<CubeRole> roleList = roleDao.FindCubeRoleByCubeId(cubeId);

            if (roleList != null && roleList.Count > 0)
            {
                foreach(CubeRole role in roleList)
                {
                    // Modified by vincent at 2007-11-20 begin
                    if (IsProcessCancelled(cubeId))
                    {
                        break;
                    }
                    // Modified by vincent at 2007-11-20 end
                    authMgr.UploadRoleToCube(role, role.TheCube.ProcessServerAddr, role.TheCube.ProcessDatabaseName, role.TheCube.ProcessCubeName);
                }
            }

            // Modified by vincent at 2007-11-13 begin
            string postUpdateRoleSQL = cube.PostUpdateRoleSQL;
            if (postUpdateRoleSQL != null && postUpdateRoleSQL.Trim().Length > 0)
            {
                sqlHelpDao.ExecuteNonQuery(postUpdateRoleSQL);
            }

            // Modified by vincent at 2007-11-13 end
        }

        //public void RollbackCube(CubeRelease lastRelease, User user)
        //{
        //    CubeRelease release = new CubeRelease();
        //    release.ReleaseUser = user;
        //    release.ReleaseDate = DateTime.Now;
        //    release.NeedWarmCache = 1;
        //    release.TheProcess = lastRelease.TheProcess;
        //    release.Status = CubeRelease.RELEASE_STATUS_Rollback;

        //    if (lastRelease.BackupFile != null)
        //    {
        //        CubeUtility lastCubeUtility = new CubeUtility(process.TheCube.ReleaseServerAddr, process.TheCube.ReleaseDatabaseName, process.TheCube.ReleaseCubeName);
                
        //    }
        //    else
        //    {
        //        release.Description = "Can't find Backup cube";
        //    }
        //}

        public IList<CubeRelease> FindAllCubeReleaseByCubeId(int cubeId, User user)
        {
            return releaseDao.FindAllCubeReleaseByCubeId(cubeId, user.Id);
        }

        public void UpdateProcessDescription(int cubeId, string description)
        {
            CubeDefinition cube = cubeDao.LoadCube(cubeId);
            // Modified by vincent at 2007-11-13 begin
            string preUpdateDescriptionSQL = cube.PreUpdateDescriptionSQL;
            if (preUpdateDescriptionSQL != null && preUpdateDescriptionSQL.Trim().Length > 0)
            {
                sqlHelpDao.ExecuteNonQuery(preUpdateDescriptionSQL);
            }

            // Modified by vincent at 2007-11-13 end

            CubeUtility cubeUtility = new CubeUtility(
                cube.ProcessServerAddr, cube.ProcessDatabaseName, cube.ProcessCubeName, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);

            cubeUtility.UpdateCubeDescription(description);

            // Modified by vincent at 2007-11-13 begin
            string postUpdateDescriptionSQL = cube.PostUpdateDescriptionSQL;
            if (postUpdateDescriptionSQL != null && postUpdateDescriptionSQL.Trim().Length > 0)
            {
                sqlHelpDao.ExecuteNonQuery(postUpdateDescriptionSQL);
            }

            // Modified by vincent at 2007-11-13 end

        }

        #endregion Customized Methods

        #region private Methods
        private string TranslateLocateFolderToRemoteFolder(string loccalServerName, string localFolder)
        {
            string dirveName = localFolder.Substring(0, localFolder.IndexOf(":"));

            return "\\\\" + loccalServerName + "\\" + dirveName + "$" + localFolder.Substring(localFolder.IndexOf(":") + 1);
        }
        #endregion private Methods
    }
}
