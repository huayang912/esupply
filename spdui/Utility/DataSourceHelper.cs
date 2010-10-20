using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Utility
{
    public class DataSourceHelper
    {
        public static string GetTempTableName(string dsNm)
        {
            //return "TEMP_" + dsNm + "_" + dsCategoryNm;
            return dsNm;
        }

        public static string GetHistoryTableName(string dsNm)
        {
            //return "HISTORY_" + dsNm + "_" + dsCategoryNm;
            return dsNm + "_HISTORY";
        }

        public static string GetArchiveTableName(string dsNm)
        {
            //return "Archive_" + dsNm + "_" + dsCategoryNm;
            return dsNm + "_ARCHIVE";
        }
    }
}
