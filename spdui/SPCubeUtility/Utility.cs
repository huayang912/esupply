using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AnalysisServices;
using System.Collections;
using System.Data;


namespace SPCubeUtility
{
    public class Utility
    {
        public Server CurrentServer
        {
            get
            {
                return currentServer;
            }
        }

        private Server currentServer;
        private string currentConnectionString;

        public Utility(string connstr)
        {
            currentServer = new Server();
            currentServer.Connect(connstr);
            currentConnectionString = connstr;
        }

        public void BackupDatabase(string dbName, string fileName)
        {
            Database db = currentServer.Databases.FindByName(dbName);
            db.Backup(fileName);
        }

        public void RestoreDatabase(string dbName, string fileName)
        {
            currentServer.Restore(fileName, dbName, true);      
            
        }

        public string[] GetDatabases()
        {
            String[] result = new String[currentServer.Databases.Count];
            for (int i = 0; i < currentServer.Databases.Count; i++ )
            {
                result[i] = currentServer.Databases[i].Name;
            }
            return result;
        }

        public string[] GetCubes(string db)
        {
            String[] result = new String[currentServer.Databases[db].Cubes.Count];
            for (int i = 0; i < currentServer.Databases[db].Cubes.Count; i++)
            {
                result[i] = currentServer.Databases[db].Cubes[i].Name;
            }
            return result;
        }

        public string[] GetMeasures(string dbName, string cubeName)
        {
            ArrayList r = new ArrayList();
            Cube c = currentServer.Databases.FindByName(dbName).Cubes.FindByName(cubeName);
            foreach (MeasureGroup mg in c.MeasureGroups)
            {
                foreach (Measure m in mg.Measures)
                {
                    r.Add(m.Name);
                }
            }

            string[] result = new string[r.Count];

            for (int i = 0; i < r.Count; i++ )
            {
                result[i] = r[i].ToString();
            }
            return result;
        }

        public string[] GetMeasureGroups(string dbName, string cubeName)
        {            
            Cube c = currentServer.Databases.FindByName(dbName).Cubes.FindByName(cubeName);
            string[] result = new string[c.MeasureGroups.Count];
            
            for (int i = 0; i < c.MeasureGroups.Count; i++)
            {
                result[i] = c.MeasureGroups[i].Name;
            }
            return result;
        }        

        public string[] GetPartitions(string dbName, string cubeName, string measureGroupName)
        {
            Cube c = currentServer.Databases.FindByName(dbName).Cubes.FindByName(cubeName);
            MeasureGroup mg = c.MeasureGroups.FindByName(measureGroupName);
            string[] result = new string[mg.Partitions.Count];
            for (int i = 0; i < mg.Partitions.Count; i++)
            {
                result[i] = mg.Partitions[i].Name;
            }
            return result;
        }

        public void FullProcessCube(string dbName, string cubeName)
        {
            Cube c = currentServer.Databases.FindByName(dbName).Cubes.FindByName(cubeName);
            c.Process(ProcessType.ProcessFull);
        }

        public void FullProcessPartitions(string dbName, string cubeName, string measureGroupName, string paritionName)
        {
            Cube c = currentServer.Databases.FindByName(dbName).Cubes.FindByName(cubeName);
            MeasureGroup mg = c.MeasureGroups.FindByName(measureGroupName);
            Partition p = mg.Partitions.FindByName(paritionName);
            p.Process(ProcessType.ProcessFull);
        }

        public string[] GetDimensions(string dbName)
        {
            Database db = currentServer.Databases.FindByName(dbName);
            string[] result = new string[db.Dimensions.Count];
            for (int i =0; i< db.Dimensions.Count; i++)
            {
                result[i]= db.Dimensions[i].Name.ToString();
            }
            return result;
        }

        public string[] GetRoleId(string dbName)
        {

            Database db = currentServer.Databases.FindByName(dbName);
            string[] result = new string[db.Roles.Count];
            for (int i = 0; i < db.Roles.Count; i++)
            {
                result[i] = db.Roles[i].ID.ToString();
            }
            return result;
        }

        public string[] GetRoleId(string dbName, string sid)
        {
            Database db = currentServer.Databases.FindByName(dbName);
            string[] result = new string[db.Roles.Count];
            for (int i = 0; i < db.Roles.Count; i++)
            {
                result[i] = db.Roles[i].ID.ToString();
            }
            
            return result;
        }

        public void CreateCubeFile(string cubeFileName, string filePath, string cube, string[] measures,
            string[] dimensions)
        {
            CreateCubeFile(cubeFileName, filePath, cube, measures,dimensions, null);
        }

        public void CreateCubeFile(string cubeFileName, string filePath, string cube, string[] measures,
            string[] dimensions, string[] roles)
        {
            string mdx = "CREATE GLOBAL CUBE [{0}] STORAGE '{1}' FROM [{2}] ( {3}, {4}) ";
            string measure = "";
            string dimension = "";
            foreach (string m in measures)
            {
                measure += string.Format("MEASURE [{0}].[{1}],", cube, m);
            }

            foreach (string d in dimensions)
            {
                dimension += string.Format("DIMENSION [{0}].[{1}],", cube, d); ;
            }

            mdx = string.Format(mdx, cubeFileName, filePath, cube, measure.TrimEnd(','), dimension.TrimEnd(','));

            Microsoft.AnalysisServices.AdomdClient.AdomdConnection adoConn
                = new Microsoft.AnalysisServices.AdomdClient.AdomdConnection(currentConnectionString);
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

        public void UpdateRoleAndPermissions(string dbName)
        {
            //Database db = currentServer.Databases.FindByName(dbName);
            //Role r = new Role("RoleId1", "RoleName");
            //DatabasePermission dp = new DatabasePermission(r.ID);
            //dp.Role.Members.Add(new RoleMember("MemberName", "MemberId"));
            //dp.Read = ReadAccess.Allowed;
            //dp.Process = false;

            //CubePermission cp = new CubePermission(r.ID);
            //cp.Read = ReadAccess.Allowed;
            //cp.Update();

        }


    }
}
