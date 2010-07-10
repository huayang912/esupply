using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SPCubeUtility
{
    class Helper
    {

        public static string[] GetStringArrary(DataRow[] drs, string columnName)
        {
            if (drs == null || drs.Length == 0)
            {
                return null;
            }

            string[] result = new string[drs.Length];
            for (int i = 0; i < drs.Length; i++)
            {
                result[i] = drs[i][columnName].ToString();
            }
            return result;
        }


        private static bool RowEqual(object[] Values, object[] OtherValues)
        {
            if (Values == null)
                return false;

            for (int i = 0; i < Values.Length; i++)
            {
                if (!Values[i].Equals(OtherValues[i]))
                    return false;
            }
            return true;
        }

        public static string[] Distinct(DataTable dt, string colName, string filter)
        {
            DataColumn dc = new DataColumn(colName, typeof(string));
            DataTable dtDistincet = Distinct(dt, new DataColumn[] { dc }, filter);
            DataRow[] drs = dtDistincet.Select();
            return GetStringArrary(drs, colName);
        }

        public static DataTable Distinct(DataTable dt, DataColumn[] cols, string filter)
        {
            //Empty table
            DataTable table = new DataTable("Distinct");
            //Sort variable
            string sort = string.Empty;
            //Add Columns & Build Sort expression
            for (int i = 0; i < cols.Length; i++)
            {
                table.Columns.Add(cols[i].ColumnName, cols[i].DataType);

                sort += cols[i].ColumnName + ",";
            }

            //Select all rows and sort
            DataRow[] sortedrows = dt.Select(filter, sort.Substring(0, sort.Length - 1));

            object[] currentrow = null;
            object[] previousrow = null;

            table.BeginLoadData();
            foreach (DataRow row in sortedrows)
            {
                //Current row
                currentrow = new object[cols.Length];

                for (int i = 0; i < cols.Length; i++)
                {
                    currentrow[i] = row[cols[i].ColumnName];
                }

                //Match Current row to previous row
                if (!RowEqual(previousrow, currentrow))
                    table.LoadDataRow(currentrow, true);

                //Previous row
                previousrow = new object[cols.Length];

                for (int i = 0; i < cols.Length; i++)
                {
                    previousrow[i] = row[cols[i].ColumnName];
                }
            }

            table.EndLoadData();
            return table;

        }
    }
}
