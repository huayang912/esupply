using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace Dndp.Utility.CSV
{
    public class CSVDataContainer
    {
        #region Private variables
        private int batchNo;
        private int recordRows;
        private string dataSourceName;
        private string dataSourceCategoryName;
        private int dataSourceCategoryid;
        private CSVReader reader;
        private IList<CSVDataSourceField> dsFieldTable;
        private CSVDataDefinitionBase[] dataDefinitionArray;        
        private IList<CSVLineData> lineDataList = new List<CSVLineData>();
        private IList<string> errorMessages = new List<string>();
        private StringBuilder clearTableSql = new StringBuilder();     
        private StringBuilder insertTableSql = new StringBuilder();
        //private StringBuilder insertHistoryTableSql = new StringBuilder();

        private StringBuilder insertTempTableSqlCommon = new StringBuilder();
        private StringBuilder insertHistoryTableSqlCommon = new StringBuilder();
        private int DEFAULT_RECORD_COUNT_PER_PARSE = 20;
        private int RecordCountPerParse = 0;
        #endregion Private variables

        #region Constructor
        public CSVDataContainer(
            int batchNo, string dataSourceName, int dataSourceCategoryId, string dataSourceCategoryName, Stream s, IList<CSVDataSourceField> dsFieldTable)
        {
            this.batchNo = batchNo;
            this.dataSourceName = dataSourceName;
            this.dataSourceCategoryid = dataSourceCategoryId;
            this.dataSourceCategoryName = dataSourceCategoryName;
            this.dsFieldTable = dsFieldTable;
            this.recordRows = 0;
            reader = new CSVReader(s);
            Init();
        }

        public CSVDataContainer(
            int batchNo, string dataSourceName, int dataSourceCategoryId, string dataSourceCategoryName, Stream s, string encode, IList<CSVDataSourceField> dsFieldTable)
        {
            this.batchNo = batchNo;
            this.dataSourceName = dataSourceName;
            this.dataSourceCategoryid = dataSourceCategoryId;
            this.dataSourceCategoryName = dataSourceCategoryName;
            this.dsFieldTable = dsFieldTable;
            this.recordRows = 0;
            reader = new CSVReader(s, Encoding.GetEncoding(encode));
            Init();
        }

        public CSVDataContainer(
            int batchNo, string dataSourceName, int dataSourceCategoryId, string dataSourceCategoryName, CSVReader reader, IList<CSVDataSourceField> dsFieldTable)
        {
            this.batchNo = batchNo;
            this.dataSourceName = dataSourceName;
            this.dataSourceCategoryid = dataSourceCategoryId;
            this.dataSourceCategoryName = dataSourceCategoryName;
            this.dsFieldTable = dsFieldTable;
            this.recordRows = 0;
            this.reader = reader;
            Init();
        }
        #endregion Constructor

        public int GetRecordRows()
        {
            return recordRows;                      
        }

        public IList<string> GetErrorMessages()
        {
            return errorMessages;
        }

        public string GetClearTableSql()
        {
            //if (errorMessages != null && errorMessages.Count > 0)
            //{
            //    return null;
            //}
            //else
            //{
                return clearTableSql.ToString();
            //}
        }

        public void setRecordCountPerParse(int recordCountPerParse)
        {
            this.RecordCountPerParse = recordCountPerParse;
        }

        public Boolean ParseNext()
        {
            //Clear the string
            insertTableSql.Remove(0, insertTableSql.Length);

            string[] data;
            for (int i = 0; i < RecordCountPerParse; i++)
            {
                data = reader.GetCSVLine();
                if (data == null)
                {
                    if (i == 0)
                    {                        
                        return false; //parse next can not get any further records
                    }
                    else
                    {
                        return true;
                    }
                }

                recordRows++;
                CSVLineData lineData = new CSVLineData(batchNo, recordRows, dataSourceCategoryid, dataSourceCategoryName);
                lineData.AddData(data, dataDefinitionArray);
                lineDataList.Add(lineData);

                //Get Error Message
                if (lineData.GetErrorMessages() != null && lineData.GetErrorMessages().Count > 0)
                {
                    foreach (string errorMessage in lineData.GetErrorMessages())
                    {
                        errorMessages.Add(errorMessage);
                    }
                    return false;
                }

                insertTableSql.Append(insertTempTableSqlCommon.ToString() + lineData.GetInsertSqlValue() + "; ");
                insertTableSql.Append(insertHistoryTableSqlCommon.ToString() + lineData.GetInsertSqlValue() + "; ");
            }

            return true;
        }

        public string GetInsertTableSql()
        {
            if (errorMessages != null && errorMessages.Count > 0)
            {
                return null;
            }
            else
            {
                return insertTableSql.ToString();
            }
        }

        //public string GetInsertHistoryTableSql()
        //{
        //    if (errorMessages != null && errorMessages.Count > 0)
        //    {
        //        return null;
        //    } 
        //    else 
        //    {
        //        return insertHistoryTableSql.ToString();
        //    }
        //}

        private Boolean Init()
        {        
            //read first line, first line is data header
            string[] header = reader.GetCSVLine();
            if (header == null)
            {
                errorMessages.Add("The file is empty");
                return false;
            }
            if (dsFieldTable == null || dsFieldTable.Count == 0)
            {
                errorMessages.Add("There is no field in the data source");
                return false;
            }

            Boolean FindValidationFlag = false;
            for (int i = 0; i < header.Length; i++)
            {
                header[i] = header[i].ToUpper();
                if (header[i].Equals("ETLENDCOLUMN"))
                {
                    FindValidationFlag = true;
                }
            }
            if (!FindValidationFlag)
            {
                errorMessages.Add("Please validate the CSV file in Excel before upload it.");
                return false;
            }
            ParseCSVHeader(header);

            //parse the body
            if (dataDefinitionArray == null || (errorMessages != null && errorMessages.Count > 0))
            {
                return false;
            }

            //append delete sql
            clearTableSql.Append("delete from ");
            clearTableSql.Append(DataSourceHelper.GetTempTableName(dataSourceName));
            clearTableSql.Append(" where CATEGORY_id = " + dataSourceCategoryid.ToString() + ";");
            clearTableSql.Append("delete from ");
            clearTableSql.Append(DataSourceHelper.GetHistoryTableName(dataSourceName));
            clearTableSql.Append(" where CATEGORY_id = " + dataSourceCategoryid.ToString() + " and BATCH_NO = " + this.batchNo + ";");

            //append insert sql common
            insertTempTableSqlCommon.Append("insert into ");
            insertTempTableSqlCommon.Append(DataSourceHelper.GetTempTableName(dataSourceName));
            insertTempTableSqlCommon.Append(" (BATCH_NO, ROW_NO, CATEGORY_id, CATEGORY ");
            insertHistoryTableSqlCommon.Append("insert into ");
            insertHistoryTableSqlCommon.Append(DataSourceHelper.GetHistoryTableName(dataSourceName));
            insertHistoryTableSqlCommon.Append(" (BATCH_NO, ROW_NO, CATEGORY_id, CATEGORY ");
            foreach (CSVDataDefinitionBase dataDefinition in dataDefinitionArray)
            {
                if (dataDefinition != null)
                {
                    insertTempTableSqlCommon.Append(", " + dataDefinition.ColumnNm);
                    insertHistoryTableSqlCommon.Append(", " + dataDefinition.ColumnNm);
                }
            }
            insertTempTableSqlCommon.Append(") values ");
            insertHistoryTableSqlCommon.Append(") values ");

            this.RecordCountPerParse = DEFAULT_RECORD_COUNT_PER_PARSE;

            return true;
        }

        private int GetHeaderPos(string[] header, CSVDataSourceField dsField)
        {
            for (int i = 0; i < header.Length; i++)
            {
                if (dsField.Name.Equals(header[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        private void ParseCSVHeader(string[] header)
        {
            //initial the DataDefinition
            int i;
            dataDefinitionArray = new CSVDataDefinitionBase[header.Length];
            foreach (CSVDataSourceField dsField in dsFieldTable)
            {
                i = GetHeaderPos(header, dsField);
                if (i == -1)
                {
                    if (!dsField.IsNullable)
                    {
                        errorMessages.Add("Do not contain the field(" + dsField.Name.ToString() + ")");
                    }
                }
                else
                {
                    if (dsField.FieldType.ToUpper().Equals("TEXT"))
                    {
                        if (dsField.FieldLength != null && dsField.FieldLength.Trim().Length > 0)
                        {
                            dataDefinitionArray[i] =
                                new CSVTextDataDefinition(dsField.IsNullable, dsField.IsDataKey, Convert.ToInt32(dsField.FieldLength), dsField.Name);
                        }
                        else
                        {
                            dataDefinitionArray[i] =
                                new CSVTextDataDefinition(dsField.IsNullable, dsField.IsDataKey, dsField.Name);
                        }

                    }
                    else if (dsField.FieldType.ToUpper().Equals("NUMERIC"))
                    {
                        if (dsField.FieldLength != null && dsField.FieldLength.Trim().Length > 0)
                        {
                            dataDefinitionArray[i] =
                                new CSVNumericDataDefinition(dsField.IsNullable, dsField.IsDataKey, dsField.FieldLength, dsField.Name);
                        }
                        else
                        {
                            dataDefinitionArray[i] =
                                new CSVNumericDataDefinition(dsField.IsNullable, dsField.IsDataKey, dsField.Name);
                        }

                    }
                    else if (dsField.FieldType.ToUpper().Equals("DATETIME"))
                    {
                        dataDefinitionArray[i] =
                            new CSVDateTimeDataDefinition(dsField.IsNullable, dsField.IsDataKey, dsField.Name);
                    }
                    else if (dsField.FieldType.ToUpper().Equals("INTEGER"))
                    {
                        dataDefinitionArray[i] =
                            new CSVIntegerDataDefinition(dsField.IsNullable, dsField.IsDataKey, dsField.Name);
                    }
                }
            }
        }
    }
}