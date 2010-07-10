using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AnalysisServices;
using System.Collections;
using System.IO;
using System.Data;

namespace SPCubeUtility
{
    public delegate void CreateCubeFileHandler(object sender, CubeFileArgs e);

    public class CubeFileArgs:EventArgs
    {
        public string FileName
        {
            get
            {
                return _fileName;
            }
        }
        private string _fileName;
        public CubeFileArgs(string fileName)
        {
            _fileName = fileName;
        }

    }
    public class CubeUtility
    {
        #region Const Definition
        // CREATE_CUBE_FILE_MDX {0} Cube Name, {1} Cube File, {2} Measures, [3] Dimensions, Auto Generate Calculate Field        
        private const string MDX_CREATE_CUBE_FILE = "CREATE GLOBAL CUBE [{0}] STORAGE '{1}' FROM [{0}] ( {2}, {3}) ";
        private const string MDX_GET_ALL_MEMBERS = "WITH MEMBER TEMP_MEMBER AS 'MEMBERTOSTR({1}.CURRENTMEMBER)' SELECT TEMP_MEMBER ON 0 , {1}.MEMBERS ON 1 FROM {0}";
        // AMO_CONNECTION_STRING {0} Server
        //private const string AMO_CONNECTION_STRING = "Provider=MSOLAP.3;Integrated Security=SSPI;Persist Security Info=True;Data Source={0}";
        private const string AMO_CONNECTION_STRING = "Provider=MSOLAP.3;Persist Security Info=True;Data Source={0};User Id={1};Password={2}";

        // ADOMD_CONNECTION_STRING {0} Server, {1} Database, {2} Roles Role=role1,role2
        private const string ADOMD_CONNECTION_STRING = "Provider=MSOLAP.3;Integrated Security=SSPI;Persist Security Info=True;Data Source={0};User Id={1};Password={2};Initial Catalog={3};{4}";
        
        private string[] emptyStringArray = new string[0];
        #endregion        
        
        //public event CreateCubeFileHandler OnCubeFileCreated;

        private Server svr;
        private Database db;
        private Cube cube;
        private string userName;
        private string password;
        public CubeUtility(string svrName, string dbName, string cubeName)
        {
            userName = "none";
            password = "none";
            string connstr = string.Format(AMO_CONNECTION_STRING, svrName, userName, password);
            svr = new Server();
            svr.Connect(connstr);
            db = svr.Databases.FindByName(dbName);
            cube = db.Cubes.FindByName(cubeName);
            
        }

        public CubeUtility(string svrName, string dbName, string cubeName, string userName, string password)
        {
            string connstr = string.Format(AMO_CONNECTION_STRING, svrName, userName, password);
            svr = new Server();
            svr.Connect(connstr);
            db = svr.Databases.FindByName(dbName);
            cube = db.Cubes.FindByName(cubeName);
            userName = userName.Trim() == "" ? "none" : userName.Trim();
            password = password.Trim() == "" ? "none" : password.Trim();
        }

        #region Get Objects ...



        public void UpdateCubeDescription(string desc)
        {
            cube.Description = desc;
            cube.Update();
 
        }

        public string[] GetDimensions()
        {
            ArrayList result = new ArrayList();
            foreach (Dimension d in db.Dimensions)
            {
                result.Add(d.Name);
            }
            return (string[])result.ToArray(typeof(string));
        }

        public string[] GetDimensionAttributes(string dimName)
        {
            Dimension dim = db.Dimensions.GetByName(dimName);
            ArrayList result = new ArrayList();
            foreach (DimensionAttribute a in dim.Attributes)
            {
                result.Add(a.Name);
            }
            return (string[])result.ToArray(typeof(string));
        }

        public string[] GetUnhiddenMeasures()
        {
            ArrayList result = new ArrayList();
            foreach (MeasureGroup mg in cube.MeasureGroups)
            {
                foreach (Measure m in mg.Measures)
                {
                    if (m.Visible)
                    {
                        result.Add(m.Name);
                    }
                }
            }
            return (string[])result.ToArray(typeof(string));
        }

        public string[] GetHiddenMeasures()
        {
            ArrayList result = new ArrayList();
            foreach (MeasureGroup mg in cube.MeasureGroups)
            {
                foreach (Measure m in mg.Measures)
                {
                    if (!m.Visible)
                    {
                        result.Add(m.Name);
                    }
                }
            }
            return (string[])result.ToArray(typeof(string));
        }

        public string[] GetPartitions(string measureGroupName)
        {
            ArrayList result = new ArrayList();
            MeasureGroup mg = cube.MeasureGroups.FindByName(measureGroupName);
            foreach (Partition p in mg.Partitions)
            {
                result.Add(p.Name);
            }
            return (string[])result.ToArray(typeof(string)); 
        }

        public string[] GetRoles()
        {
            ArrayList result = new ArrayList();            
            foreach (Role r in db.Roles)
            {
                result.Add(r.Name);
            }
            return (string[])result.ToArray(typeof(string));
        }

        public string[] GetRolesByMember(string memberName)
        {
            ArrayList result = new ArrayList();
            foreach (Role r in db.Roles)
            {
                foreach(RoleMember rm in r.Members)
                {
                    if (memberName.ToUpper() == rm.Name.ToUpper())
                    {
                        result.Add(r.Name);
                    }
                }
            }
            return (string[])result.ToArray(typeof(string));
        }

        public string[] GetMembers()
        {
            Hashtable ht = new Hashtable();
            foreach (Role r in db.Roles)
            {
                foreach (RoleMember rm in r.Members)
                {
                    if (ht[rm.Name] == null)
                    {
                        ht.Add(rm.Name, rm.Name);
                    }
                }
            }
            ArrayList result = new ArrayList(ht.Keys);
            return (string[])result.ToArray(typeof(string));
            
        }

        public DateTime GetLatestProcessDate(object o)
        {
            DateTime result = DateTime.Now;
            //Type t = typeof(o);
            return result;
        }

        #endregion

        #region Create Cube Files ...
        public static string FixMemberName(string oldMemberName)
        { 
            return oldMemberName.Replace("\\", "_");
        }

        public void WriteCubeName(string fileName, string cubeName)
        {
            DSOFile.OleDocumentPropertiesClass pc = new DSOFile.OleDocumentPropertiesClass();
            pc.Open(fileName, false, DSOFile.dsoFileOpenOptions.dsoOptionDefault);
            pc.SummaryProperties.Title = cubeName;
            pc.Close(true);
        }


        public string[] GetMeasuresPermissionByMember(string memberName)
        {
            ArrayList result = new ArrayList();
            string[] roles = GetRolesByMember(memberName);
            foreach (string role in roles)
            {
                string[] measures = GetMeasuresPermissionByRole(role);                
                foreach (string measure in measures)
                {
                    result.Add(measure);
                }
            }

            return (string[])result.ToArray(typeof(string));
        }

        public string[] GetMeasuresPermissionByRole(string roleName)
        {
            //ArrayList result = new ArrayList();
            Role r = db.Roles.GetByName(roleName);
            CubePermission cp = cube.CubePermissions.FindByRole(r.ID);
            CubeDimensionPermission cdp = cp.DimensionPermissions.Find("Measures");
            if (cdp == null) { return new string[0]; }
            AttributePermission ap = cdp.AttributePermissions.Find("Measures");
            if (ap == null) { return new string[0]; }

            string allowset = ap.AllowedSet;
            allowset = allowset.Trim('{', '}');
            if (allowset != "")
            {
                allowset = allowset.Replace("[Measures].", "");
                allowset = allowset.Replace("[", "");
                allowset = allowset.Replace("]", "");
                return allowset.Split(',');
            }
            else
            {
                return null;
            }
            //return (string[])result.ToArray(typeof(string));
        }

        //public bool IsAllowedMeasure(string m, string[] allowMeasures)
        //{
        //    bool result = false;
        //    if (allowMeasures == null || allowMeasures.Length == 0)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        foreach (string am in allowMeasures)
        //        {
        //            if (m.ToUpper() == am.ToUpper())
        //            {
        //                result = true;
        //                break;
        //            }
        //        }
        //    }
        //    return result;
        //}

        private string[] StringIntersection(string[] ary1, string[] ary2, bool firstEmptyReturnEmpty)
        {
            if (firstEmptyReturnEmpty && (ary1 == null || ary1.Length == 0))
            {
                return new string[0];
            }
            else
            {
                return StringIntersection(ary1, ary2);
            }
        }

        private string[] StringIntersection(string[] ary1, string[] ary2)
        {
            ArrayList result = new ArrayList();
            if (ary1 == null || ary1.Length == 0)
            {
                return ary2;
            }

            if (ary2 == null || ary2.Length == 0)
            {
                return ary1;
            }

            foreach(string val1 in ary1)
            {
                foreach (string val2 in ary2)
                {
                    if (val1.ToUpper() == val2.ToUpper())
                    {
                        result.Add(val1);
                    }

                }
            }
            return (string[])result.ToArray(typeof(string));
        }

        public void CreateCubeFileForMember(string fileName, string memberName,
            string[] currentMeasures, string[] currentDimensions, string[] currentRoles,
            DateTime begin, DateTime end)
        {
            string[] dimensions = GetDimensions();
            string[] allowedMeasures = GetMeasuresPermissionByMember(memberName);

            string[] unhiddenmeasures = StringIntersection(GetUnhiddenMeasures(), allowedMeasures, true);
            string[] hiddenmeasures = StringIntersection(GetHiddenMeasures(), allowedMeasures, true);

            string[] roles = GetRolesByMember(memberName);
            

            string[] usedUnhiddenMeasures = StringIntersection(unhiddenmeasures, currentMeasures, true);
            string[] usedHiddenMeasures = StringIntersection(hiddenmeasures, currentMeasures, true);
            string[] usedDimensions = StringIntersection(dimensions, currentDimensions, true);
            string[] usedRoles = StringIntersection(roles, currentRoles, true);

            if (dimensions.Length + unhiddenmeasures.Length > 0 && roles.Length > 0)
            {
                File.Delete(fileName);
                CreateCubeFile(usedDimensions, usedUnhiddenMeasures, usedHiddenMeasures, usedRoles, fileName, begin, end);
                WriteCubeName(fileName, cube.Name);
            }            
        }

        public void CreateCubeFile(string[] dimensions, string[] unHiddenmeasures, string[] hiddenMeasures, string[] roles, string fileName,
            DateTime begin, DateTime end)
        {
            string dimensionClause = "";
            string measureClause = "";
            string roleClause = "";

            foreach (string m in unHiddenmeasures)
            {
                measureClause += string.Format("MEASURE [{0}].[{1}],", cube.Name, m);
             
            }

            foreach (string m in hiddenMeasures)
            {
                measureClause += string.Format("MEASURE [{0}].[{1}] HIDDEN,", cube.Name, m);
             
            }

            measureClause = measureClause.TrimEnd(',');


            foreach (string d in dimensions)
            {
                dimensionClause += string.Format("DIMENSION [{0}].[{1}],", cube.Name, d); ;
            }
            dimensionClause = dimensionClause.TrimEnd(',');

            foreach (string r in roles)
            {
                roleClause += r + ",";
            }
            roleClause = roleClause == "" ? "" : "Roles=" + roleClause.TrimEnd(',');
            

            string mdx = string.Format(MDX_CREATE_CUBE_FILE, cube.Name, fileName, measureClause, dimensionClause);
            //
            if (begin != DateTime.MinValue && end != DateTime.MinValue)
            {
                mdx = mdx.Replace("DIMENSION [" + cube.Name + "].[Time]", GetDateRangeMdx(begin, end));
            }

            ExecuteMDX(roleClause, mdx);
        }

        public DataTable GetAttrributeMembers(string dimensionName, string attributeName)
        {
            if (dimensionName == "Measures" && attributeName == "Measures")
            {
                return GetAttributeDataOfMeasures();
            }
            return GetAttrributeMembers(string.Format("[{0}].[{1}]", dimensionName, attributeName));
        }

        public DataTable GetAttrributeMembers(string attributeName)
        {   
            //Microsoft.AnalysisServices.AdomdClient.CellSet result = new Microsoft.AnalysisServices.AdomdClient.CellSet();
            //
            string mdx = string.Format(MDX_GET_ALL_MEMBERS, cube.Name, attributeName);
            string connstr = string.Format(ADOMD_CONNECTION_STRING, svr.Name, userName, password,db.Name, "");
            Microsoft.AnalysisServices.AdomdClient.AdomdConnection adoConn
                = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connstr);
            Microsoft.AnalysisServices.AdomdClient.AdomdCommand adoComm
                = new Microsoft.AnalysisServices.AdomdClient.AdomdCommand(mdx);
            Microsoft.AnalysisServices.AdomdClient.AdomdDataAdapter adoAdap
                = new Microsoft.AnalysisServices.AdomdClient.AdomdDataAdapter(adoComm);
            adoComm.Connection = adoConn;
            DataSet ds = new DataSet();
            adoAdap.Fill(ds);            
            adoConn.Open();
            adoComm.Execute();
            adoConn.Close();
            return ds.Tables[0];
        }

        private DataTable GetAttributeDataOfMeasures()
        {
            DataTable result = new DataTable("Measures");
            result.Columns.Add("TempMember");
            result.Columns.Add("MemberValue");            

            string[] getMeasure = GetUnhiddenMeasures();
            string[] getHiddenMeasure = GetHiddenMeasures();
            foreach (string m in getMeasure)
            {
                DataRow dr = result.NewRow();
                dr[0] = m;
                dr[1] = "[Measures].[" + m + "]";
                result.Rows.Add(dr);
            }

            foreach (string m in getHiddenMeasure)
            {
                DataRow dr = result.NewRow();
                dr[0] = m;
                dr[1] = "[Measures].[" + m + "]";
                result.Rows.Add(dr);
            }
            return result;
        }

        public void ExecuteMDX(string playRoles, string mdx)
        {
            string connstr = string.Format(ADOMD_CONNECTION_STRING, svr.Name, userName, password, db.Name, playRoles);
            Microsoft.AnalysisServices.AdomdClient.AdomdConnection adoConn
                = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(connstr);
            Microsoft.AnalysisServices.AdomdClient.AdomdCommand adoComm
                = new Microsoft.AnalysisServices.AdomdClient.AdomdCommand();
            Microsoft.AnalysisServices.AdomdClient.AdomdDataAdapter adoAdap
                = new Microsoft.AnalysisServices.AdomdClient.AdomdDataAdapter(adoComm);
            adoComm.Connection = adoConn;
            adoComm.CommandText = mdx;
            adoConn.Open();
            adoComm.Execute();
            adoConn.Close();
        }

        private string GetDateRangeMdx(DateTime begin, DateTime end)
        {
            //Demo MDX
            //CREATE GLOBAL CUBE [SP_CUBE] 
            //STORAGE 'C:\Temp\SP_CUBE.cub' 
            //FROM [SP_CUBE] ( 
            //    MEASURE [SP_CUBE].[In Market Sales],	
            //    DIMENSION [SP_CUBE].[Measure Type]  ,
            //    DIMENSION [SP_CUBE].[Time].[Time]  (LEVEL [(ALL)],  MEMBER [Time].[Month].&[2006-01-01T00:00:00]	)
            //) 
            string result = "";
            for (DateTime dati = begin; dati <= end; dati = dati.AddMonths(1))
            {
                result += "MEMBER [Time].[Month].&[" + dati.Year.ToString() + "-" + dati.Month.ToString("00") + "-01T00:00:00],";                
            }
            return "DIMENSION [SP_CUBE].[Time].[Time] (LEVEL [(ALL)], " + result.TrimEnd(',') + ")";
        }

        //public void CreateCubeFile(string fileName, DateTime begin, DateTime end)
        //{
        //   CreateCubeFile(GetDimensions(), GetUnhiddenMeasures(), GetHiddenMeasures(), new string[0], fileName, begin, end);
        //}

        //public void CreateCubeFile(string fileName)
        //{
        //    CreateCubeFile(GetDimensions(), GetUnhiddenMeasures(), GetHiddenMeasures(), new string[0], fileName, DateTime.MinValue, DateTime.MinValue);
        //}

        #endregion

        #region Process Cube ...
        public void ProcessCube()
        {
            cube.Process(ProcessType.ProcessFull);
        }

        public void ProcessCubePartition(string measureGroupName, params string[] partitions)
        {
            MeasureGroup mg = cube.MeasureGroups.FindByName(measureGroupName);
            Partition p;
            foreach (string s in partitions)
            {
                p = mg.Partitions.FindByName(s);
                if (p != null)
                {
                    p.Process(ProcessType.ProcessFull);
                }
            }
        }

        public void ProcessCubePartition(string measureGroupName)
        {
            MeasureGroup mg = cube.MeasureGroups.FindByName(measureGroupName);            
            foreach (Partition p in mg.Partitions)
            {
                p.Process(ProcessType.ProcessFull);                
            }
        }
        
        public void ProcessCubePartitionLastTwoYear(string measureGroupName)
        {
            string[] partitions = GetLastTowYearPartitionName(measureGroupName);
            ProcessCubePartition(measureGroupName, partitions);
        }

        public string[] GetLastTowYearPartitionName(string measureGroupName)
        {
            int currentYear = DateTime.Now.Year;
            string[] result = new string[2];
            result[0] = (currentYear - 1) + "";
            result[1] = currentYear + "";
            return result;
        }
        #endregion

        #region Backup And Restore ...

        public void BackupDatabase(string dbName, string fileName, bool allowOverride)
        {
            Database database = svr.Databases.GetByName(dbName);
            database.Backup(fileName, allowOverride);
        }

        public void BackupDatabase(string dbName, string fileName)
        {
            BackupDatabase(dbName, fileName, false);
        }

        public void BackupDatabase(string filename)
        {
            BackupDatabase(db.Name, filename);            
        }

        public static void RestoreDatabase(string serverName, string userName, string password, string dbName, string fileName, bool allowOverride)
        {
            (ConncetServer(serverName, userName, password)).Restore(fileName, dbName, allowOverride);
        }

        public static void RestoreDatabase(string serverName, string userName, string password, string dbName, string fileName)
        {
            (ConncetServer(serverName, userName, password)).Restore(fileName, dbName, false);
        }

        public static void RestoreDatabase(string serverName, string userName, string password, string fileName)
        {
            (ConncetServer(serverName, userName, password)).Restore(fileName);
        }

        public void RenameDatabase(string newdatabaseName)
        {
            db.Name = newdatabaseName;
            db.Update();
        }
        #endregion

        #region Update Role ...

        public void DeleteRole(string roleName)
        {
            Role r = db.Roles.FindByName(roleName);
            if (r != null)
            {
                r.Drop(DropOptions.AlterOrDeleteDependents);
                db.Update();
            }
        }

        public void DeleteAllRoles()
        {
            string[] roles = GetRoles();
            foreach (string role in roles)
            {
                DeleteRole(role);
            }
        }

        private const string COLUMN_NAME_DIMENSION_NAME = "DimensionName";
        private const string COLUMN_NAME_ATTRIBUTE_NAME = "AttributeName";
        private const string COLUMN_NAME_VISUAL_TOTAL = "VisualTotal";
        private const string COLUMN_NAME_ATTRIBUTE_DATA = "AttributeData";
        
        private CubePermission SetCubePermissions(Role role)
        {
            DatabasePermission dbperm;
            CubePermission cubeperm;

            dbperm = db.DatabasePermissions.Add(role.ID);
            dbperm.ReadDefinition = ReadDefinitionAccess.Allowed;
            dbperm.Read = ReadAccess.Allowed;
            dbperm.Update();

            cubeperm = cube.CubePermissions.Add(role.ID);
            cubeperm.Read = ReadAccess.Allowed;
            cubeperm.ReadDefinition = ReadDefinitionAccess.Allowed;
            cubeperm.ReadSourceData = ReadSourceDataAccess.Allowed;
            cubeperm.Update();
            return cubeperm;
        }
        

        private void SetDimensionPermission(CubePermission cubeperm, string roleName, string dimenisionName, string attributeName, string[] allowSet, string visualTotal)
        {
            string dimId = "";
            string attrId = "";
            
            if (dimenisionName == "Measures" && attributeName == "Measures")
            {
                dimId = "Measures";
                attrId = "Measures";
            }
            else
            {
                DimensionAttribute attr;
                Dimension dim;
                dim = db.Dimensions.GetByName(dimenisionName);
                attr = dim.Attributes.GetByName(attributeName);

                CubeDimension cubedim = cube.Dimensions.GetByName(dimenisionName);
                attr = dim.Attributes.FindByName(attributeName);

                dimId = cubedim.ID;
                attrId = attr.ID;
            }

            CubeDimensionPermission cubedimperm;
            cubedimperm = cubeperm.DimensionPermissions.Find(dimId);
            if (cubedimperm == null)
            {
                cubedimperm = cubeperm.DimensionPermissions.Add(dimId);
            }
            cubedimperm.Read = ReadAccess.Allowed;

            AttributePermission attrperm;
            attrperm = cubedimperm.AttributePermissions.Find(attrId);
            if (attrperm == null)
            {
              attrperm = cubedimperm.AttributePermissions.Add(attrId);
            }
            string allowsetstring = "";
            string allsetstring = string.Format("[{0}].[{1}].[ALL],", dimenisionName.ToUpper(), attributeName.ToUpper());
            foreach(string allow in allowSet)
            {   
                //attrperm.AllowedSet += GetAllowSetString(attr, allow);
                allowsetstring += allow + ",";
            }

            if (allowsetstring != "" && allowsetstring.ToUpper() != allsetstring.ToUpper())
            {
                attrperm.AllowedSet = "{" + allowsetstring.TrimEnd(',') + "}";
            }

            // Enable Visual Total = 1
            attrperm.VisualTotals = visualTotal;
            try
            {
                cubeperm.Update();
            }
            catch (Exception ee)
            {
                string a = ee.Message;
            }
        }

        private void SetDimensionAttributeVisualTotalWithoutSelected(CubePermission cubeperm, string roleName, string dimenisionName, string attributeName, string visualTotal)
        {
            string[] attributes = GetDimensionAttributes(dimenisionName);
            foreach (string attr in attributes)
            {
                if (attr != attributeName)
                {
                    SetDimensionPermission(cubeperm, roleName, dimenisionName, attr, emptyStringArray, visualTotal);
                }
            }
        }

        private string[] GetAllowedDimensionList(DataTable dimensionData)
        {
            return Helper.Distinct(dimensionData, COLUMN_NAME_DIMENSION_NAME, "");
        }

        private string[] GetAllowedAttributeList(DataTable dimensionData, string dimensionName)
        {
            string filter = "(" + COLUMN_NAME_DIMENSION_NAME + " = '" +  dimensionName + "')";
            return Helper.Distinct(dimensionData, COLUMN_NAME_ATTRIBUTE_NAME, filter);
        }

        private string[] GetAllowedAttributeData(DataTable dimensionData, string dimensionName, string attributeName)
        {
            string filter = "(" + COLUMN_NAME_DIMENSION_NAME + " = '" + dimensionName + "') ";
            filter += " AND (" + COLUMN_NAME_ATTRIBUTE_NAME + " = '" + attributeName + "') ";
            //DataRow[] drs = dimensionData.Select(filter, COLUMN_NAME_ATTRIBUTE_DATA);
            //return Helper.GetStringArrary(drs, COLUMN_NAME_ATTRIBUTE_DATA);
            return Helper.Distinct(dimensionData, COLUMN_NAME_ATTRIBUTE_DATA, filter);
        }

        private string GetAllowedVisualTotal(DataTable dimensionData, string dimensionName, string attributeName)
        {
            string filter = "(" + COLUMN_NAME_DIMENSION_NAME + " = '" + dimensionName + "') ";
            filter += " AND (" + COLUMN_NAME_ATTRIBUTE_NAME + " = '" + attributeName + "') ";
            DataRow[] drs = dimensionData.Select(filter, COLUMN_NAME_ATTRIBUTE_DATA);
            return drs[0][COLUMN_NAME_VISUAL_TOTAL].ToString();
        }


        public void UpdateRole(string roleName, string[] memberNames, DataTable dimensionData)
        {
            DeleteRole(roleName);

            Role role = db.Roles.Add(roleName, roleName);
            Hashtable roleHt = new Hashtable();
            foreach (string memberName in memberNames)
            {
                if (!roleHt.ContainsKey(memberName))
                {
                    roleHt.Add(memberName, memberName);
                    role.Members.Add(new RoleMember(memberName));
                }
            }
            role.Update();

            CubePermission cubeperm = SetCubePermissions(role);
            if (dimensionData != null && dimensionData.Rows.Count > 0)
            {
                string[] dimensions = GetAllowedDimensionList(dimensionData);
                foreach (string dimension in dimensions)
                {
                    string[] attributes = GetAllowedAttributeList(dimensionData, dimension);
                    foreach (string attribute in attributes)
                    {
                        string visualTotal = GetAllowedVisualTotal(dimensionData, dimension, attribute);
                        string[] attributeData = GetAllowedAttributeData(dimensionData, dimension, attribute);
                        SetDimensionPermission(cubeperm, roleName, dimension, attribute, attributeData, visualTotal);
                        if (dimension.ToUpper() != "MEASURES")
                        {
                            SetDimensionAttributeVisualTotalWithoutSelected(cubeperm, roleName, dimension, attribute, visualTotal);
                        }
                    }
                }
            }
            role.Update();
        }

       

        //public void UpdateRole(string roleName, string[] memberNames)
        //{
        //    UpdateRole(roleName, memberNames, emptyStringArray, emptyStringArray, emptyStringArray, emptyStringArray,
        //        emptyStringArray, emptyStringArray);
        //}

        public string GetAllowSetString(DimensionAttribute att, string val)
        {
            return string.Format("[{0}].[{1}].&[{2}],", att.Parent, att.Name, val);
        }

        #endregion 


        private static Server ConncetServer(string serverName, string userName, string password)
        {
            string connstr = string.Format(AMO_CONNECTION_STRING, serverName, userName, password);
            Server server = new Server();
            server.Connect(connstr);
            return server;
        }
    }
}
