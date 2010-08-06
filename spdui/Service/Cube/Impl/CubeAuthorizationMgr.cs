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
using System.Data;
using Dndp.Utility;
using Dndp.Persistence.Dao;
using Dndp.Service.Property;
//TODO: Add other using statements here.

namespace Dndp.Service.Cube.Impl
{
    [Transactional]
    public class CubeAuthorizationMgr : SessionBase, ICubeAuthorizationMgr
    {
        private const string COLUMN_NAME_DIMENSION_NAME = "DimensionName";
        private const string COLUMN_NAME_ATTRIBUTE_NAME = "AttributeName";
        private const string COLUMN_NAME_VISUAL_TOTAL = "VisualTotal";
        private const string COLUMN_NAME_ATTRIBUTE_DATA = "AttributeData";

        private ICubeUserDao cubeUserDao;
        private ICubeRoleDao cubeRoleDao;
        private ICubeUserRoleDao cubeUserRoleDao;
        private ICubeRoleDimensionMemberDao cubeRoleDimensionMemberDao;
        private ICubeDimensionDao cubeDimensionDao;
        private SqlHelperDao sqlDao;
        private IPropertyMgr propertyMgr;
       

        public CubeAuthorizationMgr(ICubeUserDao cubeUserDao,
                                    ICubeRoleDao cubeRoleDao,
                                    ICubeUserRoleDao cubeUserRoleDao,
                                    ICubeRoleDimensionMemberDao cubeRoleDimensionMemberDao,
                                    ICubeDimensionDao cubeDimensionDao,
                                    SqlHelperDao sqlDao,
                                   IPropertyMgr propertyMgr)
        {
            this.cubeUserDao = cubeUserDao;
            this.cubeRoleDao = cubeRoleDao;
            this.cubeUserRoleDao = cubeUserRoleDao;
            this.cubeRoleDimensionMemberDao = cubeRoleDimensionMemberDao;
            this.cubeDimensionDao = cubeDimensionDao;
            this.sqlDao = sqlDao;
            this.propertyMgr = propertyMgr;
        }

        #region  Method Related to SetDimensionVisualTotal
        // Modified by vincent at 2007-11-09 begin
        public SqlHelperDao GetSqlDao()
        {
            return sqlDao;
        }

        public void DeleteSetDimensionVisualTotal(int roleid)
        {
            string sql = "DELETE Cube_Role_SetDimension_VisualTotal where roleId = " + roleid.ToString();
            GetData(sql);            
        }

        public void InsertSetDimensionVisualTotal(int roleid, string setDimensionName, string visualtotal)
        {
            string sql = string.Format("Insert dbo.Cube_Role_SetDimension_VisualTotal values({0}, '{1}', '{2}', 0,'','')",
                roleid, setDimensionName, visualtotal);
            GetData(sql);
        }

        public bool GetSetDimensionVisualTotal(int roleid, string setDimensionName)
        {
            bool result = true;
            DataTable vt = new DataTable();
            string sql = string.Format("SELECT * FROM Cube_Role_SetDimension_VisualTotal WHERE RoleId = {0} AND SetDimensionName='{1}'",
                roleid.ToString(), setDimensionName);
            vt = GetData(sql);
            if (vt.Rows.Count > 0 && vt.Rows[0]["VisualTotal"].ToString() == "0")
            {
                result = false;
            }
            return result;
        }

        private DataTable GetData(string sql)
        {
            DataTable result = new DataTable();
            if (sql.Trim() != "")
            {
                DataSet ds = sqlDao.ExecuteDataset(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    result = ds.Tables[0];
                }
            }
            return result;
        }

        // Modified by vincent at 2007-11-09 end
        #endregion

        #region Method Related to CubeUser

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeUser(CubeUser entity)
        {
            //TODO: Add other code here.
			
            cubeUserDao.CreateCubeUser(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeUser LoadCubeUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }
			
			//TODO: Add other code here.
			
            return cubeUserDao.LoadCubeUser(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeUser(CubeUser entity)
        {
        	//TODO: Add other code here.
            cubeUserDao.UpdateCubeUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUser(int id)
        {
            CubeUser entity = cubeUserDao.LoadCubeUser(id);
            entity.ActiveFlag = 0;
            cubeUserDao.UpdateCubeUser(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUser(CubeUser entity)
        {
            entity.ActiveFlag = 0;
            cubeUserDao.UpdateCubeUser(entity);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUser(IList<int> idList)
        {
            if ((idList != null) && (idList.Count > 0))
            {
                foreach (int id in idList)
                {
                    CubeUser entity = cubeUserDao.LoadCubeUser(id);
                    entity.ActiveFlag = 0;
                    cubeUserDao.UpdateCubeUser(entity);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUser(IList<CubeUser> entityList)
        {
            if ((entityList != null) && (entityList.Count > 0))
            {
                foreach (CubeUser entity in entityList)
                {
                    entity.ActiveFlag = 0;
                    cubeUserDao.UpdateCubeUser(entity);
                }
            }
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList LoadAllActiveCubeUser()
        {
            return cubeUserDao.LoadAllActiveCubeUser();
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeUser> FindUserForCubeDistribution(int Id)
        {
            return cubeUserDao.FindUserForCubeDistribution(Id);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeUser> FindCubeUserByName(string userName)
        {
            return cubeUserDao.FindCubeUserByName(userName);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeUser> FindUserExcludeSpecifiedRoleByNameAndDescription(int roleId, string name, string description)
        {
            return cubeUserDao.FindUserExcludeSpecifiedRoleByNameAndDescription(roleId, name, description);
        }

        #endregion Method Related to CubeUser

        #region  Method Related to CubeRole

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeRole(CubeRole entity)
        {
            //TODO: Add other code here.

            cubeRoleDao.CreateCubeRole(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeRole LoadCubeRole(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return cubeRoleDao.LoadCubeRole(id);
        }

        [Transaction(TransactionMode.NotSupported)]
        public CubeRole LoadCubeRoleWithAllInfo(int id)
        {
            CubeRole role = LoadCubeRole(id);
            role.CubeUserList = cubeUserRoleDao.FindCubeUserByRoleId(id);
            role.CubeRoleDimensionMemberList = cubeRoleDimensionMemberDao.FindCubeRoleDimensionMemberByRoleId(id);
            role.CubeDimensionList = cubeDimensionDao.FindDimensionByCubeId(role.TheCube.Id);

            return role;
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeRole(CubeRole entity)
        {
            //TODO: Add other code here.
            cubeRoleDao.UpdateCubeRole(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRole(int id)
        {
            CubeRole role = cubeRoleDao.LoadCubeRole(id);
            cubeUserRoleDao.DeleteCubeUserByRoleId(id);
            cubeRoleDimensionMemberDao.DeleteCubeRoleDimensionMemberByRoleId(id);
            cubeRoleDao.DeleteCubeRole(id);

            //CubeUtility cubeUtility = new CubeUtility(role.TheCube.ProcessServerAddr, 
            //    role.TheCube.ProcessDatabaseName, role.TheCube.ProcessCubeName, 
            //    propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);

            //cubeUtility.DeleteRole(role.Name);            
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRole(CubeRole entity)
        {
            DeleteCubeRole(entity.Id);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRole(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            foreach (int id in idList)
            {
                DeleteCubeRole(id);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRole(IList<CubeRole> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            foreach (CubeRole entity in entityList)
            {
                DeleteCubeRole(entity.Id);
            }
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeRole> FindCubeRoleByName(string roleName)
        {
            return cubeRoleDao.FindCubeRoleByName(roleName);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeRole> FindCubeRoleByNameAndDescription(string roleName, string description)
        {
            return cubeRoleDao.FindCubeRoleByNameAndDescription(roleName, description);
        }

        public void UploadRoleToCube(int roleId, string serverAddr, string databaseName, string cubeName)
        {
            UploadRoleToCube(cubeRoleDao.LoadCubeRole(roleId), serverAddr, databaseName, cubeName);
        }

        public void UploadRoleToCube(CubeRole role, string serverAddr, string databaseName, string cubeName)
        {
            role.CubeUserList = cubeUserDao.FindCubeUserByRoleId(role.Id);
            role.CubeDimensionList = cubeDimensionDao.FindDimensionByCubeId(role.TheCube.Id);
            role.CubeRoleDimensionMemberList = cubeRoleDimensionMemberDao.FindCubeRoleDimensionMemberByRoleId(role.Id);
            string[] cubeUserAccount = GetCubeUserAccount(role);
            DataTable dimensionData = GetDimensionData(role);
            
            // Modified by vincent at 2007-11-09 begin
            // Fix Get Dimension Data
            foreach (DataRow dr in dimensionData.Rows)
            {
                string setDimensionName = dr[COLUMN_NAME_DIMENSION_NAME].ToString();
                int roleid = role.Id;
                string visualtotal = GetSetDimensionVisualTotal(roleid, setDimensionName) ? "1" : "0";
                dr[COLUMN_NAME_VISUAL_TOTAL] = visualtotal;
            }
            // Modified by vincent at 2007-11-09 end

            //if (dimensionData != null && dimensionData.Rows.Count > 0 && cubeUserAccount != null && cubeUserAccount.Length > 0)
            if (cubeUserAccount != null && cubeUserAccount.Length > 0)
            {
                CubeUtility cubeUtility = new CubeUtility(serverAddr, databaseName, cubeName, propertyMgr.ProductCubeUserName, propertyMgr.ProductCubePassword);

                cubeUtility.UpdateRole(role.Name, cubeUserAccount, dimensionData);
            }
        }
        #endregion Method Related to CubeRole

        #region Method Related to CubeUserRole

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeUserRole(CubeUserRole entity)
        {
            //TODO: Add other code here.

            cubeUserRoleDao.CreateCubeUserRole(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeUserRole LoadCubeUserRole(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return cubeUserRoleDao.LoadCubeUserRole(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeUserRole(CubeUserRole entity)
        {
            //TODO: Add other code here.
            cubeUserRoleDao.UpdateCubeUserRole(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUserRole(int id)
        {
            cubeUserRoleDao.DeleteCubeUserRole(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUserRole(CubeUserRole entity)
        {
            cubeUserRoleDao.DeleteCubeUserRole(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUserRole(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            cubeUserRoleDao.DeleteCubeUserRole(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUserRole(IList<CubeUserRole> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            cubeUserRoleDao.DeleteCubeUserRole(entityList);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeUser> FindCubeUserByRoleId(int roleId)
        {
            return cubeUserRoleDao.FindCubeUserByRoleId(roleId);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUserByRoleId(int roleId)
        {
            cubeUserRoleDao.DeleteCubeUserByRoleId(roleId);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateCubeUserRole(CubeRole cubeRole, IList<int> cubeUserIdList)
        {
            foreach (int cubeUserId in cubeUserIdList)
            {
                CubeUser cubeUser = cubeUserDao.LoadCubeUser(cubeUserId);

                CubeUserRole cubeUserRole = new CubeUserRole();
                cubeUserRole.TheCubeRole = cubeRole;
                cubeUserRole.TheCubeUser = cubeUser;

                cubeUserRoleDao.CreateCubeUserRole(cubeUserRole);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeUserRole(int CubeRoleId, IList<int> cubeUserIdList)
        {
            if (cubeUserIdList != null && cubeUserIdList.Count > 0)
            {
                cubeUserRoleDao.DeleteCubeUserRole(CubeRoleId, cubeUserIdList);
            }
        }

        #endregion Method Related to CubeUserRole

        #region Method Related to CubeRoleDimensionMember

        [Transaction(TransactionMode.Requires)]
        public void CreateCubeRoleDimensionMember(CubeRoleDimensionMember entity)
        {
            //TODO: Add other code here.

            cubeRoleDimensionMemberDao.CreateCubeRoleDimensionMember(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public CubeRoleDimensionMember LoadCubeRoleDimensionMember(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invliad parameter: id");
            }

            //TODO: Add other code here.

            return cubeRoleDimensionMemberDao.LoadCubeRoleDimensionMember(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCubeRoleDimensionMember(CubeRoleDimensionMember entity)
        {
            //TODO: Add other code here.
            cubeRoleDimensionMemberDao.UpdateCubeRoleDimensionMember(entity);


        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRoleDimensionMember(int id)
        {
            cubeRoleDimensionMemberDao.DeleteCubeRoleDimensionMember(id);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRoleDimensionMember(CubeRoleDimensionMember entity)
        {
            cubeRoleDimensionMemberDao.DeleteCubeRoleDimensionMember(entity);
        }


        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRoleDimensionMember(IList<int> idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            cubeRoleDimensionMemberDao.DeleteCubeRoleDimensionMember(idList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRoleDimensionMember(IList<CubeRoleDimensionMember> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            cubeRoleDimensionMemberDao.DeleteCubeRoleDimensionMember(entityList);
        }

        [Transaction(TransactionMode.NotSupported)]
        public IList<CubeRoleDimensionMember> FindCubeRoleDimensionMemberByRoleId(int roleId)
        {
            return cubeRoleDimensionMemberDao.FindCubeRoleDimensionMemberByRoleId(roleId);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteCubeRoleDimensionMemberByRoleId(int roleId)
        {
            cubeRoleDimensionMemberDao.DeleteCubeRoleDimensionMemberByRoleId(roleId);
        }

        public IList<CubeDimensionMember> GetDimensionMembers(CubeDefinition cube, string dimensionName, string attributeName)
        {
            IList<CubeDimensionMember> list = new List<CubeDimensionMember>();
            
            try
            {
                // Modified by vincent at 2007-11-15 begin
                
                CubeUtility cubeUtility = new CubeUtility(cube.ProcessServerAddr,
                    cube.ProcessDatabaseName, cube.ProcessCubeName,
                    propertyMgr.ProductCubeUserName,
                    propertyMgr.ProductCubePassword);
                //CubeUtility cubeUtility = new CubeUtility(cube.ProcessServerAddr, cube.ProcessDatabaseName, cube.ProcessCubeName);
                // Modified by vincent at 2007-11-15 end

                
                DataTable dt = cubeUtility.GetAttrributeMembers(dimensionName, attributeName);


                
                for (int i = 0; i < dt.Rows.Count; i++)
                {   
                    CubeDimensionMember cubeDimensionMember = new CubeDimensionMember();
                    cubeDimensionMember.MemberId = dt.Rows[i][1].ToString();

                    cubeDimensionMember.MemberName = dt.Rows[i][0].ToString() == string.Empty ? "Select All" : dt.Rows[i][0].ToString();

                    cubeDimensionMember.MemberValue = dt.Rows[i][1].ToString();

                    list.Add(cubeDimensionMember);

                    if (cubeDimensionMember.MemberName == "Select All")
                    {
                        cubeDimensionMember = new CubeDimensionMember();
                        cubeDimensionMember.MemberId = "";
                        cubeDimensionMember.MemberName = "Deselect All";                        
                        list.Add(cubeDimensionMember);
                    }
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }

            return list;
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateCubeRoleDimensionMember(CubeRole cubeRole, IList<CubeRoleDimensionMember> memberList, string dimensionName, string attributeName)
        {
            cubeRoleDimensionMemberDao.DeleteCubeRoleDimensionMemberByRoleIdAndDimAndAtt(cubeRole.Id, dimensionName, attributeName);

            IList<CubeDimension> cubeDimensionList = cubeDimensionDao.FindDimensionByDimensionNameAndAttributeName(dimensionName, attributeName);

            foreach (CubeRoleDimensionMember member in memberList)
            {                
                cubeRoleDimensionMemberDao.CreateCubeRoleDimensionMember(member);

                //other CubeDimension which DimensionName and AttributeName are same with current dimension,
                //also need add CubeRoleDimensionMember same as current dimension
                foreach(CubeDimension dim in cubeDimensionList)
                {
                    if (dim.Id != member.TheDimension.Id)
                    {
                        CubeRoleDimensionMember otherMember = new CubeRoleDimensionMember();
                        otherMember.TheDimension = dim;
                        otherMember.IsVisualtotal = member.IsVisualtotal;
                        otherMember.MemberId = member.MemberId;
                        otherMember.MemberName = member.MemberName;
                        otherMember.MemberValue = member.MemberValue;
                        otherMember.TheCubeRole = member.TheCubeRole;

                        cubeRoleDimensionMemberDao.CreateCubeRoleDimensionMember(otherMember);
                    }
                }
            }            
        }

        public void SynchronizeVisualtotal(int cubeId)
        {
            //if a Dimension is set Visualtotal true, all Dimension Member belong to the dimension will all set Visualtotal true 
            string sql = @"update Cube_Role_Dimension_Member set Visualtotal = target.Visualtotal
                            from Cube_Role_Dimension_Member as member  
                            inner join Cube_Dimension as dim on member.Dimension_Id = dim.Dimension_Id
                            inner join 
                                (select Cube_Id, Dimension_Name, case when sum(Visualtotal) > 0 then 1 else 0 end as Visualtotal
                                from Cube_Role_Dimension_Member as member  
                                inner join Cube_Dimension as dim on member.Dimension_Id = dim.Dimension_Id
                                group by Cube_Id, Dimension_Name) as target on dim.Dimension_Name = target.Dimension_Name and dim.Cube_Id = target.Cube_Id
                            where dim.Cube_Id = " + cubeId.ToString() ;

            sqlDao.ExecuteNonQuery(sql);
        }

        #endregion Method Related to CubeRoleDimensionMember      
  
        #region private method

        private string[] GetCubeUserAccount(CubeRole role)
        {
            if (role.CubeUserList != null && role.CubeUserList.Count > 0)
            {
                string[] cubeUserAccount = new string[role.CubeUserList.Count];
                for (int i = 0; i < cubeUserAccount.Length; i++)
                {
                    cubeUserAccount[i] = role.CubeUserList[i].TheDistributionUser.DomainAccount;
                }

                return cubeUserAccount;
            }
            else
            {
                return null;
            }
        }

        private DataTable GetDimensionData(CubeRole role)
        {
            DataTable dt = GetFormatedDataTableForDimensionData();

            //get assigned DimensionData  
            if (role.CubeRoleDimensionMemberList != null && role.CubeRoleDimensionMemberList.Count > 0)
            {
                //foreach (CubeRoleDimensionMember member in role.CubeRoleDimensionMemberList)
                //{
                //    //only if AttributeName and DimensionName equals SetAttributeName and SetDimensionName
                //    //then add MemberValue to Dimension Data
                //    if (member.TheDimension.AttributeName == member.TheDimension.SetAttributeName
                //        && member.TheDimension.DimensionName == member.TheDimension.SetDimensionName)
                //    {                    
                //        DataRow row = dt.NewRow();
                //        row[COLUMN_NAME_DIMENSION_NAME] = member.TheDimension.DimensionName;
                //        row[COLUMN_NAME_ATTRIBUTE_NAME] = member.TheDimension.AttributeName;
                //        row[COLUMN_NAME_VISUAL_TOTAL] = member.IsVisualtotal;
                //        row[COLUMN_NAME_ATTRIBUTE_DATA] = member.MemberValue;

                //        dt.Rows.Add(row);
                //    }
                //}

                //get Related MDX DimensionData            
                foreach (CubeDimension dim in role.CubeDimensionList)
                {
                    if (dim.MDXFormula != null && dim.MDXFormula.Trim().Length > 0)
                    {
                        AddMDXDimensionRow(dt, role.CubeDimensionList, 
                            role.CubeRoleDimensionMemberList, 
                            dim.MDXFormula, 
                            dim.SetDimensionName, 
                            dim.SetAttributeName, 
                            dim.DimensionName, 
                            dim.AttributeName, 
                            dim.RelatedDimensionName, 
                            dim.RelatedAttributeName);
                    }

                    if (dim.RelatedMDXFormula != null && dim.RelatedMDXFormula.Trim().Length > 0)
                    {
                        AddMDXDimensionRow(dt, role.CubeDimensionList,
                            role.CubeRoleDimensionMemberList,
                            dim.RelatedMDXFormula,
                            dim.SetDimensionName,
                            dim.SetAttributeName,
                            dim.DimensionName,
                            dim.AttributeName,
                            dim.RelatedDimensionName,
                            dim.RelatedAttributeName);
                    }   
                }
            }

            return dt;
        }

        private void AddMDXDimensionRow(DataTable dt, 
            IList<CubeDimension> CubeDimensionList,
            IList<CubeRoleDimensionMember> CubeRoleDimensionMemberList,
            string MDXFormula,
            string SetDimensionName, string SetAttributeName, 
            string DimensionName, string AttributeName, 
            string RelatedDimensionName, string RelatedAttributeName)
        {
            if (MDXFormula != null && MDXFormula.Trim().Length > 0)
            {
                string[] MDXDimensionData = GetMDXDimensionData(
                        CubeDimensionList,
                        CubeRoleDimensionMemberList, 
                        MDXFormula,
                        SetDimensionName, 
                        SetAttributeName, 
                        DimensionName, 
                        AttributeName, 
                        RelatedDimensionName, 
                        RelatedAttributeName);

                if (MDXDimensionData != null)
                {
                    DataRow row = dt.NewRow();
                    row[COLUMN_NAME_DIMENSION_NAME] = SetDimensionName;
                    row[COLUMN_NAME_ATTRIBUTE_NAME] = SetAttributeName;
                    row[COLUMN_NAME_VISUAL_TOTAL] = int.Parse(MDXDimensionData[1]);
                    row[COLUMN_NAME_ATTRIBUTE_DATA] = MDXDimensionData[0];

                    dt.Rows.Add(row);
                }
            }

            //if (relatedMDXFormula != null && relatedMDXFormula.Trim().Length > 0)
            //{
            //    string[] MDXDimensionData = GetMDXDimensionData(CubeDimensionList,
            //        CubeRoleDimensionMemberList, 
            //        relatedMDXFormula,
            //        SetDimensionName, 
            //        SetAttributeName, 
            //        DimensionName, 
            //        AttributeName, 
            //        RelatedDimensionName, 
            //        RelatedAttributeName);

            //    if (MDXDimensionData != null)
            //    {
            //        DataRow row = dt.NewRow();
            //        row[COLUMN_NAME_DIMENSION_NAME] = SetDimensionName;
            //        row[COLUMN_NAME_ATTRIBUTE_NAME] = SetAttributeName;
            //        row[COLUMN_NAME_VISUAL_TOTAL] = int.Parse(MDXDimensionData[1]);
            //        row[COLUMN_NAME_ATTRIBUTE_DATA] = MDXDimensionData[0];

            //        dt.Rows.Add(row);
            //    }
            //}
        }

        private string[] GetMDXDimensionData(IList<CubeDimension> CubeDimensionList,
                                    IList<CubeRoleDimensionMember> cubeRoleDimensionMemberList, 
                                    string MDXFormula,
                                    string setDimensionName, string setAttributeName, 
                                    string dimensionName, string attributeName, 
                                    string relatedDimensionName, string relatedAttributeName)
        {
            //string[] realtedData = GetDimensionRelatedData(cubeRoleDimensionMemberList, setDimensionName, setAttributeName, dimensionName, attributeName);

            //string dimensionData = realtedData[0];
            //string setDimensionData = realtedData[1];
            //int isVisualtotal = int.Parse(realtedData[2]);

            string dimensionData = "";
            string setDimensionData = "";
            int isVisualtotal = 0;
            Hashtable dimensionDataHT = new Hashtable();  //to store added dimension Place holder data
            Hashtable setDimensionDataHT = new Hashtable();  //to store added replace dimension Place holder data

            foreach (CubeRoleDimensionMember member in cubeRoleDimensionMemberList)
            {
                //get dimension Place holder data
                if (member.TheDimension.DimensionName == dimensionName
                    && member.TheDimension.AttributeName == attributeName
                    && !dimensionDataHT.Contains(member.MemberValue))
                {
                    dimensionDataHT.Add(member.MemberValue, member.MemberValue);
                    dimensionData += member.MemberValue + ",";
                    if (member.IsVisualtotal == 1)
                    {
                        isVisualtotal = 1;
                    }
                }

                //get related dimension Place holder data 
                if (member.TheDimension.DimensionName == relatedDimensionName
                    && member.TheDimension.AttributeName == relatedAttributeName
                    && !setDimensionDataHT.Contains(member.MemberValue))
                {
                    setDimensionDataHT.Add(member.MemberValue, member.MemberValue);
                    setDimensionData += member.MemberValue + ",";
                    if (member.IsVisualtotal == 1)
                    {
                        isVisualtotal = 1;
                    }
                }

                //foreach (CubeDimension dim in CubeDimensionList)
                //{
                //    if (dim.RelatedDimensionName == member.TheDimension.DimensionName
                //        && dim.RelatedAttributeName == member.TheDimension.AttributeName
                //        && dim.SetDimensionName == setDimensionName
                //        && dim.SetAttributeName == setAttributeName)
                //    {
                //        setDimensionData += member.MemberValue + ",";
                //        if (member.IsVisualtotal == 1)
                //        {
                //            isVisualtotal = 1;
                //        }
                //    }
                //}
            }

            dimensionData = dimensionData.TrimEnd(',');
            setDimensionData = setDimensionData.TrimEnd(',');


            if (MDXFormula.Contains(DataParameterHelper.PARAMETER_PREFIX + CubeDimension.Dimension_Data_Place_Holder + DataParameterHelper.PARAMETER_POSTFIX))
            {
                if (dimensionData.Trim().Length > 0)
                {
                    MDXFormula = MDXFormula.Replace(DataParameterHelper.PARAMETER_PREFIX + CubeDimension.Dimension_Data_Place_Holder + DataParameterHelper.PARAMETER_POSTFIX, dimensionData);
                }
                else
                {
                    return null;
                }
            }

            if (MDXFormula.Contains(DataParameterHelper.PARAMETER_PREFIX + CubeDimension.Related_Dimension_Data_Place_Holder + DataParameterHelper.PARAMETER_POSTFIX))
            {
                if (setDimensionData.Trim().Length > 0)
                {
                    MDXFormula = MDXFormula.Replace(DataParameterHelper.PARAMETER_PREFIX + CubeDimension.Related_Dimension_Data_Place_Holder + DataParameterHelper.PARAMETER_POSTFIX, setDimensionData);
                }
                else
                {
                    return null;
                }
            }
                            
            return new string[] { MDXFormula, isVisualtotal.ToString() };           
        }

        //private string[] GetRelatedMDXDimensionData(IList<CubeDimension> CubeDimensionList,
        //                            IList<CubeRoleDimensionMember> cubeRoleDimensionMemberList,
        //                            string relatedMDXFormula,
        //                            string setDimensionName, string setAttributeName,
        //                            string dimensionName, string attributeName,
        //                            string relatedDimensionName, string relatedAttributeName)
        //{
        //    //string[] realtedData = GetDimensionRelatedData(cubeRoleDimensionMemberList, setDimensionName, setAttributeName, dimensionName, attributeName);

        //    //string dimensionData = realtedData[0];
        //    //string setDimensionData = realtedData[1];
        //    //int isVisualtotal = int.Parse(realtedData[2]);

        //    string dimensionData = "";
        //    string setDimensionData = "";
        //    int isVisualtotal = 0;

        //    foreach (CubeRoleDimensionMember member in cubeRoleDimensionMemberList)
        //    {
        //        if (member.TheDimension.DimensionName == dimensionName
        //            && member.TheDimension.AttributeName == attributeName)
        //        {
        //            dimensionData += member.MemberValue + ",";
        //            if (member.IsVisualtotal == 1)
        //            {
        //                isVisualtotal = 1;
        //            }
        //        }

        //        foreach (CubeDimension dim in CubeDimensionList)
        //        {
        //            if (dim.RelatedDimensionName == member.TheDimension.RelatedDimensionName
        //                && dim.RelatedAttributeName == member.TheDimension.RelatedAttributeName
        //                && dim.SetDimensionName == setDimensionName
        //                && dim.SetAttributeName == setAttributeName)
        //            {
        //                setDimensionData += member.MemberValue + ",";
        //                if (member.IsVisualtotal == 1)
        //                {
        //                    isVisualtotal = 1;
        //                }
        //            }
        //        }
        //    }

        //    dimensionData = dimensionData.TrimEnd(',');
        //    setDimensionData = setDimensionData.TrimEnd(',');

        //    if (dimensionData.Trim().Length > 0
        //        && setDimensionData.Trim().Length > 0)
        //    {
        //        relatedMDXFormula = relatedMDXFormula.Replace(DataParameterHelper.PARAMETER_PREFIX + CubeDimension.Dimension_Data_Place_Holder + DataParameterHelper.PARAMETER_POSTFIX, dimensionData);
        //        relatedMDXFormula = relatedMDXFormula.Replace(DataParameterHelper.PARAMETER_PREFIX + CubeDimension.Related_Dimension_Data_Place_Holder + DataParameterHelper.PARAMETER_POSTFIX, setDimensionData);

        //        return new string[] { relatedMDXFormula, isVisualtotal.ToString() };
        //    }

        //    return null;
        //}

        //private string[] GetDimensionRelatedData(IList<CubeRoleDimensionMember> cubeRoleDimensionMemberList,
        //                            string setDimensionName, string setAttributeName,
        //                            string dimensionName, string attributeName)
        //{
        //    string dimensionData = "";
        //    string setDimensionData = "";
        //    int isVisualtotal = 0;

        //    foreach (CubeRoleDimensionMember member in cubeRoleDimensionMemberList)
        //    {
        //        if (member.TheDimension.DimensionName == dimensionName
        //            && member.TheDimension.AttributeName == attributeName)
        //        {
        //            dimensionData += member.MemberValue + ",";
        //            if (member.IsVisualtotal == 1)
        //            {
        //                isVisualtotal = 1;
        //            }
        //        }

        //        if (member.TheDimension.SetDimensionName == setDimensionName
        //            && member.TheDimension.SetAttributeName == setAttributeName
        //            && member.TheDimension.DimensionName != dimensionName
        //            && member.TheDimension.AttributeName != attributeName)
        //        {
        //            setDimensionData += member.MemberValue + ",";
        //            if (member.IsVisualtotal == 1)
        //            {
        //                isVisualtotal = 1;
        //            }
        //        }
        //    }

        //    dimensionData = dimensionData.TrimEnd(',');
        //    setDimensionData = setDimensionData.TrimEnd(',');

        //    return new string[] { dimensionData, setDimensionData, isVisualtotal.ToString() };
        //}

        private DataTable GetFormatedDataTableForDimensionData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(COLUMN_NAME_DIMENSION_NAME, typeof(string));
            dt.Columns.Add(COLUMN_NAME_ATTRIBUTE_NAME, typeof(string));
            dt.Columns.Add(COLUMN_NAME_VISUAL_TOTAL, typeof(int));
            dt.Columns.Add(COLUMN_NAME_ATTRIBUTE_DATA, typeof(string));

            return dt;
        }
        
        #endregion private method
    }
}
