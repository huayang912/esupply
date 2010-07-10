using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SPCubeUtility
{
    public class CubeDBUtility
    {
        //private SqlConnection conn;
        private SqlDataAdapter adap;
        public CubeDBUtility(string connstr)
        {
            adap = new SqlDataAdapter("", connstr);
        }
        #region Log
        public DataTable GetLog()
        {
            return null;
        }

        public void InsertLog()
        {
 
        }

        #endregion

        #region Rules
        
        #endregion
    }
}
